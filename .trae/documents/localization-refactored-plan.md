# 汉化重构方案 - 基于全量扫描

> 基于 2026-05-21 全量扫描结果，按 3-4 文件/批次逐步汉化，每批次编译验证

---

## 当前状态

- **Batch 2 (Services)** 已完成并推送：ContextMenuService, ImGuiMenuService, PopupService, ItemInfoRenderer, ImGuiService, MigrationManagerService
- **其余所有批次**因 `git rebase --abort` 丢失，需要重新执行
- 扫描发现约 **600+ 处**硬编码英文待汉化

---

## 批次规划

### 批次 A01: Columns 核心 (4 文件)
- `Logic/Columns/NameColumn.cs` - RenderName: "Name", HelpText: "The name of the item."
- `Logic/Columns/NameIconColumn.cs` - RenderName: "Name", HelpText
- `Logic/Columns/QuantityColumn.cs` - RenderName: "Quantity", HelpText
- `Logic/Columns/QuantityAvailableColumn.cs` - RenderName: "Available", HelpText

### 批次 A02: Columns 市场 (4 文件)
- `Logic/Columns/MarketBoardMinPriceColumn.cs`
- `Logic/Columns/MarketBoardMinPriceHQColumn.cs`
- `Logic/Columns/MarketboardMinPriceNQColumn.cs`
- `Logic/Columns/MarketBoardMinTotalPriceColumn.cs`

### 批次 A03: Columns 市场2 (4 文件)
- `Logic/Columns/MarketBoardPriceColumn.cs`
- `Logic/Columns/MarketboardPriceHQColumn.cs`
- `Logic/Columns/MarketboardPriceNQColum.cs`
- `Logic/Columns/MarketBoardSevenDayCountColumn.cs`

### 批次 A04: Columns 市场3 + 其他 (4 文件)
- `Logic/Columns/MarketBoardTotalPriceColumn.cs`
- `Logic/Columns/RetainerMarketPriceColumn.cs`
- `Logic/Columns/SearchCategoryColumn.cs`
- `Logic/Columns/PatchColumn.cs`

### 批次 A05: Columns 布尔/状态 (4 文件)
- `Logic/Columns/AcquiredColumn.cs`
- `Logic/Columns/CanBeDesynthedColumn.cs`
- `Logic/Columns/CanBeDyedColumn.cs`
- `Logic/Columns/CanBeEquippedColumn.cs`

### 批次 A06: Columns 布尔/状态2 (4 文件)
- `Logic/Columns/CanBeGatheredColumn.cs`
- `Logic/Columns/CanBeHighQualityColumn.cs`
- `Logic/Columns/CanBePlacedOnMarketColumn.cs`
- `Logic/Columns/CanBePurchasedColumn.cs`

### 批次 A07: Columns 布尔/状态3 (4 文件)
- `Logic/Columns/CanBeTradedColumn.cs`
- `Logic/Columns/IsArmoireItemColum.cs`
- `Logic/Columns/IsCraftingItemColumn.cs`
- `Logic/Columns/IsCustomDeliveryItemColumn.cs`

### 批次 A08: Columns 布尔/状态4 (4 文件)
- `Logic/Columns/IsHousingItemColumn.cs`
- `Logic/Columns/EphemeralNodeColumn.cs`
- `Logic/Columns/HiddenNodeColumn.cs`
- `Logic/Columns/TimedNodeColumn.cs`

### 批次 A09: Columns 制作相关 (4 文件)
- `Logic/Columns/CraftAmountAvailableColumn.cs`
- `Logic/Columns/CraftAmountReadyColumn.cs`
- `Logic/Columns/CraftAmountCanCraftColumn.cs`
- `Logic/Columns/CraftAmountUnavailableColumn.cs`

### 批次 A10: Columns 制作/其他 (4 文件)
- `Logic/Columns/CraftAmountRequiredColumn.cs`
- `Logic/Columns/CraftSimpleColumn.cs`
- `Logic/Columns/CraftColumn.cs`
- `Logic/Columns/CraftMarketPriceColumn.cs`

### 批次 A11: Columns 属性/装备 (4 文件)
- `Logic/Columns/AttributeColumn.cs`
- `Logic/Columns/DelayColumn.cs`
- `Logic/Columns/MagicalDamageColumn.cs`
- `Logic/Columns/PhysicalDamageColumn.cs`

### 批次 A12: Columns 装备/其他 (4 文件)
- `Logic/Columns/EquippableColumn.cs`
- `Logic/Columns/EquippableByGenderColumn.cs`
- `Logic/Columns/EquippableByRaceColumn.cs`
- `Logic/Columns/GearSetColumn.cs`

