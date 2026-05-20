# 待汉化内容 - Batch 6: 其他杂项 (Misc)

> 本批次记录其他零散的需要汉化的内容
> 包括 PluginLogic、ListService、FilterTable、CraftsWindow 补充、Overlays 等

---

## 一、根目录文件

### 1. PluginLogic.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~271 | `"Craft List"` | "制作列表" |

---

### 2. ListService.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~122 | `"Untitled List"` | "未命名列表" |
| ~977 | `"Default Craft List"` | "默认制作列表" |

---

## 二、Logic 文件夹

### 1. FilterTable.cs

| 行号 | 英文 | 建议中文 |
|------|------|----------|
| ~126 | `"true"` | "是" |
| ~132 | `"false"` | "否" |

---

### 2. CraftDisplayMode.cs (如有英文)

需要检查文件内容确认是否有硬编码英文。

---

### 3. ColumnConfiguration.cs (如有英文)

需要检查文件内容确认是否有硬编码英文。

---

## 三、CraftsWindow 补充

由于 `CraftsWindow.cs` 文件很大（170KB），需要单独详细检查。以下是已知需要关注的区域：

### 3.1 需要全面扫描的区域

该文件大部分已通过 `_localizationService` 本地化，但需要检查：
- 任何硬编码的 `"` 字符串字面量
- 拼接字符串中的英文部分
- 条件文本如 `"N/A"`, `"Unknown"`, `"None"` 等

---

## 四、Overlays 文件夹

### 1. 所有 Overlay 文件

Overlays 文件夹包含以下文件，需要逐一检查是否有硬编码英文：

- SelectStringOverlay.cs
- SelectIconStringOverlay.cs
- RetainerListOverlay.cs
- InventoryShopOverlay.cs
- InventoryRetainerOverlay.cs
- InventoryRetainerLargeOverlay.cs
- InventoryMiragePrismBoxOverlay.cs
- InventoryLargeOverlay.cs
- InventoryGridOverlay.cs
- InventoryExpansionOverlay.cs
- InventoryBuddyOverlay2.cs
- InventoryBuddyOverlay.cs
- IAtkOverlayState.cs
- GameOverlay.cs
- FreeCompanyChestOverlay.cs
- CabinetWithdrawOverlay.cs
- ArmouryBoardOverlay.cs

这些 Overlay 文件通常包含游戏 UI 的覆盖层文本，需要检查是否有用户可见的英文。

---

## 五、Hotkeys 文件夹

### 1. 所有 Hotkey 文件

Hotkeys 文件夹包含以下文件，需要检查 `Name` 和 `HelpText` 属性：

- AirshipsWindowHotkey.cs
- ConfigurationWindowHotkey.cs
- CraftWindowHotkey.cs
- DutiesWindowHotkey.cs
- HotKey.cs
- IHotkey.cs
- ListsWindowHotkey.cs
- MobWindowHotkey.cs
- MoreInfoWindowHotkey.cs
- OpenCraftingLogHotkey.cs
- OpenFishingLogHotkey.cs
- OpenGatheringLogHotkey.cs
- OpenItemLogHotkey.cs
- RetainerTasksWindowHotkey.cs
- SubmarinesWindowHotkey.cs

---

## 六、Features 文件夹

### 1. Logic/Features/ 文件

- BasicFeature.cs
- ContextMenuFeature.cs
- Feature.cs
- FiltersFeature.cs
- HotkeysFeature.cs

需要检查这些 Feature 类的 `Name` 和 `Description` 属性。

---

## 七、Debuggers 文件夹

### 1. InventoryTools/Debuggers/ 文件

- AcquisitionDebuggerPane.cs
- ArmoireDebuggerPane.cs
- CharacterDebuggerPane.cs
- CraftMonitorDebuggerPane.cs
- LayerDebuggerPane.cs
- ListServiceDebuggerPane.cs
- QueueDebuggerPane.cs

调试窗口通常有窗口标题和标签文本需要汉化。

---

## 八、Commands 文件夹

### 1. PluginCommands.cs

需要检查命令名称和帮助文本是否需要汉化。

### 2. PluginCommandManager.cs

需要检查命令管理相关的文本。

---

## 九、Attributes 文件夹

### 1. HelpMessageAttribute.cs

如果属性中包含英文帮助消息，需要汉化。

---

## 十、Converters 文件夹

### 1. 转换器文件

- ColumnConverter.cs
- EnumDictionaryConverter.cs
- TypeDictionaryConverter.cs

需要检查是否有用户可见的错误消息或标签。

---

## 十一、Extensions 文件夹

### 1. 扩展方法文件

需要检查是否有用户可见的文本：

- Base64Extensions.cs
- FilterTypeExtensions.cs
- HotkeyExtensions.cs
- ItemFlagsExtensions.cs
- ItemRowExtensions.cs
- ListExtensions.cs
- StringExtensions.cs
- TimeSpanExtensions.cs

---

## 十二、Enums 文件夹

### 1. ShopType.cs

如果枚举在 UI 中显示，需要汉化显示文本。

---

## 十三、CriticalCommonLib 项目

### 需要检查的文件

CriticalCommonLib 作为共享库，可能也包含用户可见的英文文本：

#### 1. Crafting 文件夹
- CraftCompletionMode.cs
- CraftGroupType.cs
- CraftGrouping.cs
- 其他 Craft 相关文件

#### 2. Models 文件夹
- InventoryCategory.cs
- InventoryChangeReason.cs
- InventorySortOrder.cs
- RetainerSortOrder.cs

#### 3. Services/Ui 文件夹
- WindowName.cs
- 其他 UI 服务文件

#### 4. MarketBoard 文件夹
- MarketPricing.cs
- Universalis.cs

---

## 十四、InventoryToolsMock 项目

### 需要检查的文件

测试/模拟项目中的窗口和 UI 文本：

- MockGameItemsWindow.cs
- MockWindow.cs
- IconBrowserWindow.cs
- 其他 Mock 文件

---

## 十五、JSON 配置文件

### 1. InventoryTools.json / InventoryTools.zh.json

检查这些 JSON 文件中是否有需要汉化的配置项名称或描述。

---

## 十六、CHANGELOG.md

虽然不是代码文件，但 CHANGELOG 中的英文变更说明也可以考虑汉化。

---

## 总结

本批次为**兜底批次**，记录了以下需要进一步检查的区域：

1. **根目录文件**: PluginLogic.cs, ListService.cs, FilterTable.cs
2. **CraftsWindow.cs**: 需要全面扫描（170KB 大文件）
3. **Overlays/**: 18 个文件需要检查
4. **Hotkeys/**: 15 个文件需要检查
5. **Features/**: 5 个文件需要检查
6. **Debuggers/**: 7 个文件需要检查
7. **Commands/**: 2 个文件需要检查
8. **CriticalCommonLib/**: 需要检查 Crafting、Models、Services 等文件夹
9. **InventoryToolsMock/**: 需要检查 Mock 窗口
10. **JSON 配置文件**: InventoryTools.json 等

### 建议的下一步行动

1. **高优先级**:
   - CraftsWindow.cs 全面扫描
   - Overlays 文件夹检查
   - Hotkeys 文件夹检查

2. **中优先级**:
   - Features 文件夹检查
   - Debuggers 文件夹检查
   - CriticalCommonLib 项目检查

3. **低优先级**:
   - Commands 文件夹检查
   - InventoryToolsMock 项目检查
   - JSON 配置文件检查
