using System.Collections.Generic;
using AllaganLib.Monitors.Enums;
using AllaganLib.Monitors.Interfaces;
using CriticalCommonLib;
using Dalamud.Bindings.ImGui;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace InventoryTools.Debuggers;

public class AcquisitionDebuggerPane : DebugLogPane
{
    private readonly IAcquisitionMonitorService _acquisitionMonitorService;

    public AcquisitionDebuggerPane(IAcquisitionMonitorService acquisitionMonitorService)
    {
        _acquisitionMonitorService = acquisitionMonitorService;
    }

    public override string Name => "物品获取监控器";

    public override void SubscribeToEvents()
    {
        _acquisitionMonitorService.ItemAcquired += OnItemAcquired;
        RegisterSubscription(() => _acquisitionMonitorService.ItemAcquired -= OnItemAcquired);
    }

    private void OnItemAcquired(
        uint itemId,
        InventoryItem.ItemFlags itemFlags,
        int qtyIncrease,
        AcquisitionReason reason)
    {
        AddLog(
            $"物品已获取: 物品ID={itemId}, " +
            $"数量变化={qtyIncrease}, " +
            $"标志={itemFlags}, " +
            $"原因={reason}"
        );
    }

    public override void DrawInfo()
    {
        if (ImGui.CollapsingHeader("配置"))
        {
            var config = _acquisitionMonitorService.Configuration;

            if (config == null)
            {
                ImGui.TextUnformatted("<无配置>");
            }
            else
            {
                // Intentionally generic: avoids assumptions about configuration shape
                Utils.PrintOutObject(config, 0, new List<string>());
            }
        }

        if (ImGui.CollapsingHeader("最近活动"))
        {
            ImGui.TextUnformatted(
                "请查看下方日志面板获取按时间顺序排列的获取事件列表。"
            );
        }
    }
}