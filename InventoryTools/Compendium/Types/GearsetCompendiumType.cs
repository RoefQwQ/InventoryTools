using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.Model;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using DalaMock.Host.Mediator;
using InventoryTools.Compendium.Interfaces;
using InventoryTools.Compendium.Models;
using InventoryTools.Compendium.Sections;
using InventoryTools.Compendium.Services;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Compendium.Types;

public class GearsetCompendiumType : CompendiumType<Gearset>
{
    private readonly ItemListSection.Factory _itemListSectionFactory;
    private readonly List<Gearset> _gearsets;
    private readonly ItemSheet _itemSheet;
    private readonly ItemInfoCache _itemInfoCache;
    private readonly CompendiumMenuBuilder _menuBuilder;

    public GearsetCompendiumType(CompendiumTable<Gearset>.Factory tableFactory,
        CompendiumColumnBuilder<Gearset>.Factory columnBuilder,
        CompendiumViewBuilder.Factory viewBuilderFactory,
        ItemListSection.Factory  itemListSectionFactory,
        List<Gearset> gearsets,
        ItemSheet itemSheet,
        ItemInfoCache itemInfoCache,
        CompendiumMenuBuilder menuBuilder) : base(tableFactory,
        columnBuilder,
        viewBuilderFactory)
    {
        _itemListSectionFactory = itemListSectionFactory;
        _gearsets = gearsets;
        _itemSheet = itemSheet;
        _itemInfoCache = itemInfoCache;
        _menuBuilder = menuBuilder;
    }

    public override ICompendiumTable<WindowState, MessageBase> BuildTable()
    {
        return Factory.Invoke(new()
        {
            Key = "gearsets",
            Name = Plural,
            Columns = BuiltColumns,
            CompendiumType = this,
            BuildContextMenu = BuildContextMenu
        });
    }

    public override string? GetName(Gearset row)
    {
        return row.Name;
    }

    public override string? GetSubtitle(Gearset row)
    {
        return row.Items.Count + " 个物品";
    }

    public override (string?, uint?) GetIcon(Gearset row)
    {
        return (null, Icons.ArmorIcon);
    }

    public override uint GetRowId(Gearset row)
    {
        return (uint)GetRows().IndexOf(row);
    }

    private List<MessageBase> BuildContextMenu(Gearset arg)
    {
        _menuBuilder.Header(arg.Name);
        _menuBuilder.TryOn(arg.Items, "试穿套装");
        _menuBuilder.NewLine();
        _menuBuilder.Header("套装部件");
        _menuBuilder.Items(arg.Items);
        _menuBuilder.GroupedItems(arg.Items, "所有套装部件");
        return [];
    }

    public override string Singular => "套装";
    public override string Plural => "套装";
    public override string Description => "基于Eorzea Collection整理的套装。";
    public override string Key => "gearsets";
    public override (string?, uint?) Icon => (null, Icons.ArmorIcon);


    public override Gearset GetRow(uint row)
    {
        return _gearsets[(int)row];
    }

    public override bool HasRow(uint rowId)
    {
        return (int)rowId >= 0 && (int)rowId < _gearsets.Count;
    }

    public override List<Gearset> GetRows()
    {
        return _gearsets;
    }

    public override void BuildColumns(CompendiumColumnBuilder<Gearset> builder)
    {
        builder.AddCompendiumOpenViewColumn(new(){Key = "icon", Name = "##图标", HelpText = "套装图标", Version = "14.0.3", ValueSelector = row => (null, Icons.ArmorIcon), CompendiumType = this, RowIdSelector = row => (uint)_gearsets.IndexOf(row)});
        builder.AddStringColumn(new (){Key = "name", Name = "名称", HelpText = "套装名称", Version = "14.0.3", ValueSelector = row => row.Name});
        builder.AddItemSourcesColumn(new() { Key = "sources", Name = "来源", HelpText = "套装的综合来源。", Version = "14.0.3", ValueSelector = gearset => gearset.Items.Where(c => c.RowId != 0).SelectMany(c => _itemInfoCache.GetItemSources(c.RowId) ?? []).ToList()});
        builder.AddStringColumn(new (){Key = "patch", Name = "版本", HelpText = "套装添加的版本。", Version = "14.0.3", ValueSelector = gearset => string.Join(", ", gearset.Items.Where(c => c.RowId != 0).Select(c => _itemSheet.GetRow(c.RowId).Patch.ToString(CultureInfo.InvariantCulture)).Distinct())});
        for (int i = 0; i < 12; i++)
        {
            var index = i;
            builder.AddItemColumn(new(){Key = "item" + i, Name = "物品 " + (i + 1), HelpText = "物品", Version = "14.0.3", ValueSelector = row => row.Items[index].RowId});
        }
    }

    public override void BuildViewFields(CompendiumViewBuilder viewBuilder, Gearset row)
    {
        var itemCount = row.Items.Count(c => c.RowId != 0);
        viewBuilder.Title = row.Name;
        viewBuilder.Icon = Icons.ArmorIcon;
        viewBuilder.Subtitle = itemCount + " 个物品";
        viewBuilder.AddLink("https://ffxiv.eorzeacollection.com/gearset/" + row.Key, "在Eorzea Collection中打开", "ec");
        viewBuilder.AddSection(_itemListSectionFactory.Invoke(new(){SectionKey = "set_items", SectionName = "套装物品", Items = row.Items.Where(c => c.RowId != 0).Select(c => ItemInfo.Create(_itemSheet.GetRow(c.RowId)))}));
    }
}