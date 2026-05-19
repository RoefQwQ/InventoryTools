# InventoryTools 完整汉化方案 Spec

## Why

InventoryTools 是 FFXIV 的 Dalamud 插件（国服本地化 fork），当前存在以下核心问题：

1. **资源文件架构缺陷**：Strings.resx（默认回退）和 Strings.zh.resx（中文）内容 100% 相同（均为中文），92.8% 的值是中文而非英文，导致非中文用户回退时看到中文界面
2. **大量硬编码英文字符串未本地化**：项目中存在约 1244 个硬编码英文字符串分散在 3 个项目中
3. **代码与资源键不同步**：67 个代码引用的键在 .resx 中缺失，235 个 .resx 键未被代码引用
4. **服务注册缺陷**：GenericDecimalFilter 缺失注册、ConfigurationWizardService 重复注册

## What Changes

### Phase 0: 资源文件架构修复（P0 严重）
- 将 Strings.resx 中 920 个中文值替换为英文回退文本
- 修复 67 个代码引用但 .resx 中缺失的键
- 清理 235 个未使用的资源键
- 修复 28 个含空格的非标准键名

### Phase 1: InventoryTools 主项目本地化（~743 个字符串）
- 108 个 Column Name 属性
- 107 个 Filter Name 属性
- 20 个 Compendium Description 属性
- 7 个 Feature Description 属性
- ~47 个 SettingCategory/SettingSubCategory 名称
- **~80 个 Logic/ItemRenderers/ 渲染器名称（新发现）**
- **~65 个 Ui/Windows/FiltersWindow.cs 字符串（新发现）**
- **~50 个 Services/ImGuiMenuService.cs 菜单项（新发现）**
- **~30 个 Ui/Pages/CharacterRetainerPage.cs 标签（新发现）**
- **~30 个其他 Ui/Pages/ 页面字符串（新发现）**
- **~20 个 Tooltips/ 提示框字符串（新发现）**
- **~15 个 Highlighting/ 高亮字符串（新发现）**
- **~10 个 Lists/ 列表字符串（新发现）**
- ~30 个其他 Services/ 文件字符串
- ~60 个 Debuggers/ 调试面板字符串（低优先级）

### Phase 2: CriticalCommonLib 本地化（~390 个字符串）
- ~100 个 EnumExtensions.cs 中的 FormattedName() 字符串
- **~46 个其他 Extensions/ 文件中的 FormattedName() 字符串（新发现）**
- 33 个 CraftList.cs 中的操作提示字符串
- **~25 个 Models/ 中的用户可见字符串（新发现，如 InventoryChange.cs、Character.cs）**
- **~6 个 Services/ChatUtilities.cs 剪贴板提示（新发现）**
- ~62 个其他文件中的用户可见字符串
- ~50 个日志/异常消息（低优先级）

### Phase 3: OtterGui 本地化（~164 个字符串）
- ~45 个 Widgets 中的用户可见字符串
- ~30 个 Filesystem/Selector 中的按钮和菜单文本
- ~16 个 Filesystem 核心中的排序模式文本
- ~12 个 Classes 中的提示和标签文本
- **~11 个 CopyOnClickSelectable.cs 中的 "Click to copy to clipboard." 引用（新发现）**
- **~4 个 Backup.cs 备份恢复消息（新发现）**
- **~1 个 ModifiableHotkey.cs "No Key"（新发现）**
- 2 个 Table/YesNoColumn.cs 是/否值

### Phase 4: 服务注册和配置修复
- 修复 GenericDecimalFilter 缺失注册
- 修复 ConfigurationWizardService 重复注册
- 修复 GenericDecimalFilter Factory 返回类型错误
- 修复版本号和仓库 URL 不一致

## Impact

- **Affected projects**: InventoryTools（主项目）、CriticalCommonLib（子模块）、OtterGui（子模块）
- **Affected code**: 约 400+ 个 .cs 文件、2 个 .resx 文件、2 个 .json 配置文件
- **Risk**: 子模块修改需要 fork 维护，上游更新可能覆盖
- **Not affected**: InventoryToolsTesting（测试项目，无需本地化）、InventoryToolsMock（开发工具，最低优先级）

## 项目整体结构

