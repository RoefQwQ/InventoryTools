using System.Numerics;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class HighlightDestinationColourSetting : ColorSetting
    {
        public override Vector4 DefaultValue { get; set; } = new Vector4(0.321f, 0.239f, 0.03f, 1f);
        public override Vector4 CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DestinationHighlightColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, Vector4 newValue)
        {
            configuration.DestinationHighlightColor = newValue;
        }

        public override string Key { get; set; } = "DestinationHighlightColour";
        public override string Name { get; set; } = "目的地高亮颜色";
        public override string HelpText { get; set; } = "设置目标中匹配源筛选条件的物品的颜色（假设已开启高亮目的地重复项）。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Colours;
        public override string Version => "1.7.0.0";

        public HighlightDestinationColourSetting(ILogger<HighlightDestinationColourSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_HighlightDestinationColour_Name");
            HelpText = localizationService.GetString("Setting_HighlightDestinationColour_HelpText");
        }
    }
}