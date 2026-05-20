# 待汉化内容 - Batch 5: Tooltips / Pages / Settings / Columns 补充

> 本批次记录 Tooltips、Pages、Settings 以及 Columns 中遗漏的硬编码英文

---

## 一、Tooltips 文件夹

### 1. UseInformationTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~122 | `"\nUses: "` | "\n用途：" |

---

### 2. SourceInformationTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~122 | `"\nSources: "` | "\n来源：" |

---

### 3. LocationDisplayTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~92 | `"Missing: "` | "缺少：" |
| ~96 | `"Buy: "` | "购买：" |
| ~108 | `" / ("` + ... + `" should be retrieved)"` | " / (" + ... + " 应该取回)" |

---

### 4. IngredientPatchTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~79 | `"\nIngredient Patch: "` | "\n材料版本：" |

---

### 5. HeaderTextTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~77 | `"\n[Allagan Tools]"` | "\n[Allagan Tools]"（品牌名，可不翻译） |

---

### 6. GlamourReadySetTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~140 | `"\nOutfit Glamour: "` | "\n套装幻化：" |
| ~148 | `"\nOutfit Glamour\n"` | "\n套装幻化\n" |
| ~189 | `"\nPart of: "` | "\n属于：" |
| ~197 | `"\nPart of: "` | "\n属于：" |

---

### 7. DisplayUnlockTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~95 | `" - "` + (c.Item2 ? `"Acquired"` : `"Not Acquired"`) | " - " + (c.Item2 ? "已获得" : "未获得") |
| ~116 | `"Not Acquired:\n"` | "未获得：\n" |
| ~125 | `"Acquired:\n"` | "已获得：\n" |

---

### 8. DisplayMarketPriceTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~92 | `"Market Board Data:\n"` | "市场板数据：\n" |
| ~96 | `"Average Price: "` | "平均价格：" |
| ~97 | `"Average Price (HQ): "` | "平均价格（高品质）：" |
| ~103 | `"Minimum Price: "` | "最低价格：" |
| ~104 | `"Minimum Price (HQ): "` | "最低价格（高品质）：" |

---

### 9. CofferLootTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~102 | `"\nLoot: "` + ownedCount + `"/"` + lootItems.Count + `" items owned"` | "\n战利品：" + ownedCount + "/" + lootItems.Count + " 个物品已拥有" |
| ~110 | `"\nAvailable in:"` | "\n可获取于：" |
| ~126 | `"\nAlready acquired"` | "\n已获得" |

---

### 10. AmountOwnedTooltip.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~150 | `" other locations."` | " 个其他位置。" |
| ~176 | `" other locations."` | " 个其他位置。" |
| ~213 | `" other locations."` | " 个其他位置。" |
| ~262 | `" other locations."` | " 个其他位置。" |
| ~299 | `" other locations."` | " 个其他位置。" |
| ~305 | `"Owned: "` | "拥有：" |
| ~306 | `"Locations:\n"` | "位置：\n" |

---

## 二、Ui/Pages 文件夹

### 1. ListsPage.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~41 | `Name = "Lists"` | `Name = "列表"` |
| ~50 | `"Duplicate"` | "复制" |
| ~50 | `"Duplicate the list."` | "复制此列表。" |
| ~51 | `"Export Configuration"` | "导出配置" |
| ~51 | `"Copies the list export string to clipboard."` | "将列表导出字符串复制到剪贴板。" |
| ~52 | `"Remove"` | "移除" |
| ~52 | `"Are you sure you want to remove this list?"` | "确定要移除此列表吗？" |
| ~52 | `"Remove the list."` | "移除此列表。" |
| ~80 | `"[Export] "` | "[导出] " |
| ~80 | `"Filter Configuration"` | "筛选器配置" |
| ~111 | `"Invalid or incompatible list data in clipboard."` | "剪贴板中的列表数据无效或不兼容。" |
| ~130 | `"="` | "=" |
| ~139 | `"Moving: "` | "移动中：" |
| ~139 | `"Untitled"` | "未命名" |
| ~177 | `"Untitled"` | "未命名" |
| ~211 | `"Item Lists"` | "物品列表" |
| ~213 | `"Import from Clipboard##itemlist"` | "从剪贴板导入##itemlist" |
| ~231 | `"No item lists created yet!"` | "尚未创建物品列表！" |
| ~244 | `"Craft Lists"` | "制作列表" |
| ~246 | `"Import from Clipboard##craftlist"` | "从剪贴板导入##craftlist" |
| ~264 | `"No craft lists created yet!"` | "尚未创建制作列表！" |

