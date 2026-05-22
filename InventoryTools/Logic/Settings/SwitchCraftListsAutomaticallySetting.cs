using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class SwitchCraftListsAutomaticallySetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.SwitchCraftListsAutomatically;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.SwitchCraftListsAutomatically = newValue;
        }

        public override string Key { get; set; } = "SwitchCraftListsAutomatically";
        public override string Name { get; set; } = "自动切换制作列表？";

        public override string HelpText { get; set; } =
            "在各制作列表间移动时，当前制作列表是否自动切换？仅在已有活动制作列表时才会切换。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Lists;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public SwitchCraftListsAutomaticallySetting(ILogger<SwitchCraftListsAutomaticallySetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_SwitchCraftListsAutomatically_Name");
            HelpText = localizationService.GetString("Setting_SwitchCraftListsAutomatically_HelpText");
        }
    }
}