```
InventoryTools/                           # 解决方案根目录
├── .github/workflows/                    # CI/CD（5 个工作流）
├── .trae/                                # Trae IDE 配置（本地化文档）
│   ├── documents/
│   └── specs/complete-localization/      # 本规范
├── CriticalCommonLib/                    # 通用功能库（Git 子模块）
│   ├── Crafting/CraftList.cs             # 制作清单（33 个硬编码字符串）
│   ├── Enums/                            # 枚举定义（无 [Description] 特性）
│   ├── Extensions/
│   │   ├── EnumExtensions.cs             # 枚举格式化（~100 个硬编码字符串）
│   │   └── 其他 4 个 FormattedName 文件  # 额外 46 个字符串（新发现）
│   ├── Ipc/                              # IPC 服务
│   ├── MarketBoard/                      # 市场板
│   ├── Models/
│   │   ├── InventoryChange.cs            # 库存变更（新发现，用户可见）
│   │   └── Character.cs                  # 角色信息（新发现，用户可见）
│   ├── Services/
│   │   └── ChatUtilities.cs              # 剪贴板提示（新发现，6 个字符串）
│   └── 其他目录（Addons/Agents/Enums/GameStructs/Helpers/Interfaces - 无需汉化）
├── InventoryTools/                       # 主插件项目
│   ├── Commands/                         # 命令系统
│   ├── Compendium/Types/                 # 20 个百科类型
│   ├── Debuggers/                        # 调试器（~60 个字符串，低优先级）
│   ├── Extensions/
│   │   ├── SettingCategoryExtensions.cs  # 17 个分类名
│   │   └── SettingSubCategoryExtension.cs # 30 个子分类名
│   ├── Highlighting/                     # 高亮功能（新发现，~15 个字符串）
│   ├── IPC/                              # IPC 接口
│   ├── Lists/                            # 列表功能（新发现，~10 个字符串）
│   ├── Localization/Resources/
│   │   ├── Strings.resx                  # 默认资源（991 键，92.8% 中文 ⚠️）
│   │   └── Strings.zh.resx              # 中文资源（991 键，与 .resx 100% 相同 ⚠️）
│   ├── Localizers/                       # 12 个本地化器
│   ├── Logic/
│   │   ├── Columns/                      # 108 个列定义
│   │   ├── Features/                     # 7 个功能定义
│   │   ├── Filters/                      # 107 个筛选器定义
│   │   ├── ItemRenderers/                # 物品渲染器（新发现，~80 个字符串）
│   │   └── Settings/                     # 76 个设置（已汉化）
│   ├── Services/
│   │   ├── ILocalizationService.cs       # 本地化接口
│   │   ├── LocalizationService.cs        # 本地化实现（硬编码 zh-CN）
│   │   ├── ChineseFontService.cs         # 中文字体服务（已禁用）
│   │   └── ImGuiMenuService.cs           # 菜单服务（新发现，~50 个字符串）
│   ├── Tooltips/                         # 提示框（新发现，~20 个字符串）
│   ├── Ui/
│   │   ├── Pages/
│   │   │   ├── CharacterRetainerPage.cs  # 角色/雇员页面（新发现，~30 个字符串）
│   │   │   └── 其他页面                  # 新发现，~30 个字符串
│   │   └── Windows/
│   │       └── FiltersWindow.cs          # 筛选器窗口（新发现，~65 个字符串）
│   └── InventoryToolsPlugin.cs           # 插件入口 + Autofac 注册
├── OtterGui/                             # UI 框架库（Git 子模块）
│   ├── Widgets/                          # UI 控件（~45 个可见字符串）
│   ├── Filesystem/Selector/              # 文件系统选择器（~30 个可见字符串）
│   ├── Filesystem/SortMode.cs            # 排序模式（16 个可见字符串）
│   ├── Classes/Backup.cs                 # 备份恢复消息（新发现，4 个字符串）
│   ├── Text/Extended/
│   │   └── ImUtf8.CopyOnClickSelectable.cs # 复制提示（新发现，11 处引用）
│   ├── ModifiableHotkey.cs               # 热键绑定（新发现，"No Key"）
│   ├── Table/YesNoColumn.cs              # 是/否列（2 个字符串）
│   └── OtterGuiInternal/                 # 内部实现（确认无需汉化）
├── InventoryToolsMock/                   # 开发模拟环境（最低优先级，~50 个 UI 字符串）
└── InventoryToolsTesting/                # 测试项目（确认无需汉化）
```

## 可汉化结构完整分析

### 🔴 严重问题：资源文件架构

