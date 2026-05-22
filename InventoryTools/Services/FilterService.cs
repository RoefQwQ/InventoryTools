using System;
using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Sheets;
using Autofac;
using CriticalCommonLib.Models;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.GenericFilters;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using OtterGui;
using OtterGui.Extensions;

namespace InventoryTools.Services;

public interface IFilterService
{
    List<IFilter> AvailableFilters { get; }
    Dictionary<FilterCategory, List<IFilter>> GroupedFilters { get; }
}

public class FilterService : IFilterService
{
    private readonly GenericBooleanFilter.Factory _booleanFilterFactory;
    private readonly GenericIntegerFilter.Factory _integerFilterFactory;
    private readonly ExcelSheet<BaseParam> _baseParamSheet;
    private readonly ItemSheet _itemSheet;
    public readonly List<FilterCategory> FilterCategoryOrder = new() { FilterCategory.Settings, FilterCategory.Display, FilterCategory.Inventories, FilterCategory.Columns, FilterCategory.Basic, FilterCategory.Stats, FilterCategory.Sources, FilterCategory.Uses, FilterCategory.Acquisition, FilterCategory.Searching, FilterCategory.Advanced, FilterCategory.CompletionTracking};
    public FilterService(IEnumerable<IFilter> filters,
        GenericBooleanFilter.Factory booleanFilterFactory,
        GenericIntegerFilter.Factory integerFilterFactory,
        ExcelSheet<BaseParam> baseParamSheet,
        ItemSheet itemSheet)
    {
        _booleanFilterFactory = booleanFilterFactory;
        _integerFilterFactory = integerFilterFactory;
        _baseParamSheet = baseParamSheet;
        _itemSheet = itemSheet;

        _availableFilters = filters.ToList();

        foreach (var baseParam in baseParamSheet)
        {
            if (baseParam.RowId == 0 || baseParam.RowId == 15)
            {
                continue;
            }
            var baseParamId = baseParam.RowId;
            var helpText = baseParam.Description.ExtractText();
            var name = baseParam.Name.ExtractText();
            if (helpText == string.Empty)
            {
                helpText = $"The {name} of the item.";
            }
            var genericFilter = _integerFilterFactory.Invoke("BaseParam" + baseParam.RowId, name, helpText, FilterCategory.Stats, null,
                row =>
                {
                    var hasAttribute = row.Base.BaseParam.IndexOf(a => a.Value.RowId == baseParamId);

                    if (hasAttribute == -1)
                    {
                        hasAttribute = row.Base.BaseParamSpecial.IndexOf(a => a.Value.RowId == baseParamId);
                        return row.Base.BaseParamValueSpecial[hasAttribute];
                        return null;
                    }

                    return row.Base.BaseParamValue[hasAttribute];
                });
            _availableFilters.Add(genericFilter);

        }
    }

    public List<IFilter> AvailableFilters => _availableFilters;

    private Dictionary<FilterCategory, List<IFilter>>? _groupedFilters;
    private List<IFilter> _availableFilters;

    public Dictionary<FilterCategory, List<IFilter>> GroupedFilters
    {
        get
        {
            if (_groupedFilters == null)
            {
                _groupedFilters = AvailableFilters.GroupBy(c => c.FilterCategory).OrderBy(c => FilterCategoryOrder.IndexOf(c.Key)).ToDictionary(c => c.Key, c => c.OrderBy(c => c.Order).ThenBy(d => d.Name).ToList());
            }

            return _groupedFilters;
        }
    }
}