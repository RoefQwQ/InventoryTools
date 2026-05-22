using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ShowTooltipsSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DisplayTooltip;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.DisplayTooltip = newValue;
        }

        public override string Key { get; set; } = "ShowTooltips";
        public override string Name { get; set; } = "启用工具提示调整？";

        public override string HelpText { get; set; } =
            "禁用/启用插件的整个工具提示修改系统。关闭后，物品工具提示将不会有任何更改。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public ShowTooltipsSetting(ILogger<ShowTooltipsSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_ShowTooltips_Name");
            HelpText = localizationService.GetString("Setting_ShowTooltips_HelpText");
        }
    }
}