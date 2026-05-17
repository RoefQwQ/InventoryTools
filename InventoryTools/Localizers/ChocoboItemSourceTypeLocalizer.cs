using InventoryTools.Compendium.Types.Extra;
using InventoryTools.Services;

namespace InventoryTools.Localizers;

public class ChocoboItemSourceTypeLocalizer : ILocalizer<ChocoboItemSourceType>
{
    private readonly ILocalizationService _localizationService;

    public ChocoboItemSourceTypeLocalizer(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public string Format(ChocoboItemSourceType itemSourceType)
    {
        return itemSourceType switch
        {
            ChocoboItemSourceType.BuddyItem => _localizationService["Chocobo_BuddyItem"],
            ChocoboItemSourceType.BuddyEquip => _localizationService["Chocobo_BuddyEquip"],
            _ => _localizationService["Chocobo_Unknown"]
        };
    }
}
