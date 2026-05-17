using CriticalCommonLib.Crafting;
using CriticalCommonLib.Models;
using InventoryTools.Services;

namespace InventoryTools.Localizers;

public class CraftItemLocalizer
{
    private readonly IngredientPreferenceLocalizer _ingredientPreferenceLocalizer;
    private readonly ILocalizationService _localizationService;

    public CraftItemLocalizer(IngredientPreferenceLocalizer ingredientPreferenceLocalizer, ILocalizationService localizationService)
    {
        _ingredientPreferenceLocalizer = ingredientPreferenceLocalizer;
        _localizationService = localizationService;
    }

    public int SourceIcon(CraftItem craftItem)
    {
        return craftItem.IngredientPreference.Type switch
        {
            IngredientPreferenceType.Crafting => craftItem.Recipe?.CraftType?.Icon ?? Icons.CraftIcon,
            IngredientPreferenceType.None => craftItem.Item.Icon,
            _ => _ingredientPreferenceLocalizer.SourceIcon(craftItem.IngredientPreference)!.Value
        };
    }

    public string SourceName(CraftItem craftItem)
    {
        return craftItem.IngredientPreference.Type switch
        {
            IngredientPreferenceType.Crafting => craftItem.Recipe?.CraftType?.FormattedName ?? (craftItem.Item.CompanyCraftSequence != null ? _localizationService.GetString("CraftItem_CompanyCraft") : _localizationService.GetString("CraftItem_Unknown")),
            IngredientPreferenceType.None => _localizationService.GetString("CraftItem_NA"),
            _ => _ingredientPreferenceLocalizer.FormattedName(craftItem.IngredientPreference)
        };
    }

}
