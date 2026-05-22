using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class InvertHighlightingSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.InvertHighlighting;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.InvertHighlighting = newValue;
        }

        public override string Key { get; set; } = "InvertHighlighting";
        public override string Name { get; set; } = "反转高亮？";

        public override string HelpText { get; set; } =
            "是否高亮所有不匹配列表的物品？可在列表配置中覆盖此设置。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public InvertHighlightingSetting(ILogger<InvertHighlightingSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_InvertHighlighting_Name");
            HelpText = localizationService.GetString("Setting_InvertHighlighting_HelpText");
        }
    }
}