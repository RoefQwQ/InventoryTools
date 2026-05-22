using System.Collections.Generic;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftListModeFilter : ChoiceFilter<CraftListMode>
{
    public CraftListModeFilter(ILogger<CraftListModeFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override CraftListMode CurrentValue(FilterConfiguration configuration)
    {
        return configuration.CraftList.CraftListMode;
    }

    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }

    public override void ResetFilter(FilterConfiguration configuration)
    {
        this.UpdateFilterConfiguration(configuration, DefaultValue);
    }

    public override void UpdateFilterConfiguration(FilterConfiguration configuration, CraftListMode newValue)
    {
        configuration.CraftList.CraftListMode = newValue;
    }

    public override string Key { get; set; } = "CraftListMode";
    public override string Name { get; set; } = "列表模式";

    public override string HelpText { get; set; } =
        "制作列表应以正常模式还是库存模式运行。在正常模式下，输入数量后随着制作的进行该数字会减少。在库存模式下，输入数量后该数字会根据角色背包中的物品数量上升。";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Settings;

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    public override CraftListMode DefaultValue { get; set; } = CraftListMode.Normal;
    public override List<CraftListMode> GetChoices(FilterConfiguration configuration)
    {
        return new List<CraftListMode>()
        {
            CraftListMode.Normal,
            CraftListMode.Stock,
        };
    }

    public override string GetFormattedChoice(FilterConfiguration filterConfiguration, CraftListMode choice)
    {
        return choice.ToString();
    }
}