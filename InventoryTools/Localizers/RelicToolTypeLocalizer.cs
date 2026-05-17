using System;
using InventoryTools.Services;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicToolTypeLocalizer : ILocalizer<RelicToolType>
{
    private readonly ILocalizationService _localizationService;

    public RelicToolTypeLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(RelicToolType relicToolType)
    {
        return relicToolType switch
        {
            RelicToolType.Unknown => _localizationService["RelicToolType_Unknown"],
            RelicToolType.MastercraftBase => _localizationService["RelicToolType_MastercraftBase"],
            RelicToolType.MastercraftSupra => _localizationService["RelicToolType_MastercraftSupra"],
            RelicToolType.MastercraftLucis => _localizationService["RelicToolType_MastercraftLucis"],
            RelicToolType.SkysteelBase => _localizationService["RelicToolType_SkysteelBase"],
            RelicToolType.SkysteelBase1 => _localizationService["RelicToolType_SkysteelBase1"],
            RelicToolType.SkysteelDragonsung => _localizationService["RelicToolType_SkysteelDragonsung"],
            RelicToolType.SkysteelAugmentedDragonsung => _localizationService["RelicToolType_SkysteelAugmentedDragonsung"],
            RelicToolType.SkysteelSkysung => _localizationService["RelicToolType_SkysteelSkysung"],
            RelicToolType.SkysteelSkybuilders => _localizationService["RelicToolType_SkysteelSkybuilders"],
            RelicToolType.Resplendent => _localizationService["RelicToolType_Resplendent"],
            RelicToolType.SplendorousBase => _localizationService["RelicToolType_SplendorousBase"],
            RelicToolType.SplendorousAugmented => _localizationService["RelicToolType_SplendorousAugmented"],
            RelicToolType.SplendorousCrystalline => _localizationService["RelicToolType_SplendorousCrystalline"],
            RelicToolType.SplendorousChoraZoiCrystalline => _localizationService["RelicToolType_SplendorousChoraZoiCrystalline"],
            RelicToolType.SplendorousBrilliant => _localizationService["RelicToolType_SplendorousBrilliant"],
            RelicToolType.SplendorousVrandticVisionary => _localizationService["RelicToolType_SplendorousVrandticVisionary"],
            RelicToolType.SplendorousLodestar => _localizationService["RelicToolType_SplendorousLodestar"],
            RelicToolType.CosmicPrototype01 => _localizationService["RelicToolType_CosmicPrototype01"],
            RelicToolType.CosmicPrototype02 => _localizationService["RelicToolType_CosmicPrototype02"],
            RelicToolType.CosmicPrototype03 => _localizationService["RelicToolType_CosmicPrototype03"],
            RelicToolType.CosmicPrototype04 => _localizationService["RelicToolType_CosmicPrototype04"],
            RelicToolType.CosmicPrototype05 => _localizationService["RelicToolType_CosmicPrototype05"],
            RelicToolType.CosmicPrototype06 => _localizationService["RelicToolType_CosmicPrototype06"],
            RelicToolType.CosmicPrototype07 => _localizationService["RelicToolType_CosmicPrototype07"],
            RelicToolType.CosmicPrototype08 => _localizationService["RelicToolType_CosmicPrototype08"],
            RelicToolType.CosmicCosmic => _localizationService["RelicToolType_CosmicCosmic"],
            RelicToolType.CosmicCosmic11 => _localizationService["RelicToolType_CosmicCosmic11"],
            RelicToolType.CosmicCosmic12 => _localizationService["RelicToolType_CosmicCosmic12"],
            RelicToolType.CosmicCosmic13 => _localizationService["RelicToolType_CosmicCosmic13"],
            RelicToolType.CosmicCosmic14 => _localizationService["RelicToolType_CosmicCosmic14"],
            RelicToolType.CosmicStellar => _localizationService["RelicToolType_CosmicStellar"],
            RelicToolType.CosmicStellar11 => _localizationService["RelicToolType_CosmicStellar11"],
            RelicToolType.CosmicStellar12 => _localizationService["RelicToolType_CosmicStellar12"],
            RelicToolType.CosmicHypertools => _localizationService["RelicToolType_CosmicHypertools"],
            _ => relicToolType.ToString()
        };
    }
}
