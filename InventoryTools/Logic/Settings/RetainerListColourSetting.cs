using System.Numerics;
using Dalamud.Interface.Colors;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class RetainerListColourSetting : ColorSetting
    {
        public override Vector4 DefaultValue { get; set; } = ImGuiColors.HealerGreen;
        public override Vector4 CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.RetainerListColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, Vector4 newValue)
        {
            configuration.RetainerListColor = newValue;
        }

        public override string Key { get; set; } = "RetainerListColour";
        public override string Name { get; set; } = "雇员列表颜色";

    public override string HelpText { get; set; } =
        "设置雇员列表的颜色（当雇员包含筛选物品时）。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Colours;
        public override string Version => "1.7.0.0";

        public RetainerListColourSetting(ILogger<RetainerListColourSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_RetainerListColour_Name");
            HelpText = localizationService.GetString("Setting_RetainerListColour_HelpText");
        }
    }
}