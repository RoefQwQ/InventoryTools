# 阶段 1 实施计划：Column/Filter/Compendium/Feature 硬编码汉化（修订版）

## 紧急修复：编译错误修复（阶段 0 回归）

### 问题诊断

阶段 0 的子代理在移除 `_localizationService` 引用时，**过度删除**了 `using InventoryTools.Services;` 语句。这导致：

- `ImGuiService`（在 `InventoryTools.Services` 命名空间）无法被 ~140 个文件找到
- `ItemLocalizer`（在 `InventoryTools.Localizers` 命名空间）无法被 2 个文件找到
- `IngredientPreferenceLocalizer`（在 `InventoryTools.Localizers` 命名空间）无法被 2 个文件找到
- `CraftGroupingLocalizer` 和 `CraftItemLocalizer` 的 Autofac 注册被错误删除

**总计 342 个 CS0246 编译错误，全部由 `using` 语句被错误删除引起。**

原始代码编译 0 错误，我们的修改引入了全部错误。

---

### 步骤 0.1：恢复 `using InventoryTools.Services;`（~140 个文件）

使用脚本批量处理，为所有引用 `ImGuiService` 但缺少 `using InventoryTools.Services;` 的文件添加回该 import。

**受影响的文件目录：**

| 目录 | 文件数 |
|------|--------|
| `Ui/Windows/` | 14 |
| `Logic/Settings/Abstract/` | 9 |
| `Logic/Settings/Abstract/Generic/` | 5 |
| `Logic/Settings/` | 105 |
| `Logic/Filters/` | 3 |
| `Logic/Filters/Abstract/` | 2 |
| `EquipmentSuggest/` | 2 |
| **合计** | **~140** |

**执行方式：** 使用 PowerShell 脚本或单个子代理批量处理。

**编译验证：** `dotnet build InventoryTools --no-restore`

### 步骤 0.2：恢复 `using InventoryTools.Localizers;`（3 个文件）

| 文件 | 缺少的类型引用 |
|------|--------------|
| `Services/ImGuiTooltipService.cs` | `ItemLocalizer` |
| `InventoryToolsPlugin.cs` | `ItemLocalizer`、`IngredientPreferenceLocalizer` |
| `Logic/Filters/CraftItemIngredientOverridesFilter.cs` | `IngredientPreferenceLocalizer` |

**执行方式：** 使用 SearchReplace 逐一添加 `using InventoryTools.Localizers;`。

**编译验证：** `dotnet build InventoryTools --no-restore`

### 步骤 0.3：恢复 `InventoryToolsPlugin.cs` 中的 Localizer 注册

从 diff 中看到以下注册被错误删除：

```csharp
builder.RegisterSingletonSelfAndInterfaces<CraftGroupingLocalizer>();
builder.RegisterSingletonSelfAndInterfaces<CraftItemLocalizer>();
```

以及重复删除了 `ConfigurationWizardService` 的注册（原本就有两个，删了一个是对的，但需确认还剩一个）。

同时检查是否有其他被错误删除的注册。

**编译验证：** `dotnet build InventoryTools --no-restore`

### 步骤 0.4：完整编译验证

```
dotnet build InventoryTools
```

确保 0 错误后提交修复。

---

## 背景

阶段 0 已完成：移除了 fork 添加的 `_localizationService` 引用，恢复了上游 Localizers 目录。现在需要将 InventoryTools 主项目中的英文硬编码字符串替换为中文。

## 实际规模统计

