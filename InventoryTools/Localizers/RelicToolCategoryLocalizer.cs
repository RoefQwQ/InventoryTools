using System;
using InventoryTools.Services;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicToolCategoryLocalizer : ILocalizer<RelicToolCategory>
{
    private readonly ILocalizationService _localizationService;

    public RelicToolCategoryLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(RelicToolCategory instance)
    {
        return instance switch
        {
            RelicToolCategory.Mastercraft => _localizationService["RelicToolCategory_Mastercraft"],
            RelicToolCategory.Skysteel => _localizationService["RelicToolCategory_Skysteel"],
            RelicToolCategory.Resplendent => _localizationService["RelicToolCategory_Resplendent"],
            RelicToolCategory.Splendorous => _localizationService["RelicToolCategory_Splendorous"],
            RelicToolCategory.Cosmic => _localizationService["RelicToolCategory_Cosmic"],
            _ => instance.ToString()
        };
    }
}
