using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipColorSetting : GameColorSetting
    {
        public TooltipColorSetting(ILogger<TooltipColorSetting> logger, ImGuiService imGuiService, ExcelSheet<UIColor> uiColorSheet, ILocalizationService localizationService) : base(logger, imGuiService, uiColorSheet, localizationService)
        {
            Name = localizationService.GetString("Setting_TooltipColor_Name");
            HelpText = localizationService.GetString("Setting_TooltipColor_HelpText");
        }
        public override uint? DefaultValue { get; set; } = null;
        public override uint? CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipColor;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, uint? newValue)
        {
            configuration.TooltipColor = newValue;
        }

        public override string Key { get; set; } = "TooltipColor";
        public override string Name { get; set; } = "文字颜色";
        public override string HelpText { get; set; } = "添加到物品工具提示中的文字颜色。可以通过进入工具提示设置来为每个工具提示模块设置独立的颜色。";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Visuals;
        public override string Version => "1.7.0.0";
    }
}