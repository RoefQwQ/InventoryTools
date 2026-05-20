using System;
using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace InventoryTools.Debuggers;

public class AddonOffsetFinderDebuggerPane : DebugLogPane
{
    private readonly IGameGui _gameGui;
    private string _addonName = "";
    private int _componentId = 84;
    private int _maxScanSize = 0x5000;

    public AddonOffsetFinderDebuggerPane(IGameGui gameGui)
    {
        _gameGui = gameGui;
    }

    public override string Name => "插件偏移查找器";
    public override unsafe void DrawInfo()
    {
        ImGui.InputText("插件名称", ref _addonName, 100);
        ImGui.InputInt("组件ID", ref _componentId);
        ImGui.InputInt("最大扫描大小", ref _maxScanSize);


        if (ImGui.Button("扫描"))
        {

            var addon = _gameGui.GetAddonByName(_addonName);
            if (addon != IntPtr.Zero)
            {
                var unitBase = (AtkUnitBase*)addon.Address;

                if (unitBase != null)
                {
                    var buttonPtr = (IntPtr)unitBase->GetComponentByNodeId(_componentId < 0 ? 0 : (uint)_componentId);
                    if (buttonPtr != IntPtr.Zero)
                    {
                        int offset = FindReferenceOffset((IntPtr)unitBase, buttonPtr, _maxScanSize);
                        if (offset != -1)
                        {
                            this.AddLog($"潜在字段偏移: 0x{offset:X}");
                        }
                        else
                        {
                            this.AddLog("未找到引用偏移。");
                        }
                    }
                    else
                    {
                        this.AddLog("未找到组件。");
                    }
                }
            }
            else
            {
                this.AddLog("未找到插件。");
            }
        }
    }

    public unsafe int FindReferenceOffset(IntPtr addonBasePtr, IntPtr buttonPtr, int maxScanSize = 0x5000)
    {
        if (addonBasePtr == IntPtr.Zero || buttonPtr == IntPtr.Zero)
            throw new ArgumentException("Pointers must be valid");

        byte* baseAddress = (byte*)addonBasePtr;
        long targetAddress = buttonPtr.ToInt64();

        // Scan the memory region of the addon for a pointer that matches buttonPtr
        for (int offset = 0; offset < maxScanSize; offset += sizeof(void*))
        {
            try
            {
                long* potentialPtr = (long*)(baseAddress + offset);
                if (potentialPtr != null && *potentialPtr == targetAddress)
                {
                    return offset; // Found the reference inside the struct
                }
            }
            catch (Exception ex)
            {
                this.AddLog($"偏移0x{offset:X}处内存读取错误: {ex.Message}");
            }
        }

        return -1; // Not found
    }

    public unsafe int FindFieldOffset(IntPtr addonBasePtr, IntPtr buttonPtr)
    {
        if (addonBasePtr == IntPtr.Zero || buttonPtr == IntPtr.Zero)
        {
            return -1;
        }

        // Compute the offset
        int offset = (int)((long)buttonPtr - (long)addonBasePtr);

        return offset;
    }
}