using CriticalCommonLib.MarketBoard;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class MarketRefreshTimeHoursSetting : IntegerSetting
    {
        private readonly MarketCacheConfiguration _marketCacheConfiguration;

        public MarketRefreshTimeHoursSetting(ILogger<MarketRefreshTimeHoursSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, MarketCacheConfiguration marketCacheConfiguration) : base(logger, imGuiService, localizationService)
        {
            _marketCacheConfiguration = marketCacheConfiguration;
            Name = localizationService.GetString("Setting_MarketRefreshTime_Name");
            HelpText = localizationService.GetString("Setting_MarketRefreshTime_HelpText");
            WizardName = localizationService.GetString("Setting_MarketRefreshTime_WizardName");
        }

        public override int DefaultValue { get; set; } = 24;
        public override int CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.MarketRefreshTimeHours;
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, int newValue)
        {
            configuration.MarketRefreshTimeHours = newValue;
            _marketCacheConfiguration.CacheMaxAgeHours = newValue;
        }

        public override string Key { get; set; } = "MarketRefreshTime";
        public override string Name { get; set; } = "市场价格保留时长（小时）";

        public override string WizardName { get; } = "保留 X 小时";
        public override string HelpText { get; set; } = "从 Universalis 刷新前，市场价格应保留多长时间？";
        public override SettingCategory SettingCategory { get; set; } = SettingCategory.MarketBoard;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.Market;
        public override string Version => "1.7.0.0";
    }
}