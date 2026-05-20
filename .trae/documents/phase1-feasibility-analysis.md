# Spec 可行性分析报告

## 一、当前状态

### 1.1 编译状态
- **HEAD 代码编译：0 错误** ✅
- 所有文件已恢复到 HEAD 状态（原始 fork 代码）

### 1.2 仓库结构概览

| 目录 | 文件数 | 说明 |
|------|--------|------|
| `InventoryTools/Logic/Columns` | 100 | 表格列定义 |
| `InventoryTools/Logic/Filters` | 100 | 筛选器定义 |
| `InventoryTools/Logic/Features` | 10 | 功能模块定义 |
| `InventoryTools/Logic/Settings` | 120 | 设置项定义 |
| `InventoryTools/Ui/Windows` | 35 | UI 窗口 |
| `InventoryTools/Services` | 10+ | 服务层 |
| `InventoryTools/Localizers` | 6+ | 本地化器 |

---

## 二、上游 vs Fork 代码对比分析

### 2.1 核心差异总览

| 项目 | 上游 (Critical-Impact) | Fork (RoefQwQ) |
|------|----------------------|----------------|
| `ILocalizationService` | ❌ 不存在 | ✅ 已创建 |
| `LocalizationService` | ❌ 不存在 | ✅ 已创建 |
| `ChineseFontService` | ❌ 不存在 | ✅ 已创建 |
| `Strings.resx` | ❌ 不存在 | ✅ 已创建 |
| `Strings.zh.resx` | ❌ 不存在 | ✅ 已创建 |
| `InventoryTools.zh.json` | ❌ 不存在 | ✅ 已创建 |
| Settings 字符串 | 纯硬编码英文 | `.resx` 资源文件 + 构造函数覆盖 |
| Window 字符串 | 纯硬编码英文 | `_localizationService["Key"]` |
| Column 字符串 | 纯硬编码英文 | 纯硬编码英文（未改动） |
| Filter 字符串 | 纯硬编码英文 | 纯硬编码英文（未改动） |
| Feature 字符串 | 纯硬编码英文 | 纯硬编码英文（未改动） |

### 2.2 详细代码对比

#### Columns（100 个文件）

