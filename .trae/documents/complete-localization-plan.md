# 完整本地化实施计划

## 目标
将 InventoryTools 插件中所有 Filter 和 Column 的 `Name` 属性汉化，提供一致的中文用户体验。

## 现状统计
- **Filters**: 100 个文件，已完成 39 个，剩余 **61 个**
- **Columns**: 100 个文件，已完成 48 个，剩余 **52 个**
- **总计**: 200 个文件，已完成 87 个，剩余 **113 个**

## 实施策略
采用"小步快跑"策略，每批任务控制在 **5-7 个文件**，确保可管理、可验证、可回滚。

---

## 阶段一：Filters 本地化（61 个文件）

### 批次 1：制作追踪筛选器（7 个文件）
**任务编号**: F-01
**预计耗时**: 10 分钟
**文件列表**:
1. `CraftTrackerTrackCraftsFilter.cs` - "Track Crafts?" → "追踪制作？"
2. `CraftTrackerTrackGatheringFilter.cs` - "Track Gathering?" → "追踪采集？"
3. `CraftTrackerTrackMarketBoardFilter.cs` - "Track Market Board?" → "追踪市场板？"
4. `CraftTrackerTrackShoppingFilter.cs` - "Track Shopping?" → "追踪购物？"
5. `CraftTrackerTrackOtherFilter.cs` - "Track Other?" → "追踪其他？"
6. `CraftTrackerTrackCombatDropFilter.cs` - "Track Combat Drops?" → "追踪战斗掉落？"
7. `CraftStagingAreaFilter.cs` - "Staging Area" → "暂存区"

**验证方式**: 编译检查 + 运行时查看制作列表筛选器面板

---

### 批次 2：制作背包与分组筛选器（7 个文件）
**任务编号**: F-02
**预计耗时**: 10 分钟
**文件列表**:
1. `CraftSourceInventoriesFilter.cs` - "Inventories to Retrieve From" → "来源背包"
2. `CraftDestinationInventoriesFilter.cs` - "Inventories to Retrieve To" → "目标背包"
3. `CraftRetrieveGroupFilter.cs` - "Group Retrieval Items By" → "按检索物品分组"
4. `CraftPrecraftGroupFilter.cs` - "Group Precrafts By" → "按预制作分组"
5. `CraftCrystalGroupFilter.cs` - "Group Crystals By" → "按水晶分组"
6. `CraftCurrencyGroupFilter.cs` - "Group Currency By" → "按货币分组"
7. `CraftEverythingElseGroupFilter.cs` - "Group Everything Else By" → "按其他物品分组"

**验证方式**: 编译检查 + 运行时查看制作列表分组选项

---

### 批次 3：制作设置与排序筛选器（7 个文件）
**任务编号**: F-03
**预计耗时**: 10 分钟
**文件列表**:
1. `CraftReverseListDisplayFilter.cs` - "Reverse Craft List Order?" → "反转制作列表顺序？"
2. `CraftOutputOrderingFilter.cs` - "Output Ordering" → "输出排序"
3. `CraftItemIngredientOverridesFilter.cs` - "Per-Item Ingredient Source Overrides" → "逐物品材料来源覆盖"
4. `CraftIngredientPreferenceFilter.cs` - "Default Ingredient Sourcing" → "默认材料来源"
5. `CraftHouseVendorFilter.cs` - "Group House Vendors By" → "按房屋商人分组"
6. `CraftDefaultRetrieveFromRetainerFilter.cs` - "Retainer Retrieval" → "雇员取回"
7. `CraftDefaultRetrieveFromRetainerOutputFilter.cs` - "Retainer Retrieval (Output)" → "雇员取回（输出）"

**验证方式**: 编译检查 + 运行时查看制作设置面板

---

### 批次 4：制作显示与价格筛选器（8 个文件）
**任务编号**: F-04
**预计耗时**: 12 分钟
**文件列表**:
1. `CraftDefaultHQRequiredFilter.cs` - "HQ Required" → "需要高品质"
2. `CraftCraftCompletionModeFilter.cs` - "Craft Completion Mode" → "制作完成模式"
3. `CraftWorldPriceUseHomeWorldFilter.cs` - "Use Home World?" → "使用家乡服务器？"
4. `CraftWorldPriceUseDefaultsFilter.cs` - "Use Default Worlds?" → "使用默认服务器？"
5. `CraftWorldPriceUseActiveWorldFilter.cs` - "Use Active World?" → "使用当前服务器？"
6. `CraftWorldPricePreferenceFilter.cs` - "World Price Preference" → "服务器价格偏好"
7. `CraftListModeFilter.cs` - "Craft List Mode" → "制作列表模式"
8. `CraftDisplayModeFilter.cs` - "Craft Display Mode" → "制作显示模式"

