using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib.Extensions;
using CriticalCommonLib.Models;
using CriticalCommonLib.Services;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Filters
{
    public class DestinationInventoriesFilter : InventoryScopeFilter
    {
        public DestinationInventoriesFilter(ILogger<DestinationInventoriesFilter> logger, InventoryScopePicker scopePicker, ImGuiService imGuiService) : base(scopePicker, logger, imGuiService)
        {
        }
        public override int LabelSize { get; set; } = 240;
        public override string Key { get; set; } = "DestinationInventories";
        public override string Name { get; set; } = "去向背包";
        public override string HelpText { get; set; } =
            "定义作为去向的背包，插件将尝试从「来源背包」中找到的物品整理到「去向背包」中。可根据下方的范围配置查看已找到的背包。";
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Inventories;
        public override List<InventorySearchScope>? DefaultValue { get; set; } = null;
        public override FilterType AvailableIn { get; set; } = FilterType.SortingFilter;

        public override List<InventorySearchScope>? GenerateDefaultScope()
        {
            return null;
        }
    }
}