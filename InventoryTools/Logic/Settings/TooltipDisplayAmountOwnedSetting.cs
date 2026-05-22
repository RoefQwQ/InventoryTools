using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipDisplayAmountOwnedSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipDisplayAmountOwned;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipDisplayAmountOwned = newValue;
        }

        public override string Key { get; set; } = "TooltipDisplayOwned";
        public override string Name { get; set; } = "添加物品位置";

        public override string WizardName { get; } = "添加物品位置";

        public override string HelpText { get; set; } =
            "悬停物品时，工具提示是否显示您当前拥有的该物品的位置？";

        public override uint? Order { get; } = 0;

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.AddItemLocations;
        public override string Version => "1.7.0.0";

        public TooltipDisplayAmountOwnedSetting(ILogger<TooltipDisplayAmountOwnedSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_TooltipDisplayOwned_Name");
            HelpText = localizationService.GetString("Setting_TooltipDisplayOwned_HelpText");
            WizardName = localizationService.GetString("Setting_TooltipDisplayOwned_WizardName");
        }
    }
}