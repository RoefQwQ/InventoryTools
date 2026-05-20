using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Features;

public class MarketboardIntegrationFeature : Feature
{
    public MarketboardIntegrationFeature(IEnumerable<ISetting> settings) : base(new[]
        {
            typeof(AutomaticallyDownloadPricesSetting),
            typeof(MarketRefreshTimeHoursSetting),
            typeof(MarketBoardSaleCountLimitSetting),
        },
        settings)
    {
    }
    public override string Name { get; } = "市场板";
    public override string Description { get; } =
        "配置市场板集成功能。此功能会按设定的时间间隔从 Universalis 下载数据，允许您根据多个服务器的物品最低价格和平均价格进行筛选。";
}