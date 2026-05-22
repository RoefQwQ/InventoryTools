using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class ShowItemNumberRetainerListSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = true;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ShowItemNumberRetainerList;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.ShowItemNumberRetainerList = newValue;
        }

        public override string Key { get; set; } = "ShowItemNumberRetainerList";
        public override string Name { get; set; } = "雇员列表显示物品数量？";

        public override string HelpText { get; set; } =
            "召唤铃列表中的雇员名称是否显示待整理或背包中可用的物品数量？";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Highlighting;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
        public override string Version => "1.7.0.0";

        public ShowItemNumberRetainerListSetting(ILogger<ShowItemNumberRetainerListSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_ShowItemNumberRetainerList_Name");
            HelpText = localizationService.GetString("Setting_ShowItemNumberRetainerList_HelpText");
        }
    }
}