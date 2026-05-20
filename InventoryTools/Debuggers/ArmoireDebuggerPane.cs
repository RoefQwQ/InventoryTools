using System;
using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace InventoryTools.Debuggers;

public class ArmoireDebuggerPane : IDebugPane
{
    private readonly IGameGui _gameGui;

    public ArmoireDebuggerPane(IGameGui gameGui)
    {
        _gameGui = gameGui;
    }

    public string Name => "衣橱";
    public unsafe void Draw()
    {
        var uiState = UIState.Instance();
        if (uiState == null)
        {
            ImGui.Text("未找到UIState。");
        }
        else
        {
            ImGui.Text(uiState->Cabinet.IsCabinetLoaded() ? "柜子已加载" : "柜子未加载");
        }

        var addon = this._gameGui.GetAddonByName("CabinetWithdraw");
        if (addon != IntPtr.Zero)
        {
            var cabinetWithdraw = (AddonCabinetWithdraw*)addon.Address;
            if (cabinetWithdraw != null)
            {
                ImGui.Text($"神话装备已选中: { (cabinetWithdraw->ArtifactArmorRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"季节装备1已选中: { (cabinetWithdraw->SeasonalGear1RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"季节装备2已选中: { (cabinetWithdraw->SeasonalGear2RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"季节装备3已选中: { (cabinetWithdraw->SeasonalGear3RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"季节装备4已选中: { (cabinetWithdraw->SeasonalGear4RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"季节装备5已选中: { (cabinetWithdraw->SeasonalGear5RadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"成就已选中: { (cabinetWithdraw->AchievementsRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"专属额外已选中: { (cabinetWithdraw->ExclusiveExtrasRadioButton->IsChecked ? "是" : "否") }");
                ImGui.Text($"搜索已选中: { (cabinetWithdraw->SearchRadioButton->IsChecked ? "是" : "否") }");
            }
        }
    }
}