| 问题 | 详情 | 影响 |
|------|------|------|
| Strings.resx 包含中文 | 991 键中 920 个值是中文 | 非中文用户回退看到中文 |
| 两个文件 100% 相同 | Strings.zh.resx 是 Strings.resx 的复制品 | 翻译文件形同虚设 |
| 67 个键缺失定义 | 代码引用但 .resx 中不存在 | 运行时显示键名 |
| 235 个键未被引用 | .resx 定义但代码不使用 | 资源浪费 |
| 28 个非标准键名 | 包含空格和标点 | 命名规范违反 |

### 已汉化内容（无需额外工作）

| 内容类型 | 条目数 | 状态 |
|---------|--------|------|
| .resx 资源键（有效） | ~756 | ✅ 已翻译（需修复回退） |
| Settings 设置 | 76 个类 | ✅ 已通过 .resx 翻译 |
| Window 窗口 | 13 个窗口 | ✅ 已通过 .resx 翻译 |
| 命令帮助文本 | 21 条 | ✅ 已通过 .resx 翻译 |
| JSON 元数据 | 2 文件 | ✅ 已翻译 |
| 游戏数据字段 | 自动 | ✅ 跟随游戏语言 |

### 待汉化内容（按项目分组，含新发现）

#### InventoryTools 主项目

| 内容类型 | 文件数 | 字符串数 | 优先级 | 状态 |
|---------|--------|---------|--------|------|
| Column Name 属性 | 108 | 108 | 高 | 已知 |
| Filter Name 属性 | 107 | 107 | 高 | 已知 |
| SettingCategory 名称 | 1 | 17 | 高 | 已知 |
| SettingSubCategory 名称 | 1 | 30 | 高 | 已知 |
| ItemRenderers 渲染器名称 | ~15 | ~80 | 高 | **新发现** |
| FiltersWindow 窗口字符串 | 1 | ~65 | 高 | **新发现** |
| ImGuiMenuService 菜单项 | 1 | ~50 | 高 | **新发现** |
| CharacterRetainerPage 标签 | 1 | ~30 | 高 | **新发现** |
| 其他 Ui/Pages/ 页面 | ~5 | ~30 | 中 | **新发现** |
| Tooltips 提示框 | ~3 | ~20 | 中 | **新发现** |
| Compendium Description | 20 | 20 | 中 | 已知 |
| Feature Description | 7 | 7 | 中 | 已知 |
| Highlighting 高亮 | ~3 | ~15 | 中 | **新发现** |
| Lists 列表 | ~2 | ~10 | 中 | **新发现** |
| Debuggers 调试面板 | ~5 | ~60 | 低 | 已知 |
| **小计** | **~380** | **~743** | - | - |

#### CriticalCommonLib（子模块）

| 内容类型 | 文件数 | 字符串数 | 优先级 | 状态 |
|---------|--------|---------|--------|------|
| EnumExtensions FormattedName() | 1 | ~100 | 高 | 已知 |
| 其他 Extensions FormattedName() | 4 | ~46 | 高 | **新发现** |
| CraftList 操作提示 | 1 | 33 | 高 | 已知 |
| Models 用户可见字符串 | ~3 | ~25 | 中 | **新发现** |
| ChatUtilities 剪贴板提示 | 1 | 6 | 中 | **新发现** |
| 其他用户可见字符串 | ~10 | ~62 | 中 | 已知 |
| 日志/异常消息 | ~15 | ~50 | 低 | 已知 |
| **小计** | **~35** | **~322** | - | - |

#### OtterGui（子模块）

| 内容类型 | 文件数 | 字符串数 | 优先级 | 状态 |
|---------|--------|---------|--------|------|
| Widgets UI 控件 | 13 | ~45 | 高 | 已知 |
| Filesystem/Selector 按钮菜单 | 4 | ~30 | 高 | 已知 |
| Filesystem 排序模式 | 1 | 16 | 高 | 已知 |
| CopyOnClickSelectable 复制提示 | 1 | ~11 | 中 | **新发现** |
| Classes 提示标签 | 5 | ~12 | 中 | 已知 |
| Backup 备份恢复消息 | 1 | 4 | 中 | **新发现** |
| ModifiableHotkey 热键 | 1 | 1 | 中 | **新发现** |
| Table 列值 | 1 | 2 | 中 | 已知 |
| **小计** | **~27** | **~121** | - | - |

#### 不需要汉化的项目

