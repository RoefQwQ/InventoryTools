using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftTrackerTrackMarketBoardFilter : BooleanFilter
{
    public CraftTrackerTrackMarketBoardFilter(ILogger<CraftTrackerTrackMarketBoardFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override bool? DefaultValue { get; set; } = true;

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;

    public override string Key { get; set; } = "CraftTrackerTrackMarketBoard";
    public override string Name { get; set; } = "追踪市场板？";

    public override string HelpText { get; set; } =
        "当从市场购买物品且该物品与本制作列表中的产出物品匹配时，是否应减少该物品的数量？制作列表必须处于活动状态才会生效。";

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.CompletionTracking;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }
}