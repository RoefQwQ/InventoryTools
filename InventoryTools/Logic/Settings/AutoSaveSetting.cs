using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class AutoSaveSetting : BooleanSetting
    {
        private readonly PluginLogic _pluginLogic;

        public AutoSaveSetting(ILogger<AutoSaveSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, PluginLogic pluginLogic) : base(logger, imGuiService, localizationService)
        {
            _pluginLogic = pluginLogic;
            Name = localizationService.GetString("Setting_AutoSave_Name");
            HelpText = localizationService.GetString("Setting_AutoSave_HelpText");
            WizardName = localizationService.GetString("Setting_AutoSave_WizardName");
        }
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.AutoSave;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.AutoSave = newValue;
            _pluginLogic.ClearAutoSave();
        }

        public override string Key { get; set; } = "AutoSave";
        public override string Name { get; set; } = "自动保存背包/配置？";

        public override string WizardName { get; } = "自动保存背包？";

        public override string HelpText { get; set; } =
            "是否按设定间隔自动保存背包/配置？虽然插件会在游戏关闭和配置更改时保存，但崩溃时不会保存，此功能可缓解此问题。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.AutoSave;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";
    }
}