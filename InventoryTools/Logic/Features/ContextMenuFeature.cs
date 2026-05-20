using System.Collections.Generic;
using InventoryTools.Logic.Settings;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Logic.Features;

public class ContextMenuFeature : Feature
{
    public ContextMenuFeature(IEnumerable<ISetting> settings) : base(new[]
        {
            typeof(ContextMenuMoreInformationSetting),
            typeof(ContextMenuMoreInformationNpcsSetting),
            typeof(ContextMenuMoreInformationMonstersSetting),
            typeof(ContextMenuAddToCraftListSetting),
            typeof(ContextMenuAddToActiveCraftListSetting),
            typeof(ContextMenuAddToCuratedListSetting),
            typeof(ContextMenuAddToFavouritesSetting),
            typeof(ContextMenuOpenCraftingLogSetting),
            typeof(ContextMenuOpenGatheringLogSetting),
            typeof(ContextMenuOpenFishingLogSetting),
        },
        settings)
    {
    }

    public override string Name { get; } = "右键菜单";

    public override string Description { get; } =
        "为游戏中的物品右键菜单添加新选项。";
}