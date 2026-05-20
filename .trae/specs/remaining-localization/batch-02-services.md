# 待汉化内容 - Batch 2: 服务层 (Services)

> 本批次记录 InventoryTools/Services/ 目录下需要汉化的内容
> 排除已通过 `_localizationService` 本地化的字符串

---

## 1. ContextMenuService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~126 | `"More Information"` | "更多信息" |
| ~135 | `"Search"` | "搜索" |
| ~147 | `"Add to Active Craft List"` | "添加到当前制作列表" |
| ~157 | `"Add to Craft List"` | "添加到制作列表" |
| ~167 | `"Add to Curated List"` | "添加到精选列表" |
| ~189 | `"Open Crafting Log"` | "打开制作笔记" |
| ~202 | `"Open Gathering Log"` | "打开采集笔记" |
| ~214 | `"Open Fishing Log"` | "打开捕鱼笔记" |
| ~226 | `"More Information"` | "更多信息" |
| ~237 | `"More Information"` | "更多信息" |
| ~407 | `"Add to New Craft List"` | "添加到新建制作列表" |
| ~412 | `"Add to New Ephemeral Craft List"` | "添加到新建临时制作列表" |
| ~434 | `"Add to New Curated List"` | "添加到新建精选列表" |

---

## 2. ImGuiMenuService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~99 | `searchResults.Count + (searchResults.Count == 1 ? " item" : " items")` | `searchResults.Count + (searchResults.Count == 1 ? " 个物品" : " 个物品")` |
| ~101 | `"Try on"` | "试穿" |
| ~111 | `"Mark as favourite"` | "标记为收藏" |
| ~119 | `"Unmark as favourite"` | "取消收藏标记" |
| ~132 | `"Add to Curated List"` | "添加到精选列表" |
| ~149 | `"Add to new Curated List"` | "添加到新建精选列表" |
| ~167 | `"Add to Craft List"` | "添加到制作列表" |
| ~185 | `"Add to new Craft List"` | "添加到新建制作列表" |
| ~197 | `"Add to new Craft List (ephemeral)"` | "添加到新建临时制作列表" |
| ~244 | `"Remove from Curated List"` | "从精选列表移除" |
| ~292 | `"Remove from Craft List"` | "从制作列表移除" |
| ~304 | `"Switch to All Phases"` | "切换到全部阶段" |
| ~320 | `"Switch to "` + ... + `" (Phase "` + ... + `")"` | "切换到 " + ... + "（阶段 " + ... + "）" |
| ~354 | `"Add "` + ... + `" item to new craft list"` | "将 " + ... + " 个物品添加到新建制作列表" |
| ~365 | `"Add "` + ... + `" items to new craft list (ephemeral)"` | "将 " + ... + " 个物品添加到新建临时制作列表" |
| ~385 | `"Open in Garland Tools"` | "在 Garland Tools 中打开" |
| ~389 | `"Open in Teamcraft"` | "在 Teamcraft 中打开" |
| ~393 | `"Open in Universalis"` | "在 Universalis 中打开" |
| ~397 | `"Open in Gamer Escape"` | "在 Gamer Escape 中打开" |
| ~406 | `"Open in Console Games Wiki"` | "在 Console Games Wiki 中打开" |
| ~416 | `"Copy Name"` | "复制名称" |
| ~420 | `"Link"` | "链接" |
| ~424 | `"Try On"` | "试穿" |
| ~431 | `"Search"` | "搜索" |
| ~444 | `... ? "Unmark Favourite" : "Mark Favourite"` | `... ? "取消收藏" : "标记收藏"` |
| ~451 | `"More Information"` | "更多信息" |
| ~469 | `"Open Crafting Log"` | "打开制作笔记" |
| ~484 | `"Open Crafting Log(Recipes)"` | "打开制作笔记（配方）" |
| ~490 | `recipe.CraftType?.FormattedName ?? "Unknown"` | `recipe.CraftType?.FormattedName ?? "未知"` |
| ~500 | `"Open Gathering Log"` | "打开采集笔记" |
| ~505 | `"Open Fishing Log"` | "打开捕鱼笔记" |
| ~512 | `"Gather (Gatherbuddy)"` | "采集 (Gatherbuddy)" |
| ~522 | `"Gather (Advanced)"` | "采集（高级）" |
| ~550 | `"Open Map"` | "打开地图" |
| ~581 | `"Gather (Gatherbuddy)"` | "采集 (Gatherbuddy)" |
| ~585 | `"Gather (Advanced)"` | "采集（高级）" |
| ~624 | `"Open Map"` | "打开地图" |
| ~668 | `"Gather (Gatherbuddy)"` | "采集 (Gatherbuddy)" |
| ~673 | `"Gather (Advanced)"` | "采集（高级）" |
| ~715 | `"Open Map"` | "打开地图" |
| ~766 | `"Buy"` | "购买" |
| ~793 | `shopSource.Shop.Name + " - Teleport"` | `shopSource.Shop.Name + " - 传送"` |
| ~819 | `"Hunt"` | "狩猎" |
| ~845 | `... + " - Teleport ("` + ... + `")"` | `... + " - 传送 ("` + ... + `")"` |

