# 待汉化内容 - Batch 1: UI 窗口 (Windows)

> 本批次记录 InventoryTools/Ui/Windows/ 目录下需要汉化的内容
> 排除已通过 `_localizationService` 本地化的字符串

---

## 1. FiltersWindow.cs

### 1.1 PopupMenu 硬编码英文

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~144 | `"Mob Window"` | "怪物窗口" |
| ~144 | `"Open the mobs window."` | "打开怪物窗口。" |
| ~146 | `"Npcs Window"` | "NPC窗口" |
| ~146 | `"Open the npcs window."` | "打开NPC窗口。" |
| ~148 | `"Duties Window"` | "副本窗口" |
| ~148 | `"Open the duties window."` | "打开副本窗口。" |
| ~150 | `"Airships Window"` | "飞空艇窗口" |
| ~150 | `"Open the airships window."` | "打开飞空艇窗口。" |
| ~152 | `"Submarines Window"` | "潜水艇窗口" |
| ~152 | `"Open the submarines window."` | "打开潜水艇窗口。" |
| ~154 | `"Retainer Ventures Window"` | "雇员探险窗口" |
| ~155 | `"Open the retainer ventures window."` | "打开雇员探险窗口。" |
| ~157 | `"Help"` | "帮助" |
| ~157 | `"Open the help window."` | "打开帮助窗口。" |
| ~164 | `"Search List"` | "搜索列表" |
| ~164 | `"New Search List"` | "新建搜索列表" |
| ~166 | `"This will create a new list that let's you search for specific items within your characters and retainers inventories."` | "这将创建一个新列表，用于在角色和雇员背包中搜索特定物品。" |
| ~167 | `"Sort List"` | "整理列表" |
| ~167 | `"New Sort List"` | "新建整理列表" |
| ~168 | `"This will create a new list that let's you search for specific items within your characters and retainers inventories then determine where they should be moved to."` | "这将创建一个新列表，用于在角色和雇员背包中搜索特定物品并决定它们应该被移动到哪里。" |
| ~169 | `"Game Item List"` | "游戏物品列表" |
| ~169 | `"New Game Item List"` | "新建游戏物品列表" |
| ~170 | `"This will create a list that lets you search for all items in the game."` | "这将创建一个可以搜索游戏中所有物品的列表。" |
| ~171 | `"History List"` | "历史列表" |
| ~171 | `"New History List"` | "新建历史列表" |
| ~173 | `"This will create a list that lets you view historical data of how your inventory has changed."` | "这将创建一个可以查看背包历史变化数据的列表。" |
| ~174 | `"Curated List"` | "精选列表" |
| ~174 | `"New Curated List"` | "新建精选列表" |
| ~175 | `"This will create a list that lets you add individual items to it manually."` | "这将创建一个可以手动添加单个物品的列表。" |
| ~328 | `"Edit"` | "编辑" |
| ~328 | `"Edit the filter."` | "编辑筛选器。" |
| ~330 | `"Duplicate"` | "复制" |
| ~330 | `"Duplicate the filter."` | "复制筛选器。" |
| ~332 | `"Move Left"` / `"Move Up"` | "向左移动" / "向上移动" |
| ~334 | `"Move the filter left."` / `"Move the filter up."` | "将筛选器向左移动。" / "将筛选器向上移动。" |
| ~335 | `"Move Right"` / `"Move Down"` | "向右移动" / "向下移动" |
| ~337 | `"Move the filter right."` / `"Move the filter down."` | "将筛选器向右移动。" / "将筛选器向下移动。" |
| ~338 | `"Remove"` | "移除" |
| ~338 | `"Are you sure you want to remove this filter?"` | "确定要移除此筛选器吗？" |
| ~338 | `"Remove the filter."` | "移除筛选器。" |

