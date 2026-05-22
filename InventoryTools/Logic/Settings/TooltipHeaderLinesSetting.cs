using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipHeaderLinesSetting : IntegerSetting
{
    public override int DefaultValue { get; set; } = 0;
    public override int CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipHeaderLines;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
    {
        configuration.TooltipHeaderLines = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayHeader";
    public override string Name { get; set; } = "标题空行";

    public override string HelpText { get; set; } =
        "此插件对工具提示进行的修改上方应添加多少空行？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Visuals;
    public override string Version => "1.7.0.0";

    public TooltipHeaderLinesSetting(ILogger<TooltipHeaderLinesSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_TooltipDisplayHeader_Name");
        HelpText = localizationService.GetString("Setting_TooltipDisplayHeader_HelpText");
    }
}