using System;
using InventoryTools.Services;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicWeaponCategoryLocalizer : ILocalizer<RelicWeaponCategory>
{
    private readonly ILocalizationService _localizationService;

    public RelicWeaponCategoryLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(RelicWeaponCategory instance)
    {
        return instance switch
        {
            RelicWeaponCategory.Zodiac => _localizationService["RelicWeaponCategory_Zodiac"],
            RelicWeaponCategory.Anima => _localizationService["RelicWeaponCategory_Anima"],
            RelicWeaponCategory.Eurekan => _localizationService["RelicWeaponCategory_Eurekan"],
            RelicWeaponCategory.Resistance => _localizationService["RelicWeaponCategory_Resistance"],
            RelicWeaponCategory.Manderville => _localizationService["RelicWeaponCategory_Manderville"],
            RelicWeaponCategory.Phantom => _localizationService["RelicWeaponCategory_Phantom"],
            _ => instance.ToString()
        };
    }
}
