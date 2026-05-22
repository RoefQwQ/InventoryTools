using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class TooltipAverageMarketPriceSetting : BooleanSetting
    {
        public override bool DefaultValue { get; set; } = false;

        public override bool CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.TooltipDisplayMarketAveragePrice;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
        {
            configuration.TooltipDisplayMarketAveragePrice = newValue;
        }

        public override string Key { get; set; } = "TooltipDisplayMBAverage";
        public override string Name { get; set; } = "添加市场均价（普通品/高品质）";

        public override string HelpText { get; set; } =
        "悬停物品时，工具提示是否显示普通品和高品质的市场均价？请确保已启用「自动下载价格」。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.MarketPricing;
        public override string Version => "1.7.0.0";

        public override uint? Order => 0;

        public TooltipAverageMarketPriceSetting(ILogger<TooltipAverageMarketPriceSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base(logger, imGuiService, localizationService)
        {
            Name = localizationService.GetString("Setting_TooltipDisplayMBAverage_Name");
            HelpText = localizationService.GetString("Setting_TooltipDisplayMBAverage_HelpText");
        }
    }
}