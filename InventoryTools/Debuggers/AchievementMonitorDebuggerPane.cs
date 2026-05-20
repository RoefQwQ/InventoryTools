using System.Collections.Generic;
using System.Linq;
using AllaganLib.Monitors.Interfaces;
using AllaganLib.Shared.Extensions;
using CriticalCommonLib;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Debuggers;

public class AchievementDebuggerPane : DebugLogPane
{
    private readonly IAchievementMonitorService _achievementMonitorService;

    public AchievementDebuggerPane(IAchievementMonitorService achievementMonitorService)
    {
        _achievementMonitorService = achievementMonitorService;
    }

    public override string Name => "成就监控器";

    public override void SubscribeToEvents()
    {
    }

    public override void DrawInfo()
    {
        if (ImGui.CollapsingHeader("状态"))
        {
            ImGui.TextUnformatted($"已加载: {_achievementMonitorService.IsLoaded}");
            ImGui.TextUnformatted($"已完成成就数量: {_achievementMonitorService.GetCompletedAchievementIds().Count}");
        }

        if (ImGui.CollapsingHeader("已完成成就"))
        {
            var completed = _achievementMonitorService.GetCompletedAchievements();

            if (completed.Count == 0)
            {
                ImGui.TextUnformatted("<无>");
            }
            else
            {
                foreach (var rowRef in completed.OrderBy(r => r.RowId))
                {
                    var name = rowRef.ValueNullable?.Name.ToImGuiString() ?? $"<未知名称>";
                    ImGui.TextUnformatted($"ID={rowRef.RowId}, 名称={name}");
                }
            }
        }

        if (ImGui.CollapsingHeader("配置"))
        {
            var config = _achievementMonitorService.Configuration;
            if (config == null)
            {
                ImGui.TextUnformatted("<无配置>");
            }
            else
            {
                // Print configuration recursively
                Utils.PrintOutObject(config, 0, new List<string>());
            }
        }
    }
}