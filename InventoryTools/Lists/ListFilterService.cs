using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AllaganLib.GameSheets.Sheets;
using AllaganLib.Shared.Interfaces;
using AllaganLib.Shared.Services;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Interfaces;
using CriticalCommonLib.Models;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using InventoryTools.Logic;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters;
using InventoryTools.Mediator;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Lists;

/// <summary>
/// A class that takes a set of items, a filter configuration and returns a filter result
/// </summary>
public class ListFilterService : DisposableMediatorBackgroundService
{

    private readonly InventoryToolsConfiguration _configuration;
    private readonly ICharacterMonitor _characterMonitor;
    private readonly HostedInventoryHistory _inventoryHistory;
    private readonly IInventoryMonitor _inventoryMonitor;
    private readonly IFilterService _filterService;
    private readonly ItemSheet _itemSheet;
    private readonly InventoryItem.Factory _inventoryItemFactory;
    private readonly SourceInventoriesFilter _sourceInventoriesFilter;
    private readonly InventoryScopeCalculator _inventoryScopeCalculator;

    public NamedBackgroundTaskQueue FilterQueue { get; }

    public ListFilterService(InventoryToolsConfiguration configuration, ICharacterMonitor characterMonitor,
        HostedInventoryHistory inventoryHistory, IInventoryMonitor inventoryMonitor, NamedBackgroundTaskQueue.Factory taskQueueFactory,
        ILogger<ListFilterService> logger,
        IFilterService filterService, MediatorService mediatorService, ItemSheet itemSheet,
        InventoryItem.Factory inventoryItemFactory,
        SourceInventoriesFilter sourceInventoriesFilter,
        InventoryScopeCalculator inventoryScopeCalculator) : base(logger,
        mediatorService)
    {
        _configuration = configuration;
        _characterMonitor = characterMonitor;
        _inventoryHistory = inventoryHistory;
        _inventoryMonitor = inventoryMonitor;
        _filterService = filterService;
        _itemSheet = itemSheet;
        _inventoryItemFactory = inventoryItemFactory;
        _sourceInventoriesFilter = sourceInventoriesFilter;
        _inventoryScopeCalculator = inventoryScopeCalculator;
        FilterQueue = taskQueueFactory.Invoke("List Filter Queue", 1);
        MediatorService.Subscribe<RequestListUpdateMessage>(this, message => RequestRefresh(message.FilterConfiguration));
    }

    public List<SearchResult> RefreshList(FilterConfiguration filterConfiguration, CancellationToken ct = default)
    {
        var inventories = _inventoryMonitor.Inventories.Select(c => c.Value).ToList();

        List<SearchResult>? searchResult;
        ct.ThrowIfCancellationRequested();
        searchResult = GenerateFilterResult(filterConfiguration, inventories.ToList(), ct);
        ct.ThrowIfCancellationRequested();
        filterConfiguration.NeedsRefresh = false;
        filterConfiguration.Refreshing = false;
        filterConfiguration.SearchResults = searchResult;
        MediatorService.Publish(new ListUpdatedMessage(filterConfiguration));
        return searchResult;
    }

