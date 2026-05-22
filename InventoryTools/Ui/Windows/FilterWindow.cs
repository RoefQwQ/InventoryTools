using System;
using System.Collections.Generic;
using System.Numerics;
using CriticalCommonLib;
using CriticalCommonLib.MarketBoard;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using DalaMock.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic;
using InventoryTools.Ui.Widgets;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Plugin.Services;
using InventoryTools.Mediator;
using InventoryTools.Lists;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ImGuiUtil = OtterGui.ImGuiUtil;

namespace InventoryTools.Ui
{
    public class FilterWindow : StringWindow
    {
        private readonly TableService _tableService;
        private readonly IListService _listService;
        private readonly ICharacterMonitor _characterMonitor;
        private readonly IUniversalis _universalis;
        private readonly IFileDialogManager _fileDialogManager;

        private readonly IFramework _framework;
        private readonly InventoryToolsConfiguration _configuration;
        private readonly ILocalizationService _localizationService;

        public FilterWindow(ILogger<FilterWindow> logger, MediatorService mediator, ImGuiService imGuiService, InventoryToolsConfiguration configuration, TableService tableService, IListService listService, ICharacterMonitor characterMonitor, IUniversalis universalis, IFileDialogManager fileDialogManager, IFramework framework, ILocalizationService localizationService, string name = "Filter Window") : base(logger, mediator, imGuiService, configuration, name)
        {
            _tableService = tableService;
            _listService = listService;
            _characterMonitor = characterMonitor;
            _universalis = universalis;
            _fileDialogManager = fileDialogManager;
            _framework = framework;
            _configuration = configuration;
            _localizationService = localizationService;
        }
        public override void Initialize(string filterKey)
        {
            base.Initialize(filterKey);
            _filterKey = filterKey;
            if (SelectedConfiguration != null)
            {
                Key = "filter_" + filterKey;
                WindowName = _localizationService.GetString("Window_FilterWindow_WindowName") + SelectedConfiguration.Name;
            }
            else
            {
                Key = "filter_invalid";
                WindowName = _localizationService["Window_Filter_InvalidList"];
            }

            _settingsMenu = new PopupMenu("configMenu", PopupMenu.PopupMenuButtons.All,
                new List<PopupMenu.IPopupMenuItem>
                {
                    new PopupMenu.PopupMenuItemSelectable(_localizationService["Window_Filter_MenuHelp"], "help", _ => MediatorService.Publish(new OpenGenericWindowMessage(typeof(HelpWindow))), _localizationService["Window_Filter_TooltipHelpWindow"]),
                });
        }

        private HoverButton _settingsIcon = new();
        private HoverButton _csvIcon = new();
        private HoverButton _clearIcon = new();
        private HoverButton _marketIcon = new();
        private HoverButton _menuIcon = new();
        private HoverButton _filtersIcon = new();

        private PopupMenu _settingsMenu;

        public override void Invalidate()
        {

        }

        private string _filterKey;

        public override FilterConfiguration? SelectedConfiguration =>
            _listService.GetListByKey(_filterKey);

        public override string GenericKey { get; } = "filter";
        public override string GenericName => _localizationService["Window_Filter_GenericName"];
        public override bool DestroyOnClose => true;
        public override bool SaveState => true;

        public override void OnClose()
        {
            if (SelectedConfiguration != null)
            {
                SelectedConfiguration.Active = false;
            }
            base.OnClose();
        }

        public override void DrawWindow()
        {
            var filterConfiguration = SelectedConfiguration;
            if (filterConfiguration != null)
            {
                if (filterConfiguration.Active != true)
                {
                    filterConfiguration.Active = true;
                    filterConfiguration.NeedsRefresh = true;
                }

                if (ImGui.IsWindowFocused())
                {
                    if (_configuration.SwitchFiltersAutomatically &&
                        _configuration.ActiveUiFilter != filterConfiguration.Key &&
                        _configuration.ActiveUiFilter != null)
                    {
                        _framework.RunOnFrameworkThread(() =>
                        {
                            _listService.ToggleActiveUiList(filterConfiguration);
                        });
                    }
                }

                var table = _tableService.GetListTable(filterConfiguration);
                DrawFilter(table, filterConfiguration);
                if (ImGui.IsWindowFocused())
                {
                    if (_configuration.SwitchFiltersAutomatically &&
                        _configuration.ActiveUiFilter != filterConfiguration.Key &&
                        _configuration.ActiveUiFilter != null)
                    {
                        _framework.RunOnFrameworkThread(() =>
                        {
                            _listService.ToggleActiveUiList(filterConfiguration);
                        });
                    }
                }
            }
        }

