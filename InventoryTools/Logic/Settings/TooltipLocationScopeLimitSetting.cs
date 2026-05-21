using System.Collections.Generic;
using Dalamud.Interface.Colors;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;



public class TooltipLocationScopeLimitSetting : Setting<List<InventorySearchScope>?>
{
    private readonly InventoryScopePicker _scopePicker;

    public TooltipLocationScopeLimitSetting(ILogger<TooltipLocationScopeLimitSetting> logger, ImGuiService imGuiService, InventoryScopePicker scopePicker, ILocalizationService localizationService) : base(logger, imGuiService)
    {
        _scopePicker = scopePicker;
        Name = localizationService.GetString("Setting_TooltipLocationScopeLimit_Name");
        HelpText = localizationService.GetString("Setting_TooltipLocationScopeLimit_HelpText");
    }
    public override List<InventorySearchScope>? DefaultValue { get; set; } = null;
    public override List<InventorySearchScope>? CurrentValue(InventoryToolsConfiguration configuration)
    {
        if (configuration.TooltipSearchScope == null || configuration.TooltipSearchScope.Count == 0)
        {
            return DefaultValue;
        }
        return configuration.TooltipSearchScope;
    }

    public override void Draw(InventoryToolsConfiguration configuration, string? customName, bool? disableReset,
        bool? disableColouring)
    {
        var currentScopes = CurrentValue(configuration) ?? new List<InventorySearchScope>();
        if (disableColouring != true && HasValueSet(configuration))
        {
            ImGui.PushStyleColor(ImGuiCol.Text, ImGuiColors.HealerGreen);
            ImGui.LabelText("##" + Key + "Label", Name);
            ImGui.PopStyleColor();
        }
        else
        {
            ImGui.LabelText("##" + Key + "Label", Name);
        }

        ImGui.SetNextItemWidth(InputSize - 26);
        if (_scopePicker.Draw("##tooltipScope", currentScopes))
        {
            UpdateFilterConfiguration(configuration, currentScopes);
        }

        ImGui.SameLine();
        ImGuiService.HelpMarker(HelpText, Image, ImageSize);
        if (disableReset != true && HasValueSet(configuration))
        {
            ImGui.SameLine();
            if (ImGui.Button("Reset##" + Key + "Reset"))
            {
                Reset(configuration);
            }
        }
    }

    public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, List<InventorySearchScope>? newValue)
    {
        configuration.TooltipSearchScope = newValue;
    }

    public override string Key { get; set; } = "TooltipLocationScopeLimit";
    public override string Name { get; set; } = "添加物品位置（搜索位置）";
    public override string HelpText { get; set; } = "在工具提示中显示你拥有的物品位置时，应该包含哪些库存的搜索结果？";

    public override SettingCategory SettingCategory { get; set; } = SettingCategory.ToolTips;
    public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.AddItemLocations;
    public override string Version => "1.7.0.11";
}