**验证方式**: 编译检查 + 运行时查看制作价格设置

---

### 批次 5：制作列与杂项筛选器（7 个文件）
**任务编号**: F-05
**预计耗时**: 10 分钟
**文件列表**:
1. `CraftColumnsFilter.cs` - "Craft Columns" → "制作列"
2. `CraftHeaderColourFilter.cs` - "Header Text Colour" → "标题文字颜色"
3. `CraftIsEphemeralFilter.cs` - "Ephemeral?" → "临时物品？"
4. `ColumnsFilter.cs` - "Columns" → "列"
5. `IngredientSearchFilter.cs` - "Ingredient Search Filter" → "材料搜索筛选"
6. `RecipeTotalFilter.cs` - "Recipe Total Count" → "配方总数"
7. `IngredientPatchSearchFilter.cs` - 无 Name 属性，跳过

**验证方式**: 编译检查 + 运行时查看列选择器

---

### 批次 6：属性统计筛选器（7 个文件）
**任务编号**: F-06
**预计耗时**: 10 分钟
**文件列表**:
1. `Stats/ItemLevelFilter.cs` - "iLevel" → "物品等级"
2. `Stats/PhysicalDamageFilter.cs` - "Physical Damage" → "物理伤害"
3. `Stats/MagicalDamageFilter.cs` - "Magical Damage" → "魔法伤害"
4. `Stats/MateriaCountFilter.cs` - "Materia Count" → "魔晶石数量"
5. `Stats/DyeCountFilter.cs` - "Dye Count" → "染色数量"
6. `Stats/DelayFilter.cs` - "Delay" → "延迟"
7. `Stats/RequiredLevelFilter.cs` - "Required Level" → "需求等级"

**验证方式**: 编译检查 + 运行时查看装备筛选器

---

### 批次 7：高亮筛选器（5 个文件）
**任务编号**: F-07
**预计耗时**: 8 分钟
**文件列表**:
1. `HighlightWhenFilter.cs` - "Highlight When?" → "何时高亮？"
2. `HighlightDestinationFilter.cs` - "Highlight Destination Duplicates?" → "高亮目标重复？"
3. `HighlightDestinationColourFilter.cs` - "Highlight Destination Color" → "目标高亮颜色"
4. `HighlightColorFilter.cs` - "Highlight Color" → "高亮颜色"
5. `InvertHighlightingFilter.cs` - "Invert Highlighting?" → "反转高亮？"

**验证方式**: 编译检查 + 运行时查看高亮设置

---

### 批次 8：标签与雇员外观筛选器（5 个文件）
**任务编号**: F-08
**预计耗时**: 8 分钟
**文件列表**:
1. `TabHighlightColorFilter.cs` - "Tab Highlight Color" → "标签高亮颜色"
2. `InvertTabHighlightingFilter.cs` - "Invert Tab Highlighting?" → "反转标签高亮？"
3. `RetainerListColorFilter.cs` - "Retainer List Color" → "雇员列表颜色"
4. `RetainerRetrieveOrderFilter.cs` - "Retainer Retrieve Order" → "雇员取回顺序"
5. `TableRowHeightFilter.cs` - "Table Row Height" → "表格行高"

**验证方式**: 编译检查 + 运行时查看标签和雇员设置

---

### 批次 9：表格与排序筛选器（7 个文件）
**任务编号**: F-09
**预计耗时**: 10 分钟
**文件列表**:
1. `TableFreezeColumnsFilter.cs` - "Freeze Columns" → "冻结列"
2. `TableCraftFreezeColumnsFilter.cs` - "Freeze Columns" → "冻结列（制作）"
3. `DefaultSortColumnFilter.cs` - "Default Sort Column" → "默认排序列"
4. `DefaultSortColumnDirectionFilter.cs` - "Default Sort Column Order" → "默认排序方向"
5. `DisplayFilterInRetainersFilter.cs` - "Filter Items when in Retainer?" → "在雇员中筛选物品？"
6. `FilterORFilter.cs` - "Use OR when filtering items." → "筛选时使用OR"
7. `IgnoreHQFilter.cs` - "Ignore HQ Filter?" → "忽略高品质筛选？"

