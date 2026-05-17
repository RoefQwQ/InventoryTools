using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ChineseFontEnabledSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ChineseFontEnabled;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ChineseFontEnabled = newValue;
        }

        public override string Key { get; set; } = "ChineseFontEnabled";
        public override string Name { get; set; } = "启用中文字体";

        public override string HelpText { get; set; } =
            "加载 Noto Sans CJK SC 字体以支持中文显示。需要重建字体后生效。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public ChineseFontEnabledSetting(ILogger<ChineseFontEnabledSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}