---

### 2. FilterPage.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~58 | `"General"` | "常规" |
| ~61 | `"Name: "` | "名称：" |
| ~70 | `"Export Configuration to Clipboard"` | "导出配置到剪贴板" |
| ~74 | `"[Export] "` | "[导出] " |
| ~74 | `"Filter Configuration"` | "筛选器配置" |
| ~79 | `"Filter Type: "` | "筛选器类型：" |
| ~84 | `"Display in Tab List: "` | "在标签列表中显示：" |

---

### 3. CharacterRetainerPage.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~40 | `Name = "Characters/Retainers"` | `Name = "角色/雇员"` |
| ~58 | `"Clear Inventories"` | "清空背包" |
| ~58 | `"Are you sure you want to clear the inventories of this "` | "确定要清空此 " |
| ~59 | `"Delete "` | "删除 " |
| ~59 | `"Are you sure you want to delete this "` | "确定要删除此 " |
| ~95 | `"Characters ("` + ... + `")"` | "角色 (" + ... + ")" |
| ~120 | `"\n\nRight Click: Options"` | "\n\n右键：选项" |
| ~133 | `"Free Companies ("` + ... + `")"` | "部队 (" + ... + ")" |
| ~151 | `"\n\nRight Click: Options"` | "\n\n右键：选项" |
| ~163 | `"Residences ("` + ... + `")"` | "住宅 (" + ... + ")" |
| ~181 | `"\n\nRight Click: Options"` | "\n\n右键：选项" |
| ~194 | `"Retainers ("` + ... + `")"` | "雇员 (" + ... + ")" |
| ~237 | `"Orphaned Retainers:"` | "孤儿雇员：" |
| ~259 | `"\n\nRight Click: Options"` | "\n\n右键：选项" |
| ~278 | `"World: "` | "服务器：" |
| ~282 | `"All"` | "全部" |
| ~325 | `"Edit name, set the name to blank to return it to the original name."` | "编辑名称，将名称设为空可恢复为原始名称。" |
| ~330 | `"Custom Name: "` | "自定义名称：" |
| ~339 | `"Original Name: "` | "原始名称：" |
| ~342 | `"Save"` | "保存" |
| ~363 | `"Level: "` | "等级：" |
| ~364 | `"Gil: "` | "金币：" |
| ~365 | `"Gender: "` | "性别：" |
| ~366 | `"Free Company: "` | "部队：" |
| ~367 | `"World: "` | "服务器：" |
| ~368 | `"Class/Job: "` | "职业：" |
| ~373 | `"World: "` | "服务器：" |
| ~374 | `"Plot Size: "` | "地块大小：" |
| ~375 | `"Location: "` | "位置：" |
| ~376 | `"Owners: "` | "所有者：" |
| ~380 | `"Missing Character"` | "缺失角色" |
| ~386 | `"World: "` | "服务器：" |
| ~387 | `"Related Characters: "` | "相关角色：" |
| ~396 | `"Inventories: "` | "背包：" |
| ~451 | `" - "` + ... + `" in slot "` | " - " + ... + " 在槽位 " |
| ~502 | `"No inventories found."` | "未找到背包。" |
| ~508 | `"Invalid character selected."` | "选择的角色无效。" |

---

## 三、Logic/Settings 文件夹

### 1. WindowIgnoreEscapeSetting.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~24 | `"Should the escape key be ignored for the "` + window.GenericName + `" window?"` | "是否忽略 " + window.GenericName + " 窗口的 ESC 键？" |

---

### 2. AutoSaveTimeSetting.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~37 | `"Next Autosave: "` | "下次自动保存：" |
| ~37 | `"N/A"` | "无" |

---

### 3. TrackMobSpawnSetting.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~51 | `"Export CSV"` | "导出 CSV" |
| ~57 | `"Export a CSV containing the mob spawn IDs and their positions."` | "导出包含怪物生成 ID 和位置的 CSV 文件。" |

---

## 四、Logic/Editors 文件夹

