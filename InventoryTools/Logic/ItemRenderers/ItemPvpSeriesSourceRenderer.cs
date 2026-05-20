using System;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemPvpSeriesSourceRenderer : ItemInfoRenderer<ItemPVPSeriesSource>
{
    public ItemPvpSeriesSourceRenderer(ITextureProvider textureProvider, IDalamudPluginInterface dalamudPluginInterface, ItemSheet itemSheet, MapSheet mapSheet) : base(textureProvider, dalamudPluginInterface, itemSheet, mapSheet)
    {
    }

    public override RendererType RendererType { get; } = RendererType.Source;
    public override ItemInfoType Type { get; } = ItemInfoType.PVPSeries;
    public override string SingularName { get; } = "PVP Series";
    public override string HelpText { get; } = "Is this item rewarded from a PVP series?";
    public override bool ShouldGroup { get; } = true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        ImGui.Text("PVP系列奖励 " + asSource.PvpSeries.Value.RowId);
        ImGui.Text("在等级解锁 " + asSource.Level);
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = AsSource(source);
        return $"Series {asSource.PvpSeries.Value.RowId} Level {asSource.Level}";
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.PVPIcon;
    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        return $"Series {asSource.PvpSeries.Value.RowId} Level {asSource.Level}";
    };
}