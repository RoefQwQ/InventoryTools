using System.Numerics;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters
{
    public class HighlightDestinationColourFilter : ColorFilter
    {
        public override FilterType AvailableIn { get; set; } =
            FilterType.SearchFilter | FilterType.CraftFilter | FilterType.SortingFilter | FilterType.GameItemFilter | FilterType.HistoryFilter | FilterType.CuratedList;
        public override string Key { get; set; } = "HighlightDestinationColor";
        public override string Name { get; set; } = "高亮去向颜色";

        public override string HelpText { get; set; } =
            "设置去向中与来源筛选匹配的物品的高亮颜色（需开启高亮去向重复项）。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Display;

        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
        {
            return null;
        }


        public override Vector4? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.DestinationHighlightColor;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, Vector4? newValue)
        {
            configuration.DestinationHighlightColor = newValue;
        }

        public HighlightDestinationColourFilter(ILogger<HighlightDestinationColourFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}