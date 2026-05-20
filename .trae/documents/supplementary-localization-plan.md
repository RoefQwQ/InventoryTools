# 补充汉化方案 - 基于二次扫描

> 基于 2026-05-21 二次扫描结果，补充遗漏的未汉化内容
> 按 3-4 文件/批次逐步汉化，每批次编译验证并清理临时文件

---

## 批次 S01: Compendium Types - 航线/蛮族/BGM (4 文件)

- `Compendium/Types/AirshipRoutesCompendium.cs` - Name/HelpText/SectionName 全部英文
- `Compendium/Types/SubmarineRoutesCompendiumType.cs` - Name/HelpText/SectionName 全部英文
- `Compendium/Types/BeastTribeCompendiumType.cs` - Name/HelpText/SectionName 全部英文
- `Compendium/Types/BGMCompendiumType.cs` - Name/HelpText 全部英文

## 批次 S02: Compendium Types - 陆行鸟/职业/NPC/成就 (4 文件)

- `Compendium/Types/ChocoboItemCompendiumType.cs` - Name/HelpText 全部英文
- `Compendium/Types/ClassJobCompendiumType.cs` - Name/HelpText/SectionName 全部英文
- `Compendium/Types/ENpcCompendiumType.cs` - Name/HelpText/SectionName 全部英文
- `Compendium/Types/AchievementCompendiumType.cs` - Name/HelpText 全部英文

## 批次 S03: Compendium Types - 套装/竞速/老主顾/共享模型 (4 文件)

- `Compendium/Types/GearsetCompendiumType.cs` - Name/HelpText 全部英文
- `Compendium/Types/RacingChocoboCompendiumType.cs` - Name/HelpText 全部英文
- `Compendium/Types/SatisfactionNpcCompendiumType.cs` - Name/HelpText 部分英文
- `Compendium/Types/SharedModelCompendiumType.cs` - Name/HelpText 部分英文

## 批次 S04: Compendium Types - 古武/副本/理符 (3 文件)

- `Compendium/Types/RelicWeaponCompendiumType.cs` - Name/HelpText/SectionName 部分英文
- `Compendium/Types/RelicToolCompendiumType.cs` - Name/HelpText/SectionName 部分英文
- `Compendium/Types/InstanceContentCompendiumType.cs` - Name/HelpText 部分英文

## 批次 S05: Compendium Types - 理符/区域 (2 文件)

- `Compendium/Types/LeveCompendiumType.cs` - Name/HelpText/SectionName 部分英文
- `Compendium/Types/TerritoryTypeCompendiumType.cs` - Name/HelpText 部分英文

## 批次 S06: Compendium Windows + EquipmentSuggest (4 文件)

- `Compendium/Windows/CompendiumViewWindow.cs` - 菜单项/提示文本英文
- `Compendium/Windows/CompendiumTypesWindow.cs` - 搜索提示/GenericName 英文
- `Compendium/Windows/CompendiumMapFeaturesWindow.cs` - 提示文本/GenericName 英文
- `EquipmentSuggest/EquipmentSuggestSelectedItemColumn.cs` - "Add to Craft List" 等英文

## 批次 S07: Compendium Windows 续 + Settings 1 (4 文件)

- `Compendium/Windows/CompendiumListWindow.cs` - 菜单项/提示文本英文
- `Logic/Settings/ContextMenuMoreInformationSetting.cs` - WizardName = "More Information"
- `Logic/Settings/ContextMenuAddToCraftListSetting.cs` - WizardName = "Add to Craft List"
- `Logic/Settings/ContextMenuAddToActiveCraftListSetting.cs` - WizardName = "Add to Active Craft List"

## 批次 S08: Settings 2 (4 文件)

- `Logic/Settings/ContextMenuAddToCuratedListSetting.cs` - WizardName = "Add to Curated List"
- `Logic/Settings/ContextMenuOpenCraftLogSetting.cs` - WizardName = "Open Crafting Log"
- `Logic/Settings/ContextMenuOpenGatheringLogSetting.cs` - WizardName = "Open Gathering Log"
- `Logic/Settings/ContextMenuOpenFishingLogSetting.cs` - WizardName = "Open Fishing Log"

## 批次 S09: Settings 3 (4 文件)

