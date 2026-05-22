using System;
using InventoryTools.Logic.Filters;

namespace InventoryTools.Extensions;

public static class FilterCategoryExtensions
{
    public static string FormattedName(this FilterCategory filterCategory)
    {
        return filterCategory switch
        {
            FilterCategory.Basic => "基础",
            FilterCategory.Acquisition => "获取",
            FilterCategory.Crafting => "制作",
            FilterCategory.Gathering => "采集",
            FilterCategory.Searching => "搜索",
            FilterCategory.Market => "市场",
            FilterCategory.Display => "显示",
            FilterCategory.Inventories => "库存",
            FilterCategory.Columns => "列",
            FilterCategory.Advanced => "高级",
            FilterCategory.CraftColumns => "制作列",
            FilterCategory.IngredientSourcing => "材料来源",
            FilterCategory.ZonePreference => "区域偏好",
            FilterCategory.WorldPricePreference => "服务器价格偏好",
            FilterCategory.Sources => "来源",
            FilterCategory.Uses => "用途",
            FilterCategory.SourceCategories => "来源（分类）",
            FilterCategory.UseCategories => "用途（分类）",
            FilterCategory.Settings => "设置",
            FilterCategory.Stats => "统计",
            FilterCategory.CompletionTracking => "完成追踪",
            FilterCategory.ItemIngredientOverrides => "材料覆盖",
            _ => filterCategory.ToString().ToSentence()
        };
    }
}