| 类别                       | 文件数       | 属性                                | 说明                              |
| ------------------------ | --------- | --------------------------------- | ------------------------------- |
| Column (根目录)             | 94        | `Name`                            | `Logic/Columns/`                |
| Column (Stats)           | 8         | `Name`                            | `Logic/Columns/Stats/`          |
| Column (Buttons)         | 8         | `Name`                            | `Logic/Columns/Buttons/`        |
| Column (ColumnSettings)  | 11        | `Name`                            | `Logic/Columns/ColumnSettings/` |
| Column (特殊)              | 2         | `Name`                            | 动态拼接/非 override                 |
| Filter (根目录)             | 89        | `Name`                            | `Logic/Filters/` 直接赋值           |
| Filter (Stats)           | 7         | `Name`                            | `Logic/Filters/Stats/`          |
| Filter (GenericFilter子类) | 4         | 构造函数参数                            | 需要特殊处理                          |
| Filter (SampleFilters)   | 3         | `Name`                            | `Logic/SampleFilters/`          |
| CompendiumType           | 21        | `Singular`/`Plural`/`Description` | `Compendium/Types/`             |
| Feature                  | 7         | `Name`/`Description`              | `Logic/Features/`               |
| **总计**                   | **~254**  |                                   |                                 |

## 执行策略

**核心原则：每个子批次 → 子代理执行 → 编译验证 → 下一批次**

每个子代理负责：
1. 读取指定文件
2. 将英文字符串替换为中文
3. 返回变更摘要

每批次完成后执行 `dotnet build InventoryTools --no-restore` 验证编译通过。

## 批次规划

### 批次 1：Column - 基础列（20 个文件）✅ 已完成

子代理已处理 `Logic/Columns/` 下的前 20 个文件（Name 属性汉化），已完成。

### 批次 2：Column - 市场/交易列（20 个文件）

| 文件                                | 英文 Name                                                | 中文 Name                 |
| --------------------------------- | ------------------------------------------------------ | ----------------------- |
| MarketBoardPriceColumn.cs         | "Market Board Average Price NQ/HQ"                     | "市场板均价 普通/高品质"          |
| MarketboardPriceNQColum.cs        | "Market Board Average Price NQ"                        | "市场板均价 普通"              |
| MarketboardPriceHQColumn.cs       | "Market Board Average Price HQ"                        | "市场板均价 高品质"             |
| MarketBoardMinPriceColumn.cs      | "Market Board Minimum Price NQ/HQ"                     | "市场板最低价 普通/高品质"         |
| MarketBoardMinPriceHQColumn.cs    | "Market Board Minimum Price HQ"                        | "市场板最低价 高品质"            |
| MarketboardMinPriceNQColumn.cs    | "Market Board Minimum Price NQ"                        | "市场板最低价 普通"             |
| MarketBoardTotalPriceColumn.cs    | "Market Board Average Total Price(Qty \* Price) NQ/HQ" | "市场板总价(数量×价格) 普通/高品质"   |
| MarketBoardMinTotalPriceColumn.cs | "Market Board Minimum Total Price(Qty \* Price) NQ/HQ" | "市场板最低总价(数量×价格) 普通/高品质" |
| RetainerMarketPriceColumn.cs      | "Retainer Selling Unit Price"                          | "雇员出售单价"                |
| SellToVendorPriceColumn.cs        | "Sell to Vendor Price"                                 | "商店出售价"                 |
| SellToVendorPriceTotalColumn.cs   | "Sell to Vendor Price (Total)"                         | "商店出售价（总计）"             |
| BuyFromVendorPriceColumn.cs       | "Buy from Vendor Price"                                | "商店购买价"                 |
| CanBePlacedOnMarketColumn.cs      | "Can be Placed on Market?"                             | "可上架市场？"                |
| CraftMarketPriceColumn.cs         | "Market Pricing"                                       | "市场定价"                  |
| RecentlySeenColumn.cs             | "Last Seen Date/Time"                                  | "最后出现时间"                |
| CanBeTradedColumn.cs              | "Is Tradable?"                                         | "可交易？"                  |
| CanBePurchasedColumn.cs           | "Is Purchasable?"                                      | "可购买？"                  |
| StoreColumn.cs                    | "Is sold in Square Store?"                             | "商城出售？"                 |
| FromCalamitySalvagerColumn.cs     | "Is from Calamity Salvager?"                           | "灾难回收商？"                |
| ExpertDeliverySealsColumn.cs      | "Expert Delivery Reward Seal Count"                    | "专家交付军票奖励"              |

**编译验证** → 通过后继续

### 批次 3：Column - 制作/采集列（20 个文件）

