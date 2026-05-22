using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;
using ImGuiService = InventoryTools.Services.ImGuiService;

namespace InventoryTools.EquipmentSuggest;

public class EquipmentSuggestFilterStatsField : BooleanFormField<EquipmentSuggestConfig>
{
    public EquipmentSuggestFilterStatsField(ImGuiService imGuiService) : base(imGuiService)
    {
    }

    public override bool DefaultValue { get; set; } = true;
    public override string Key { get; set; } = "FilterStats";
    public override string Name { get; set; } = "筛选属性";

    public override string HelpText { get; set; } =
        "是否根据属性筛选物品，例如选择制作职业时只显示具有制作相关属性的物品";
    public override string Version { get; set; } = "1.12.0.10";
}