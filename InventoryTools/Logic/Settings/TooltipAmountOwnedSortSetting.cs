using System.Collections.Generic;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipAmountOwnedSortSetting : ChoiceSetting<TooltipAmountOwnedSort>
{
    public TooltipAmountOwnedSortSetting(ILogger<TooltipAmountOwnedSortSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_TooltipAmountOwnedSort_Name");
        HelpText = localizationService.GetString("Setting_TooltipAmountOwnedSort_HelpText");
    }

    public override TooltipAmountOwnedSort DefaultValue { get; set; } = TooltipAmountOwnedSort.Alphabetically;
    public override TooltipAmountOwnedSort CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipAmountOwnedSort;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, TooltipAmountOwnedSort newValue)
    {
        configuration.TooltipAmountOwnedSort = newValue;
    }

    public override string Key { get; set; } = "TooltipAmountOwnedSort";
    public override string Name { get; set; } = "添加物品位置（排序）";

    public override string HelpText { get; set; } =
        "工具提示中拥有的物品应如何排序？如果排序性能不佳，可选择「无」。";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.AddItemLocations;
    public override string Version { get; } = "1.7.0.17";

    public override Dictionary<TooltipAmountOwnedSort, string> Choices { get; } =
        new Dictionary<TooltipAmountOwnedSort, string>()
        {
            { TooltipAmountOwnedSort.Alphabetically, "Alphabetical Order(Character/Retainer/etc)" },
            { TooltipAmountOwnedSort.Categorically, "Alphabetical Order(Category)" },
            { TooltipAmountOwnedSort.Quantity, "Item Quantity" },
            { TooltipAmountOwnedSort.None, "No Order" },
        };
}

public enum TooltipAmountOwnedSort
{
    Alphabetically,
    Categorically,
    Quantity,
    None
}