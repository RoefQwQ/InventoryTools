using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Plugin.Services;
using InventoryTools.Services;
using Lumina.Excel.Sheets;

namespace InventoryTools.Localizers;

public class CompanionLocalizer : ILocalizer<Companion>
{
    private readonly ISeStringEvaluator _seStringEvaluator;
    private readonly ILocalizationService _localizationService;

    public CompanionLocalizer(ISeStringEvaluator seStringEvaluator, ILocalizationService localizationService)
    {
        _seStringEvaluator = seStringEvaluator;
        _localizationService = localizationService;
    }

    public string Format(Companion instance)
    {
        return _seStringEvaluator.EvaluateObjStr(ObjectKind.Companion, instance.RowId);
    }
}