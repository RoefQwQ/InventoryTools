using System.Collections.Generic;
using AllaganLib.Shared.Debuggers;
using AllaganLib.Shared.Interfaces;
using CriticalCommonLib;
using CriticalCommonLib.Models;
using CriticalCommonLib.Services;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace InventoryTools.Debuggers;

public class CharacterDebuggerPane : DebugLogPane
{
    private readonly ICharacterMonitor _characterMonitor;
    private readonly IClientState _clientState;
    private readonly InventoryToolsConfiguration _configuration;

    public CharacterDebuggerPane(ICharacterMonitor characterMonitor, IClientState clientState, InventoryToolsConfiguration configuration)
    {
        _characterMonitor = characterMonitor;
        _clientState = clientState;
        _configuration = configuration;
    }
    public override string Name => "角色监控器";

    public override void SubscribeToEvents()
    {
        _characterMonitor.OnActiveRetainerChanged += OnCharacterMonitorOnOnActiveRetainerChanged;
        RegisterSubscription(() => _characterMonitor.OnActiveRetainerChanged -= OnCharacterMonitorOnOnActiveRetainerChanged);

        _characterMonitor.OnActiveRetainerLoaded += OnCharacterMonitorOnOnActiveRetainerLoaded;
        RegisterSubscription(() => _characterMonitor.OnActiveRetainerLoaded -= OnCharacterMonitorOnOnActiveRetainerLoaded);

        _characterMonitor.OnActiveFreeCompanyChanged += OnCharacterMonitorOnOnActiveFreeCompanyChanged;
        RegisterSubscription(() => _characterMonitor.OnActiveFreeCompanyChanged -= OnCharacterMonitorOnOnActiveFreeCompanyChanged);

        _characterMonitor.OnActiveHouseChanged += OnCharacterMonitorOnOnActiveHouseChanged;
        RegisterSubscription(() => _characterMonitor.OnActiveHouseChanged -= OnCharacterMonitorOnOnActiveHouseChanged);

        _characterMonitor.OnCharacterUpdated += OnCharacterMonitorOnOnCharacterUpdated;
        RegisterSubscription(() => _characterMonitor.OnCharacterUpdated -= OnCharacterMonitorOnOnCharacterUpdated);

        _characterMonitor.OnCharacterRemoved += OnCharacterMonitorOnOnCharacterRemoved;
        RegisterSubscription(() => _characterMonitor.OnCharacterRemoved -= OnCharacterMonitorOnOnCharacterRemoved);

        _characterMonitor.OnCharacterJobChanged += OnCharacterJobChanged;
        RegisterSubscription(() => _characterMonitor.OnCharacterJobChanged -= OnCharacterJobChanged);

        _characterMonitor.OnCharacterLoggedIn += OnCharacterMonitorOnOnCharacterLoggedIn;
        RegisterSubscription(() => _characterMonitor.OnCharacterLoggedIn -= OnCharacterMonitorOnOnCharacterLoggedIn);

        _characterMonitor.OnCharacterLoggedOut += OnCharacterMonitorOnOnCharacterLoggedOut;
        RegisterSubscription(() => _characterMonitor.OnCharacterLoggedOut -= OnCharacterMonitorOnOnCharacterLoggedOut);
    }

    private void OnCharacterMonitorOnOnCharacterLoggedOut(ulong id)
    {
        AddLog($"角色已登出: {id}");
    }

    private void OnCharacterMonitorOnOnCharacterLoggedIn(ulong id)
    {
        AddLog($"角色已登录: {id}");
    }

    private void OnCharacterJobChanged()
    {
        AddLog($"角色职业已变更");
    }

    private void OnCharacterMonitorOnOnCharacterRemoved(ulong id)
    {
        AddLog($"角色已移除: {id}");
    }

    private void OnCharacterMonitorOnOnCharacterUpdated(Character? c)
    {
        AddLog($"角色已更新: {c}");
    }

    private void OnCharacterMonitorOnOnActiveHouseChanged(ulong houseId, sbyte wardId, sbyte plotId, byte divisionId, short roomId, bool hasHousePermission)
    {
        AddLog($"活动房屋已变更: {houseId}, {wardId}, {plotId}, {divisionId}, {roomId}, {hasHousePermission}");
    }

    private void OnCharacterMonitorOnOnActiveFreeCompanyChanged(ulong c)
    {
        AddLog($"活动部队已变更: {c}");
    }

    private void OnCharacterMonitorOnOnActiveRetainerLoaded(ulong c)
    {
        AddLog($"活动雇员已加载: {c}");
    }

    private void OnCharacterMonitorOnOnActiveRetainerChanged(ulong c)
    {
        AddLog($"活动雇员已变更: {c}");
    }