**验证方式**: 编译检查 + 运行时查看表格设置

---

### 批次 10：其他筛选器（2 个文件）
**任务编号**: F-10
**预计耗时**: 5 分钟
**文件列表**:
1. `ZonePreferenceFilter.cs` - "Default Zone Order" → "默认区域顺序"
2. `VentureTypeFilter.cs` - "Venture Type" → "探险类型"

**验证方式**: 编译检查 + 运行时查看区域和探险设置

---

## 阶段二：Columns 本地化（52 个文件）

### 批次 11：制作数量列（5 个文件）
**任务编号**: C-01
**预计耗时**: 8 分钟
**文件列表**:
1. `CraftAmountAvailableColumn.cs` - "Amount to Retrieve" → "可取回数量"
2. `CraftAmountCanCraftColumn.cs` - "Amount can Craft" → "可制作数量"
3. `CraftAmountReadyColumn.cs` - "Amount in Character Inventory" → "角色背包数量"
4. `CraftAmountRequiredColumn.cs` - "Amount Required" → "需求数量"
5. `CraftAmountUnavailableColumn.cs` - "Amount Missing" → "缺少数量"

**验证方式**: 编译检查 + 运行时查看制作列表列标题

---

### 批次 12：制作信息列（5 个文件）
**任务编号**: C-02
**预计耗时**: 8 分钟
**文件列表**:
1. `CraftCalculatorColumn.cs` - "Craft Calculator" → "制作计算器"
2. `CraftColumn.cs` - "Is Craftable?" → "是否可制作？"
3. `CraftMarketPriceColumn.cs` - "Market Pricing" → "市场价格"
4. `CraftSimpleColumn.cs` - "Next Step in Craft" → "制作下一步"
5. `CraftZoneColumn.cs` - "Zone" → "区域"

**验证方式**: 编译检查 + 运行时查看制作列表列标题

---

### 批次 13：调试与设置列（3 个文件）
**任务编号**: C-03
**预计耗时**: 5 分钟
**文件列表**:
1. `DebugCraftColumn.cs` - "Debug - Craft" → "调试 - 制作"
2. `DebugColumn.cs` - "Debug - General Information" → "调试 - 常规信息"
3. `ColumnSettings/UseTypeSelectorSetting.cs` - "Types" → "类型"

**验证方式**: 编译检查 + 运行时查看调试列

---

### 批次 14：市场板列（5 个文件）
**任务编号**: C-04
**预计耗时**: 8 分钟
**文件列表**:
1. `MarketboardPriceHQColumn.cs` - "Market Board Average Price HQ" → "市场板平均价格（高品质）"
2. `MarketboardPriceNQColum.cs` - "Market Board Average Price NQ" → "市场板平均价格（普通）"
3. `MarketboardMinPriceNQColumn.cs` - "Market Board Minimum Price NQ" → "市场板最低价格（普通）"
4. `MarketBoardMinTotalPriceColumn.cs` - "Market Board Minimum Total Price(Qty * Price) NQ/HQ" → "市场板最低总价（数量×价格）普通/高品质"
5. `RetainerMarketPriceColumn.cs` - "Retainer Selling Unit Price" → "雇员出售单价"

**验证方式**: 编译检查 + 运行时查看市场板列

---

### 批次 15：采集与位置列（6 个文件）
**任务编号**: C-05
**预计耗时**: 10 分钟
**文件列表**:
1. `TimedNodeColumn.cs` - "Is From Timed Node?" → "是否来自限时采集点？"
2. `HiddenNodeColumn.cs` - "Is From Hidden Node?" → "是否来自隐藏采集点？"
3. `EphemeralNodeColumn.cs` - "Is From Ephemeral Node?" → "是否来自临时采集点？"
4. `UptimeColumn.cs` - "Next Gather Uptime" → "下次采集时间"
5. `HasBeenGatheredColumn.cs` - "Logged in Gathering Log?" → "已记录于采集笔记？"
6. `LocationColumn.cs` - "Location" → "背包位置"

**验证方式**: 编译检查 + 运行时查看采集列

---

