using System;
using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using InventoryTools.Services;

namespace InventoryTools.Debuggers;

public class WindowServiceDebuggerPane : IDebugPane
{
    private readonly Lazy<WindowService> _windowService;

    public WindowService WindowService => _windowService.Value;

    public WindowServiceDebuggerPane(Lazy<WindowService> windowService)
    {
        _windowService = windowService;
    }
    public string Name => "窗口服务";
    public void Draw()
    {
        ImGui.Text($"过滤器窗口已打开: {WindowService.HasFilterWindowOpen}");
    }
}