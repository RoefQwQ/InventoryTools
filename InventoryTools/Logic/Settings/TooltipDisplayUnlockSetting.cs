using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipDisplayUnlockSetting : BooleanSetting
{
    public TooltipDisplayUnlockSetting(ILogger<TooltipDisplayUnlockSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_TooltipDisplayUnlockSetting_Name");
        HelpText = localizationService.GetString("Setting_TooltipDisplayUnlockSetting_HelpText");
    }

    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipDisplayUnlock;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TooltipDisplayUnlock = newValue;
    }

    public override string Key { get; set; } = "TooltipDisplayUnlockSetting";
    public override string Name { get; set; } = "添加物品解锁状态";
    public override string HelpText { get; set; } = "如果物品可以解锁/获得，显示你的角色是否已解锁/获得该物品。可以在配置窗口中配置显示特定角色。";
    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.ItemUnlockStatus;
    public override string Version { get; } = "1.11.0.4";

    public override uint? Order => 0;
}