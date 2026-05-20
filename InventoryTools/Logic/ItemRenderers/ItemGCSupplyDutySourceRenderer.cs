using System;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemGcSupplyDutySourceRenderer : ItemInfoRenderer<ItemGCSupplyDutySource>
{
    public ItemGcSupplyDutySourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider,
        IDalamudPluginInterface dalamudPluginInterface) : base(textureProvider, dalamudPluginInterface, itemSheet, mapSheet)
    {
    }

    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.GCDailySupply;
    public override string SingularName => "Grand Company Supply & Provisioning";
    public override string HelpText => "Can the item be handed in for 'Supply & Provisioning' at your grand company?";
    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        var rewardRow = asSource.DailySupplyRewardRow;
        if (rewardRow != null)
        {
            var baseReward = rewardRow.Base.ExperienceSupply;
            var sealsSupply = rewardRow.Base.SealsSupply;
            ImGui.Text("等级：" + asSource.GCSupplyDutyRow.RowId);
            ImGui.Text("经验：" + baseReward);
            ImGui.Text("军票：" + sealsSupply);
        }
        else
        {
            ImGui.Text("Unknown rewards");
        }
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = AsSource(source);
        var rewardRow = asSource.DailySupplyRewardRow;
        return rewardRow != null ? asSource.GCSupplyDutyRow.RowId.ToString() : "";
    };
    public override Func<ItemSource, int> GetIcon => _ => Icons.FlameSealIcon;

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        var rewardRow = asSource.DailySupplyRewardRow;
        if (rewardRow != null)
        {
            var baseReward = rewardRow.Base.ExperienceSupply;
            var sealsSupply = rewardRow.Base.SealsSupply;
            return $"等级 {asSource.GCSupplyDutyRow.RowId}（{baseReward} 经验，{sealsSupply} 军票）";
        }
        else
        {
            return "未知奖励";
        }
    };
}