| 文件                              | 英文 Name                         | 中文 Name    |
| ------------------------------- | ------------------------------- | ---------- |
| CraftColumn.cs                  | "Is Craftable?"                 | "可制作？"     |
| CraftSimpleColumn.cs            | "Next Step in Craft"            | "制作下一步"    |
| CraftCalculatorColumn.cs        | "Craft Calculator"              | "制作计算器"    |
| CraftAmountRequiredColumn.cs    | "Amount Required"               | "需要数量"     |
| CraftAmountReadyColumn.cs       | "Amount in Character Inventory" | "背包中数量"    |
| CraftAmountAvailableColumn.cs   | "Amount to Retrieve"            | "可取出数量"    |
| CraftAmountCanCraftColumn.cs    | "Amount can Craft"              | "可制作数量"    |
| CraftAmountUnavailableColumn.cs | "Amount Missing"                | "缺少数量"     |
| CraftZoneColumn.cs              | "Zone"                          | "区域"       |
| IsCraftingItemColumn.cs         | "Is Craft Component?"           | "制作材料？"    |
| IsRecipeCompletedColumn.cs      | "Are Recipes Completed?"        | "配方已完成？"   |
| RecipeTotalColumn.cs            | "Recipe Total Count"            | "配方总数"     |
| RecipeUnlockedColumn.cs         | "Is Recipe Unlocked?"           | "配方已解锁？"   |
| GatheredByColumn.cs             | "Gathered By?"                  | "采集职业"     |
| CanBeGatheredColumn.cs          | "Is Gatherable?"                | "可采集？"     |
| HasBeenGatheredColumn.cs        | "Logged in Gathering Log?"      | "已记录采集日志？" |
| TimedNodeColumn.cs              | "Is From Timed Node?"           | "限时采集点？"   |
| HiddenNodeColumn.cs             | "Is From Hidden Node?"          | "隐藏采集点？"   |
| EphemeralNodeColumn.cs          | "Is From Ephemeral Node?"       | "临时采集点？"   |
| UptimeColumn.cs                 | "Next Gather Uptime"            | "下次采集时间"   |

**编译验证** → 通过后继续

### 批次 4：Column - 装备/属性列（20 个文件）

| 文件                             | 英文 Name                   | 中文 Name  |
| ------------------------------ | ------------------------- | -------- |
| EquippableColumn.cs            | "Equipped By (Class/Job)" | "可装备职业"  |
| EquippableByRaceColumn.cs      | "Equipped By (Race)"      | "可装备种族"  |
| EquippableByGenderColumn.cs    | "Equipped By (Gender)"    | "可装备性别"  |
| CanBeEquippedColumn.cs         | "Can be Equipped?"        | "可装备？"   |
| CanBeHighQualityColumn.cs      | "Can be High Quality?"    | "可高品质？"  |
| CanBeDyedColumn.cs             | "Is Dyeable?"             | "可染色？"   |
| CanBeDesynthedColumn.cs        | "Is Desynthable?"         | "可分解？"   |
| DesynthesisClassColumn.cs      | "Desynthesis Class"       | "分解职业"   |
| DesynthesisSkillDeltaColumn.cs | "Desynthesis Skill Delta" | "分解技能差值" |
| IsGearSetColumn.cs             | "In Gearset?"             | "在套装中？"  |
| GearSetColumn.cs               | "Gearset Number"          | "套装编号"   |
| BestInSlotColumn.cs            | "Relative Item Level"     | "相对物品等级" |
| OutdatedGearColumn.cs          | "Outdated Gear?"          | "过时装备？"  |
| IsGlamourReadySetColumn.cs     | "Is Outfit Glamour Set?"  | "投影套装？"  |
| IsGlamourReadySetItemColumn.cs | "Is Outfit Glamour Item?" | "投影物品？"  |
| IsArmoireItemColum.cs          | "Is Armoire Item?"        | "军柜物品？"  |
| IsAquariumItemColumn.cs        | "Is Aquarium Item?"       | "水族箱物品？" |
| IsHousingItemColumn.cs         | "Is Housing Item?"        | "房屋物品？"  |
| IsCollectableColumn.cs         | "Is Collectable?"         | "收藏品？"   |
| UseIconsColumn.cs              | "Uses"                    | "用途"     |

