using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class AddTitleMenuButtonSetting : BooleanSetting
{
    public AddTitleMenuButtonSetting(ILogger<AddTitleMenuButtonSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_AddTitleMenuButton_Name");
        HelpText = localizationService.GetString("Setting_AddTitleMenuButton_HelpText");
    }

    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.AddTitleMenuButton;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.AddTitleMenuButton = newValue;
    }

    public override string Key { get; set; } = "AddTitleMenuButton";
    public override string Name { get; set; } = "添加标题菜单按钮？";

    public override string HelpText { get; set; } =
        "在标题菜单中添加按钮，允许您在未登录时打开 Allagan Tools。";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.TitleMenuButtons;
    public override SettingSubCategory SettingSubCategory => SettingSubCategory.General;
    public override string Version => "1.7.0.0"; 
}