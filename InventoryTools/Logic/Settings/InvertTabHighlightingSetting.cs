using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class InvertTabHighlightingSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.InvertTabHighlighting;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.InvertTabHighlighting = newValue;
        }

        public override string Key { get; set; } = "InvertTabHighlighting";
        public override string Name { get; set; } = "反转标签高亮？";

        public override string HelpText { get; set; } =
            "是否高亮所有不匹配列表的标签？可在列表配置中覆盖此设置。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public InvertTabHighlightingSetting(ILogger<InvertTabHighlightingSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_InvertTabHighlighting_Name");
            HelpText = localizationService.GetString("Setting_InvertTabHighlighting_HelpText");
        }
    }
}