### 1. InventoryScopePicker.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~57 | `"All"` | "全部" |

---

### 2. CharacterScopePicker.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~378 | `"All"` | "全部" |

---

## 五、Columns 补充（已汉化 Name 但遗漏 HelpText / RenderName / 内部文本）

| 文件 | 行号 | 英文 | 建议中文 |
|------|------|------|----------|
| FavouritesColumn.cs | ~43 | `"Click to favourite/unfavourite."` | "点击以收藏/取消收藏。" |
| FavouritesColumn.cs | ~49 | `"Is this item in your list of favourites?"` | "此物品是否在收藏列表中？" |
| UptimeColumn.cs | ~35 | `"Shows how long an item will be available to gather..."` | "显示物品可采集的剩余时间..." |
| UptimeColumn.cs | ~52 | `"Up in "` | "将在 " |
| UptimeColumn.cs | ~61 | `"Up for "` | "持续 " |
| UptimeColumn.cs | ~86 | `" (Up in "` | "（将在 " |
| UptimeColumn.cs | ~95 | `" (Up for "` | "（持续 " |
| DebugCraftColumn.cs | ~30 | `"Required: "` | "需求：" |
| DebugCraftColumn.cs | ~31 | `"Needed: "` | "需要：" |
| DebugCraftColumn.cs | ~32 | `"Needed Pre Update: "` | "更新前需要：" |
| DebugCraftColumn.cs | ~33 | `"Available: "` | "可用：" |
| DebugCraftColumn.cs | ~34 | `"Ready: "` | "就绪：" |
| DebugCraftColumn.cs | ~35 | `"Can Craft: "` | "可制作：" |
| DebugCraftColumn.cs | ~36 | `"Will Retrieve: "` | "将取回：" |
| DebugCraftColumn.cs | ~42 | `"Shows craft debug information"` | "显示制作调试信息" |
| CraftSimpleColumn.cs | ~66 | `"Delete item"` | "删除物品" |
| CraftSimpleColumn.cs | ~98 | `"N/A"` | "无" |
| CraftSimpleColumn.cs | ~123 | `"Available: "` | "可用：" |
| CraftSimpleColumn.cs | ~127 | `"Missing: "` | "缺少：" |
| CraftSimpleColumn.cs | ~144 | `"Missing Ingredients: "` | "缺少材料：" |
| CraftSimpleColumn.cs | ~163 | `RenderName = "Next Step"` | `RenderName = "下一步"` |
| CraftSimpleColumn.cs | ~169 | `"Shows a simplified version of what you should do next in your craft"` | "显示制作下一步的简化版本" |
| CraftMarketPriceColumn.cs | ~74 | `"N/A"` | "无" |
| CraftMarketPriceColumn.cs | ~101 | `"Available: "` | "可用：" |
| CraftMarketPriceColumn.cs | ~105 | `"Missing: "` | "缺少：" |
| CraftMarketPriceColumn.cs | ~117 | `"The current market pricing for the given item. "` | "当前物品的市场价格。" |
| CraftCalculatorColumn.cs | ~48 | `"This will calculate the total amount of an item..."` | "这将计算物品的总数量..." |
| CraftCalculatorColumn.cs | ~61 | `"Inventories to search in:"` | "要搜索的背包：" |
| CraftCalculatorColumn.cs | ~64 | `"Please make sure you include at least one inventory..."` | "请确保至少包含一个背包..." |
| CraftCalculatorColumn.cs | ~76 | `"Calculate Crafts"` | "计算制作" |
| CraftCalculatorColumn.cs | ~149 | `"Stop Calculating Crafts"` | "停止计算制作" |
| CraftAmountRequiredColumn.cs | ~157 | `"Ingredient Breakdown:"` | "材料分解：" |
| CraftAmountRequiredColumn.cs | ~158 | `"Amount Originally Required: "` | "原始需求数量：" |
| CraftAmountRequiredColumn.cs | ~159 | `"Amount Required: "` | "需求数量：" |
| CraftAmountRequiredColumn.cs | ~160 | `"Amount in Inventory: "` | "背包中的数量：" |
| CraftAmountRequiredColumn.cs | ~161 | `"Amount to Retrieve: "` | "要取回的数量：" |
| CraftAmountRequiredColumn.cs | ~163 | `"Amount Missing: "` | "缺少数量：" |
| CraftAmountRequiredColumn.cs | ~166 | `"Amount Craftable: "` | "可制作数量：" |
| CraftAmountRequiredColumn.cs | ~170 | `"Craft Operations Required: "` | "所需制作操作：" |
| CraftAmountRequiredColumn.cs | ~172 | `"Recipe Yield: "` | "配方产出：" |
| CraftAmountRequiredColumn.cs | ~180 | `"Ingredients: "` | "材料：" |
| CraftAmountRequiredColumn.cs | ~200 | `"The amount required with inventory and external sources factored in/The amount required without inventory and external sources factored in."` | "考虑背包和外部来源后的需求数量/不考虑背包和外部来源的需求数量。" |
| QuantityColumn.cs | ~96 | `"Characters to search in:"` | "要搜索的角色：" |
| QuantityColumn.cs | ~100 | `"This lets you set which characters you want to generate a total based off."` | "设置要基于哪些角色生成总数。" |
| MarketBoardPriceColumn.cs | ~31 | `"loading..."` | "加载中..." |
| MarketBoardPriceColumn.cs | ~32 | `"untradable"` | "不可交易" |
| MarketBoardPriceColumn.cs | ~100 | `"No data available"` | "无可用数据" |
| MarketBoardPriceColumn.cs | ~105 | `"Listings: "` | "上架：" |
| MarketBoardPriceColumn.cs | ~117 | `"History: "` | "历史：" |