**编译验证** → 通过后继续

### 批次 5：Column - 剩余列 + Buttons + Stats（26 个文件）

**剩余根目录列（14 个）：**

| 文件                              | 英文 Name                       | 中文 Name     |
| ------------------------------- | ----------------------------- | ----------- |
| IsGCSupplyItemColumn.cs         | "Is GC Turn-in item?"         | "军队补给品？"    |
| IsFromFateColumn.cs             | "Is From Fate?"               | "危命任务？"     |
| IsCustomDeliveryItemColumn.cs   | "Is custom delivery item?"    | "莫雯委托？"     |
| IsIshgardCraftColumn.cs         | "Is Ishgardian Craft?"        | "伊什加尔德制作？"  |
| IsMobDropColumn.cs              | "Is Dropped by Mobs?"         | "怪物掉落？"     |
| LeveIsCraftLevelColumn.cs       | "Is Leve(Craft) Item?"        | "理符（制作）物品？" |
| VentureTypeColumn.cs            | "Venture Type"                | "雇员探险类型"    |
| DebugColumn.cs                  | "Debug - General Information" | "调试 - 常规信息" |
| DebugCraftColumn.cs             | "Debug - Craft"               | "调试 - 制作"   |
| HistoryChangeReasonColumn.cs    | "History Event Reason"        | "历史事件原因"    |
| HistoryChangeDateColumn.cs      | "History Event Date/Time"     | "历史事件时间"    |
| HistoryChangeAmountColumn.cs    | "History Event Amount"        | "历史事件数量"    |
| IngredientPatchSearchColumn.cs  | "Ingredient Patch Search"     | "材料版本搜索"    |
| AcquisitionSourceIconsColumn.cs | "Acquisition"                 | "获取方式"      |

**Stats（8 个）：**

| 文件                      | 英文 Name           | 中文 Name |
| ----------------------- | ----------------- | ------- |
| AttributeColumn.cs      | "Attribute"       | "属性"    |
| PhysicalDamageColumn.cs | "Physical Damage" | "物理伤害"  |
| MagicalDamageColumn.cs  | "Magical Damage"  | "魔法伤害"  |
| ItemLevelColumn.cs      | "Required Level"  | "需求等级"  |
| ItemILevelColumn.cs     | "iLevel"          | "物品等级"  |
| DelayColumn.cs          | "Delay"           | "延迟"    |
| MateriaCountColumn.cs   | "Materia Count"   | "魔晶石数"  |
| DyeCountColumn.cs       | "Dye Count"       | "染色数"   |

**Buttons（8 个）：**

| 文件                          | 英文 Name                 | 中文 Name   |
| --------------------------- | ----------------------- | --------- |
| CraftGatherColumn.cs        | "Gather/Purchase/Buy"   | "采集/购买"   |
| RemoveButtonColumn.cs       | "Remove"                | "移除"      |
| GatherButtonColumn.cs       | "Gathering Log Button"  | "采集日志按钮"  |
| CustomLinkButtonColumn.cs   | "Custom Link Button"    | "自定义链接按钮" |
| CustomButtonColumn.cs       | "Custom Button"         | "自定义按钮"   |
| CraftBuyColumn.cs           | "Buy Button"            | "购买按钮"    |
| CraftButtonColumn.cs        | "Craft Button"          | "制作按钮"    |
| CopyItemNameButtonColumn.cs | "Copy Item Name Button" | "复制物品名按钮" |

**编译验证** → 通过后继续

### 批次 6：Column - ColumnSettings + 特殊列（13 个文件）

**ColumnSettings（11 个）：**

