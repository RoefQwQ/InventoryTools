using System.Collections.Generic;
using System.Linq;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class TooltipCategoryWhitelistSetting : MultipleChoiceSetting<uint>
{
    private readonly ExcelSheet<ItemUICategory> _itemUiCategorySheet;

    public TooltipCategoryWhitelistSetting(ILogger<TooltipCategoryWhitelistSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, ExcelSheet<ItemUICategory> itemUiCategorySheet) : base(logger, imGuiService, localizationService)
    {
        _itemUiCategorySheet = itemUiCategorySheet;
        Name = localizationService.GetString("Setting_TooltipWhitelistCategories_Name");
        HelpText = localizationService.GetString("Setting_TooltipWhitelistCategories_HelpText");
    }

    public override List<uint> DefaultValue { get; set; } = new();
    public override List<uint> CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TooltipWhitelistCategories;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, List<uint> newValue)
    {
        configuration.TooltipWhitelistCategories = newValue;
    }

    public override string Key { get; set; } = "TooltipWhitelistCategories";
    public override string Name { get; set; } = "工具提示分类白名单";
    public override string HelpText { get; set; } = "向工具提示添加信息时，是否应将受影响的物品限制在这些分类中？如果勾选了「工具提示分类黑名单」，此功能将反转。";
    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;
    public override Dictionary<uint, string> GetChoices(InventoryToolsConfiguration configuration)
    {
        return _itemUiCategorySheet
            .ToDictionary(c => c.RowId, c => c.Name.ExtractText());
    }

    public override bool HideAlreadyPicked { get; set; } = true;
    public override string Version => "1.7.0.0";

}