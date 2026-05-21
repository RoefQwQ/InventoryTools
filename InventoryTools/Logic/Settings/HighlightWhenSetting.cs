using System.Collections.Generic;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class HighlightWhenSetting : ChoiceSetting<HighlightWhen>
    {
        private readonly Dictionary<HighlightWhen, string> _staticChoices = new()
        {
            {HighlightWhen.Always, "Always"}, {HighlightWhen.WhenSearching, "When Searching"}
        };

        public override HighlightWhen DefaultValue { get; set; } = HighlightWhen.WhenSearching;

        public override HighlightWhen CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.HighlightWhenEnum;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, HighlightWhen newValue)
        {
            configuration.HighlightWhenEnum = newValue;
        }

        public override string Key { get; set; } = "HighlightWhen";
        public override string Name { get; set; } = "何时高亮？";
        public override string HelpText { get; set; } = "为列表开启高亮时，应该始终生效还是仅在搜索列时生效？";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;

        public override Dictionary<HighlightWhen, string> Choices => _staticChoices;
        public override string Version => "1.7.0.0";

        public HighlightWhenSetting(ILogger<HighlightWhenSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_HighlightWhen_Name");
            HelpText = localizationService.GetString("Setting_HighlightWhen_HelpText");
        }
    }
}