| 文件                                   | 英文 Name                  | 中文 Name  |
| ------------------------------------ | ------------------------ | -------- |
| UseTypeSelectorSetting.cs            | "Types"                  | "类型"     |
| StainColumnSetting.cs                | "Display Mode"           | "显示模式"   |
| SourceTypeSelectorSetting.cs         | "Types"                  | "类型"     |
| SourceCategorySelectorSetting.cs     | "Categories"             | "分类"     |
| QualitySelectorSetting.cs            | "Qualities"              | "品质"     |
| MarketboardWorldSetting.cs           | "World"                  | "服务器"    |
| CharacterColumnSetting.cs            | "Character"              | "角色"     |
| ButtonColumnSetting.cs               | "Button Types"           | "按钮类型"   |
| AttributeColumnSetting.cs            | "Attribute"              | "属性"     |
| ScopePickerColumnSetting.cs          | "Inventory Search Scope" | "库存搜索范围" |
| CharacterScopePickerColumnSetting.cs | "Character Search Scope" | "角色搜索范围" |

**特殊列（2 个）：**

| 文件                                | 说明                                                            |
| --------------------------------- | ------------------------------------------------------------- |
| CraftSettingsColumn.cs            | 非 override，`Name { get; set; } = "Settings"` → `"设置"`           |
| MarketBoardSevenDayCountColumn.cs | 动态拼接，需特殊处理 → `"市场板 {_configuration.MarketSaleHistoryLimit} 天销售量"` |

**编译验证** → 通过后继续

### 批次 7：Filter - 基础筛选器（25 个文件）

`Logic/Filters/` 下的前 25 个文件，按字母顺序：

| 文件                                 | 英文 Name                       | 中文 Name  |
| ---------------------------------- | ----------------------------- | -------- |
| AcquiredFilter.cs                  | "Has Been Acquired?"          | "已获得？"   |
| AcquisitionSourceFilter.cs         | "Acquisition Source"          | "获取来源"   |
| AcquisitionSourceCategoryFilter.cs | "Acquisition Source Category" | "获取来源分类" |
| AttributeFilter.cs                 | "Attribute"                   | "属性"     |
| BestInSlotFilter.cs                | "Relative Item Level"         | "相对物品等级" |
| BuyFromVendorPriceFilter.cs        | "Buy from Vendor Price"       | "商店购买价"  |
| CanBeDesynthedFilter.cs            | "Is Desynthable?"             | "可分解？"   |
| CanBeDyedFilter.cs                 | "Is Dyeable?"                 | "可染色？"   |
| CanBeEquippedFilter.cs             | "Can be Equipped?"            | "可装备？"   |
| CanBeHighQualityFilter.cs          | "Can be High Quality?"        | "可高品质？"  |
| CanBePurchasedFilter.cs            | "Is Purchasable?"             | "可购买？"   |
| CanBeTradedFilter.cs               | "Is Tradable?"                | "可交易？"   |
| CharacterOwnerFilter.cs            | "Character Owner"             | "角色持有者"  |
| CraftAmountFilter.cs               | "Craft Amount"                | "制作数量"   |
| CraftFilter.cs                     | "Is Craftable?"               | "可制作？"   |
| CraftMarketPriceFilter.cs          | "Craft Market Price"          | "制作市场价"  |
| CraftReadyFilter.cs                | "Craft Ready?"                | "制作就绪？"  |
| CraftingClassFilter.cs             | "Crafting Class"              | "制作职业"   |
| DestinationFilter.cs               | "Destination"                 | "目的地"    |
| DyeableFilter.cs                   | "Is Dyeable?"                 | "可染色？"   |
| EquippableByGenderFilter.cs        | "Equipped By (Gender)"        | "可装备性别"  |
| EquippableByRaceFilter.cs          | "Equipped By (Race)"          | "可装备种族"  |
| EquippableFilter.cs                | "Equipped By (Class/Job)"     | "可装备职业"  |
| FavouritesFilter.cs                | "Favourite?"                  | "收藏？"    |
| FromCalamitySalvagerFilter.cs      | "Is from Calamity Salvager?"  | "灾难回收商？" |

**编译验证** → 通过后继续

### 批次 8：Filter - 中间筛选器（25 个文件）

