using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ContextMenuItemSearchSetting : BooleanSetting
    {
        public ContextMenuItemSearchSetting(ILogger<ContextMenuItemSearchSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_ItemSearchContextMenu_Name");
            HelpText = localizationService.GetString("Setting_ItemSearchContextMenu_HelpText");
            WizardName = localizationService.GetString("Setting_ItemSearchContextMenu_WizardName");
        }
        
        public override bool DefaultValue { get; set; } = false;
        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ItemSearchContextMenu;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ItemSearchContextMenu = newValue;
        }

        public override string Key { get; set; } = "ItemSearchContextMenu";
        public override string Name { get; set; } = "右键菜单 - 搜索";

        public override string WizardName { get; } = "搜索";

        public override string HelpText { get; set; } =
            "在所有背包或下方定义的范围内进行搜索？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ContextMenu;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.13";
    }
}