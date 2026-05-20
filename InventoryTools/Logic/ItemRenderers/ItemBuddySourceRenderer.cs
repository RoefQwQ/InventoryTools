using System;
using System.Collections.Generic;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemBuddySourceRenderer : ItemInfoRenderer<ItemBuddySource>
{
    public ItemBuddySourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider,
        IDalamudPluginInterface dalamudPluginInterface) : base(textureProvider, dalamudPluginInterface, itemSheet, mapSheet)
    {
    }

    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.BuddyItem;
    public override string SingularName => "搭档";
    public override bool ShouldGroup => false;
    public override string HelpText => "Can the item be used on your chocobo companion?";

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;

        if (usedField)
        {
            ImGui.Text("战斗：增加陆行鸟搭档获得的经验值。");
        }

        if (usedTraining)
        {
            ImGui.Text("陆行鸟房：用于圈养陆行鸟的训练食物。");
        }

        if (usedDyeing)
        {
            ImGui.Text("染色：用于陆行鸟染色。");
        }
    };
    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;
        var name = new List<string>();


        if (usedField)
        {
            name.Add("battle");
        }

        if (usedTraining)
        {
            name.Add("training");
        }

        if (usedDyeing)
        {
            name.Add("dyeing");
        }

        return "chocobo " + string.Join(", ", name);
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.ChocoboIcon;

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;
        var name = new List<string>();

        if (usedField)
        {
            name.Add("battle");
        }

        if (usedTraining)
        {
            name.Add("training");
        }

        if (usedDyeing)
        {
            name.Add("dyeing");
        }

        return "Used for " + string.Join(", ", name);
    };
}