**上游代码示例** - [NameColumn.cs](https://github.com/Critical-Impact/InventoryTools/blob/main/InventoryTools/Logic/Columns/NameColumn.cs):
```csharp
public class NameColumn : ColoredTextColumn
{
    public NameColumn(ILogger<NameColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService) { }
    public override string Name { get; set; } = "Name";
    public override string HelpText { get; set; } = "The name of the item.";
}
```

**Fork 代码** - [NameColumn.cs](file:///e:/Program%20Files%20(x86)/claude%20code/InventoryTools/InventoryTools/Logic/Columns/NameColumn.cs):
```csharp
public class NameColumn : ColoredTextColumn
{
    public NameColumn(ILogger<NameColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService) { }
    public override string Name { get; set; } = "Name";              // ← 仍为英文
    public override string HelpText { get; set; } = "The name of the item.";  // ← 仍为英文
}
```

**结论**：Column 文件在 Fork 中**完全没有改动**，Name/HelpText 仍为纯硬编码英文。基类构造函数不接收 `ILocalizationService`。

#### Filters（100 个文件）

**上游代码示例** - [NameFilter.cs](https://github.com/Critical-Impact/InventoryTools/blob/main/InventoryTools/Logic/Filters/NameFilter.cs):
```csharp
public class NameFilter : StringFilter
{
    public override string Name { get; set; } = "Name";
    public override string HelpText { get; set; } = "Searches by the name of the item.";
    public NameFilter(ILogger<NameFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService) { }
}
```

**Fork 代码** - [NameFilter.cs](file:///e:/Program%20Files%20(x86)/claude%20code/InventoryTools/InventoryTools/Logic/Filters/NameFilter.cs):
```csharp
public class NameFilter : StringFilter
{
    public override string Name { get; set; } = "Name";              // ← 仍为英文
    public override string HelpText { get; set; } = "Searches by the name of the item.";  // ← 仍为英文
    public NameFilter(ILogger<NameFilter> logger, ImGuiService imGuiService) : base(logger, imGuiService) { }
}
```

**结论**：Filter 文件在 Fork 中**完全没有改动**（除了少数抽象基类增加了 `ILocalizationService` 参数用于按钮文本），具体 Filter 子类的 Name/HelpText 仍为纯硬编码英文。

#### Features（10 个文件）

**上游代码示例** - [BasicFeature.cs](https://github.com/Critical-Impact/InventoryTools/blob/main/InventoryTools/Logic/Features/BasicFeature.cs):
```csharp
public class BasicFeature : Feature
{
    public override string Name { get; } = "Basic";
    public override string Description { get; } = "Configure the basic settings of Allagan Tools";
}
```

**Fork 代码** - [BasicFeature.cs](file:///e:/Program%20Files%20(x86)/claude%20code/InventoryTools/InventoryTools/Logic/Features/BasicFeature.cs):
```csharp
public class BasicFeature : Feature
{
    public override string Name { get; } = "Basic";                   // ← 仍为英文
    public override string Description { get; } = "Configure the basic settings of Allagan Tools";  // ← 仍为英文
}
```

**结论**：Feature 文件在 Fork 中**完全没有改动**，Name/Description 仍为纯硬编码英文。

#### Settings（120 个文件，其中 100 个具体类）

**上游代码示例** - [AutoSaveSetting.cs](https://github.com/Critical-Impact/InventoryTools/blob/main/InventoryTools/Logic/Settings/AutoSaveSetting.cs):
```csharp
public class AutoSaveSetting : BooleanSetting
{
    public AutoSaveSetting(ILogger<AutoSaveSetting> logger, ImGuiService imGuiService, PluginLogic pluginLogic) : base(logger, imGuiService) { }
    public override string Name { get; set; } = "Auto save inventories/configuration?";
    public override string HelpText { get; set; } = "Should the inventories/configuration be automatically saved...";
}
```

**Fork 代码** - [AutoSaveSetting.cs](file:///e:/Program%20Files%20(x86)/claude%20code/InventoryTools/InventoryTools/Logic/Settings/AutoSaveSetting.cs):
```csharp
public class AutoSaveSetting : BooleanSetting
{
    public AutoSaveSetting(ILogger<AutoSaveSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, PluginLogic pluginLogic) : base(logger, imGuiService, localizationService)
    {
        Name = localizationService.GetString("Setting_AutoSave_Name");           // ← 从 .resx 获取中文
        HelpText = localizationService.GetString("Setting_AutoSave_HelpText");   // ← 从 .resx 获取中文
        WizardName = localizationService.GetString("Setting_AutoSave_WizardName");
    }
    public override string Name { get; set; } = "Auto save inventories/configuration?";  // ← 英文回退值
    public override string HelpText { get; set; } = "Should the inventories/configuration be automatically saved...";  // ← 英文回退值
}
```

**结论**：Settings 是 Fork 中**唯一真正实现了本地化**的模块。通过构造函数注入 `ILocalizationService`，在运行时从 `.resx` 资源文件加载中文文本覆盖英文默认值。

#### Windows（35 个文件）

**上游代码示例** - [CraftsWindow.cs](https://github.com/Critical-Impact/InventoryTools/blob/main/InventoryTools/Ui/Windows/CraftsWindow.cs) Initialize 方法:
```csharp
public override void Initialize()
{
    WindowName = "Crafts";
    _settingsMenu = new PopupMenu("configMenu", PopupMenu.PopupMenuButtons.All,
        new List<PopupMenu.IPopupMenuItem>()
        {
            new PopupMenu.PopupMenuItemSelectable("Mob Window", "mobs", OpenMobsWindow, "Open the mobs window."),
            new PopupMenu.PopupMenuItemSelectable("Npcs Window", "npcs", OpenNpcsWindow, "Open the npcs window."),
            // ... 全部硬编码英文
        });
}
```

**Fork 代码** - [CraftsWindow.cs](file:///e:/Program%20Files%20(x86)/claude%20code/InventoryTools/InventoryTools/Ui/Windows/CraftsWindow.cs) Initialize 方法:
```csharp
public override void Initialize()
{
    WindowName = _localizationService["Window_Crafts_Title"];  // ← 从 .resx 获取中文
    _settingsMenu = new PopupMenu("configMenu", PopupMenu.PopupMenuButtons.All,
        new List<PopupMenu.IPopupMenuItem>()
        {
            new PopupMenu.PopupMenuItemSelectable(
                _localizationService["Window_Crafts_MobWindow"], "mobs", OpenMobsWindow,
                _localizationService["Window_Crafts_MobWindowTooltip"]),  // ← 从 .resx 获取中文
            // ...
        });
}
```

**结论**：Window 文件在 Fork 中**大量使用了 `ILocalizationService`**，几乎所有 UI 文本都通过 `_localizationService["Key"]` 从 `.resx` 获取中文。

---

## 三、ILocalizationService 使用范围详细统计

### 3.1 直接引用 `ILocalizationService` 的文件（100 个）

| 分类 | 文件数 | 用途 | 是否用于 Name/HelpText |
|------|--------|------|----------------------|
| Settings 具体类 | 95 | `Name`/`HelpText`/`WizardName` | ✅ 是 |
| Settings 抽象基类 | 11 | 重置按钮、搜索框、警告文本 | ❌ 否（UI 控件文本） |
| Window 文件 | 21 | 窗口标题、菜单、按钮、提示 | ❌ 否（UI 文本） |
| Filter 抽象基类 | 5 | 重置按钮文本 | ❌ 否（UI 控件文本） |
| Command 文件 | 2 | 命令帮助消息 | ❌ 否 |
| Localizer 文件 | 12 | 领域字符串（物品、容器、分类名） | ❌ 否 |
| SampleFilter | 3 | 间接（通过父类） | - |
| EquipmentSuggest | 2 | 间接（通过父类） | - |

### 3.2 未使用 ILocalizationService 的模块（仍为英文硬编码）

| 模块 | 文件数 | 属性数 | 说明 |
|------|--------|--------|------|
| **Column** | 100 | ~200 (Name+HelpText) | 完全未使用本地化 |
| **Filter 具体类** | ~95 | ~190 (Name+HelpText) | 具体类未使用，仅基类用于按钮 |
| **Feature** | 10 | ~20 (Name+Description) | 完全未使用本地化 |
| **CompendiumType** | ~21 | ~60 (Singular/Plural/Description) | 需要调研 |

---

## 四、Strings.resx 资源文件分析

### 4.1 资源文件概况

- **991 个条目**，涵盖 Window、Setting、Item、Role、Relic 等所有类别
- **Strings.resx 和 Strings.zh.resx 内容完全相同**（都是中文）
- 所有翻译已完成，无空白条目

### 4.2 资源文件结构问题

```
Strings.resx          ← 默认资源文件（应该是英文，但实际是中文）
Strings.zh.resx       ← 中文卫星资源（也是中文）
```

**问题**：`Strings.resx` 作为默认/回退资源文件，其内容应该是英文，但实际被填充了中文。这导致：
- 如果某个 key 在 `Strings.zh.resx` 中缺失，回退到 `Strings.resx` 时仍得到中文（看似正确，但架构混乱）
- 没有独立的英文资源文件作为真正的回退
- 如果将来需要支持英文用户，无法通过切换文化实现

### 4.3 资源键命名规范

| 前缀 | 用途 | 示例 |
|------|------|------|
| `Setting_` | Settings 名称/帮助文本 | `Setting_AutoSave_Name` |
| `Window_` | Window 标题/菜单/提示 | `Window_Crafts_Title` |
| `Filter_` | Filter 按钮文本 | `Filter_Integer_ButtonReset` |
| `Role_` | 职业角色名称 | `Role_Tank` |
| `Item_` | 物品相关文本 | `Item_Xxx` |
| `Feature_` | Feature 名称/描述 | 需要确认是否存在 |
| `Column_` | Column 名称/帮助文本 | **不存在！** |

**关键发现**：`.resx` 文件中**没有 Column/Filter/Feature 的 Name/HelpText 资源键**。这意味着即使想为这些模块添加本地化，也需要：
1. 为每个 Column/Filter/Feature 创建资源键
2. 修改基类构造函数接收 `ILocalizationService`
3. 修改所有子类在构造函数中加载资源

---

## 五、策略可行性分析

### 策略 A：保留 ILocalizationService，扩展 .resx 覆盖 Column/Filter/Feature（推荐）

**做法**：
1. 保留现有的 `ILocalizationService`、`.resx` 文件、Settings/Window 的本地化代码
2. 为 Column/Filter/Feature 的 Name/HelpText/Description 添加 `.resx` 资源键
3. 修改 Column/Filter/Feature 基类，注入 `ILocalizationService`
4. 修改所有子类，在构造函数中从 `.resx` 加载中文文本

**优点**：
- 与现有架构一致（Settings/Window 已经使用此模式）
- 不破坏已有的 991 个翻译条目
- 编译风险低（只需添加构造函数参数和赋值语句）
- 如果将来需要支持多语言，架构已经就绪

**缺点**：
- 与上游架构不一致（上游没有 `ILocalizationService`）
- 需要修改基类构造函数，影响所有子类
- 需要为 ~200 个 Column/Filter/Feature 创建资源键
- 需要维护 ResourceManager 基础设施

**改动范围**：
- Column 基类：修改构造函数，注入 `ILocalizationService`
- Column 子类：~100 个，添加 `ILocalizationService` 参数，在构造函数中加载资源
- Filter 基类：已有 `ILocalizationService`，但具体类未使用
- Filter 子类：~95 个，在构造函数中加载资源
- Feature 基类：修改构造函数，注入 `ILocalizationService`
- Feature 子类：~10 个，添加参数，加载资源
- `.resx` 文件：添加 ~200 个新资源键
- **总计：~210 个文件需要修改**

**编译风险**：🟡 中等（需要修改基类构造函数，可能影响依赖注入注册）

---

### 策略 B：完全移除 ILocalizationService，全部硬编码中文（与上游对齐）

**做法**：
1. 删除 `ILocalizationService.cs`、`LocalizationService.cs`
2. 删除 `Strings.resx`、`Strings.zh.resx`
3. 删除 `ChineseFontService.cs`（如果需要）
4. 将所有 `localizationService.GetString("Key")` 替换为硬编码中文
5. 将所有 `_localizationService["Key"]` 替换为硬编码中文
6. 移除所有构造函数中的 `ILocalizationService` 参数

**优点**：
- 与上游架构完全一致
- 移除不必要的 ResourceManager 基础设施
- 代码更简洁，无需维护 .resx 文件
- 上游更新时合并冲突更少

**缺点**：
- 需要修改 ~169 个文件（远超原 spec 估计）
- Settings 文件需要从 `localizationService.GetString("Setting_Xxx_Name")` 改为 `Name = "中文名"`
- Window 文件需要从 `_localizationService["Key"]` 改为硬编码中文字符串
- 991 个 .resx 翻译需要全部转移到代码中
- 编译风险高（大量文件修改，容易引入错误）
- 阶段 0 的失败已经证明了移除的难度

**改动范围**：
- Window 文件：21 个（~260 处替换）
- Settings 具体类：~95 个（~190 处替换）
- Settings 抽象基类：11 个（~30 处替换）
- Filter 文件：5 个（~10 处替换）
- Command 文件：2 个（~10 处替换）
- Localizer 文件：12 个（~50 处替换）
- 删除文件：4 个（ILocalizationService.cs, LocalizationService.cs, Strings.resx, Strings.zh.resx）
- **总计：~169 个文件，~500+ 处替换**

**编译风险**：🔴 高（阶段 0 的失败已经证明了这一点）

---

### 策略 C：分阶段渐进式汉化（平衡方案）

**做法**：分 3 个子阶段逐步完成汉化。

#### 子阶段 C1：Column/Filter/Feature Name 属性直接硬编码中文（~210 个文件）

**范围**：
- Column 文件：~100 个（Name + HelpText）
- Filter 文件：~95 个（Name + HelpText）
- Feature 文件：~10 个（Name + Description）
- CompendiumType 文件：~21 个（Singular/Plural/Description）

**做法**：直接修改属性初始值为中文，不涉及 `ILocalizationService`

**示例**：
```csharp
// 修改前
public override string Name { get; set; } = "Name";
public override string HelpText { get; set; } = "The name of the item.";

// 修改后
public override string Name { get; set; } = "名称";
public override string HelpText { get; set; } = "物品的名称。";
```

**优点**：
- 不涉及任何架构改动
- 不触碰 `ILocalizationService`
- 编译风险极低
- 每个文件只改 1-3 行

**编译风险**：🟢 极低

#### 子阶段 C2：Settings 移除 ILocalizationService，改为硬编码中文（~100 个文件）

**范围**：
- Settings 具体类：~95 个
- Settings 抽象基类：11 个

**做法**：
1. 移除构造函数中的 `ILocalizationService` 参数
2. 将 `localizationService.GetString("Setting_Xxx_Name")` 替换为 `Name = "中文名"`
3. 保留属性初始值为中文（去掉英文回退值）

**示例**：
```csharp
// 修改前
public AutoSaveSetting(..., ILocalizationService localizationService) : base(..., localizationService)
{
    Name = localizationService.GetString("Setting_AutoSave_Name");
    HelpText = localizationService.GetString("Setting_AutoSave_HelpText");
}
public override string Name { get; set; } = "Auto save inventories/configuration?";

// 修改后
public AutoSaveSetting(...) : base(...)
{
}
public override string Name { get; set; } = "自动保存背包/配置？";
public override string HelpText { get; set; } = "是否按设定间隔自动保存背包/配置？";
```

**编译风险**：🟡 中等（需要修改构造函数签名，可能影响依赖注入）

#### 子阶段 C3：Window 移除 ILocalizationService，改为硬编码中文（~21 个文件）

**范围**：
- Window 文件：21 个

**做法**：
1. 移除构造函数中的 `ILocalizationService` 参数
2. 将 `_localizationService["Key"]` 替换为 `.resx` 中对应的中文值
3. 将 `_localizationService.GetString("Key")` 替换为硬编码中文

**编译风险**：🟡 中等（需要修改构造函数签名和字段声明）

#### 子阶段 C4：清理基础设施（最后执行）

**范围**：
- 删除 `ILocalizationService.cs`
- 删除 `LocalizationService.cs`
- 删除 `Strings.resx`、`Strings.zh.resx`
- 删除 `ChineseFontService.cs`（如不需要）
- 更新 `InventoryToolsPlugin.cs` 中的服务注册

**编译风险**：🔴 高（需要确保所有引用已清理）

---

## 六、推荐方案

### 推荐：策略 C1（立即执行）+ 保留 ILocalizationService（后续评估）

#### 立即执行：C1 - Column/Filter/Feature 直接硬编码中文

**原因**：
1. **这些模块完全不使用 `ILocalizationService`**，直接改字符串值即可
2. **改动最小**（每个文件 1-3 行）
3. **编译风险极低**（只改字符串初始值）
4. **不影响现有架构**（Settings/Window 的本地化不受影响）
5. **最快实现汉化目标**

**具体文件清单**：

| 模块 | 文件数 | 属性 | 示例 |
|------|--------|------|------|
| Column | ~100 | Name, HelpText | `NameColumn`, `QuantityColumn`, `IconColumn` ... |
| Filter | ~95 | Name, HelpText | `NameFilter`, `QuantityFilter`, `PatchFilter` ... |
| Feature | ~10 | Name, Description | `BasicFeature`, `TooltipsFeature`, `LayoutFeature` ... |
| CompendiumType | ~21 | Singular, Plural, Description | 需要进一步调研 |

**总计**：~226 个文件，每个文件修改 1-3 行

#### 保留 ILocalizationService（暂不执行 C2/C3/C4）

**原因**：
1. Settings 和 Window 已经通过 `.resx` 实现了中文翻译
2. 移除 `ILocalizationService` 需要修改 ~169 个文件，风险高
3. 保留现有架构不影响 C1 的汉化效果
4. 如果将来需要与上游对齐，再执行 C2/C3/C4

### 关于 Strings.resx 的问题

当前 `Strings.resx` 和 `Strings.zh.resx` 内容完全相同（都是中文）。这确实是一个架构问题，但不影响功能：
- `Strings.resx` 作为默认资源，包含中文
- `Strings.zh.resx` 作为中文卫星资源，也包含中文
- 无论系统语言是什么，都会显示中文

**如果将来需要支持英文用户**：
1. 将 `Strings.resx` 改为英文（作为默认回退）
2. 保留 `Strings.zh.resx` 为中文
3. 这样英文用户看到英文，中文用户看到中文

---

## 七、工作量估算

### C1 阶段工作量

| 模块 | 文件数 | 每文件修改行数 | 总修改行数 | 预估时间 |
|------|--------|--------------|-----------|---------|
| Column | 100 | 2 (Name+HelpText) | 200 | 2-3 小时 |
| Filter | 95 | 2 (Name+HelpText) | 190 | 2-3 小时 |
| Feature | 10 | 2 (Name+Description) | 20 | 15 分钟 |
| CompendiumType | 21 | 3 (Singular+Plural+Description) | 63 | 30 分钟 |
| **总计** | **226** | - | **473** | **5-7 小时** |

### 翻译工作量

需要翻译的英文字符串约 **400-500 个**，涵盖：
- Column 名称（如 "Name", "Quantity", "Market Board Price"）
- Column 帮助文本（如 "The name of the item.", "Shows the icon of the item."）
- Filter 名称（如 "Name", "Patch", "Is HQ"）
- Filter 帮助文本（如 "Searches by the name of the item.", "Filters by the patch the item was added in."）
- Feature 名称（如 "Basic", "Tooltips", "Marketboard Integration"）
- Feature 描述（如 "Configure the basic settings of Allagan Tools"）

---

## 八、风险与注意事项

### 8.1 编译风险

| 阶段 | 风险等级 | 说明 |
|------|---------|------|
| C1 | 🟢 极低 | 只改字符串值，不改代码结构 |
| C2 | 🟡 中等 | 修改构造函数签名，可能影响 DI |
| C3 | 🟡 中等 | 修改构造函数签名，可能影响 DI |
| C4 | 🔴 高 | 删除基础设施，需确保无残留引用 |

### 8.2 上游同步风险

| 策略 | 上游同步难度 | 说明 |
|------|------------|------|
| 保留 ILocalizationService | 🟡 中等 | 上游更新可能冲突，但 C1 的改动（字符串值）冲突容易解决 |
| 完全移除 | 🟢 低 | 与上游一致，同步最容易 |

**建议**：即使执行 C1，也保留 `ILocalizationService`。因为：
- C1 的改动（字符串值）与上游的改动（也是字符串值）冲突时，容易手动合并
- 如果将来上游也添加了本地化支持，Fork 的架构已经就绪

### 8.3 测试建议

1. **编译测试**：每修改一批文件后编译，确保无错误
2. **功能测试**：启动插件，检查 Column/Filter/Feature 的显示是否正常
3. **回归测试**：确保 Settings/Window 的中文显示不受影响

---

## 九、结论

1. **Fork 相比上游新增了完整的本地化基础设施**（`ILocalizationService`、`LocalizationService`、`.resx` 资源文件、`ChineseFontService`）

2. **但实际的 Column/Filter/Feature 模块仍未汉化**，Name/HelpText/Description 全部为硬编码英文

3. **Settings 和 Window 模块已经通过 `.resx` 实现了中文翻译**，这是 Fork 的主要汉化成果

4. **推荐立即执行 C1**：直接修改 Column/Filter/Feature 的字符串值为中文，不涉及架构改动，风险极低

5. **保留 `ILocalizationService`**：Settings 和 Window 的本地化已经工作正常，无需移除

6. **后续可选**：如果将来需要与上游完全对齐，再评估执行 C2/C3/C4 移除 `ILocalizationService`
