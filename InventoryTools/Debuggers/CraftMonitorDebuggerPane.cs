using AllaganLib.GameSheets.Sheets;
using AllaganLib.Shared.Interfaces;
using CriticalCommonLib.Crafting;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Debuggers;

public class CraftMonitorDebuggerPane : IDebugPane
{
    private readonly ICraftMonitor _craftMonitor;
    private readonly ItemSheet _itemSheet;

    public CraftMonitorDebuggerPane(ICraftMonitor craftMonitor, ItemSheet itemSheet)
    {
        _craftMonitor = craftMonitor;
        _itemSheet = itemSheet;
    }
    public string Name =>  "制作监控器";
    public unsafe void Draw()
    {
        var craftMonitorAgent = _craftMonitor.Agent;
        var simpleCraftMonitorAgent = _craftMonitor.SimpleAgent;
        if (craftMonitorAgent != null)
        {
            ImGui.Text($"制作监控器指针: {(ulong)craftMonitorAgent.Agent:X}");
            ImGui.TextUnformatted("是试验合成: " + craftMonitorAgent.IsTrialSynthesis);
            ImGui.TextUnformatted("进度: " + craftMonitorAgent.Progress);
            ImGui.TextUnformatted("所需总进度: " +
                _craftMonitor.RecipeLevelTable?.ProgressRequired(_craftMonitor
                    .CurrentRecipe) ?? "未知");
            ImGui.TextUnformatted("品质: " + craftMonitorAgent.Quality);
            ImGui.TextUnformatted("状态: " + craftMonitorAgent.Status);
            ImGui.TextUnformatted("步骤: " + craftMonitorAgent.Step);
            ImGui.TextUnformatted("耐久度: " + craftMonitorAgent.Durability);
            ImGui.TextUnformatted("HQ概率: " + craftMonitorAgent.HqChance);
            ImGui.TextUnformatted("物品: " +
                                  (_itemSheet.GetRow(craftMonitorAgent.ResultItemId)
                                      ?.NameString ?? "未知"));
            ImGui.TextUnformatted(
                "当前配方: " + _craftMonitor.CurrentRecipe?.RowId ?? "未知");
            ImGui.TextUnformatted(
                "配方难度: " + _craftMonitor.RecipeLevelTable?.Base.Difficulty ??
                "未知");
            ImGui.TextUnformatted(
                "配方难度系数: " +
                _craftMonitor.CurrentRecipe?.Base.DifficultyFactor ??
                "未知");
            ImGui.TextUnformatted(
                "配方耐久度: " + _craftMonitor.RecipeLevelTable?.Base.Durability ??
                "未知");
            ImGui.TextUnformatted("建议工艺: " +
                _craftMonitor.RecipeLevelTable?.Base.SuggestedCraftsmanship ?? "未知");
            ImGui.TextUnformatted(
                "当前制作类型: " + _craftMonitor.CraftType ?? "未知");
        }
        else if (simpleCraftMonitorAgent != null)
        {
            ImGui.Text($"简易制作监控器指针: {(ulong)simpleCraftMonitorAgent.Agent:X}");
            ImGui.TextUnformatted("NQ完成: " + simpleCraftMonitorAgent.NqCompleted);
            ImGui.TextUnformatted("HQ完成: " + simpleCraftMonitorAgent.HqCompleted);
            ImGui.TextUnformatted("失败: " + simpleCraftMonitorAgent.TotalFailed);
            ImGui.TextUnformatted("总完成数: " + simpleCraftMonitorAgent.TotalCompleted);
            ImGui.TextUnformatted("总数: " + simpleCraftMonitorAgent.Total);
            ImGui.TextUnformatted("物品: " + _itemSheet
                .GetRowOrDefault(simpleCraftMonitorAgent.ResultItemId)?.NameString.ToString() ?? "未知");
            ImGui.TextUnformatted(
                "当前配方: " + _craftMonitor.CurrentRecipe?.RowId ?? "未知");
            ImGui.TextUnformatted(
                "当前制作类型: " + _craftMonitor.CraftType ?? "未知");
        }
        else
        {
            ImGui.TextUnformatted("未在制作中。");
        }
    }
}