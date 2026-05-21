using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Interface.Textures;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;
using InventoryTools.Services;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemDesynthSourceRenderer : ItemSupplementSourceRenderer<ItemDesynthSource>
{
    public ItemDesynthSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Desynthesis, Icons.DesynthesisIcon)
    {
    }

    public override string SingularName => "分解";
    public override string HelpText => "该物品能否通过分解获得？";
}

public class ItemReductionSourceRenderer : ItemSupplementSourceRenderer<ItemReductionSource>
{
    public ItemReductionSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Reduction, Icons.ReductionIcon)
    {
    }

    public override string SingularName => "精选";
    public override string HelpText => "该物品能否通过精选获得？";
}

public class ItemLootSourceRenderer : ItemSupplementSourceRenderer<ItemLootSource>
{
    public ItemLootSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Loot, Icons.LootIcon)
    {
    }

    public override string SingularName => "战利品";
    public override string HelpText => "该物品能否从其他物品（通常是宝箱/材料容器/礼盒）中获得？";
}

public class ItemGardeningSourceRenderer : ItemSupplementSourceRenderer<ItemGardeningSource>
{
    public ItemGardeningSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Gardening, Icons.SproutIcon)
    {
    }

    public override string SingularName => "园艺";
    public override string HelpText => "该物品能否通过园艺种植获得？";
}

public class ItemDesynthUseRenderer : ItemSupplementUseRenderer<ItemDesynthSource>
{
    public ItemDesynthUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Desynthesis, Icons.DesynthesisIcon)
    {
    }

    public override string SingularName => "分解";
    public override string HelpText => "该物品能否被分解？";
}

public class ItemReductionUseRenderer : ItemSupplementUseRenderer<ItemReductionSource>
{
    public ItemReductionUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Reduction, Icons.ReductionIcon)
    {
    }

    public override string SingularName => "精选";
    public override string HelpText => "该物品能否被精选？";
}

public class ItemLootUseRenderer : ItemSupplementUseRenderer<ItemLootSource>
{
    public ItemLootUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Loot, Icons.LootIcon)
    {
    }

    public override string SingularName => "战利品";
    public override string HelpText => "该物品是否包含其他物品？";
}

public class ItemGardeningUseRenderer : ItemSupplementUseRenderer<ItemGardeningSource>
{
    public ItemGardeningUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Gardening, Icons.SproutIcon)
    {
    }

    public override string SingularName => "园艺";
    public override string HelpText => "该物品能否用于园艺种植？";
}

public class ItemCardPackSourceRenderer : ItemSupplementSourceRenderer<ItemCardPackSource>
{
    public ItemCardPackSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.CardPack, Icons.CardPackIcon)
    {
    }

    public override string SingularName => "卡包";
    public override string HelpText => "该物品能否从卡包中获得？";
}

public class ItemCardPackUseRenderer : ItemSupplementUseRenderer<ItemCardPackSource>
{
    public ItemCardPackUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.CardPack, Icons.CardPackIcon)
    {
    }

    public override string SingularName => "卡包";
    public override string HelpText => "该物品是否包含卡牌？";
}

public class ItemCofferSourceRenderer : ItemSupplementSourceRenderer<ItemCofferSource>
{
    public ItemCofferSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Coffer, Icons.CofferIcon)
    {
    }

    public override string SingularName => "宝箱";
    public override string HelpText => "该物品能否从宝箱中获得？";
}

public class ItemCofferUseRenderer : ItemSupplementUseRenderer<ItemCofferSource>
{
    public ItemCofferUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Coffer, Icons.CofferIcon)
    {
    }

    public override string SingularName => "宝箱";
    public override string HelpText => "该物品是否是包含其他物品的宝箱？";
}


public class ItemPalaceOfTheDeadSourceRenderer : ItemSupplementSourceRenderer<ItemPalaceOfTheDeadSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemPalaceOfTheDeadSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.PalaceOfTheDead, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "死者宫殿";
    public override string HelpText => "该物品能否从死者宫殿的战利品中获得？";
}