### 1.2 MenuBar 硬编码英文

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~578 | `"File"` | "文件" |
| ~587 | `"Changelog"` | "更新日志" |
| ~592 | `"Help"` | "帮助" |
| ~597 | `"Enable Verbose Logging"` | "启用详细日志" |
| ~610 | `"Report a Issue"` | "报告问题" |
| ~627 | `"Edit"` | "编辑" |
| ~644 | `"Teamcraft Format"` | "Teamcraft 格式" |
| ~653 | `"JSON Format"` | "JSON 格式" |
| ~662 | `"Paste List Contents"` | "粘贴列表内容" |
| ~680 | `"Clear List"` | "清空列表" |
| ~694 | `"Add to Craft List"` | "添加到制作列表" |
| ~728 | `"New Craft List"` | "新建制作列表" |
| ~756 | `"New Craft List (Ephemeral)"` | "新建制作列表（临时）" |
| ~787 | `"Add to Curated List"` | "添加到精选列表" |
| ~815 | `"New Curated List"` | "新建精选列表" |
| ~850 | `"View"` | "视图" |
| ~874 | `"Export"` | "导出" |
| ~888 | `"Refresh All Prices"` | "刷新所有价格" |
| ~903 | `"Lists"` | "列表" |
| ~907 | `"Add"` | "添加" |
| ~968 | `"Add (Preconfigured)"` | "添加（预配置）" |
| ~998 | `"Import/Export"` | "导入/导出" |
| ~1002 | `"Export Current List (Share Code)"` | "导出当前列表（分享代码）" |
| ~1012 | `"Import List (Share Code)"` | "导入列表（分享代码）" |
| ~1014 | `"Please enter a valid share code for a list below and then hit ok to import it."` | "请在下方输入有效的列表分享代码，然后点击确定导入。" |

### 1.3 设置面板硬编码英文

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~1466 | `"General"` | "常规" |
| ~1470 | `"Name: "` | "名称：" |
| ~1481 | `"Save"` | "保存" |
| ~1499 | `"Filter Type: "` | "筛选器类型：" |
| ~1507 | `"Search..."` | "搜索..." |
| ~1643 | `"No results found"` | "未找到结果" |
| ~1661 | `"You are currently editing the list's configuration. Press the tick on the right hand side to save configuration."` | "你正在编辑列表配置。点击右侧的勾选按钮保存配置。" |
| ~1723 | `"Clear the current search."` | "清除当前搜索。" |
| ~1739 | `"Toggles the add item side bar."` | "切换添加物品侧边栏。" |
| ~1750 | `"Edit the list's configuration."` | "编辑列表配置。" |
| ~1791 | `"Refresh Market Prices"` | "刷新市场价格" |
| ~1802 | `"Add Company Craft to List"` | "将公司制作添加到列表" |
| ~1826 | `"Pending Market Requests: "` | "待处理的市场请求：" |
| ~1830 | `"Total Cost NQ: "` | "普通品质总成本：" |
| ~1832 | `"Total Cost HQ: "` | "高品质总成本：" |
| ~1890 | `"Are you sure you want to clear all your stored history?"` | "确定要清空所有存储的历史记录吗？" |
| ~1897 | `"Clear your history."` | "清空历史记录。" |
| ~1900 | `" items"` | " 个物品" |
| ~1911 | `" historical records"` | " 条历史记录" |
| ~1915 | `"History tracking is currently disabled"` | "历史追踪当前已禁用" |
| ~1921 | `"No List"` | "无列表" |
| ~1246 | `"Clear the current search."` | "清除当前搜索。" |
| ~1251 | `"Start typing to search..."` | "开始输入以搜索..." |
| ~1258 | `"Name"` | "名称" |
| ~1451 | `"Add a new list"` | "添加新列表" |

### 1.4 GenericName (未通过 localizationService)

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~204 | `"Filters"` | "筛选器" |

---

## 2. CraftsWindow.cs

### 2.1 需要检查的硬编码英文

该文件很大（170KB），大部分已使用 `_localizationService`，但需要逐行检查是否有遗漏的硬编码英文。

---

