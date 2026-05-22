using CriticalCommonLib.Services;
using DalaMock.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;
using OtterGui;

namespace InventoryTools.Logic.Settings;

public class TrackMobSpawnSetting : BooleanSetting
{
    private readonly IFileDialogManager _fileDialogManager;
    private readonly IMobTracker _mobTracker;

    public TrackMobSpawnSetting(ILogger<TrackMobSpawnSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, IFileDialogManager fileDialogManager, IMobTracker mobTracker) : base(logger, imGuiService, localizationService)
    {
        _fileDialogManager = fileDialogManager;
        _mobTracker = mobTracker;
        Name = localizationService.GetString("Setting_TrackMobSpawns_Name");
        HelpText = localizationService.GetString("Setting_TrackMobSpawns_HelpText");
    }
    public override bool DefaultValue { get; set; } = false;
    public override bool CurrentValue(InventoryToolsConfiguration configuration)
    {
        return configuration.TrackMobSpawns;
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, bool newValue)
    {
        configuration.TrackMobSpawns = newValue;
    }

    public override string Key { get; set; } = "TrackMobSpawns";
    public override string Name { get; set; } = "追踪怪物刷新";

    public override string HelpText { get; set; } =
        "插件是否应追踪怪物的刷新位置。此数据目前尚未被插件使用，但收集足够后可点击复选框旁的按钮导出包含位置的文件。如上传CSV并发送链接，可为所有人提供准确的怪物刷新数据。";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.MobSpawnTracker;

    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.General;

    public override void Draw(InventoryToolsConfiguration configuration, string? customName, bool? disableReset,
        bool? disableColouring)
    {
        base.Draw(configuration, null, null, null);
        if (configuration.TrackMobSpawns)
        {
            ImGui.SameLine();
            if (ImGui.Button("导出CSV"))
            {
                _fileDialogManager.SaveFileDialog("保存为csv", "*.csv", "mob_spawns.csv", ".csv",
                    (b, s) => { SaveMobSpawns(b, s); }, null, true);
            }

            ImGuiUtil.HoverTooltip("导出包含怪物刷新ID及其位置的CSV文件。");
        }
    }

    private void SaveMobSpawns(bool b, string s)
    {
        if (b)
        {
            var entries = _mobTracker.GetEntries();
            _mobTracker.SaveCsv(s, entries);
        }
    }
    public override string Version => "1.7.0.0";
}