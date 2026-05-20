using System;
using InventoryTools.Logic.Settings.Abstract;

namespace InventoryTools.Extensions
{
    public static class SettingSubCategoryExtensions
    {
        public static string FormattedName(this SettingSubCategory settingSubCategory)
        {
            switch (settingSubCategory)
            {
                case SettingSubCategory.Experimental:
                    return "实验性";
                case SettingSubCategory.Fun:
                    return "趣味";
                case SettingSubCategory.Highlighting:
                    return "高亮";
                case SettingSubCategory.DestinationHighlighting:
                    return "目标高亮";
                case SettingSubCategory.RetainerHighlighting:
                    return "雇员高亮";
                case SettingSubCategory.Market:
                    return "市场";
                case SettingSubCategory.General:
                    return "通用";
                case SettingSubCategory.Subsetting:
                    return "设置";
                case SettingSubCategory.Visuals:
                    return "外观";
                case SettingSubCategory.WindowLayout:
                    return "窗口布局";
                case SettingSubCategory.AutoSave:
                    return "自动保存";
                case SettingSubCategory.FilterSettings:
                    return "列表设置";
                case SettingSubCategory.ActiveLists:
                    return "活动列表";
                case SettingSubCategory.ContextMenus:
                    return "右键菜单";
                case SettingSubCategory.Hotkeys:
                    return "快捷键";
                case SettingSubCategory.IgnoreEscape:
                    return "忽略ESC键";
                case SettingSubCategory.SourceGrouping:
                    return "来源分组";
                case SettingSubCategory.UseGrouping:
                    return "用途分组";
                case SettingSubCategory.Colours:
                    return "颜色";
                case SettingSubCategory.AddItemLocations:
                    return "添加物品位置";
                case SettingSubCategory.MarketPricing:
                    return "市场定价";
                case SettingSubCategory.AmountToRetrieve:
                    return "取出数量";
                case SettingSubCategory.ItemUnlockStatus:
                    return "物品解锁状态";
                case SettingSubCategory.SourceInformation:
                    return "来源信息";
                case SettingSubCategory.UseInformation:
                    return "用途信息";
                case SettingSubCategory.AcquisitionTracker:
                    return "获取追踪";
                case SettingSubCategory.IngredientPatch:
                    return "材料版本";
                case SettingSubCategory.GlamourReadySet:
                    return "外观幻化";
                case SettingSubCategory.CofferLoot:
                    return "宝箱战利品";
                case SettingSubCategory.ShopHighlighting:
                    return "商店高亮";
            }
            return settingSubCategory.ToString();
        }
    }
}