| 文件                                | 英文 Name                            | 中文 Name    |
| --------------------------------- | ---------------------------------- | ---------- |
| FromFateFilter.cs                 | "Is From Fate?"                    | "危命任务？"    |
| GCSupplyFilter.cs                 | "Is GC Turn-in item?"              | "军队补给品？"   |
| GearSetFilter.cs                  | "In Gearset?"                      | "在套装中？"    |
| GlamourReadyCombinedFilter.cs     | "Outfit Glamour Combined"          | "投影合并"     |
| GlamourReadyFilter.cs             | "Outfit Glamour"                   | "投影"       |
| HasBeenGatheredFilter.cs          | "Logged in Gathering Log?"         | "已记录采集日志？" |
| HiddenNodeFilter.cs               | "Is From Hidden Node?"             | "隐藏采集点？"   |
| IsArmoireFilter.cs                | "Is Armoire Item?"                 | "军柜物品？"    |
| IsCraftingItemFilter.cs           | "Is Craft Component?"              | "制作材料？"    |
| IsCustomDeliveryItemFilter.cs     | "Is custom delivery item?"         | "莫雯委托？"    |
| IsFromTimedFilter.cs              | "Is From Timed Node?"              | "限时采集点？"   |
| IsHousingItemFilter.cs            | "Is Housing Item?"                 | "房屋物品？"    |
| IsIshgardCraftFilter.cs           | "Is Ishgardian Craft?"             | "伊什加尔德制作？" |
| IsMobDropFilter.cs                | "Is Dropped by Mobs?"              | "怪物掉落？"    |
| IsOutdatedGearFilter.cs           | "Outdated Gear?"                   | "过时装备？"    |
| ItemLevelFilter.cs                | "Item Level"                       | "物品等级"     |
| LeveFilter.cs                     | "Is Leve Item?"                    | "理符物品？"    |
| MarketBoardAvgPriceFilter.cs      | "Market Board Average Price"       | "市场板均价"    |
| MarketBoardMinPriceFilter.cs      | "Market Board Minimum Price"       | "市场板最低价"   |
| MarketBoardTotalPriceFilter.cs    | "Market Board Total Price"         | "市场板总价"    |
| MarketboardMinTotalPriceFilter.cs | "Market Board Minimum Total Price" | "市场板最低总价"  |
| NameFilter.cs                     | "Name"                             | "名称"       |
| QualityFilter.cs                  | "Quality"                          | "品质"       |
| QuantityFilter.cs                 | "Quantity"                         | "数量"       |
| RarityFilter.cs                   | "Rarity"                           | "稀有度"      |

**编译验证** → 通过后继续

### 批次 9：Filter - 剩余筛选器（25 个文件）

| 文件                           | 英文 Name                        | 中文 Name     |
| ---------------------------- | ------------------------------ | ----------- |
| RecipeFilter.cs              | "Is Recipe?"                   | "配方？"       |
| RecipeUnlockedFilter.cs      | "Is Recipe Unlocked?"          | "配方已解锁？"    |
| RetainerMarketPriceFilter.cs | "Retainer Selling Price"       | "雇员出售价"     |
| SearchCategoryFilter.cs      | "Search Category"              | "搜索分类"      |
| SellToVendorPriceFilter.cs   | "Sell to Vendor Price"         | "商店出售价"     |
| SpiritbondFilter.cs          | "Spiritbond"                   | "灵纹"        |
| StoreFilter.cs               | "Is sold in Square Store?"     | "商城出售？"     |
| SubmarineUnlockedFilter.cs   | "Is Submarine Route Unlocked?" | "潜水艇路线已解锁？" |
| TimedFilter.cs               | "Is From Timed Node?"          | "限时采集点？"    |
| UiCategoryFilter.cs          | "UI Category"                  | "UI 分类"     |
| VentureFilter.cs             | "Has Venture?"                 | "有雇员探险？"    |
| AquariumFilter.cs            | "Is Aquarium Item?"            | "水族箱物品？"    |
| CollectableFilter.cs         | "Is Collectable?"              | "收藏品？"      |
| DesynthesisClassFilter.cs    | "Desynthesis Class"            | "分解职业"      |
| EphemeralNodeFilter.cs       | "Is From Ephemeral Node?"      | "临时采集点？"    |
| EquippableByFilter.cs        | "Equippable By"                | "可装备"       |
| IsGatherableFilter.cs        | "Is Gatherable?"               | "可采集？"      |
| SourceWorldFilter.cs         | "Source World"                 | "来源服务器"     |
| ItemTypeFilter.cs            | "Item Type"                    | "物品类型"      |
| JobCategoryFilter.cs         | "Job Category"                 | "职业分类"      |
| LevelFilter.cs               | "Level"                        | "等级"        |
| LocationFilter.cs            | "Location"                     | "位置"        |
| MateriaFilter.cs             | "Has Materia?"                 | "有魔晶石？"     |
| MateriaGradeFilter.cs        | "Materia Grade"                | "魔晶石品级"     |
| StackSizeFilter.cs           | "Stack Size"                   | "堆叠数"       |