### 批次 A13: Columns 历史/位置/其他 (4 文件)
- `Logic/Columns/HistoryChangeAmountColumn.cs`
- `Logic/Columns/HistoryChangeDateColumn.cs`
- `Logic/Columns/HistoryChangeReasonColumn.cs`
- `Logic/Columns/LocationColumn.cs`

### 批次 A14: Columns 剩余 (4 文件)
- `Logic/Columns/RecentlySeenColumn.cs`
- `Logic/Columns/RecipeUnlockedColumn.cs`
- `Logic/Columns/DesynthesisClassColumn.cs`
- `Logic/Columns/StoreColumn.cs`

### 批次 A15: Columns 按钮 + ColumnSettings (4 文件)
- `Logic/Columns/Buttons/RemoveButtonColumn.cs`
- `Logic/Columns/Buttons/GatherButtonColumn.cs`
- `Logic/Columns/Buttons/CraftGatherColumn.cs`
- `Logic/Columns/Buttons/CraftBuyColumn.cs`

### 批次 A16: Columns 按钮2 + ColumnSettings (4 文件)
- `Logic/Columns/Buttons/CraftButtonColumn.cs`
- `Logic/Columns/Buttons/CopyItemNameButtonColumn.cs`
- `Logic/Columns/ColumnSettings/AttributeColumnSetting.cs`
- `Logic/Columns/ColumnSettings/ButtonColumnSetting.cs`

### 批次 A17: ColumnSettings (4 文件)
- `Logic/Columns/ColumnSettings/CharacterColumnSetting.cs`
- `Logic/Columns/ColumnSettings/CharacterScopePickerColumnSetting.cs`
- `Logic/Columns/ColumnSettings/MarketboardWorldSetting.cs`
- `Logic/Columns/ColumnSettings/QualitySelectorSetting.cs`

### 批次 A18: ColumnSettings 剩余 + Columns 散落 (4 文件)
- `Logic/Columns/ColumnSettings/ScopePickerColumnSetting.cs`
- `Logic/Columns/ColumnSettings/SourceCategorySelectorSetting.cs`
- `Logic/Columns/ColumnSettings/SourceTypeSelectorSetting.cs`
- `Logic/Columns/ColumnSettings/StainColumnSetting.cs`

### 批次 A19: Columns 散落文件 (4 文件)
- `Logic/Columns/SourceColumn.cs`
- `Logic/Columns/SourceWorldColumn.cs`
- `Logic/Columns/SpiritbondColumn.cs`
- `Logic/Columns/StainColumn.cs`

### 批次 A20: Columns 最后一批 (4 文件)
- `Logic/Columns/TypeColumn.cs`
- `Logic/Columns/UiCategoryColumn.cs`
- `Logic/Columns/UptimeColumn.cs`
- `Logic/Columns/VentureTypeColumn.cs`

### 批次 A21: Columns 遗漏补充 (3 文件)
- `Logic/Columns/BuyFromVendorPriceColumn.cs`
- `Logic/Columns/SellToVendorPriceColumn.cs`
- `Logic/Columns/SellToVendorPriceTotalColumn.cs`

---

### 批次 B01: ItemRenderers 来源1 (4 文件)
- `Logic/ItemRenderers/ItemGatheringSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFishingSourceRenderer.cs`
- `Logic/ItemRenderers/ItemSpearFishingSourceRenderer.cs`
- `Logic/ItemRenderers/ItemMonsterDropSourceRenderer.cs`

### 批次 B02: ItemRenderers 来源2 (4 文件)
- `Logic/ItemRenderers/ItemVentureSourceRenderer.cs`
- `Logic/ItemRenderers/ItemQuickVentureSourceRenderer.cs`
- `Logic/ItemRenderers/ItemDungeonDropSourceRenderer.cs`
- `Logic/ItemRenderers/ItemDungeonChestSourceRenderer.cs`

### 批次 B03: ItemRenderers 来源3 (4 文件)
- `Logic/ItemRenderers/ItemDungeonBossDropSourceRenderer.cs`
- `Logic/ItemRenderers/ItemDungeonBossChestSourceRenderer.cs`
- `Logic/ItemRenderers/ItemAirshipDropSourceRenderer.cs`
- `Logic/ItemRenderers/ItemSubmarineDropSourceRenderer.cs`

### 批次 B04: ItemRenderers 商店/交付 (4 文件)
- `Logic/ItemRenderers/ItemGilShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemSpecialShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemGCShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemGCSupplyDutySourceRenderer.cs`

### 批次 B05: ItemRenderers 交付/商店 (4 文件)
- `Logic/ItemRenderers/ItemGCExpertDeliverySourceRenderer.cs`
- `Logic/ItemRenderers/ItemCustomDeliverySourceRenderer.cs`
- `Logic/ItemRenderers/ItemCollectablesShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCashShopSourceRenderer.cs`

