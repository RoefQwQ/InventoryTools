using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class ContextMenuOpenCraftingLogSetting : BooleanSetting
{
    public ContextMenuOpenCraftingLogSetting(ILogger<ContextMenuOpenCraftingLogSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_OpenCraftingLogContextMenu_Name");
        HelpText = localizationService.GetString("Setting_OpenCraftingLogContextMenu_HelpText");
        WizardName = localizationService.GetString("Setting_OpenCraftingLogContextMenu_WizardName");
    }

    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.OpenCraftingLogContextMenu;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.OpenCraftingLogContextMenu = newValue;
    }

    public override string Key { get; set; } = "OpenCraftingLogContextMenu";
    public override string Name { get; set; } = "右键菜单 - 打开制作笔记";

    public override string WizardName { get; } = "打开制作笔记";

    public override string HelpText { get; set; } =
        "在可制作物品的右键菜单中添加打开制作笔记的选项？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ContextMenu;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
    public override string Version { get; } = "1.11.0.2";
}