public class ItemPalaceOfTheDeadUseRenderer : ItemSupplementUseRenderer<ItemPalaceOfTheDeadSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemPalaceOfTheDeadUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.PalaceOfTheDead, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "死者宫殿";
    public override string HelpText => "该物品是否是在死者宫殿中获得的战利品？";
}
public class ItemHeavenOnHighSourceRenderer : ItemSupplementSourceRenderer<ItemHeavenOnHighSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemHeavenOnHighSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.HeavenOnHigh, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "天之御柱";
    public override string HelpText => "该物品能否从天之御柱的战利品中获得？";
}

public class ItemHeavenOnHighUseRenderer : ItemSupplementUseRenderer<ItemHeavenOnHighSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemHeavenOnHighUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.HeavenOnHigh, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "天之御柱";
    public override string HelpText => "该物品是否是在天之御柱中获得的战利品？";
}
public class ItemEurekaOrthosSourceRenderer : ItemSupplementSourceRenderer<ItemEurekaOrthosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemEurekaOrthosSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.EurekaOrthos, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "优雷卡正统";
    public override string HelpText => "该物品能否从优雷卡正统的战利品中获得？";
}

public class ItemEurekaOrthosUseRenderer : ItemSupplementUseRenderer<ItemEurekaOrthosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemEurekaOrthosUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.EurekaOrthos, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "优雷卡正统";
    public override string HelpText => "该物品是否是在优雷卡正统中获得的战利品？";
}

public class ItemAnemosSourceRenderer : ItemSupplementSourceRenderer<ItemAnemosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemAnemosSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Anemos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡风脉";
    public override string HelpText => "该物品能否从优雷卡风脉的战利品中获得？";
}

public class ItemAnemosUseRenderer : ItemSupplementUseRenderer<ItemAnemosSource>
{
    public ItemAnemosUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Anemos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡风脉";
    public override string HelpText => "该物品是否是在优雷卡风脉中获得的战利品？";
}
public class ItemPagosSourceRenderer : ItemSupplementSourceRenderer<ItemPagosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pagos];
    public ItemPagosSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Pagos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡恒冰";
    public override string HelpText => "该物品能否从优雷卡恒冰的战利品中获得？";
}

public class ItemPagosUseRenderer : ItemSupplementUseRenderer<ItemPagosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pagos];
    public ItemPagosUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Pagos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡恒冰";
    public override string HelpText => "该物品是否是在优雷卡恒冰中获得的战利品？";
}
public class ItemPyrosSourceRenderer : ItemSupplementSourceRenderer<ItemPyrosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pyros];
    public ItemPyrosSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Pyros, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡火光";
    public override string HelpText => "该物品能否从优雷卡火光的战利品中获得？";
}

public class ItemPyrosUseRenderer : ItemSupplementUseRenderer<ItemPyrosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pyros];
    public ItemPyrosUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Pyros, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡火光";
    public override string HelpText => "该物品是否是在优雷卡火光中获得的战利品？";
}

public class ItemHydatosSourceRenderer : ItemSupplementSourceRenderer<ItemHydatosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Hydatos];
    public ItemHydatosSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Hydatos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡丰水";
    public override string HelpText => "该物品能否从优雷卡丰水的战利品中获得？";
}

public class ItemPilgrimsTraverseSourceRenderer : ItemSupplementSourceRenderer<ItemPilgrimsTraverseSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.DeepDungeon];
    public ItemPilgrimsTraverseSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.PilgrimsTraverse, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "巡礼者之路";
    public override string HelpText => "该物品能否从巡礼者之路的战利品中获得？";
}

public class ItemOizysSourceRenderer : ItemSupplementSourceRenderer<ItemOizysSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemOizysSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Oizys, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "奥伊兹";
    public override string HelpText => "该物品能否从奥伊兹的战利品中获得？";
}

public class ItemHydatosUseRenderer : ItemSupplementUseRenderer<ItemHydatosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Hydatos];
    public ItemHydatosUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Hydatos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "优雷卡丰水";
    public override string HelpText => "该物品是否是在优雷卡丰水中获得的战利品？";
}

public class ItemBozjaSourceRenderer : ItemSupplementSourceRenderer<ItemBozjaSource>
{
    public ItemBozjaSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Bozja, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "博兹雅";
    public override string HelpText => "该物品能否从博兹雅的战利品中获得？";
}