    private List<SearchResult> GenerateFilterResult(FilterConfiguration filter, List<Inventory> inventories, CancellationToken ct = default)
    {
        var searchResults = new List<SearchResult>();

        var activeCharacter = _characterMonitor.ActiveCharacterId;
        var activeRetainer = _characterMonitor.ActiveRetainerId;

        Logger.LogTrace("Filter Information:");
        Logger.LogTrace("Filter Name:" + filter.Name);
        Logger.LogTrace("Filter Type: " + filter.FilterType);

        var filtersWithValues = _filterService.AvailableFilters.Where(c => c.HasValueSet(filter) && c.AvailableIn.HasFlag(filter.FilterType)).ToList();

        if (filter.FilterType == FilterType.SortingFilter)
        {
            ct.ThrowIfCancellationRequested();
            //Determine which source and destination inventories we actually need to examine
            GenerateSourceAndDestinations(filter, inventories, out var sourceInventories, out var destinationInventories);
            ct.ThrowIfCancellationRequested();
            //Filter the source and destination inventories based on the applicable items so we have less to sort
            Dictionary<(ulong, InventoryType), List<FilteredItem>> filteredSources = new();
            var sourceKeys = sourceInventories.Select(c => c.Key);
            Logger.LogTrace(sourceInventories.Count() + " inventories to examine.");

            foreach (var availableFilter in filtersWithValues)
            {
                availableFilter.InvalidateSearchCache();
            }

            foreach (var sourceInventory in sourceInventories)
            {
                if (!filteredSources.ContainsKey(sourceInventory.Key))
                {
                    filteredSources.Add(sourceInventory.Key, new List<FilteredItem>());
                }

                foreach (var item in sourceInventory.Value)
                {
                    var filteredItem = filter.FilterItem(filtersWithValues, item);
                    if (filteredItem != null)
                    {
                        filteredSources[sourceInventory.Key].Add(filteredItem);
                    }
                }
            }

            var slotsAvailable = new Dictionary<(ulong, InventoryType), Queue<InventoryItem>>();
            var itemLocations = new Dictionary<int, List<InventoryItem>>();
            var absoluteItemLocations = new Dictionary<int, HashSet<(ulong, InventoryType)>>();
            foreach (var destinationInventory in destinationInventories)
            {
                foreach (var destinationItem in destinationInventory.Value)
                {
                    if (!slotsAvailable.ContainsKey(destinationInventory.Key))
                    {
                        slotsAvailable.Add(destinationInventory.Key, new Queue<InventoryItem>());
                    }

                    destinationItem.TempQuantity = destinationItem.Quantity;
                    if (destinationItem.IsEmpty && !destinationItem.IsEquippedGear)
                    {
                        slotsAvailable[destinationInventory.Key].Enqueue(destinationItem);
                    }
                    else
                    {
                        var filteredDestinationItem = filter.FilterItem(filtersWithValues, destinationItem);
                        if (filteredDestinationItem != null)
                        {
                            var itemHashCode = destinationItem.GenerateHashCode(filter.IgnoreHQWhenSorting ?? false);
                            if (!itemLocations.ContainsKey(itemHashCode))
                            {
                                itemLocations.Add(itemHashCode, new List<InventoryItem>());
                            }

                            itemLocations[itemHashCode].Add(destinationItem);
                        }
                    }
                }
            }

            foreach (var sourceInventory in filteredSources)
            {
                for (var index = 0; index < sourceInventory.Value.Count; index++)
                {
                    ct.ThrowIfCancellationRequested();
                    var filteredItem = sourceInventory.Value[index];
                    var sourceItem = filteredItem.Item;
                    if (sourceItem.IsEmpty) continue;
                    sourceItem.TempQuantity = sourceItem.Quantity;
                    //Item already seen, try to put it into that container
                    var hashCode = sourceItem.GenerateHashCode(filter.IgnoreHQWhenSorting ?? false);
                    if (itemLocations.ContainsKey(hashCode))
                    {
                        for (var i = 0; i < itemLocations[hashCode].Count; i++)
                        {
                            ct.ThrowIfCancellationRequested();
                            var existingItem = itemLocations[hashCode][i];
                            //Don't compare inventory to itself
                            if (existingItem.RetainerId == sourceItem.RetainerId && existingItem.SortedCategory == sourceItem.SortedCategory)
                            {
                                continue;
                            }

                            if (!existingItem.FullStack)
                            {
                                var existingCapacity = existingItem.RemainingTempStack;
                                var canFit = Math.Min(existingCapacity, sourceItem.TempQuantity);
                                if (canFit != 0)
                                {
                                    //All the item can fit, stick it in and continue
                                    if (filter.InActiveInventories(activeCharacter, activeRetainer,
                                        sourceInventory.Key.Item1, existingItem.RetainerId))
                                    {
                                        var sortingResult = new SortingResult(sourceInventory.Key.Item1,
                                            existingItem.RetainerId, sourceItem.SortedContainer,
                                            existingItem.SortedContainer,existingItem.BagLocation(existingItem.SortedContainer),false, sourceItem, existingItem, (int) canFit);

                                        searchResults.Add(new SearchResult(sortingResult));
                                    }

                                    if (!absoluteItemLocations.ContainsKey(hashCode))
                                    {
                                        absoluteItemLocations.Add(hashCode,
                                            new HashSet<(ulong, InventoryType)>());
                                    }

                                    absoluteItemLocations[hashCode]
                                        .Add((existingItem.RetainerId, existingItem.SortedContainer));
                                    existingItem.TempQuantity += canFit;
                                    sourceItem.TempQuantity -= canFit;
                                }
                            }
                            else
                            {
                                if (!absoluteItemLocations.ContainsKey(hashCode))
                                {
                                    absoluteItemLocations.Add(hashCode, new HashSet<(ulong, InventoryType)>());
                                }

                                absoluteItemLocations[hashCode]
                                    .Add((existingItem.RetainerId, existingItem.SortedContainer));
                            }

                            if (sourceItem.TempQuantity == 0)
                            {
                                break;
                            }
                        }


                        //The item is empty, continue, otherwise work out what inventory we should try to place it in
                        if (sourceItem.TempQuantity == 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (absoluteItemLocations.ContainsKey(hashCode))
                            {
                                var seenInventoryLocations = absoluteItemLocations[hashCode];
                                while (seenInventoryLocations.Count != 0 && sourceItem.TempQuantity != 0)
                                {
                                    ct.ThrowIfCancellationRequested();
                                    var seenInventoryLocation = seenInventoryLocations.First();
                                    if (slotsAvailable.ContainsKey(seenInventoryLocation))
                                    {
                                        var slotCount = slotsAvailable[seenInventoryLocation].Count;
                                        if (slotCount != 0)
                                        {
                                            var nextEmptySlot = slotsAvailable[seenInventoryLocation].Dequeue();
                                            if (sourceItem.Item.Base is {IsUnique: false})
                                            {
                                                if (sourceInventory.Key.Item1 != seenInventoryLocation.Item1 ||
                                                    sourceItem.SortedContainer != seenInventoryLocation.Item2)
                                                {
                                                    if (filter.InActiveInventories(activeCharacter, activeRetainer,
                                                        sourceInventory.Key.Item1, seenInventoryLocation.Item1))
                                                    {
                                                        var sortingResult = new SortingResult(sourceInventory.Key.Item1,
                                                            seenInventoryLocation.Item1, sourceItem.SortedContainer,
                                                            seenInventoryLocation.Item2, nextEmptySlot.BagLocation(nextEmptySlot.SortedContainer),true, sourceItem, nextEmptySlot,
                                                            (int) sourceItem.TempQuantity);
                                                        searchResults.Add(new SearchResult(sortingResult));
                                                    }

                                                    sourceItem.TempQuantity -= sourceItem.TempQuantity;
                                                }
                                            }

                                            continue;
                                        }
                                    }

                                    seenInventoryLocations.Remove(seenInventoryLocation);
                                }
                            }
                        }
                    }

                    if (filter.DuplicatesOnly == true)
                    {
                        continue;
                    }

                    if (sourceItem.TempQuantity == 0)
                    {
                        continue;
                    }

                    if (slotsAvailable.Count == 0)
                    {
                        continue;
                    }

                    foreach (var slot in slotsAvailable)
                    {
                        if (slot.Value.Count == 0)
                        {
                            slotsAvailable.Remove(slot.Key);
                        }
                    }

                    var nextSlots = slotsAvailable.Where(c =>
                        c.Value.Count != 0 &&
                        filter.InActiveInventories(activeCharacter, activeRetainer, sourceInventory.Key.Item1,
                            c.Key.Item1) && !(c.Key.Item1 == sourceItem.RetainerId &&
                                              c.Key.Item2.ToInventoryCategory() == sourceItem.SortedCategory)).ToList();

                    if (!nextSlots.Any())
                    {
                        continue;
                    }

                    var nextSlot = nextSlots.First();

                    //Don't compare inventory to itself
                    if (nextSlot.Value.Count != 0)
                    {
                        var nextEmptySlot = nextSlot.Value.Dequeue();
                        if (filter.InActiveInventories(activeCharacter, activeRetainer, sourceInventory.Key.Item1,
                            nextSlot.Key.Item1))
                        {
                            //This check stops the item from being sorted into it's own bag, this generally means its already in the optimal place
                            if (sourceInventory.Key.Item1 != nextSlot.Key.Item1 ||
                                sourceItem.SortedContainer != nextSlot.Key.Item2)
                            {
                                var sortingResult = new SortingResult(sourceInventory.Key.Item1, nextSlot.Key.Item1,
                                    sourceItem.SortedContainer, nextSlot.Key.Item2, nextEmptySlot.BagLocation(nextEmptySlot.SortedContainer),true, sourceItem, nextEmptySlot,
                                    (int) sourceItem.TempQuantity);
                                searchResults.Add(new SearchResult(sortingResult));

                                //We want to add the empty slot to the list of locations we know about, we need to create a copy and add that so any further items with the same ID can properly check how much room is left in the stack
                                nextEmptySlot = _inventoryItemFactory.Invoke();
                                nextEmptySlot.FromInventoryItem(nextEmptySlot);
                                nextEmptySlot.ItemId = sourceItem.ItemId;
                                nextEmptySlot.Flags = sourceItem.Flags;
                                nextEmptySlot.Quantity = sourceItem.Quantity;

                                //Add the destination item into the list of locations in case we have an empty slot for an item but multiple sources of the item.
                                var newLocationHash = sourceItem.GenerateHashCode(filter.IgnoreHQWhenSorting ?? false);
                                itemLocations.TryAdd(newLocationHash, new List<InventoryItem>());
                                itemLocations[newLocationHash].Add(nextEmptySlot);
                                absoluteItemLocations.TryAdd(newLocationHash,
                                    new HashSet<(ulong, InventoryType)>());
                                absoluteItemLocations[newLocationHash].Add((nextSlot.Key.Item1, nextEmptySlot.SortedContainer));
                            }
                        }
                    }
                    else
                    {
                        var sortingResult = new SortingResult(sourceInventory.Key.Item1, sourceItem.SortedContainer, sourceItem, (int) sourceItem.TempQuantity, false);
                        searchResults.Add(new SearchResult(sortingResult));
                    }
                }
            }
        }
        else if(filter.FilterType == FilterType.SearchFilter)
        {
            //Determine which source and destination inventories we actually need to examine
            GenerateSources(filter, inventories, out var sourceInventories);

            HashSet<int> distinctItems = new HashSet<int>();
            HashSet<int> duplicateItems = new HashSet<int>();

            //Filter the source and destination inventories based on the applicable items so we have less to sort
            Dictionary<(ulong, InventoryType), List<FilteredItem>> filteredSources = new();
            //Dictionary<(ulong, InventoryCategory), List<InventoryItem>> filteredDestinations = new();
            Logger.LogTrace(sourceInventories.Count() + " inventories to examine.");
            foreach (var sourceInventory in sourceInventories)
            {
                ct.ThrowIfCancellationRequested();
                if (!filteredSources.ContainsKey(sourceInventory.Key))
                {
                    filteredSources.Add(sourceInventory.Key, new List<FilteredItem>());
                }
                foreach (var item in sourceInventory.Value)
                {
                    ct.ThrowIfCancellationRequested();
                    if (item != null)
                    {
                        var filteredItem = filter.FilterItem(filtersWithValues, item);
                        if (filteredItem != null)
                        {
                            filteredSources[sourceInventory.Key].Add(filteredItem);
                        }
                    }
                }
            }
            if (filter.DuplicatesOnly.HasValue && filter.DuplicatesOnly == true)
            {
                foreach (var filteredSource in filteredSources)
                {
                    ct.ThrowIfCancellationRequested();
                    foreach (var item in filteredSource.Value)
                    {
                        ct.ThrowIfCancellationRequested();
                        var hashCode = item.Item.GenerateHashCode(filter.IgnoreHQWhenSorting ?? false);
                        if (distinctItems.Contains(hashCode))
                        {
                            if (!duplicateItems.Contains(hashCode))
                            {
                                duplicateItems.Add(hashCode);
                            }
                        }
                        else
                        {
                            distinctItems.Add(hashCode);
                        }
                    }
                }
            }

            foreach (var filteredSource in filteredSources)
            {
                ct.ThrowIfCancellationRequested();
                foreach (var filteredItem in filteredSource.Value)
                {
                    ct.ThrowIfCancellationRequested();
                    var item = filteredItem.Item;
                    if (filter.DuplicatesOnly.HasValue && filter.DuplicatesOnly == true)
                    {
                        if (duplicateItems.Contains(item.GenerateHashCode(filter.IgnoreHQWhenSorting ?? false)))
                        {
                            var sortingResult = new SortingResult(filteredSource.Key.Item1, item.SortedContainer,
                                item, (int)item.Quantity);
                            searchResults.Add(new SearchResult(sortingResult));
                        }

                    }
                    else
                    {
                        var sortingResult = new SortingResult(filteredSource.Key.Item1, item.SortedContainer,
                            item, (int)item.Quantity);
                        searchResults.Add(new SearchResult(sortingResult));
                    }

                }
            }
        }
        else if(filter.FilterType == FilterType.HistoryFilter)
        {
            var currentScopes = _sourceInventoriesFilter.CurrentValue(filter);

            var history = _inventoryHistory.GetHistory();
            var matchedItems = new List<InventoryChange>();
            if (currentScopes != null)
            {
                foreach (var item in history)
                {
                    ct.ThrowIfCancellationRequested();
                    var wasMatched = false;
                    if (item.FromItem != null)
                    {
                        wasMatched = _inventoryScopeCalculator.Filter(currentScopes, item.FromItem);
                    }

                    if (item.ToItem != null)
                    {
                        if (!wasMatched)
                        {
                            wasMatched = _inventoryScopeCalculator.Filter(currentScopes, item.ToItem);
                        }
                    }

                    if (wasMatched)
                    {
                        matchedItems.Add(item);
                    }
                }
            }

            foreach (var change in matchedItems.OrderByDescending(c => c.ChangeDate ?? new DateTime()))
            {
                ct.ThrowIfCancellationRequested();
                if (filter.FilterItem(filtersWithValues, change))
                {
                    searchResults.Add(new SearchResult(change));
                }
            }
        }
        else if (filter.FilterType == FilterType.CuratedList)
        {
            if (filter.CuratedItems != null)
            {
                foreach (var curatedItem in filter.CuratedItems)
                {
                    ct.ThrowIfCancellationRequested();
                    var itemRow = _itemSheet.GetRowOrDefault(curatedItem.ItemId);
                    if (itemRow != null)
                    {
                        searchResults.Add(new SearchResult(itemRow, curatedItem));
                    }
                }
            }
        }
        else
        {
            ct.ThrowIfCancellationRequested();
            searchResults = _itemSheet.Where(c => filter.FilterItem(filtersWithValues, c)).Where(c => c.RowId != 0).Select(c => new SearchResult(c)).ToList();
        }


        return searchResults;
    }

