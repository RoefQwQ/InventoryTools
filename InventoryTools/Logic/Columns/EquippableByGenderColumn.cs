using System.Collections.Generic;
using AllaganLib.GameSheets.Model;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Extensions;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Columns
{
    public class EquippableByGenderColumn : TextColumn
    {
        public EquippableByGenderColumn(ILogger<EquippableByGenderColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
        public override ColumnCategory ColumnCategory => ColumnCategory.Basic;

        public override string? CurrentValue(ColumnConfiguration columnConfiguration, SearchResult searchResult)
        {
            return searchResult.Item.EquippableByGender.FormattedName();
        }

        public override string Name { get; set; } = "性别可装备？";
        public override float Width { get; set; } = 200;
        public override string HelpText { get; set; } = "显示该物品是否可由特定性别装备";
        public override bool HasFilter { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}