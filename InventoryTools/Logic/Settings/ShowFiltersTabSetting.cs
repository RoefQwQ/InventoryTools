using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ShowFiltersTabSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ShowFilterTab;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ShowFilterTab = newValue;
        }

        public override string Key { get; set; } = "ShowFiltersTab";
        public override string Name { get; set; } = "显示「所有列表」标签？";

        public override string HelpText { get; set; } =
            "主窗口是否显示包含所有可用列表的「所有列表」标签？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Windows;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public ShowFiltersTabSetting(ILogger<ShowFiltersTabSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_ShowFiltersTab_Name");
            HelpText = localizationService.GetString("Setting_ShowFiltersTab_HelpText");
        }
    }
}