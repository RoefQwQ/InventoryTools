using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Logic.Settings.Abstract.Generic;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings;

public class ContextMenuAddToFavouritesSetting : GenericBooleanSetting
{
    public ContextMenuAddToFavouritesSetting(ILogger<ContextMenuAddToFavouritesSetting> logger,
        ImGuiService imGuiService, ILocalizationService localizationService) : base("AddToFavouritesContextMenu",
        "Context Menu - Add/Remove to Favourites",
        "Add a submenu to add/remove the item to/from your favourites?",
        false,
        SettingCategory.ContextMenu,
        SettingSubCategory.General,
        "1.13.1.9",
        logger,
        imGuiService,
        localizationService)
    {
    }

    public override string WizardName { get; } = "添加/移除收藏";

}