    public override unsafe void DrawInfo()
    {
        if (ImGui.CollapsingHeader("会话 / 活动状态"))
        {
            ImGui.TextUnformatted($"已登录: {_characterMonitor.IsLoggedIn}");
            ImGui.TextUnformatted($"本地内容ID: {_characterMonitor.LocalContentId}");
            ImGui.TextUnformatted($"内部角色ID: {_characterMonitor.InternalCharacterId}");

            ImGui.Separator();
            ImGui.TextUnformatted("活动角色:");
            ImGui.TextUnformatted(_characterMonitor.ActiveCharacter != null
                ? $"{_characterMonitor.ActiveCharacter.Name} ({_characterMonitor.ActiveCharacterId})"
                : "<无>");

            ImGui.TextUnformatted("活动雇员:");
            ImGui.TextUnformatted(_characterMonitor.ActiveRetainer != null
                ? $"{_characterMonitor.ActiveRetainer.Name} ({_characterMonitor.ActiveRetainerId})"
                : "<无>");

            ImGui.TextUnformatted("活动部队:");
            ImGui.TextUnformatted(_characterMonitor.ActiveFreeCompany != null
                ? $"{_characterMonitor.ActiveFreeCompany.Name} ({_characterMonitor.ActiveFreeCompanyId})"
                : "<无>");
        }

        if (ImGui.CollapsingHeader("房屋"))
        {
            ImGui.TextUnformatted($"活动房屋ID: {_characterMonitor.ActiveHouseId}");
            ImGui.TextUnformatted($"缓存区ID: {_characterMonitor.InternalWardId}");
            ImGui.TextUnformatted($"缓存地块ID: {_characterMonitor.InternalPlotId}");
            ImGui.TextUnformatted($"缓存分区ID: {_characterMonitor.InternalDivisionId}");
            ImGui.TextUnformatted($"缓存房间ID: {_characterMonitor.InternalRoomId}");
            ImGui.TextUnformatted($"缓存房屋ID: {_characterMonitor.InternalHouseId}");
            ImGui.TextUnformatted($"领地类型ID: {_characterMonitor.CorrectedTerritoryTypeId}");

            var hm = HousingManager.Instance();
            if (hm != null)
            {
                if (hm->OutdoorTerritory != null)
                    ImGui.TextUnformatted($"室外房屋ID: {hm->OutdoorTerritory->HouseId.Id}");
                if (hm->IndoorTerritory != null)
                    ImGui.TextUnformatted($"室内房屋ID: {hm->IndoorTerritory->HouseId.Id}");
                if (hm->CurrentTerritory != null)
                    ImGui.TextUnformatted($"当前领地: {(ulong)hm->CurrentTerritory:X}");
            }

            ImGui.Separator();
            ImGui.TextUnformatted("拥有的房屋:");
            foreach (var id in _characterMonitor.GetOwnedHouseIds())
                ImGui.BulletText(id.ToString());

            ImGui.TextUnformatted("有房屋权限: " +
                (_characterMonitor.InternalHasHousePermission ||
                 _characterMonitor.GetOwnedHouseIds().Contains(_characterMonitor.InternalHouseId)
                    ? "是"
                    : "否"));
        }

        //
        // Worlds
        //
        if (ImGui.CollapsingHeader("世界"))
        {
            foreach (var wid in _characterMonitor.GetWorldIds())
                ImGui.BulletText($"世界 {wid}");
        }

        if (ImGui.CollapsingHeader("角色"))
        {
            foreach (var kv in _characterMonitor.Characters)
                ImGui.BulletText($"{kv.Key}: {kv.Value.Name}");
        }

        if (ImGui.CollapsingHeader("雇员"))
        {
            using (var table = ImRaii.Table("retainerTable", 6))
            {
                if (table)
                {
                    ImGui.TableSetupColumn("雇佣顺序");
                    ImGui.TableSetupColumn("名称");
                    ImGui.TableSetupColumn("类型");
                    ImGui.TableSetupColumn("金币");
                    ImGui.TableSetupColumn("ID");
                    ImGui.TableSetupColumn("所有者ID");
                    ImGui.TableHeadersRow();

                    foreach (var retainer in _characterMonitor.GetRetainerCharacters())
                    {
                        if (retainer.Value.Name == "Unhired")
                            continue;

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted((retainer.Value.HireOrder + 1).ToString());

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(retainer.Value.CharacterType == CharacterType.Housing
                            ? retainer.Value.HousingName
                            : retainer.Value.Name);

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(retainer.Value.CharacterType.ToString());

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(retainer.Value.Gil.ToString());

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(retainer.Value.CharacterId.ToString());

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(retainer.Value.OwnerId.ToString());
                    }
                }
            }
        }

        if (ImGui.CollapsingHeader("角色对象"))
        {
            foreach (var kv in _characterMonitor.Characters)
            {
                var label = kv.Value.CharacterType == CharacterType.Housing
                    ? kv.Value.HousingName
                    : kv.Value.Name;

                if (ImGui.TreeNode($"{label}##{kv.Key}"))
                {
                    Utils.PrintOutObject(kv.Value, 0, new List<string>());
                    ImGui.TreePop();
                }
            }
        }

        if (ImGui.CollapsingHeader("已获取物品"))
        {
            foreach (var characterPair in _configuration.AcquiredItems)
            {
                var character = _characterMonitor.GetCharacterById(characterPair.Key);
                ImGui.TextUnformatted(character?.FormattedName ?? "未知角色");
                ImGui.Text($"{characterPair.Value.Count} 个已解锁物品");
            }
        }
    }
}