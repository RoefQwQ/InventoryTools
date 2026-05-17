using System;
using InventoryTools.Services;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Localizers;

public class RelicWeaponTypeLocalizer : ILocalizer<RelicWeaponType>
{
    private readonly ILocalizationService _localizationService;

    public RelicWeaponTypeLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(RelicWeaponType relicWeaponType)
    {
        return relicWeaponType switch
        {
            RelicWeaponType.Unknown => _localizationService["RelicWeaponType_Unknown"],
            RelicWeaponType.ZodiacBase => _localizationService["RelicWeaponType_ZodiacBase"],
            RelicWeaponType.ZodiacZenith => _localizationService["RelicWeaponType_ZodiacZenith"],
            RelicWeaponType.ZodiacAtma => _localizationService["RelicWeaponType_ZodiacAtma"],
            RelicWeaponType.ZodiacAnimus => _localizationService["RelicWeaponType_ZodiacAnimus"],
            RelicWeaponType.ZodiacNovus => _localizationService["RelicWeaponType_ZodiacNovus"],
            RelicWeaponType.ZodiacNexus => _localizationService["RelicWeaponType_ZodiacNexus"],
            RelicWeaponType.ZodiacZodiac => _localizationService["RelicWeaponType_ZodiacZodiac"],
            RelicWeaponType.ZodiacZeta => _localizationService["RelicWeaponType_ZodiacZeta"],
            RelicWeaponType.AnimaAnimated => _localizationService["RelicWeaponType_AnimaAnimated"],
            RelicWeaponType.AnimaAwoken => _localizationService["RelicWeaponType_AnimaAwoken"],
            RelicWeaponType.AnimaAnima => _localizationService["RelicWeaponType_AnimaAnima"],
            RelicWeaponType.AnimaHyperconductive => _localizationService["RelicWeaponType_AnimaHyperconductive"],
            RelicWeaponType.AnimaReconditioned => _localizationService["RelicWeaponType_AnimaReconditioned"],
            RelicWeaponType.AnimaSharpened => _localizationService["RelicWeaponType_AnimaSharpened"],
            RelicWeaponType.AnimaComplete => _localizationService["RelicWeaponType_AnimaComplete"],
            RelicWeaponType.AnimaLux => _localizationService["RelicWeaponType_AnimaLux"],
            RelicWeaponType.EurekanAntiquated => _localizationService["RelicWeaponType_EurekanAntiquated"],
            RelicWeaponType.EurekanBase => _localizationService["RelicWeaponType_EurekanBase"],
            RelicWeaponType.EurekanBase1 => _localizationService["RelicWeaponType_EurekanBase1"],
            RelicWeaponType.EurekanBase2 => _localizationService["RelicWeaponType_EurekanBase2"],
            RelicWeaponType.EurekanAnemos => _localizationService["RelicWeaponType_EurekanAnemos"],
            RelicWeaponType.EurekanPagos => _localizationService["RelicWeaponType_EurekanPagos"],
            RelicWeaponType.EurekanPagos1 => _localizationService["RelicWeaponType_EurekanPagos1"],
            RelicWeaponType.EurekanElemental => _localizationService["RelicWeaponType_EurekanElemental"],
            RelicWeaponType.EurekanElemental1 => _localizationService["RelicWeaponType_EurekanElemental1"],
            RelicWeaponType.EurekanElemental2 => _localizationService["RelicWeaponType_EurekanElemental2"],
            RelicWeaponType.EurekanPyros => _localizationService["RelicWeaponType_EurekanPyros"],
            RelicWeaponType.EurekanHydatos => _localizationService["RelicWeaponType_EurekanHydatos"],
            RelicWeaponType.EurekanHydatos1 => _localizationService["RelicWeaponType_EurekanHydatos1"],
            RelicWeaponType.EurekanBaseEureka => _localizationService["RelicWeaponType_EurekanBaseEureka"],
            RelicWeaponType.EurekanEureka => _localizationService["RelicWeaponType_EurekanEureka"],
            RelicWeaponType.EurekanPhyseos => _localizationService["RelicWeaponType_EurekanPhyseos"],
            RelicWeaponType.ResistanceResistance => _localizationService["RelicWeaponType_ResistanceResistance"],
            RelicWeaponType.ResistanceAugmentedResistance => _localizationService["RelicWeaponType_ResistanceAugmentedResistance"],
            RelicWeaponType.ResistanceRecollection => _localizationService["RelicWeaponType_ResistanceRecollection"],
            RelicWeaponType.ResistanceLawsOrder => _localizationService["RelicWeaponType_ResistanceLawsOrder"],
            RelicWeaponType.ResistanceAugmentedLawsOrder => _localizationService["RelicWeaponType_ResistanceAugmentedLawsOrder"],
            RelicWeaponType.ResistanceBlades => _localizationService["RelicWeaponType_ResistanceBlades"],
            RelicWeaponType.MandervilleManderville => _localizationService["RelicWeaponType_MandervilleManderville"],
            RelicWeaponType.MandervilleAmazing => _localizationService["RelicWeaponType_MandervilleAmazing"],
            RelicWeaponType.MandervilleMajestic => _localizationService["RelicWeaponType_MandervilleMajestic"],
            RelicWeaponType.MandervilleMandervillous => _localizationService["RelicWeaponType_MandervilleMandervillous"],
            RelicWeaponType.PhantomPenumbrae => _localizationService["RelicWeaponType_PhantomPenumbrae"],
            RelicWeaponType.PhantomUmbrae => _localizationService["RelicWeaponType_PhantomUmbrae"],
            RelicWeaponType.PhantomObscurum => _localizationService["RelicWeaponType_PhantomObscurum"],
            _ => relicWeaponType.ToString()
        };
    }
}
