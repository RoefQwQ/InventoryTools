using System;
using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Extensions;
using AllaganLib.GameSheets.Model;
using AllaganLib.Shared.Extensions;
using CriticalCommonLib.Models;
using DalaMock.Host.Mediator;
using Dalamud.Interface.Colors;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using InventoryTools.Compendium.Columns.Options;
using InventoryTools.Compendium.Interfaces;
using InventoryTools.Compendium.Models;
using InventoryTools.Compendium.Sections;
using InventoryTools.Compendium.Sections.Options;
using InventoryTools.Compendium.Services;
using InventoryTools.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Icons = AllaganLib.Shared.Misc.Icons;

namespace InventoryTools.Compendium.Types;

public class InstanceContentCompendiumType : CompendiumType<InstanceContent>
{
    private readonly ExcelSheet<InstanceContent> _instanceContentSheet;
    private readonly ExcelSheet<Quest> _questSheet;
    private readonly IUnlockState _unlockState;
    private readonly IUIStateService _uiStateService;

    public InstanceContentCompendiumType(ExcelSheet<InstanceContent> instanceContentSheet, ExcelSheet<Quest> questSheet, IUnlockState unlockState, IUIStateService uiStateService, CompendiumTable<InstanceContent>.Factory tableFactory, CompendiumColumnBuilder<InstanceContent>.Factory columnBuilder, CompendiumViewBuilder.Factory viewBuilderFactory) : base(tableFactory, columnBuilder, viewBuilderFactory)
    {
        _instanceContentSheet = instanceContentSheet;
        _questSheet = questSheet;
        _unlockState = unlockState;
        _uiStateService = uiStateService;
    }

    public override ICompendiumTable<WindowState, MessageBase> BuildTable()
    {
        return Factory.Invoke(new CompendiumTableOptions<InstanceContent>()
        {
            CompendiumType = this,
            Key = "instance_content",
            Name = "副本内容",
            Columns = BuiltColumns
        });
    }

    public override string? GetName(InstanceContent row)
    {
        return row.ContentFinderCondition.Value.Name.ToImGuiString().FirstCharToUpper();
    }

    public override string? GetSubtitle(InstanceContent row)
    {
        return row.ContentFinderCondition.Value.ContentType.Value.Name.ToImGuiString();
    }

    public override (string?, uint?) GetIcon(InstanceContent row)
    {
        return (null, row.ContentFinderCondition.Value.ContentType.Value.IconDutyFinder);
    }

    public override uint GetRowId(InstanceContent row)
    {
        return row.RowId;
    }

    public override InstanceContent GetRow(uint row)
    {
        return _instanceContentSheet.GetRow(row);
    }

    public override bool HasRow(uint rowId)
    {
        return _instanceContentSheet.GetRowOrDefault(rowId) != null;
    }

    public override List<InstanceContent> GetRows()
    {
        return _instanceContentSheet.Where(c => c.ContentFinderCondition.RowId != 0).ToList();
    }

