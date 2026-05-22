using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class ContextMenuOpenGatheringLogSetting : BooleanSetting
{
    public ContextMenuOpenGatheringLogSetting(ILogger<ContextMenuOpenGatheringLogSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_OpenGatheringLogContextMenu_Name");
        HelpText = localizationService.GetString("Setting_OpenGatheringLogContextMenu_HelpText");
        WizardName = localizationService.GetString("Setting_OpenGatheringLogContextMenu_WizardName");
    }

    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.OpenGatheringLogContextMenu;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.OpenGatheringLogContextMenu = newValue;
    }

    public override string Key { get; set; } = "OpenGatheringLogContextMenu";
    public override string Name { get; set; } = "右键菜单 - 打开采集笔记";

    public override string WizardName { get; } = "打开采集笔记";

    public override string HelpText { get; set; } =
        "在可采集物品的右键菜单中添加打开采集笔记的选项？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ContextMenu;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
    public override string Version { get; } = "1.11.0.2";
}