## 3. ChangelogWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~28 | `"Changelog"` | "更新日志" |
| ~84 | `"Added"` | "新增" |
| ~85 | `"Fixed"` | "修复" |
| ~86 | `"Changed"` | "变更" |
| ~87 | `"Removed"` | "移除" |
| ~135 | GenericName = `"Changelog"` | "更新日志" |

---

## 4. SourceWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~63 | GenericName = `"Sources"` | "来源" |
| ~72 | WindowName = `"Submarines"` | "潜水艇" |
| ~75 | `"Name"` | "名称" |
| ~99 | `"Help Text"` | "帮助文本" |
| ~123 | `"Relationship Type"` | "关系类型" |
| ~147 | `"Sample"` | "示例" |
| ~180 | `{0, "All"}, {1, "Sources"}, {2, "Uses"}` | `{0, "全部"}, {1, "来源"}, {2, "用途"}` |

---

## 5. DutiesWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~27 | 构造函数 `"Duties Window"` | "副本窗口" |

---

## 6. AirshipsWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~29 | 构造函数 `"Airships Window"` | "飞空艇窗口" |

---

## 7. SubmarinesWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~30 | 构造函数 `"Submarines Window"` | "潜水艇窗口" |

---

## 8. RetainerTasksWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~27 | 构造函数 `"Retainer Ventures"` | "雇员探险" |

---

## 9. ENpcsWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~37 | 构造函数 `"NPCs Window"` | "NPC窗口" |

---

## 10. BNpcsWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~47 | 构造函数 `"Mobs Window"` | "怪物窗口" |
| ~250 | `"Unknown"` | "未知" |

---

## 11. ConfigurationWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~72 | 构造函数 `"Configuration Window"` | "配置窗口" |
| ~97 | `"Settings"` | "设置" |
| ~104 | `"Modules"` | "模块" |
| ~116 | `"Data"` | "数据" |
| ~123 | `"Search List"` | "搜索列表" |
| ~123 | `"New Search List"` | "新建搜索列表" |
| ~124 | `"Sort List"` | "整理列表" |
| ~124 | `"New Sort Filter"` | "新建整理筛选器" |
| ~125 | `"Game Item List"` | "游戏物品列表" |
| ~125 | `"New Game Item List"` | "新建游戏物品列表" |
| ~126 | `"History List"` | "历史列表" |
| ~126 | `"New History Item List"` | "新建历史物品列表" |
| ~165 | `"Items Window"` | "物品窗口" |
| ~165 | `"Open the items window."` | "打开物品窗口。" |
| ~166 | `"Craft Window"` | "制作窗口" |
| ~166 | `"Open the crafts window."` | "打开制作窗口。" |
| ~181 | `"Configure new settings"` | "配置新设置" |
| ~181 | `"Configure new settings."` | "配置新设置。" |
| ~182 | `"Configure all settings"` | "配置所有设置" |
| ~182 | `"Configure all settings."` | "配置所有设置。" |
| ~420 | GenericName = `"Configuration"` | "配置" |

---

## 12. ItemWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~98 | 构造函数 `"Item Window"` | "物品窗口" |

---

## 13. ConfigurationWizard.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~22 | 构造函数 `"Configuration Wizard"` | "配置向导" |

---

## 14. HelpWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~19 | 构造函数 `"Help Window"` | "帮助窗口" |
| ~134 | `"Example Usages:"` | "使用示例：" |

---

## 15. IntroWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~17 | 构造函数 `"Intro Window"` | "介绍窗口" |

---

## 16. TeamCraftImportWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~31 | GenericName = `"Teamcraft Import"` | "Teamcraft 导入" |

---

## 17. ListDebugWindow.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~27 | GenericName = `"List Debug"` | "列表调试" |

---

## 总结

本批次共涉及 **17 个文件**，主要是 UI 窗口的标题、菜单、按钮、提示文本等硬编码英文。
大部分窗口的 `WindowName` 已通过 `_localizationService` 本地化，但 `GenericName` 和部分内部文本仍为硬编码英文。