**编译验证** → 通过后继续

### 批次 10：Filter - Stats + SampleFilters + GenericFilter子类（14 个文件）

**Stats（7 个）：**

| 文件                         | 英文 Name           | 中文 Name |
| -------------------------- | ----------------- | ------- |
| RequiredLevelFilter.cs     | "Required Level"  | "需求等级"  |
| PhysicalDamageFilter.cs    | "Physical Damage" | "物理伤害"  |
| MagicalDamageFilter.cs     | "Magical Damage"  | "魔法伤害"  |
| ItemLevelFilter.cs (Stats) | "iLevel"          | "物品等级"  |
| DelayFilter.cs             | "Delay"           | "延迟"    |
| MateriaCountFilter.cs      | "Materia Count"   | "魔晶石数"  |
| DyeCountFilter.cs          | "Dye Count"       | "染色数"   |

**SampleFilters（3 个）：**

| 文件                             | 英文 Name             | 中文 Name    |
| ------------------------------ | ------------------- | ---------- |
| SampleFilterMaterialCleanup.cs | "Material clean-up" | "材料清理"     |
| SampleFilterDuplicateItems.cs  | "Duplicate Items"   | "重复物品"     |
| SampleFilter100GillOrLess.cs   | "100 gil or less"   | "100 金币以下" |

**GenericFilter 子类（4 个，Name 通过构造函数传入）：**

| 文件                             | 构造函数中的 Name 参数                                     |
| ------------------------------ | -------------------------------------------------- |
| CanBeHighQualityFilter.cs      | "CanBeHq", "Can be High Quality?"                  |
| GlamourReadyCombinedFilter.cs  | "grCombined", "Outfit Glamour Combined"            |
| IsCollectableFilter.cs         | "collectable", "Is Collectable?"                   |
| IngredientPatchSearchFilter.cs | "ingredientPatchSearch", "Ingredient Patch Search" |

**编译验证** → 通过后继续

### 批次 11：CompendiumType（21 个文件）

每个文件需要翻译 `Singular`、`Plural`、`Description` 三个属性。

