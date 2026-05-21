using System;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Features;

public class SampleFilter100GillOrLess : BooleanSetting, ISampleFilter
{
    private readonly IListService _listService;
    private readonly BuyFromVendorPriceFilter _buyFromVendorPriceFilter;
    private readonly FilterConfiguration.Factory _filterConfigFactory;
    private readonly Lazy<SourceInventoriesFilter> _sourceInventoriesFilter;

    public SampleFilter100GillOrLess(ILogger<SampleFilter100GillOrLess> logger, ImGuiService imGuiService, ILocalizationService localizationService, IListService listService, BuyFromVendorPriceFilter buyFromVendorPriceFilter, FilterConfiguration.Factory filterConfigFactory, Lazy<SourceInventoriesFilter> sourceInventoriesFilter) : base(logger, imGuiService, localizationService)
    {
        _listService = listService;
        _buyFromVendorPriceFilter = buyFromVendorPriceFilter;
        _filterConfigFactory = filterConfigFactory;
        _sourceInventoriesFilter = sourceInventoriesFilter;
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

    public override string Key { get; set; } = "sample1";
    public override string Name { get; set; } = "100金币以下";
    public string SampleDefaultName => "100金币以下";

    public string SampleDescription =>
        "这将添加一个列表，显示所有可在金币商店以100金币以下购买的物品。它会搜索角色和雇员背包。";

    public SampleFilterType SampleFilterType => SampleFilterType.Sample;
    public override string HelpText { get; set; } = "显示商店中售价低于100金币的物品。";
    public override SettingCategory SettingCategory { get; set; } = SettingCategory.None;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.None;
    public override string Version => "1.7.0.0";
    public bool ShouldAdd => _shouldAdd;
    public FilterConfiguration AddFilter()
    {
        var sampleFilter = _filterConfigFactory.Invoke();
        sampleFilter.Name = Name;
        sampleFilter.FilterType = FilterType.SearchFilter;
        sampleFilter.DisplayInTabs = true;
        _sourceInventoriesFilter.Value.UpdateFilterConfiguration(sampleFilter, [
            new InventorySearchScope()
            {
                ActiveCharacter = true,
                CharacterTypes = [CharacterType.Character, CharacterType.Retainer, CharacterType.FreeCompanyChest]
            }
        ]);
        _buyFromVendorPriceFilter.UpdateFilterConfiguration(sampleFilter, "<=100");
        _listService.AddDefaultColumns(sampleFilter);
        _listService.AddList(sampleFilter);
        return sampleFilter;
    }
}