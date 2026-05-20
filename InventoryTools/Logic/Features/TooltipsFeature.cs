using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Features;

public class TooltipsFeature : Feature
{
    public TooltipsFeature(IEnumerable<ISetting> settings) : base(new[]
        {
            typeof(TooltipDisplayAmountOwnedSetting),
            typeof(TooltipMinimumMarketPriceSetting),
            typeof(TooltipDisplayUnlockSetting),
            typeof(TooltipSourceInformationEnabledSetting),
            typeof(TooltipUseInformationEnabledSetting),
            typeof(TooltipDisplayIngredientPatchSetting),
            typeof(TooltipDisplayCofferLootSetting),
            typeof(TooltipDisplayGlamourReadySetSetting),
        },
        settings)
    {
    }

    public override string Name { get; } = "工具提示";
    public override string Description { get; } =
        "Allagan Tools 可以在物品的工具提示中添加额外信息。请选择您希望在工具提示中显示的内容。如需进一步配置，包括更改每个工具提示的颜色及各工具提示的专属设置，请打开配置窗口。";
}