| 文件                               | Singular              | Plural                 | Description                                                 |
| -------------------------------- | --------------------- | ---------------------- | ----------------------------------------------------------- |
| ItemCompendiumType.cs            | "Item"                | "Items"                | "All the items available in the game"                       |
| MountCompendiumType.cs           | "Mount"               | "Mounts"               | "Unlockable mounts usable by the player."                   |
| MinionCompendiumType.cs          | "Minion"              | "Minions"              | "Unlockable minions summonable by the player."              |
| AchievementCompendiumType.cs     | "Achievement"         | "Achievements"         | "Achievements earned by the player."                        |
| ENpcCompendiumType.cs            | "NPC"                 | "NPCs"                 | "A list of all the NPCs in the game"                        |
| ClassJobCompendiumType.cs        | "Class"               | "Classes"              | "The classes/jobs your character can learn."                |
| InstanceContentCompendiumType.cs | "Instance"            | "Instances"            | "Instances including duties, trials, etc"                   |
| QuestCompendiumType.cs           | "Quest"               | "Quests"               | "Quests the character can complete."                        |
| LeveCompendiumType.cs            | "Leve"                | "Leves"                | "Leves the character can undertake."                        |
| BeastTribeCompendiumType.cs      | "Allied Society"      | "Allied Societies"     | "Allied Societies the player can gain reputation with."     |
| GearsetCompendiumType.cs         | "Gearset"             | "Gearsets"             | "Gearsets based on Eorzea Collection's organizing."         |
| SharedModelCompendiumType.cs     | "Shared Model Set"    | "Shared Model Sets"    | "Items that share the same model."                          |
| BGMCompendiumType.cs             | "BGM"                 | "BGM"                  | "Music from the game"                                       |
| ChocoboItemCompendiumType.cs     | "Chocobo Item"        | "Chocobo Items"        | "Items consumed or equipped by chocobo companions."         |
| RacingChocoboCompendiumType.cs   | "Racing Chocobo Item" | "Racing Chocobo Items" | "Items used for Racing Chocobo training and breeding."      |
| RelicToolCompendiumType.cs       | "Relic Tool"          | "Relic Tools"          | "Relic Tools"                                               |
| RelicWeaponCompendiumType.cs     | "Relic Weapon"        | "Relic Weapons"        | "Relic Weapons"                                             |
| SatisfactionNpcCompendiumType.cs | "Custom Delivery"     | "Custom Deliveries"    | "NPCs that accept Custom Deliveries (Satisfaction system)." |
| AirshipRoutesCompendium.cs       | "Airship Route"       | "Airship Routes"       | "Routes flown by Company Airships"                          |
| SubmarineRoutesCompendiumType.cs | "Submarine Route"     | "Submarine Routes"     | "Routes traversed by company submarines"                    |
| TerritoryTypeCompendiumType.cs   | "Territory"           | "Territories"          | "Territories available in the game"                         |

**编译验证** → 通过后继续

### 批次 12：Feature（7 个文件）

每个文件需要翻译 `Name` 和 `Description` 两个属性。

| 文件                               | Name                | Description                                     |
| -------------------------------- | ------------------- | ----------------------------------------------- |
| BasicFeature.cs                  | "Basic"             | "Configure the basic settings of Allagan Tools" |
| TooltipsFeature.cs               | "Tooltips"          | "Allagan Tools can add extra information..."    |
| MarketboardIntegrationFeature.cs | "Marketboard"       | "Configure the marketboard integration..."      |
| LayoutFeature.cs                 | "Layout"            | "How should the main items window\..."          |
| HotkeysFeature.cs                | "Hotkeys"           | "Set hotkeys for opening the various..."        |
| FiltersFeature.cs                | "Sample Item Lists" | "Select which sample item lists..."             |
| ContextMenuFeature.cs            | "Context Menus"     | "Adds new items to the right click..."          |

**编译验证** → 通过后继续

### 批次 13：最终验证

* 完整编译 `dotnet build InventoryTools`
* 运行测试 `dotnet test InventoryToolsTesting`（如有）
* 检查残留英文字符串

## 子代理调用模式

每个批次的子代理调用格式：

```
Task(
  subagent_type: "general_purpose_task",
  description: "批次 N: XXX 汉化",
  query: "
    在 InventoryTools 项目中，将以下文件的 XXX 属性从英文替换为中文。
    
    文件路径前缀：e:\Program Files (x86)\claude code\InventoryTools\InventoryTools\
    
    替换规则：
    - 读取每个文件
    - 使用 SearchReplace 将英文字符串替换为中文
    - 保持代码结构不变
    - 不添加任何注释
    
    文件列表：
    1. xxx.cs: 'English' → '中文'
    2. ...
  "
)
```

## 风险与注意事项

1. **编译验证是关键**：每批次后必须编译验证，发现问题立即修复再继续
2. **子模块不在此阶段处理**：CriticalCommonLib 和 OtterGui 的汉化是后续阶段
3. **动态 Name 需特殊处理**：MarketBoardSevenDayCountColumn 使用字符串拼接
4. **GenericFilter 子类**：Name 通过构造函数传入，需修改构造函数调用
5. **翻译一致性**：相同英文词在不同文件中应翻译为相同的中文
6. **不要删除 using 语句**：阶段 0 的教训 - 只替换目标字符串，不要动 using 语句