| 项目/目录 | 原因 |
|----------|------|
| InventoryToolsTesting | 测试项目，字符串为测试数据和断言消息 |
| InventoryToolsMock | 开发工具，最低优先级（~50 个 UI 字符串） |
| OtterGuiInternal | 底层 ImGui 封装，无用户可见文本 |
| CriticalCommonLib/Addons/ | 结构体定义 |
| CriticalCommonLib/Agents/ | 代理定义 |
| CriticalCommonLib/Enums/ | 枚举声明（无 [Description]） |
| CriticalCommonLib/GameStructs/ | 游戏结构体 |
| CriticalCommonLib/Helpers/ | 辅助类 |
| CriticalCommonLib/Interfaces/ | 接口定义 |
| InventoryTools/Hotkeys/ | 无用户可见字符串 |
| InventoryTools/Host/ | 无用户可见字符串 |
| InventoryTools/Boot/ | 无用户可见字符串 |
| InventoryTools/Attributes/ | 无用户可见字符串 |
| InventoryTools/Converters/ | 无用户可见字符串 |
| InventoryTools/Enums/ | 无用户可见字符串 |
| InventoryTools/Images/ | 无用户可见字符串 |

#### 总计

| 项目 | 字符串数 | 文件数 |
|------|---------|--------|
| InventoryTools | ~743 | ~380 |
| CriticalCommonLib | ~322 | ~35 |
| OtterGui | ~121 | ~27 |
| **总计** | **~1186** | **~442** |

## 汉化实现机制

### 现有本地化架构

```
用户操作 → Dalamud 插件加载
    ↓
InventoryToolsPlugin.ConfigureServices()
    ↓
Autofac 注册 LocalizationService（单例）
    ↓
ResourceManager 初始化，绑定 Strings 资源
    ↓
根据 CultureInfo.CurrentUICulture 选择语言（当前硬编码 zh-CN）
    ↓
消费者通过 ILocalizationService["key"] 获取翻译
```

### 汉化实现模式

#### 模式 1: Settings 类（已实现，参考模式）

```csharp
public class AutoSaveSetting : BooleanSetting
{
    public AutoSaveSetting(ILocalizationService localizationService)
        : base("AutoSave", ...) { }
}
```

#### 模式 2: Column/Filter 类（待实现）

```csharp
// 当前（硬编码）
public class NameColumn : Column
{
    public override string Name { get; } = "Name";
}

// 改造后（资源键）
public class NameColumn : Column
{
    private readonly ILocalizationService _localizationService;
    public NameColumn(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public override string Name => _localizationService["Column_Name"];
}
```

#### 模式 3: EnumExtensions FormattedName()（待实现）

```csharp
// 当前（硬编码）
public static string FormattedName(this InventoryCategory category)
{
    return category switch
    {
        InventoryCategory.Currency => "Currency",
        ...
    };
}

// 改造后（资源键）
public static string FormattedName(this InventoryCategory category, ILocalizationService loc)
{
    return category switch
    {
        InventoryCategory.Currency => loc["Enum_InventoryCategory_Currency"],
        ...
    };
}
```

#### 模式 4: OtterGui 框架级字符串（待实现）

```csharp
// 方案：通过构造函数注入翻译委托或使用静态本地化服务
// 由于 OtterGui 是子模块，需要在应用层提供翻译覆盖
```

## ADDED Requirements

### Requirement: 资源文件架构修复
系统 SHALL 将 Strings.resx 恢复为英文回退文件，确保 Strings.zh.resx 是唯一的中文翻译文件。

### Requirement: Column Name 本地化
系统 SHALL 为所有 Column 类的 Name 属性提供本地化支持。

### Requirement: Filter Name 本地化
系统 SHALL 为所有 Filter 类的 Name 属性提供本地化支持。

### Requirement: Compendium Description 本地化
系统 SHALL 为所有 CompendiumType 类的 Description 属性提供本地化支持。

### Requirement: Feature Description 本地化
系统 SHALL 为所有 Feature 类的 Description 属性提供本地化支持。

### Requirement: CriticalCommonLib 枚举本地化
系统 SHALL 为所有 EnumExtensions.FormattedName() 方法提供本地化支持。

### Requirement: OtterGui UI 文本本地化
系统 SHALL 为 OtterGui 中的用户可见 UI 文本提供本地化支持。

### Requirement: 服务注册完整性
系统 SHALL 确保所有 Filter/Column/Feature 类正确注册到 Autofac 容器。

## MODIFIED Requirements

### Requirement: 现有 .resx 资源文件

当前 Strings.resx 和 Strings.zh.resx 各有 991 条资源键，需要：
- 修复 Strings.resx 中 920 个中文值为英文
- 新增约 1186 个本地化资源键
- 清理 235 个未使用键
- 最终预期约 1942 个有效资源键

## REMOVED Requirements

无