    public override void BuildColumns(CompendiumColumnBuilder<InstanceContent> builder)
    {
        builder.AddCompendiumOpenViewColumn(new()
        {
            Key = "icon",
            Name = "图标",
            HelpText = "副本图标",
            Version = "14.0.3",
            CompendiumType = this,
            RowIdSelector = row => row.RowId,
            ValueSelector = GetIcon
        });

        builder.AddStringColumn(new()
        {
            Key = "name",
            Name = "名称",
            HelpText = "副本名称",
            Version = "14.0.3",
            ValueSelector = GetName
        });

        builder.AddIntegerColumn(new()
        {
            Key = "level",
            Name = "等级",
            HelpText = "所需职业等级",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.ClassJobLevelRequired.ToString()
        });

        builder.AddBooleanColumn(new BooleanColumnOptions<InstanceContent>()
        {
            Key = "unlocked",
            Name = "已解锁？",
            HelpText = "此副本是否已解锁？",
            Version = "14.1.3",
            ValueSelector = row => _unlockState.IsInstanceContentUnlocked(row)
        });

        builder.AddBooleanColumn(new BooleanColumnOptions<InstanceContent>()
        {
            Key = "completed",
            Name = "已完成？",
            HelpText = "此副本是否已完成？",
            Version = "14.1.3",
            ValueSelector = row => _uiStateService.IsInstanceContentCompleted(row)
        });

        builder.AddIntegerColumn(new()
        {
            Key = "sync_level",
            Name = "同步等级",
            HelpText = "副本中应用的等级同步",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.ClassJobLevelSync.ToString()
        });

        builder.AddIntegerColumn(new()
        {
            Key = "item_level",
            Name = "装等要求",
            HelpText = "最低装备品级要求",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.ItemLevelRequired.ToString()
        });

        builder.AddIntegerColumn(new()
        {
            Key = "item_level_sync",
            Name = "品级同步",
            HelpText = "最大同步品级",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.ItemLevelSync.ToString()
        });

        builder.AddBooleanColumn(new()
        {
            Key = "allows_undersized",
            Name = "允许少人",
            HelpText = "此副本是否允许少人模式",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.AllowUndersized
        });

        builder.AddBooleanColumn(new()
        {
            Key = "allows_explorer_mode",
            Name = "允许探索模式",
            HelpText = "此副本是否支持探索模式",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.AllowExplorerMode
        });

        builder.AddBooleanColumn(new()
        {
            Key = "pvp",
            Name = "PvP",
            HelpText = "该副本是否为PvP",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.PvP
        });

        builder.AddStringColumn(new()
        {
            Key = "accepted_classes",
            Name = "允许职业",
            HelpText = "允许进入的职业类别",
            Version = "14.0.3",
            ValueSelector = row =>
                row.ContentFinderCondition.Value.AcceptClassJobCategory.ValueNullable?.Name.ToImGuiString() ?? "Unknown"
        });
    }

    public override void BuildViewFields(CompendiumViewBuilder viewBuilder, InstanceContent row)
    {
        viewBuilder.SetupDefaults(this, row);

        viewBuilder.AddTag("已解锁", "该副本是否已解锁？", () => _unlockState.IsInstanceContentUnlocked(row) ? ImGuiColors.HealerGreen : ImGuiColors.DalamudRed);
        viewBuilder.AddTag("已完成", "该副本是否已完成？", () => _uiStateService.IsInstanceContentCompleted(row) ? ImGuiColors.HealerGreen : ImGuiColors.DalamudRed);

        var relatedQuests = _questSheet.Where(c => c.InstanceContent.Any(c => c.RowId == row.RowId) || c.QuestParams.Any(c => c.ScriptArg == row.RowId && c.ScriptInstruction.ToString().StartsWith("INSTANCEDUNGEON")))
            .Select(c => c.AsUntypedRowRef()).ToList();
        relatedQuests.Add(row.ContentFinderCondition.Value.UnlockCriteria);
        relatedQuests.Add(row.ContentFinderCondition.Value.UnlockCriteria2);
        viewBuilder.AddCollectionRowRefSection(new CollectionRowRefSectionOptions()
        {
            RelatedRefs = relatedQuests,
            HideWhenEmpty = true,
            SectionKey = "related_quests",
            SectionName = "相关任务"
        });
        viewBuilder.AddSingleRowRefSection(new SingleRowRefSectionOptions()
        {
            RelatedRef = row.ContentFinderCondition.Value.TerritoryType.Value.AsUntypedRowRef(),
            SectionKey = "related_map",
            SectionName = "相关地图"
        });
    }

    public override bool HasLocation => true;

    public override ILocation? GetLocation(InstanceContent row)
    {
        var territoryType = row.ContentFinderCondition.ValueNullable?.TerritoryType;
        if (territoryType == null || territoryType.Value.RowId == 0)
        {
            return null;
        }

        return new TerritoryLocation(territoryType.Value.Value);
    }

    public override string Singular => "副本";
    public override string Plural => "副本";
    public override string Description => "包括副本、讨伐战等";
    public override string Key => "instance";
    public override (string?, uint?) Icon => (null, Icons.DutyIcon);
}