public class ItemBozjaUseRenderer : ItemSupplementUseRenderer<ItemBozjaSource>
{
    public ItemBozjaUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Bozja, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "博兹雅";
    public override string HelpText => "该物品是否是在博兹雅中获得的战利品？";
}
public class ItemLogogramSourceRenderer : ItemSupplementSourceRenderer<ItemLogogramSource>
{
    public ItemLogogramSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface,  ItemInfoType.Logogram, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "风脉石";
    public override string HelpText => "该物品能否从风脉石中获得？";
}

public class ItemLogogramUseRenderer : ItemSupplementUseRenderer<ItemLogogramSource>
{
    public ItemLogogramUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.Logogram, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "风脉石";
    public override string HelpText => "该物品是否是风脉石？";

    public override Func<ItemSource, int> GetIcon => source =>
    {
        return source.CostItem!.Icon;
    };
}


public abstract class ItemSupplementUseRenderer<T> : ItemSupplementSourceRenderer<T> where T : ItemSupplementSource
{
    public override RendererType RendererType => RendererType.Use;

    protected ItemSupplementUseRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface, ItemInfoType itemInfoType, ushort icon) : base(itemSheet, mapSheet, textureProvider, pluginInterface, itemInfoType, icon)
    {
    }

    public override Action<List<ItemSource>>? DrawTooltipGrouped => sources =>
    {
        var asSources = AsSource(sources);
        foreach (var source in asSources.OrderBy(c => c.Item.NameString))
        {
            ImGui.Image(TextureProvider.GetFromGameIcon(new GameIconLookup(source.Item.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
            ImGui.SameLine();
            ImGui.Text(source.Item.NameString);
            if (source.Supplement.Min != null && source.Supplement.Max != null)
            {
                ImGui.SameLine();
                if (source.Supplement.Min == source.Supplement.Max)
                {
                    ImGui.Text("(掉落1)");
                }
                else
                {
                    ImGui.Text("(掉落" + source.Supplement.Min.Value + " - " + source.Supplement.Max.Value + ")");
                }
            }

            if (source.Supplement.Probability != null)
            {
                ImGui.SameLine();
                ImGui.TextUnformatted($"{source.Supplement.Probability.Value}%");
            }
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        ImGui.Image(TextureProvider.GetFromGameIcon(new GameIconLookup(source.Item.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
        ImGui.SameLine();
        ImGui.Text(source.Item.NameString);
        if (asSource.Supplement.Min != null && asSource.Supplement.Max != null)
        {
            ImGui.SameLine();
            if (asSource.Supplement.Min == asSource.Supplement.Max)
            {
                ImGui.Text("(掉落1)");
                }
                else
                {
                    ImGui.Text("(掉落" + asSource.Supplement.Min.Value + " - " + asSource.Supplement.Max.Value + ")");
            }
        }

        if (asSource.Supplement.Probability != null)
        {
            ImGui.SameLine();
            ImGui.TextUnformatted($"{asSource.Supplement.Probability.Value}%");
        }
    };

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        return source.Item.NameString;
    };
}

public abstract class ItemSupplementSourceRenderer<T> : ItemInfoRenderer<T> where T : ItemSupplementSource
{
    public ITextureProvider TextureProvider { get; }
    private readonly IDalamudPluginInterface _pluginInterface;
    private readonly ItemInfoType _itemInfoType;
    private readonly ushort _icon;

    public ItemSupplementSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface, ItemInfoType itemInfoType, ushort icon) : base(textureProvider, pluginInterface, itemSheet, mapSheet)
    {
        TextureProvider = textureProvider;
        _pluginInterface = pluginInterface;
        _itemInfoType = itemInfoType;
        _icon = icon;
    }

    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => _itemInfoType;
    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);

        this.DrawItems("奖励物品：", asSource.RewardItems);
        this.DrawItems("需求物品：", asSource.CostItems);

        if (asSource.Supplement.Probability != null)
        {
            ImGui.SameLine();
            ImGui.TextUnformatted($"{asSource.Supplement.Probability.Value}%");
        }
    };

    public override Func<ItemSource, int> GetIcon => _ => _icon;

    public override Func<ItemSource, string> GetName => _ => "";

    public override Func<ItemSource, string> GetDescription => source =>
    {
        return source.CostItem!.NameString;
    };
}