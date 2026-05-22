using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Models;

using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters
{
    public class FilterORFilter : BooleanFilter
    {
        public override string Key { get; set; } = "ORFilter";
        public override string Name { get; set; } = "使用或筛选器";

        public override string HelpText { get; set; } =
            "筛选物品时，每组筛选默认使用AND缩小物品范围，启用此选项将改用OR逻辑。";

        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Advanced;



        public override bool? CurrentValue(FilterConfiguration configuration)
        {
            return configuration.UseORFiltering;
        }

        public override void UpdateFilterConfiguration(FilterConfiguration configuration, bool? newValue)
        {
            configuration.UseORFiltering = newValue;
        }

        public override bool? FilterItem(FilterConfiguration configuration, InventoryItem item)
        {
            return null;
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemRow item)
        {
            return null;
        }

        public FilterORFilter(ILogger<FilterORFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}