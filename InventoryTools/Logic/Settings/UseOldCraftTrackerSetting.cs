using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class UseOldCraftTrackerSetting : GenericBooleanSetting
{
    public UseOldCraftTrackerSetting(ILogger<UseOldCraftTrackerSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService) : base("UseOldCraftTracker", "使用旧版制作追踪器？", "使用旧版制作追踪器，此追踪器仅追踪制作，并使用与新版获取追踪器不同的机制。", false, SettingCategory.CraftTracker, SettingSubCategory.General, "1.12.0.5", logger, imGuiService, localizationService)
    {
    }
}