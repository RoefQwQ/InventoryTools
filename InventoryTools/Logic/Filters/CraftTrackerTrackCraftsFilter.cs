using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftTrackerTrackCraftsFilter : BooleanFilter
{
    public CraftTrackerTrackCraftsFilter(ILogger<CraftTrackerTrackCraftsFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }
    
    public override bool? DefaultValue { get; set; } = true;
    
    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;
    
    public override string Key { get; set; } = "CraftTrackerTrackCrafts";
    public override string Name { get; set; } = "追踪制作？";
    
    public override string HelpText { get; set; } =
        "当完成制作且制作出的物品与本制作列表中的产出物品匹配时，是否应减少该物品的数量？制作列表必须处于活动状态才会生效。";
    
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