    private void GenerateSources(FilterConfiguration filter, List<Inventory> inventories, out Dictionary<(ulong, InventoryType), InventoryItem?[]> sourceInventories)
    {
        sourceInventories = new();

        var sourceScopes = _sourceInventoriesFilter.CurrentValue(filter);
        if (sourceScopes == null)
        {
            return;
        }

        foreach (var character in inventories)
        {
            foreach (var inventory in character.GetAllInventoriesByType())
            {
                var inventoryKey = (character.CharacterId, inventory.Key);
                if (!_inventoryScopeCalculator.Filter(sourceScopes, character.CharacterId, inventory.Key))
                {
                    continue;
                }
                if (!sourceInventories.ContainsKey(inventoryKey))
                {
                    sourceInventories.Add(inventoryKey, inventory.Value);
                }
            }
        }
    }

    private void GenerateSourceAndDestinations(FilterConfiguration filter, List<Inventory> inventories,
        out Dictionary<(ulong, InventoryType), List<InventoryItem>> sourceInventories, out Dictionary<(ulong, InventoryType), List<InventoryItem>> destinationInventories)
    {
        sourceInventories = new();
        destinationInventories = new();

        if(filter.FilterType == FilterType.SortingFilter)
        {
            var sourceScopes = _sourceInventoriesFilter.CurrentValue(filter);
            if (sourceScopes == null)
            {
                return;
            }

            foreach (var character in inventories)
            {
                foreach (var inventory in character.GetAllInventoriesByType())
                {
                    var type = inventory.Key;
                    var inventoryKey = (character.CharacterId, inventory.Key);
                    if (!_inventoryScopeCalculator.Filter(sourceScopes, character.CharacterId, inventory.Key))
                    {
                        continue;
                    }
                    if (!sourceInventories.ContainsKey(inventoryKey))
                    {
                        sourceInventories.Add(inventoryKey,
                            inventory.Value.Where(c => c != null && c.SortedContainer == type).ToList()!);
                    }
                }
            }
        }
        else
        {
            var sourceScopes = _sourceInventoriesFilter.CurrentValue(filter);
            if (sourceScopes == null)
            {
                return;
            }

            foreach (var character in inventories)
            {
                foreach (var inventory in character.GetAllInventoriesByType())
                {
                    var type = inventory.Key;
                    var inventoryKey = (character.CharacterId, inventory.Key);
                    if (!_inventoryScopeCalculator.Filter(sourceScopes, character.CharacterId, inventory.Key))
                    {
                        continue;
                    }
                    if (!sourceInventories.ContainsKey(inventoryKey))
                    {
                        sourceInventories.Add(inventoryKey,
                            inventory.Value.Where(c => c != null && c.SortedContainer == type).ToList()!);
                    }
                }
            }
        }
    }

    public Task RequestRefresh(FilterConfiguration configuration, List<Inventory>? inventories = null)
    {
        if (!configuration.AllowRefresh)
        {
            return Task.CompletedTask;
        }
        configuration.Refreshing = true;
        return FilterQueue.QueueBackgroundWorkItemAsync(configuration.Key,token =>
        {
            return Task.Run(() =>
            {
                try
                {
                    RefreshList(configuration, token);
                }
                catch (OperationCanceledException) {}
            }, token);
        });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem =
                await FilterQueue.DequeueAsync(stoppingToken);

            try
            {
                await workItem(stoppingToken);
            }
            catch (TaskCanceledException){}
            catch (Exception ex)
            {
                Logger.LogError(ex,
                    "Error occurred executing {WorkItem}.", nameof(workItem));
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogTrace("Stopping service {Type} ({This})", GetType().Name, this);
        await base.StopAsync(cancellationToken);
        Logger.LogTrace("Stopped service {Type} ({This})", GetType().Name, this);
    }
}