---

## 六、Filters 补充（已汉化 Name 但遗漏内部 UI 文本）

| 文件 | 行号 | 英文 | 建议中文 |
|------|------|------|----------|
| ZonePreferenceFilter.cs | ~125 | `"Add new zone: "` | "添加新区域：" |
| ZonePreferenceFilter.cs | ~133 | `"##ItemSearch"` | "##物品搜索"（内部 ID，可不翻译） |
| ZonePreferenceFilter.cs | ~142 | `"Start typing to search..."` | "开始输入以搜索..." |
| IngredientSearchFilter.cs | ~74 | `"Add all from filter"` | "从筛选器添加全部" |
| IngredientSearchFilter.cs | ~76 | `"AddAllFilterSelect"` | "AddAllFilterSelect"（内部 ID） |
| IngredientSearchFilter.cs | ~89 | `"Add all from "` | "从 " |
| ColumnsFilter.cs | ~165 | `"Edit##Column"` | "编辑##列" |
| ColumnsFilter.cs | ~194 | `"Add Column"` | "添加列" |
| ColumnsFilter.cs | ~197 | `"##ItemSearch"` | "##物品搜索"（内部 ID） |
| ColumnsFilter.cs | ~206 | `"Start typing to search..."` | "开始输入以搜索..." |
| ColumnsFilter.cs | ~262 | `"Default Column"` | "默认列" |
| ColumnsFilter.cs | ~269 | `"Configurable"` | "可配置" |
| ColumnsFilter.cs | ~280 | `"Add"` | "添加" |
| ColumnsFilter.cs | ~306 | `"Custom Column Name: "` | "自定义列名：" |
| ColumnsFilter.cs | ~317 | `"Custom Export Name: "` | "自定义导出名称：" |
| ColumnsFilter.cs | ~370 | `"Cancel"` | "取消" |
| ColumnsFilter.cs | ~390 | `"Current Columns:"` | "当前列：" |
| CraftColumnsFilter.cs | ~132 | `"Edit##Column"` | "编辑##列" |
| CraftColumnsFilter.cs | ~162 | `"Add Column"` | "添加列" |
| CraftColumnsFilter.cs | ~165 | `"##ItemSearch"` | "##物品搜索"（内部 ID） |
| CraftColumnsFilter.cs | ~174 | `"Start typing to search..."` | "开始输入以搜索..." |
| CraftColumnsFilter.cs | ~230 | `"Default Column"` | "默认列" |
| CraftColumnsFilter.cs | ~237 | `"Configurable"` | "可配置" |
| CraftColumnsFilter.cs | ~248 | `"Add"` | "添加" |
| CraftColumnsFilter.cs | ~274 | `"Custom Column Name: "` | "自定义列名：" |
| CraftColumnsFilter.cs | ~285 | `"Custom Export Name: "` | "自定义导出名称：" |
| CraftColumnsFilter.cs | ~338 | `"Cancel"` | "取消" |
| CraftColumnsFilter.cs | ~358 | `"Current Columns:"` | "当前列：" |
| CraftWorldPricePreferenceFilter.cs | ~115 | `"Add new world: "` | "添加新服务器：" |
| CraftWorldPricePreferenceFilter.cs | ~123 | `"##ItemSearch"` | "##物品搜索"（内部 ID） |
| CraftWorldPricePreferenceFilter.cs | ~132 | `"Start typing to search..."` | "开始输入以搜索..." |
| CraftIngredientPreferenceFilter.cs | ~154 | `"=##DragHandle"` | "=##DragHandle"（内部 ID） |
| CraftIngredientPreferenceFilter.cs | ~159 | `"Click and drag to reorder"` | "点击并拖动以重新排序" |
| CraftIngredientPreferenceFilter.cs | ~169 | `"Moving: "` | "移动中：" |
| CraftIngredientPreferenceFilter.cs | ~229 | `"Add Preference:"` | "添加偏好：" |
| CraftIngredientPreferenceFilter.cs | ~246 | `"Add Item Preference:"` | "添加物品偏好：" |
| CraftIngredientPreferenceFilter.cs | ~254 | `"##ItemSearch"` | "##物品搜索"（内部 ID） |
| CraftIngredientPreferenceFilter.cs | ~263 | `"Start typing to search..."` | "开始输入以搜索..." |
| CraftIngredientPreferenceFilter.cs | ~278 | `"Reset to Default##IngredientPref"` | "重置为默认##IngredientPref" |
| CraftItemIngredientOverridesFilter.cs | ~104 | `"Add Override:"` | "添加覆盖：" |
| CraftItemIngredientOverridesFilter.cs | ~117 | `"##ItemOverrideSearch"` | "##物品覆盖搜索"（内部 ID） |
| CraftItemIngredientOverridesFilter.cs | ~126 | `"Start typing to search..."` | "开始输入以搜索..." |
| CraftItemIngredientOverridesFilter.cs | ~172 | `"Add##ItemOverride"` | "添加##ItemOverride" |
| CraftItemIngredientOverridesFilter.cs | ~187 | `"Clear All##ItemOverrides"` | "全部清除##ItemOverrides" |
| CraftItemIngredientOverridesFilter.cs | ~213 | `"No per-item overrides set."` | "未设置逐物品覆盖。" |
| CraftItemIngredientOverridesFilter.cs | ~244 | `"Unknown Item ("` | "未知物品 (" |
| SourcesFilter.cs | ~51 | `"Source Information: "` | "来源信息：" |

