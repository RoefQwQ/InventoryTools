using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftDefaultHQRequiredFilter : BooleanFilter
{
    public override bool? DefaultValue { get; set; } = false;
    public override string Key { get; set; } = "DefaultHQRequired";
    public override string Name { get; set; } = "默认需要HQ？";
    public override string HelpText { get; set; } = "列表中的每个物品是否都需要HQ版本（如适用）？";
    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Crafting;
    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }

    public override string[] GetChoices()
    {
        return ["是", "否"];
    }

    public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
    {
        configuration.CraftList.HQRequired = newValue ?? false;
        configuration.NotifyConfigurationChange();
    }

    public override bool? CurrentValue(FilterConfiguration configuration)
    {
        return configuration.CraftList.HQRequired;
    }

    public CraftDefaultHQRequiredFilter(ILogger<CraftDefaultHQRequiredFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
}