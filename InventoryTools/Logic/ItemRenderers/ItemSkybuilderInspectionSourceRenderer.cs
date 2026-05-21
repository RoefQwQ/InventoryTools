using System;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemSkybuilderInspectionUseRenderer : ItemSkybuilderInspectionSourceRenderer
{
    public ItemSkybuilderInspectionUseRenderer(ItemSheet itemSheet, MapSheet mapSheet,
        GatheringItemSheet gatheringItemSheet, ITextureProvider textureProvider,
        IDalamudPluginInterface dalamudPluginInterface) : base(itemSheet, mapSheet, gatheringItemSheet, textureProvider, dalamudPluginInterface)
    {
    }

    public override RendererType RendererType => RendererType.Use;
    public override string HelpText => "该物品是否是在天穹街检验后的认可形态？";
    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        ImGui.Text($"Reward: {asSource.Item.NameString}");
        ImGui.Text($"Required: {asSource.InspectionData.AmountRequired}");
    };
}

public class ItemSkybuilderInspectionSourceRenderer : ItemInfoRenderer<ItemSkybuilderInspectionSource>
{
    private readonly GatheringItemSheet _gatheringItemSheet;

    public ItemSkybuilderInspectionSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet,
        GatheringItemSheet gatheringItemSheet, ITextureProvider textureProvider,
        IDalamudPluginInterface dalamudPluginInterface) : base(textureProvider, dalamudPluginInterface, itemSheet, mapSheet)
    {
        _gatheringItemSheet = gatheringItemSheet;
    }
    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => ItemInfoType.SkybuilderInspection;
    public override string SingularName => "天穹街检验";
    public override string HelpText => "该物品能否在天穹街检验并转换为认可形态？";
    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        ImGui.Text($"需求物品：{asSource.CostItem?.NameString ?? "未知物品"}");
        ImGui.Text($"需求数量：{asSource.InspectionData.AmountRequired}");
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = AsSource(source);
        return asSource.Item.NameString;
    };
    public override Func<ItemSource, int> GetIcon => _ => Icons.SkybuildersScripIcon;

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        return $"{asSource.CostItem?.NameString} x {asSource.InspectionData.AmountRequired}";
    };
}