### 批次 B06: ItemRenderers 制作 (4 文件)
- `Logic/ItemRenderers/ItemCraftResultSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCraftRequirementSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCraftLeveSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCraftLeveUseRenderer.cs`

### 批次 B07: ItemRenderers 部队/理符 (4 文件)
- `Logic/ItemRenderers/ItemCompanyCraftResultSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCompanyCraftRequirementSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCompanyCraftDraftSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCompanyLeveSourceRenderer.cs`

### 批次 B08: ItemRenderers 武器/工具 (4 文件)
- `Logic/ItemRenderers/ItemRelicWeaponSourceRenderer.cs`
- `Logic/ItemRenderers/ItemToolWeaponSourceRenderer.cs`
- `Logic/ItemRenderers/ItemSkybuilderInspectionSourceRenderer.cs`
- `Logic/ItemRenderers/ItemSkybuilderHandInSourceRenderer.cs`

### 批次 B09: ItemRenderers 房屋/家具/外观 (4 文件)
- `Logic/ItemRenderers/ItemHouseSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFurnitureSourceRenderer.cs`
- `Logic/ItemRenderers/ItemExteriorFurnitureSourceRenderer.cs`
- `Logic/ItemRenderers/ItemGlamourReadySourceRenderer.cs`

### 批次 B10: ItemRenderers 外观/染色/其他 (4 文件)
- `Logic/ItemRenderers/ItemGlamourReadySetSourceRenderer.cs`
- `Logic/ItemRenderers/ItemStainUseRenderer.cs`
- `Logic/ItemRenderers/ItemGearsetUseRenderer.cs`
- `Logic/ItemRenderers/ItemArmoireSourceRenderer.cs`

### 批次 B11: ItemRenderers 任务/成就/其他 (4 文件)
- `Logic/ItemRenderers/ItemQuestSourceRenderer.cs`
- `Logic/ItemRenderers/ItemAchievementSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFateSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFateShopSourceRenderer.cs`

### 批次 B12: ItemRenderers 补充/杂项 (4 文件)
- `Logic/ItemRenderers/ItemSupplementSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFieldOpCofferSourceRenderer.cs`
- `Logic/ItemRenderers/ItemTripleTriadSourceRenderer.cs`
- `Logic/ItemRenderers/ItemCalamitySalvagerShopSourceRenderer.cs`

### 批次 B13: ItemRenderers 杂项2 (4 文件)
- `Logic/ItemRenderers/ItemBuddySourceRenderer.cs`
- `Logic/ItemRenderers/ItemAquariumUseRenderer.cs`
- `Logic/ItemRenderers/ItemAnimaShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemBattleLeveSourceRenderer.cs`

### 批次 B14: ItemRenderers 杂项3 (4 文件)
- `Logic/ItemRenderers/ItemGatheringLeveSourceRenderer.cs`
- `Logic/ItemRenderers/ItemGardeningCrossbreedSourceRenderer.cs`
- `Logic/ItemRenderers/ItemFccShopSourceRenderer.cs`
- `Logic/ItemRenderers/ItemPvpSeriesSourceRenderer.cs`

---

### 批次 C01: Compendium Types 1 (4 文件)
- `Compendium/Types/QuestCompendiumType.cs`
- `Compendium/Types/MountCompendiumType.cs`
- `Compendium/Types/MinionCompendiumType.cs`
- `Compendium/Types/ItemCompendiumType.cs`

### 批次 C02: Compendium Types 2 (4 文件)
- `Compendium/Types/AchievementCompendiumType.cs`
- `Compendium/Types/InstanceContentCompendiumType.cs`
- `Compendium/Types/TerritoryTypeCompendiumType.cs`
- `Compendium/Types/SubmarineRoutesCompendiumType.cs`

### 批次 C03: Compendium Types 3 (4 文件)
- `Compendium/Types/SharedModelCompendiumType.cs`
- `Compendium/Types/SatisfactionNpcCompendiumType.cs`
- `Compendium/Types/RelicWeaponCompendiumType.cs`
- `Compendium/Types/RelicToolCompendiumType.cs`

### 批次 C04: Compendium Types 4 (4 文件)
- `Compendium/Types/RacingChocoboCompendiumType.cs`
- `Compendium/Types/LeveCompendiumType.cs`
- `Compendium/Types/GearsetCompendiumType.cs`
- `Compendium/Types/ENpcCompendiumType.cs`

### 批次 C05: Compendium Types 5 (4 文件)
- `Compendium/Types/ClassJobCompendiumType.cs`
- `Compendium/Types/ChocoboItemCompendiumType.cs`
- `Compendium/Types/BeastTribeCompendiumType.cs`
- `Compendium/Types/BGMCompendiumType.cs`

