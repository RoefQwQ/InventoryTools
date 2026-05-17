using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ChineseFontSizeSetting : IntegerSetting
    {
        public override int DefaultValue { get; set; } = 16;
        public override int MinValue { get; set; } = 10;
        public override int MaxValue { get; set; } = 32;

        public override int CurrentValue(InventoryToolsConfiguration configuration)
        {
            return (int)configuration.ChineseFontSize;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
        {
            configuration.ChineseFontSize = newValue;
        }

        public override string Key { get; set; } = "ChineseFontSize";
        public override string Name { get; set; } = "中文字体大小";

        public override string HelpText { get; set; } =
            "中文字体的显示大小（像素）。更改后需要重建字体。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.General;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public ChineseFontSizeSetting(ILogger<ChineseFontSizeSetting> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
    }
}
