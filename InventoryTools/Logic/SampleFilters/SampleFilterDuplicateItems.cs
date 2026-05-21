using System.Collections.Generic;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Features;

public class SampleFilterDuplicateItems : BooleanSetting, ISampleFilter
{
    private readonly IListService _listService;
    private readonly FilterConfiguration.Factory _filterConfigFactory;
    private readonly SourceInventoriesFilter _sourceInventoriesFilter;
    private readonly DestinationInventoriesFilter _destinationInventoriesFilter;
    private readonly HighlightWhenFilter _highlightWhenFilter;

    public SampleFilterDuplicateItems(ILogger<SampleFilterDuplicateItems> logger, ImGuiService imGuiService,
        ILocalizationService localizationService, IListService listService, FilterConfiguration.Factory filterConfigFactory,
        SourceInventoriesFilter sourceInventoriesFilter,
        DestinationInventoriesFilter destinationInventoriesFilter,
        HighlightWhenFilter highlightWhenFilter) : base(logger, imGuiService, localizationService)
    {
        _listService = listService;
        _filterConfigFactory = filterConfigFactory;
        _sourceInventoriesFilter = sourceInventoriesFilter;
        _destinationInventoriesFilter = destinationInventoriesFilter;
        _highlightWhenFilter = highlightWhenFilter;
    }
    private bool _shouldAdd;
    public override bool DefaultValue { get; set; }
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return _shouldAdd;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        _shouldAdd = newValue;
    }

    public override string Key { get; set; } = "sample2";
    public override string Name { get; set; } = "重复物品";
    public override string HelpText { get; set; } = "查找在雇员和角色背包中有两个独立堆叠的物品，并尝试将它们合并为单个堆叠。这有助于确保你的雇员背包尽可能紧凑。";
    public string SampleDefaultName => "重复物品";
    public string SampleDescription =>
        "这将添加一个列表，显示在两个库存中同时出现的所有独立堆叠。你可以用它来确保只有一个雇员拥有特定类型的物品。";
    public SampleFilterType SampleFilterType => SampleFilterType.Sample;
    public override SettingCategory SettingCategory { get; set; } = SettingCategory.None;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.None;
    public override string Version => "1.7.0.0";
    public bool ShouldAdd => _shouldAdd;
    public FilterConfiguration AddFilter()
    {
        var sampleFilter = _filterConfigFactory.Invoke();
        sampleFilter.Name = Name;
        sampleFilter.FilterType = FilterType.SortingFilter;
        sampleFilter.DisplayInTabs = true;
        _sourceInventoriesFilter.UpdateFilterConfiguration(sampleFilter, [
            new InventorySearchScope()
            {
                ActiveCharacter = true,
                Categories = [InventoryCategory.CharacterBags, InventoryCategory.RetainerBags]
            }
        ]);
        _destinationInventoriesFilter.UpdateFilterConfiguration(sampleFilter, [
            new InventorySearchScope()
            {
                ActiveCharacter = true,
                Categories = [InventoryCategory.RetainerBags]
            }
        ]);
        _highlightWhenFilter.UpdateFilterConfiguration(sampleFilter, HighlightWhen.Always);
        sampleFilter.FilterItemsInRetainersEnum = FilterItemsRetainerEnum.Yes;
        sampleFilter.DuplicatesOnly = true;
        _listService.AddDefaultColumns(sampleFilter);
        _listService.AddList(sampleFilter);
        return sampleFilter;
    }
}