### 批次 C06: Compendium Types 6 (2 文件)
- `Compendium/Types/AirshipRoutesCompendium.cs`
- `Compendium/Types/SourceCompendiumType.cs` (如有)

---

### 批次 D01: Features (4 文件)
- `Logic/Features/BasicFeature.cs`
- `Logic/Features/ContextMenuFeature.cs`
- `Logic/Features/FiltersFeature.cs`
- `Logic/Features/HotkeysFeature.cs`

### 批次 D02: Features2 (3 文件)
- `Logic/Features/LayoutFeature.cs`
- `Logic/Features/MarketboardIntegrationFeature.cs`
- `Logic/Features/TooltipsFeature.cs`

---

### 批次 E01: Settings 1 (4 文件)
- `Logic/Settings/WindowFilterSetting.cs`
- `Logic/Settings/UseIconGroupingSetting.cs`
- `Logic/Settings/UseOldCraftTrackerSetting.cs`
- `Logic/Settings/TrackMobSpawnSetting.cs`

### 批次 E02: Settings 2 (4 文件)
- `Logic/Settings/TooltipUseInformationSetting.cs`
- `Logic/Settings/TooltipSourceInformationSetting.cs`
- `Logic/Settings/TooltipMinimumMarketPriceSetting.cs`
- `Logic/Settings/WindowIgnoreEscapeSetting.cs`

### 批次 E03: Settings 3 (2 文件)
- `Logic/Settings/Abstract/GameColorSetting.cs`
- `Logic/Settings/Abstract/HotKeySetting.cs`

---

### 批次 F01: Windows 1 (4 文件)
- `Ui/Windows/TeamCraftImportWindow.cs`
- `Ui/Windows/SourceWindow.cs`
- `Ui/Windows/ListDebugWindow.cs`
- `Ui/Windows/ItemWindow.cs`

### 批次 F02: Windows 2 (3 文件)
- `Ui/Windows/FiltersWindow.cs`
- `Ui/Windows/ChangelogWindow.cs`
- `Ui/Windows/ConfigurationWindow.cs`

---

### 批次 G01: Pages (3 文件)
- `Ui/Pages/ListsPage.cs`
- `Ui/Pages/FilterPage.cs`
- `Ui/Pages/CharacterRetainerPage.cs`

---

### 批次 H01: Tooltips + Widgets + Filters + Misc (4 文件)
- `Logic/Tooltips/LocationDisplayTooltip.cs`
- `Logic/Tooltips/GlamourReadySetTooltip.cs`
- `Logic/Tooltips/DisplayUnlockTooltip.cs`
- `Logic/Tooltips/DisplayMarketPriceTooltip.cs`

### 批次 H02: Tooltips2 + Widgets + Misc (4 文件)
- `Logic/Tooltips/AmountOwnedTooltip.cs`
- `Ui/Widgets/Utils.cs`
- `Ui/Widgets/PopupMenu.cs`
- `Logic/Filters/MarketBoardSaleCountFilter.cs`

### 批次 H03: Misc 杂项 (4 文件)
- `Logic/Filters/ZonePreferenceFilter.cs`
- `Logic/Filters/CraftWorldPricePreferenceFilter.cs`
- `Logic/Filters/CraftIngredientPreferenceFilter.cs`
- `PluginLogic.cs`

### 批次 H04: Misc 杂项2 (2 文件)
- `Logic/FilterTable.cs`
- `ListService.cs`

---

### 批次 I01: Services 补充 (1 文件)
- `Services/ImGuiTooltipService.cs`

---

## 执行策略

1. 每批次 3-4 个文件，使用 Task agent 并行汉化
2. 每批次完成后 `dotnet build InventoryTools/InventoryTools.csproj --no-restore` 编译验证
3. 编译通过后 `git add` + `git commit` 提交
4. 每 5 个批次推送一次 `git push`
5. 遇到编译错误立即修复，不继续下一批次

## 预估总批次数

- Columns (A01-A21): 21 批次
- ItemRenderers (B01-B14): 14 批次
- Compendium (C01-C06): 6 批次
- Features (D01-D02): 2 批次
- Settings (E01-E03): 3 批次
- Windows (F01-F02): 2 批次
- Pages (G01): 1 批次
- Tooltips/Widgets/Misc (H01-H04): 4 批次
- Services 补充 (I01): 1 批次

**总计约 54 批次**

## 注意事项

- 品牌名不翻译：Allagan Tools, Garland Tools, Teamcraft, Universalis, Gamer Escape, Console Games Wiki, Gatherbuddy
- 游戏专有名词参考国服翻译：理符、军票、蛮族、武魂、魂武等
- 中文括号使用全角（）而非半角()
- 每个文件先 Read 确认当前内容，再精确 Edit
- 同一字符串多处出现时使用 replace_all: true
