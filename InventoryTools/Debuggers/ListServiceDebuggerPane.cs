using System;
using AllaganLib.Shared.Interfaces;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Mediator;
using InventoryTools.Services.Interfaces;
using OtterGui.Raii;

namespace InventoryTools.Ui.DebugWindows;

public class ListServiceDebuggerPane : IDebugPane
{
    private Lazy<IListService> _listService;
    private readonly MediatorService _mediatorService;

    public IListService ListService => _listService.Value;

    public ListServiceDebuggerPane(Lazy<IListService> listService, MediatorService mediatorService)
    {
        _listService = listService;
        _mediatorService = mediatorService;
    }

    public string Name => "列表服务";

    public void Draw()
    {
        var activeBackgroundList = ListService.GetActiveBackgroundList();
        var activeUiList = ListService.GetActiveUiList(false);
        var lists = ListService.Lists;
        ImGui.Text($"活动后台列表: {(activeBackgroundList == null ? "无列表" : activeBackgroundList.Name)}");
        ImGui.Text($"活动UI列表: {(activeUiList == null ? "无列表" : activeUiList.Name)}");
        foreach (var list in lists)
        {
            using var id = ImRaii.PushId(list.Key);
            ImGui.Text($"{list.Name}:");
            ImGui.Text($"{(list.Active ? "活动中" : "未活动")}");
            ImGui.SameLine();
            if (ImGui.Button("请求刷新"))
            {
                _mediatorService.Publish(new RequestListUpdateMessage(list));
            }
        }
    }
}
