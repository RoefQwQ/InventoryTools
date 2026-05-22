using CriticalCommonLib.Services;

using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Columns;

public class HasBeenGatheredColumn : CheckboxColumn
{
    private readonly IGameInterface _gameInterface;

    public HasBeenGatheredColumn(ILogger<HasBeenGatheredColumn> logger, ImGuiService imGuiService, IGameInterface gameInterface) : base(logger, imGuiService)
    {
        _gameInterface = gameInterface;
    }
    public override ColumnCategory ColumnCategory => ColumnCategory.Basic;

    public override bool? CurrentValue(ColumnConfiguration columnConfiguration, SearchResult searchResult)
    {
        return _gameInterface.IsItemGathered(searchResult.Item.RowId);
    }

    public override string Name { get; set; } = "已采集过？";
    public override string RenderName => "已记录？";
    public override float Width { get; set; } = 80;

    public override string HelpText { get; set; } =
        "该物品是否已记录在采集日志中？";

    public override bool HasFilter { get; set; } = true;
    public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Boolean;
}