---

## 七、Ui/Widgets 文件夹

### 1. PopupMenu.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~151 | `_question + "\nThis operation cannot be undone!\n\n"` | `_question + "\n此操作无法撤销！\n\n"` |
| ~154 | `"OK"` | "确定" |
| ~160 | `"Cancel"` | "取消" |

---

### 2. Utils.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~121 | `"Yes"` | "是" |
| ~122 | `"No"` | "否" |

---

### 3. GenericTabbedTable.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~45 | `"All"` | "全部" |

---

## 八、Logic/Settings 补充

### 1. UseIconGroupingSetting.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~111 | `"Reset##Reset"` | "重置##Reset" |

---

### 2. TooltipLocationScopeLimitSetting.cs / TooltipGlamourReadySetScopeSetting.cs / TooltipCofferLootScopeSetting.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~59 / ~66 / ~66 | `"Reset##"` + Key + `"Reset"` | "重置##" + Key + "Reset" |

---

## 总结

本批次涉及以下目录：
1. **Tooltips/**: 7 个文件 - 提示文本前缀和状态文本
2. **Ui/Pages/**: 3 个文件 - 页面标题、按钮、标签
3. **Logic/Settings/**: 3 个文件 - 设置项标签和提示
4. **Logic/Editors/**: 2 个文件 - 选择器默认值
5. **Logic/Columns/**: 10+ 个文件 - HelpText、RenderName、内部文本
6. **Logic/Filters/**: 10+ 个文件 - 内部 UI 标签和按钮
7. **Ui/Widgets/**: 3 个文件 - 弹窗按钮和通用文本

总计约 **40+ 个文件** 需要补充汉化。
