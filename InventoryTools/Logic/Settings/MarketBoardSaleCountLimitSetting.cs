using System;
using CriticalCommonLib.MarketBoard;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class MarketBoardSaleCountLimitSetting : IntegerSetting
    {
        public MarketBoardSaleCountLimitSetting(ILogger<MarketBoardSaleCountLimitSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, IHostedUniversalisConfiguration universalisConfiguration) : base(logger, imGuiService, localizationService)
        {
            _universalisConfiguration = universalisConfiguration;
            Name = localizationService.GetString("Setting_MBSaleCountLimit_Name");
            HelpText = localizationService.GetString("Setting_MBSaleCountLimit_HelpText");
            WizardName = localizationService.GetString("Setting_MBSaleCountLimit_WizardName");
        }
        
        private readonly IHostedUniversalisConfiguration _universalisConfiguration;
        public override int DefaultValue { get; set; } = 7;
        public override int CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.MarketSaleHistoryLimit;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
        {
            newValue = Math.Min(30, newValue);
            newValue = Math.Max(1, newValue);
            _universalisConfiguration.SaleHistoryLimit = newValue;
            configuration.MarketSaleHistoryLimit = newValue;
        }

        public override string Key { get; set; } = "MBSaleCountLimit";
        public override string Name { get; set; } = "市场板销售历史天数";

        public override string WizardName { get; } = "交易历史限制";

        public override string HelpText { get; set; } =
            "计算物品总销售量时，应回溯多少天的销售数据。更改后现有数据不会被清除，需手动请求刷新市场板价格或等待自动刷新。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.MarketBoard;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Market;
        public override string Version => "1.7.0.0";
    }
}