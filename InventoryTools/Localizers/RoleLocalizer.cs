using AllaganLib.GameSheets.Sheets.Rows;
using InventoryTools.Services;

namespace InventoryTools.Localizers;

public class RoleLocalizer : ILocalizer<RoleType>
{
    private readonly ILocalizationService _localizationService;

    public RoleLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(RoleType instance)
    {
        return instance switch
        {
            RoleType.Tank => _localizationService["Role_Tank"],
            RoleType.DPSMelee => _localizationService["Role_DPSMelee"],
            RoleType.DPSRanged => _localizationService["Role_DPSRanged"],
            RoleType.Healer => _localizationService["Role_Healer"],
            RoleType.Crafting => _localizationService["Role_Crafting"],
            RoleType.Gathering => _localizationService["Role_Gathering"],
            RoleType.Other => _localizationService["Role_Other"],
            _ => _localizationService["Role_Unknown"]
        };
    }
}