---

## 3. PopupService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~114 | `"Enter New Name..."` | "输入新名称..." |
| ~191 | `"OK"` | "确定" |
| ~200 | `"Cancel"` | "取消" |
| ~238 | `_question + "\nThis operation cannot be undone!\n\n"` | `_question + "\n此操作无法撤销！\n\n"` |
| ~242 | `"OK"` | "确定" |
| ~249 | `"Cancel"` | "取消" |

---

## 4. ItemInfoRenderer.cs

### 4.1 GetCategoryName() 方法中的英文分类名

| 英文 | 建议中文 |
|------|----------|
| `"Sources"` | "来源" |
| `"Uses"` | "用途" |
| `"Vendors"` | "商人" |
| `"Crafting"` | "制作" |
| `"Gathering"` | "采集" |
| `"Drops"` | "掉落" |
| `"Duties"` | "副本" |
| `"Other"` | "其他" |
| `"Achievements"` | "成就" |
| `"Quests"` | "任务" |
| `"Shops"` | "商店" |

### 4.2 GetRelationshipName() 方法中的英文关系名

| 英文 | 建议中文 |
|------|----------|
| `"Source"` | "来源" |
| `"Use"` | "用途" |
| `"Related"` | "相关" |
| `"Required"` | "所需" |
| `"Reward"` | "奖励" |
| `"Ingredient"` | "材料" |
| `"Result"` | "结果" |

### 4.3 其他硬编码英文

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~275 | `"Can this item be sourced via "` + type.ToString() | "此物品是否可以通过 " + type.ToString() + " 获得" |
| ~279 | `"Can this item be used for "` + type.ToString() | "此物品是否可以用于 " + type.ToString() |
| ~533 | `"Item"` | "物品" |
| ~553 | `"Related Items:"` | "相关物品：" |
| ~575 | `"Items:"` | "物品：" |
| ~596 | `"Related Items"` | "相关物品" |
| ~624 | `"Pick a "` + (typeName.Plural ?? typeName.Singular) | "选择一个 " + (typeName.Plural ?? typeName.Singular) |
| ~708 | `"Related Items"` | "相关物品" |
| ~758 | `"Items:"` | "物品：" |
| ~787 | `"Related Items"` | "相关物品" |
| ~847 | `"Copy Source Information"` | "复制来源信息" |
| ~847 | `"Copy Use Information"` | "复制用途信息" |
| ~865 | `"Compendium Entries"` | "百科条目" |
| ~959 | `"Source "` + (currentIndex + 1) + `" of "` + totalSources | "来源 " + (currentIndex + 1) + " / " + totalSources |
| ~1032 | `"No tooltip configured for "` + ... + `", please report this!"` | "未为 " + ... + " 配置提示文本，请报告此问题！" |

---

## 5. ImGuiService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~176 | `"Invalid Icon ID"` | "无效图标 ID" |
| ~636 | `"..."` | "..." |

---

## 6. MigrationManagerService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~278 | `"History"` | "历史" |

---

## 7. WotsitIpc.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~35 | `"Allagan Tools"` | "Allagan Tools"（品牌名，通常不翻译） |

---

## 总结

本批次共涉及 **7 个文件**，主要是服务层中的菜单项、按钮、提示文本、分类名称等硬编码英文。
其中 `ImGuiMenuService.cs` 和 `ItemInfoRenderer.cs` 包含大量需要汉化的字符串。
