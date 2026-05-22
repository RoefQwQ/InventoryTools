using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipAddCharacterNameSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipAddCharacterNameOwned;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipAddCharacterNameOwned = newValue;
        }

        public override string Key { get; set; } = "TooltipCharacterName";
        public override string Name { get; set; } = "添加物品位置（附加角色名）";

        public override string HelpText { get; set; } =
            "悬停物品时，如果雇员拥有该物品，是否在工具提示中标注该雇员的所有者？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.AddItemLocations;
        public override string Version => "1.7.0.0";

        public TooltipAddCharacterNameSetting(ILogger<TooltipAddCharacterNameSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_TooltipCharacterName_Name");
            HelpText = localizationService.GetString("Setting_TooltipCharacterName_HelpText");
        }
    }
}