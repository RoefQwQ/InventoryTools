using System.Collections.Generic;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;

namespace InventoryTools.Logic.Columns.Buttons;

public class CopyItemNameButtonColumn(IClipboardService clipboardService) : ButtonColumn
{
    public override string Name { get; set; } = "Copy Item Name Button";
    public override float Width { get; set; } = 80;
    public override string HelpText { get; set; } = "将物品名称复制到剪贴板";

    public override List<MessageBase>? Draw(FilterConfiguration configuration, ColumnConfiguration columnConfiguration, SearchResult searchResult,
        int rowIndex, int columnIndex)
    {
        ImGui.TableNextColumn();
        if (ImGui.TableGetColumnFlags().HasFlag(ImGuiTableColumnFlags.IsEnabled))
        {
            if (ImGui.Button("Copy Name##" + rowIndex + "_" + columnIndex))
            {
                clipboardService.CopyToClipboard(searchResult.Item.NameString);
            }
        }

        return null;
    }
}