### 批次 16：特殊物品列（6 个文件）
**任务编号**: C-06
**预计耗时**: 10 分钟
**文件列表**:
1. `IsRecipeCompletedColumn.cs` - "Are Recipes Completed?" → "配方是否已完成？"
2. `IsGlamourReadySetItemColumn.cs` - "Is Outfit Glamour Item?" → "是否幻化套装物品？"
3. `IsGCSupplyItemColumn.cs` - "Is GC Turn-in item?" → "是否军队筹备物品？"
4. `IsCustomDeliveryItemColumn.cs` - "Is custom delivery item?" → "是否老主顾交付物品？"
5. `FromCalamitySalvagerColumn.cs` - "Is from Calamity Salvager?" → "是否来自灾害 Salvager？"
6. `FavouritesColumn.cs` - "Favourite?" → "是否收藏？"

**验证方式**: 编译检查 + 运行时查看特殊物品列

---

### 批次 17：历史与搜索列（4 个文件）
**任务编号**: C-07
**预计耗时**: 6 分钟
**文件列表**:
1. `HistoryChangeReasonColumn.cs` - "History Event Reason" → "历史事件原因"
2. `HistoryChangeDateColumn.cs` - "History Event Date/Time" → "历史事件日期/时间"
3. `HistoryChangeAmountColumn.cs` - "History Event Amount" → "历史事件数量"
4. `IngredientPatchSearchColumn.cs` - "Ingredient Patch Search" → "材料版本搜索"

**验证方式**: 编译检查 + 运行时查看历史列

---

### 批次 18：属性统计列（8 个文件）
**任务编号**: C-08
**预计耗时**: 12 分钟
**文件列表**:
1. `Stats/ItemILevelColumn.cs` - "iLevel" → "物品等级"
2. `Stats/ItemLevelColumn.cs` - "Required Level" → "需求等级"
3. `Stats/PhysicalDamageColumn.cs` - "Physical Damage" → "物理伤害"
4. `Stats/MagicalDamageColumn.cs` - "Magical Damage" → "魔法伤害"
5. `Stats/MateriaCountColumn.cs` - "Materia Count" → "魔晶石数量"
6. `Stats/DyeCountColumn.cs` - "Dye Count" → "染色数量"
7. `Stats/DelayColumn.cs` - "Delay" → "延迟"
8. `Stats/AttributeColumn.cs` - "Attribute" → "属性"

**验证方式**: 编译检查 + 运行时查看属性列

---

### 批次 19：界面与类型列（5 个文件）
**任务编号**: C-09
**预计耗时**: 8 分钟
**文件列表**:
1. `UiCategoryColumn.cs` - "Category (Basic)" → "分类（基础）"
2. `TypeColumn.cs` - "Type" → "类型"
3. `SearchCategoryColumn.cs` - "Category (Marketboard)" → "分类（市场板）"
4. `ShortcutColumn.cs` - "Shortcuts" → "快捷方式"
5. `UseIconsColumn.cs` - "Uses" → "用途"

**验证方式**: 编译检查 + 运行时查看界面列

---

### 批次 20：其他杂项列（5 个文件）
**任务编号**: C-10
**预计耗时**: 8 分钟
**文件列表**:
1. `StainColumn.cs` - "Dye" → "染色"
2. `RecentlySeenColumn.cs` - "Last Seen Date/Time" → "上次可见日期/时间"
3. `QuantityAvailableColumn.cs` - "Total Quantity Available" → "总可用数量"
4. `LeveIsCraftLevelColumn.cs` - "Is Leve(Craft) Item?" → "是否理符（制作）物品？"
5. `VentureTypeColumn.cs` - "Venture Type" → "探险类型"

**验证方式**: 编译检查 + 运行时查看其他列

---

## 总体时间估算
- **Filters**: 10 批次 × 平均 9 分钟 = **90 分钟**
- **Columns**: 10 批次 × 平均 8 分钟 = **80 分钟**
- **总计**: **170 分钟**（约 3 小时）

## 验证策略
每批次完成后：
1. 编译项目确保无语法错误
2. 随机抽查 2-3 个文件的翻译质量
3. 更新 checklist.md 标记已完成项

## 风险与回滚
- **风险**: 翻译不准确导致用户困惑
- **缓解**: 参考已有翻译保持一致性，使用 FFXIV CN 官方术语
- **回滚**: 每个批次独立，可通过 git 回滚单个批次
