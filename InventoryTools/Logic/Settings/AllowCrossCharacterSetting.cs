using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class AllowCrossCharacterSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.DisplayCrossCharacter;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.DisplayCrossCharacter = newValue;
        }

        public override string Key { get; set; } = "DisplayCrossCharacter";
        public override string Name { get; set; } = "在列表中显示跨角色背包？";
        public override string WizardName { get; } = "跨角色背包？";

        public override string HelpText { get; set; } =
            "是否显示未登录角色及其关联雇员的背包以供查看/搜索和筛选？每个列表可配置来源/去向以包含/排除与当前登录角色相关的雇员/部队。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Lists;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public AllowCrossCharacterSetting(ILogger<AllowCrossCharacterSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_AllowCrossCharacter_Name");
            WizardName = localizationService.GetString("Setting_AllowCrossCharacter_WizardName");
            HelpText = localizationService.GetString("Setting_AllowCrossCharacter_HelpText");
        }
    }
}