using System;
using System.Collections.Generic;
using System.Numerics;
using AllaganLib.Shared.Interfaces;
using CriticalCommonLib;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Ui;
using Dalamud.Bindings.ImGui;
using InventoryTools.Services.Interfaces;

namespace InventoryTools.Ui.DebugWindows;

public class OverlayServiceDebuggerPane : IDebugPane
{
    private readonly ICharacterMonitor _characterMonitor;
    private readonly IGameUiManager _gameUiManager;
    private readonly Lazy<IOverlayService> _overlayService;

    public IOverlayService OverlayService => _overlayService.Value;

    public OverlayServiceDebuggerPane(ICharacterMonitor characterMonitor, IGameUiManager gameUiManager, Lazy<IOverlayService> overlayService)
    {
        _characterMonitor = characterMonitor;
        _gameUiManager = gameUiManager;
        _overlayService = overlayService;
    }

    public string Name => "覆盖层系统";

    public void Draw()
    {
        ImGui.Text($"当前状态: {(OverlayService.LastState == null ? "无状态" : "有状态")}");
        if (OverlayService.LastState != null)
        {
            ImGui.Text($"过滤器: {OverlayService.LastState.FilterConfiguration.Name}");
            ImGui.Text($"是否高亮: {(OverlayService.LastState.ShouldHighlight ? "是" : "否")}");
            ImGui.TextUnformatted($"当前雇员ID: {_characterMonitor.ActiveRetainerId}");
            ImGui.TextUnformatted($"雇员列表已打开?: {_gameUiManager.IsWindowVisible(CriticalCommonLib.Services.Ui.WindowName.RetainerList)}");
            ImGui.Text($"是否高亮目标: {(OverlayService.LastState.ShouldHighlightDestination ? "是" : "否")}");
            ImGui.Text($"反转高亮: {(OverlayService.LastState.InvertHighlighting ? "是" : "否")}");
            ImGui.Text($"有过滤结果: {(OverlayService.LastState.HasFilterResult ? "是" : "否")}");

            var retainerBags1 = OverlayService.LastState.GetBagHighlights(InventoryType.RetainerBag0);
            var retainerBags2 = OverlayService.LastState.GetBagHighlights(InventoryType.RetainerBag1);
            var tabHighlights = OverlayService.LastState.GetTabHighlights(new List<Dictionary<Vector2, Vector4?>>()
                { retainerBags1, retainerBags2 });
            ImGui.Text($"{(tabHighlights.HasValue ? "将高亮标签1" : "无高亮")}");

            var retainerBags3 = OverlayService.LastState.GetBagHighlights(InventoryType.RetainerBag2);
            var retainerBags4 = OverlayService.LastState.GetBagHighlights(InventoryType.RetainerBag3);
            var tabHighlights2 = OverlayService.LastState.GetTabHighlights(new List<Dictionary<Vector2, Vector4?>>()
                { retainerBags3, retainerBags4 });
            ImGui.Text($"{(tabHighlights2.HasValue ? "将高亮标签2" : "无高亮")}");
        }

        ImGui.Text("覆盖层: ");
        foreach (var overlay in OverlayService.Overlays)
        {
            ImGui.Text($"{overlay.GetType()}");
            ImGui.Text($"需要状态刷新: {(overlay.NeedsStateRefresh ? "是" : "否")}");
            ImGui.Text($"应绘制: {(overlay.ShouldDraw ? "是" : "否")}");
        }

        if (ImGui.CollapsingHeader("当前状态:") && OverlayService.LastState != null)
        {
            Utils.PrintOutObject(OverlayService.LastState, 0, new List<string>());
            if (OverlayService.LastState.FilterResult != null)
            {
                foreach (var result in OverlayService.LastState.FilterResult)
                {
                    Utils.PrintOutObject(result, 0, new List<string>());
                }
            }
        }

    }
}