        public string DrawFilter(FilterTable itemTable, FilterConfiguration filterConfiguration)
        {
            using (var topBarChild = ImRaii.Child("TopBar", new Vector2(0, 40) * ImGui.GetIO().FontGlobalScale, true,
                       ImGuiWindowFlags.NoScrollbar))
            {
                if (topBarChild.Success)
                {
                    var highlightItems = itemTable.HighlightItems;
                    ImGuiService.CenterElement(20 * ImGui.GetIO().FontGlobalScale);
                    ImGui.Checkbox(_localizationService["Window_Filter_CheckboxHighlight"] + "###" + itemTable.Key + "VisibilityCheckbox",
                        ref highlightItems);
                    if (highlightItems != itemTable.HighlightItems)
                    {
                        _framework.RunOnFrameworkThread(() =>
                        {
                            _listService.ToggleActiveUiList(itemTable.FilterConfiguration);
                        });
                    }

                    ImGui.SameLine();
                    ImGuiService.CenterElement(20 * ImGui.GetIO().FontGlobalScale);
                    if(_clearIcon.Draw(ImGuiService.GetIconTexture(66308).Handle, "clearSearch"))
                    {
                        itemTable.ClearFilters();
                    }

                    ImGuiUtil.HoverTooltip(_localizationService["Window_Filter_TooltipClearSearch"]);

                }
            }
            using (var contentChild = ImRaii.Child("Content", new Vector2(0, -40) * ImGui.GetIO().FontGlobalScale, true,
                       ImGuiWindowFlags.NoScrollbar))
            {
                if (contentChild.Success)
                {
                    MediatorService.Publish(itemTable.Draw(new Vector2(0, 0)));
                }
            }

            //Need to have these buttons be determined dynamically or moved elsewhere
            using (var bottomBarChild =
                   ImRaii.Child("BottomBar", new Vector2(0, 0), true, ImGuiWindowFlags.NoScrollbar))
            {
                if (bottomBarChild.Success)
                {
                    ImGuiService.CenterElement(24 * ImGui.GetIO().FontGlobalScale);
                    if(_marketIcon.Draw(ImGuiService.GetImageTexture("refresh-web").Handle, "refreshMarket"))
                    {
                        var activeCharacter = _characterMonitor.ActiveCharacter;
                        if (activeCharacter != null)
                        {
                            foreach (var item in itemTable.SearchResults)
                            {
                                _universalis.QueuePriceCheck(item.Item.RowId, activeCharacter.WorldId);
                            }
                        }
                    }

                    ImGuiUtil.HoverTooltip(_localizationService["Window_Filter_TooltipRefreshMarket"]);
                    ImGui.SameLine();
                    ImGuiService.CenterElement(24 * ImGui.GetIO().FontGlobalScale);
                    if (_csvIcon.Draw(ImGuiService.GetImageTexture("export2").Handle, "exportCsv"))
                    {
                        _fileDialogManager.SaveFileDialog("Save to csv", "*.csv", "export.csv", ".csv",
                            (b, s) => { SaveCallback(itemTable, b, s); }, null, true);
                    }

                    ImGuiUtil.HoverTooltip(_localizationService["Window_Filter_TooltipExportCsv"]);
                    ImGui.SameLine();
                    ImGuiService.VerticalCenter(_localizationService["Window_Filter_PendingMarketRequests"] + _universalis.QueuedCount);

                    itemTable.DrawFooterItems();

                    var width = ImGui.GetWindowSize().X;
                    width -= 30 * ImGui.GetIO().FontGlobalScale;
                    ImGui.SetCursorPosX(width);
                    ImGuiService.CenterElement(24 * ImGui.GetIO().FontGlobalScale);
                    if (_menuIcon.Draw(ImGuiService.GetImageTexture("menu").Handle, "openMenu"))
                    {
                    }
                    _settingsMenu.Draw();

                    width -= 30 * ImGui.GetIO().FontGlobalScale;
                    ImGuiService.CenterElement(24 * ImGui.GetIO().FontGlobalScale);
                    ImGui.SetCursorPosX(width);
                    if (_settingsIcon.Draw(ImGuiService.GetIconTexture(66319).Handle,"openConfig"))
                    {
                        MediatorService.Publish(new ToggleGenericWindowMessage(typeof(ConfigurationWindow)));
                    }

                    ImGuiUtil.HoverTooltip(_localizationService["Window_Filter_TooltipOpenConfig"]);

                    ImGui.SetCursorPosY(0);
                    width -= 30 * ImGui.GetIO().FontGlobalScale;
                    ImGui.SetCursorPosX(width);
                    ImGuiService.CenterElement(24 * ImGui.GetIO().FontGlobalScale);
                    if (_filtersIcon.Draw(ImGuiService.GetImageTexture("filters").Handle, "openFilters"))
                    {
                        MediatorService.Publish(new ToggleGenericWindowMessage(typeof(FiltersWindow)));
                    }

                    ImGuiUtil.HoverTooltip(_localizationService["Window_Filter_TooltipOpenItems"]);



                    var totalItems =  itemTable.RenderSearchResults.Count + " items";

                    if (SelectedConfiguration != null && SelectedConfiguration.FilterType == FilterType.GameItemFilter)
                    {
                        totalItems =  itemTable.RenderSearchResults.Count + " items";
                    }

                    if (SelectedConfiguration != null && SelectedConfiguration.FilterType == FilterType.HistoryFilter)
                    {
                        totalItems =  itemTable.RenderSearchResults.Count + " historical records";
                    }

                    var calcTextSize = ImGui.CalcTextSize(totalItems);
                    width -= calcTextSize.X + 15;
                    ImGui.SetCursorPosX(width);
                    ImGuiService.VerticalCenter(totalItems);
                }
            }

            return filterConfiguration.Key;
        }

        private void SaveCallback(FilterTable filterTable, bool arg1, string arg2)
        {
            if (arg1)
            {
                filterTable.ExportToCsv(arg2);
            }
        }

        public override Vector2? DefaultSize => new Vector2(600, 500);
        public override Vector2? MaxSize => new Vector2(1500, 1500);
        public override Vector2? MinSize => new Vector2(200, 200);
    }
}