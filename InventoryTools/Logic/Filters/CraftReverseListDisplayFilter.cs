using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters;

public class CraftReverseListDisplayFilter : BooleanFilter
{
    public CraftReverseListDisplayFilter(ILogger<CraftReverseListDisplayFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
    {
    }

    public override string Key { get; set; } = "CraftReverseListDisplay";
    public override string Name { get; set; } = "反转列表显示？";

    public override string HelpText { get; set; } =
        "是否以反转顺序显示制作列表？即产出物品是否从底部开始？（仅在制作显示模式为单表格时适用）";

    public override FilterType AvailableIn { get; set; } = FilterType.CraftFilter;

    public override FilterCategory FilterCategory { get; set; } = FilterCategory.Display;
    public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
    {
        return null;
    }

    public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
    {
        return null;
    }
}