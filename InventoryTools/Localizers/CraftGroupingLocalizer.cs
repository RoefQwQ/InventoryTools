using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Extensions;
using InventoryTools.Services;

namespace InventoryTools.Localizers;

public class CraftGroupingLocalizer
{
    private readonly CraftTypeSheet _craftTypeSheet;
    private readonly MapSheet _mapSheet;
    private readonly ILocalizationService _localizationService;

    public CraftGroupingLocalizer(CraftTypeSheet craftTypeSheet, MapSheet mapSheet, ILocalizationService localizationService)
    {
        _craftTypeSheet = craftTypeSheet;
        _mapSheet = mapSheet;
        _localizationService = localizationService;
    }
    public string FormattedName(CraftGrouping craftGrouping)
    {
        var name = craftGrouping.CraftGroupType.FormattedName();
        if (craftGrouping.Depth != null)
        {
            name = craftGrouping.Depth.Value.ConvertToOrdinal() + " " + _localizationService.GetString("CraftGrouping_Tier") + " " + name;
        }

        if (craftGrouping.CraftTypeId != null)
        {
            var classJob = _craftTypeSheet.GetRowOrDefault(craftGrouping.CraftTypeId.Value);
            if (classJob != null)
            {
                name = classJob.FormattedName + " - " + name;
            }
        }

        if (craftGrouping.MapId != null)
        {
            var map = _mapSheet.GetRowOrDefault(craftGrouping.MapId.Value);
            if (map != null)
            {
                name = map.FormattedName;
            }
        }

        return name;
    }
}
