# 本地化架构设计

## 架构概述

InventoryTools 的本地化系统采用 .NET 标准的 `ResourceManager` + `.resx` 资源文件方案，通过依赖注入（Autofac）提供本地化服务。

## 核心组件

### 1. LocalizationService

**接口**: `ILocalizationService`
**实现**: `LocalizationService`
**位置**: `InventoryTools/Services/`

```csharp
public interface ILocalizationService
{
    string GetString(string key);
    string GetString(string key, params object[] args);
    string GetItemName(uint itemId);
    string GetTypeName(string type, uint id);
    void ReloadResources();
}
```

**注册**: `InventoryToolsPlugin.cs` 第 201 行，Autofac Singleton

### 2. 资源文件结构

```
InventoryTools/
├── Localization/
│   └── Resources/
│       ├── Strings.resx        # 英文回退（默认）
│       └── Strings.zh.resx     # 中文翻译
├── InventoryTools.zh.json      # 插件元数据中文版
└── Localizers/                 # 各模块的本地化器
    ├── ILocalizer.cs           # 本地化器接口
    ├── ItemLocalizer.cs        # 物品名称本地化
    ├── RoleLocalizer.cs        # 角色职业本地化
    └── ...                     # 其他本地化器
```

### 3. Localizer 分类

#### Group 1: 无注入 ILocalizer<T>（需改造）
| 文件 | 资源键数 | 状态 |
|------|---------|------|
| RoleLocalizer.cs | 8 | 待改造 |
| RelicWeaponTypeLocalizer.cs | 46 | 待改造 |
| RelicWeaponCategoryLocalizer.cs | 6 | 待改造 |
| RelicToolTypeLocalizer.cs | 34 | 待改造 |
| RelicToolCategoryLocalizer.cs | 5 | 待改造 |
| ChocoboItemSourceTypeLocalizer.cs | 3 | 待改造 |

#### Group 2: 已有注入（无需改造）
已有依赖注入的 Localizer，直接使用 ResourceManager。

#### Group 3: 非接口 Localizer（需改造）
| 文件 | 资源键数 | 状态 |
|------|---------|------|
| ItemLocalizer.cs | 57 | 待改造（最复杂） |
| IngredientPreferenceLocalizer.cs | 7 | 待改造 |
| CraftItemLocalizer.cs | 3 | 待改造 |
| CraftGroupingLocalizer.cs | - | 无需改造（预留注入） |

### 4. 中文字体服务

**文件**: `InventoryTools/Services/ChineseFontService.cs`
**注册**: `InventoryToolsPlugin.cs` 第 181 行，HostedService

**功能**:
- 支持 Noto Sans CJK SC 字体加载
- 配置项: ChineseFontEnabled, ChineseFontSize, ChineseFontPath
- PushFont/PopFont/CreateFontScope API
- 完整异常处理和回退机制

## 数据流

```
用户操作 → Dalamud 插件加载
    ↓
InventoryToolsPlugin.Initialize()
    ↓
Autofac 注册服务
    ↓
LocalizationService 初始化
    ↓
ResourceManager 加载 .resx 文件
    ↓
根据系统语言选择资源文件
    ↓
Localizer 调用 GetString() 获取翻译
```

## 关键决策

1. **选择 ResourceManager + .resx**: .NET 标准方案，工具链成熟
2. **Autofac 单例**: 保证全局一致的本地化状态
3. **分离式 Localizer**: 每个模块有自己的 Localizer，便于维护
4. **字体服务独立**: ChineseFontService 作为 HostedService，生命周期独立

## 待解决问题

1. SA2 资源文件位置错误（需移动到正确目录）
2. 编译环境网络问题（NuGet 下载超时）
3. 术语一致性（需建立术语表）
