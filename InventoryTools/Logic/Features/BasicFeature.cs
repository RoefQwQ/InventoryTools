using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Features;

public class BasicFeature : Feature
{
    public BasicFeature(IEnumerable<ISetting> settings) : base(new[]
        {
            typeof(AutoSaveSetting),
            typeof(AllowCrossCharacterSetting),
            typeof(HistoryEnabledSetting)
        },
        settings)
    {
    }
    
    public override string Name { get; } = "基本";
    public override string Description { get; } = "配置 Allagan Tools 的基本设置";
}