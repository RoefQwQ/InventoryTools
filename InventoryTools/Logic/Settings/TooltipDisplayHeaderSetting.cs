using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayHeaderSetting : BooleanSetting
{
    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayHeader;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayHeader = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayHeader";
    public override string Name { get; set; } = "添加插件名称";

    public override string HelpText { get; set; } =
        "是否在工具提示修改上方显示 [Allagan Tools]？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
    public override string Version => "1.7.0.0";

    public TooltipDisplayHeaderSetting(ILogger<TooltipDisplayHeaderSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_TooltipDisplayHeader_Name");
        HelpText = localizationService.GetString("Setting_TooltipDisplayHeader_HelpText");
    }
}