- `Logic/Settings/ContextMenuItemSearchSetting.cs` - WizardName = "Search"
- `Logic/Settings/ContextMenuAddToFavouritesSetting.cs` - WizardName = "Add/Remove to Favourites"
- `Logic/Settings/ContextMenuItemSearchScopeSetting.cs` - WizardName = "Search Scope"
- `Logic/Settings/TooltipSourceInformationSetting.cs` - WizardName = "Show Source Information"

## 批次 S10: Settings 4 (4 文件)

- `Logic/Settings/TooltipUseInformationSetting.cs` - WizardName = "Show Use Information"
- `Logic/Settings/TooltipLocationDisplayModeSetting.cs` - WizardName = "Display Mode"
- `Logic/Settings/TooltipGlamourReadySetDisplayModeSetting.cs` - WizardName = "Display Mode"
- `Logic/Settings/TooltipDisplayRetrieveAmountSetting.cs` - WizardName = "Amount to Retrieve"

## 批次 S11: Settings 5 (4 文件)

- `Logic/Settings/TooltipDisplayGlamourReadySetSetting.cs` - WizardName = "Outfit Glamour Info"
- `Logic/Settings/TooltipDisplayAmountOwnedSetting.cs` - WizardName = "Add Item Locations"
- `Logic/Settings/TooltipDisplayCofferLootSetting.cs` - WizardName = "Coffer Loot Info"
- `Logic/Settings/MarketRefreshTimeHoursSetting.cs` - WizardName = "Persist for X hours"

## 批次 S12: Settings 6 + 其他 (4 文件)

- `Logic/Settings/MarketBoardSaleCountLimitSetting.cs` - WizardName = "Sale History Limit"
- `Logic/Settings/HistoryEnabledSetting.cs` - WizardName = "Track Item History?"
- `Logic/Settings/FiltersWindowLayoutSetting.cs` - WizardName = "Items Window"
- `Logic/Settings/CraftWindowLayoutSetting.cs` - WizardName = "Craft Window"

## 批次 S13: Settings 7 + ContextMenu + AutoSave (4 文件)

- `Logic/Settings/AutomaticallyDownloadPricesSetting.cs` - WizardName = "Download Pricing Data"
- `Logic/Settings/AutoSaveSetting.cs` - WizardName = "Auto save inventories?"
- `Logic/Settings/AllowCrossCharacterSetting.cs` - WizardName = "Cross-Character Inventories?"
- `Services/ContextMenuService.cs` - "More Information" (3处)

## 批次 S14: Market Columns + Filters + N/A (4 文件)

- `Logic/Columns/MarketBoardPriceColumn.cs` - "loading...", "untradable", "No data available"
- `Logic/Columns/MarketboardPriceHQColumn.cs` + `MarketboardPriceNQColum.cs` + `MarketBoardMinPriceHQColumn.cs` + `MarketboardMinPriceNQColumn.cs` + `MarketBoardSevenDayCountColumn.cs` - "loading...", "untradable"
- `Logic/Filters/CanBeTradedFilter.cs` + `CanBePlacedOnMarketFilter.cs` - HelpText 英文
- `Logic/Columns/Abstract/TextColumn.cs` 等 - "N/A" (9处)

## 批次 S15: GenericName + Debuggers + CompendiumListWindow (3 文件)

- `EquipmentSuggest/EquipmentSuggestWindow.cs` - GenericName = "Equipment Recommendations"
- `Compendium/Windows/CompendiumTypesWindow.cs` - GenericName = "Compendium"
- `Compendium/Windows/CompendiumMapFeaturesWindow.cs` - GenericName = "Territory Compendium"

---

## 执行策略

1. 每批次 3-4 个文件，使用 Task agent 并行汉化
2. 每批次完成后 `dotnet build InventoryTools/InventoryTools.csproj` 编译验证
3. 编译通过后清理临时文件：`dotnet clean InventoryTools/InventoryTools.csproj`
4. 遇到编译错误立即修复，不继续下一批次
5. 每 5 个批次 `git add` + `git commit` 提交一次

## 注意事项

- 品牌名不翻译：Allagan Tools, Garland Tools, Teamcraft, Universalis, Gamer Escape, Console Games Wiki, Gatherbuddy
- 游戏专有名词参考国服翻译
- 中文括号使用全角（）而非半角()
- Settings 的 WizardName 默认值是 fallback，运行时会被 localizationService 覆盖，但仍需汉化默认值
- "N/A" 统一翻译为 "无"
- "loading..." 翻译为 "加载中..."
- "untradable" 翻译为 "不可交易"
