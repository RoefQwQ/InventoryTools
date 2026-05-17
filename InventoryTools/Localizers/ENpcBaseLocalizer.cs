using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Plugin.Services;
using InventoryTools.Services;
using Lumina.Excel.Sheets;

namespace InventoryTools.Localizers;

public class ENpcBaseLocalizer : ILocalizer<ENpcBase>
{
    private readonly ISeStringEvaluator _seStringEvaluator;
    private readonly ILocalizationService _localizationService;

    public ENpcBaseLocalizer(ISeStringEvaluator seStringEvaluator, ILocalizationService localizationService)
    {
        _seStringEvaluator = seStringEvaluator;
        _localizationService = localizationService;
    }
    public string Format(ENpcBase instance)
    {
        return _seStringEvaluator.EvaluateObjStr(ObjectKind.EventNpc, instance.RowId);
    }
}