using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class HighlightDestinationSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.HighlightDestination;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.HighlightDestination = newValue;
        }

        public override string Key { get; set; } = "HighlightDestination";
        public override string Name { get; set; } = "高亮去向？";

        public override string HelpText { get; set; } =
            "是否高亮物品的去向？可在筛选器配置中覆盖此设置。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public HighlightDestinationSetting(ILogger<HighlightDestinationSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_HighlightDestinationDuplicates_Name");
        HelpText = localizationService.GetString("Setting_HighlightDestinationDuplicates_HelpText");
    }
    }
}