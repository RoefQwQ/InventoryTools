# 国服插件封装打包指南

## 目录

1. [项目背景与目标](#1-项目背景与目标)
2. [国服环境说明](#2-国服环境说明)
3. [Fork 与仓库设置](#3-fork-与仓库设置)
4. [本地化实施步骤](#4-本地化实施步骤)
5. [构建与打包](#5-构建与打包)
6. [远程连接配置](#6-远程连接配置)
7. [发布与更新流程](#7-发布与更新流程)
8. [常见问题](#8-常见问题)

---

## 1. 项目背景与目标

### 1.1 背景

本项目基于 [RoefQwQ/InventoryTools](https://github.com/RoefQwQ/InventoryTools) Fork，目标是为 **FFXIV 国服** 环境提供完整的中文本地化插件。

### 1.2 目标

- 在 **个人 Fork 仓库** 内完成所有本地化工作
- **不向上游原项目推送任何代码**
- 构建可在国服客户端使用的插件包
- 提供远程仓库连接，方便个人使用和更新

### 1.3 工作原则

```
┌─────────────────────────────────────────────────────────────┐
│                     工作隔离原则                              │
├─────────────────────────────────────────────────────────────┤
│  ✓ 所有修改在个人 Fork 中进行                                │
│  ✗ 不向上游 (RoefQwQ/InventoryTools) 推送代码                │
│  ✓ 独立维护国服专用分支                                      │
│  ✓ 可自行发布插件包                                          │
└─────────────────────────────────────────────────────────────┘
```

---

## 2. 国服环境说明

### 2.1 国服与国际服差异

| 项目 | 国际服 | 国服 |
|------|--------|------|
| 客户端语言 | 英/日/德/法 | 简体中文 |
| Dalamud 版本 | 国际服版 | 国服适配版 |
| 插件仓库 | 官方插件库 | 第三方/自建源 |
| 游戏版本 | 最新 | 通常落后 1-2 个小版本 |

### 2.2 国服插件加载方式

国服通常使用以下方式加载 Dalamud 插件：

1. **XIVLauncher CN** - 国服启动器（内置 Dalamud）
2. **手动安装** - 将插件 DLL 放入指定目录
3. **自定义插件源** - 配置个人插件仓库 URL

### 2.3 插件目录位置

```
# XIVLauncher CN 插件目录（默认）
%APPDATA%\XIVLauncherCN\installedPlugins\InventoryTools\

# 或 Dalamud 开发插件目录
%APPDATA%\XIVLauncherCN\devPlugins\
```

---

## 3. Fork 与仓库设置

### 3.1 创建 Fork

1. 访问 https://github.com/RoefQwQ/InventoryTools
2. 点击右上角 **Fork** 按钮
3. 选择你的 GitHub 账号
4. 等待 Fork 完成

### 3.2 克隆到本地

```bash
# 1. 克隆你的 Fork（注意：是你的用户名，不是 RoefQwQ）
git clone https://github.com/你的用户名/InventoryTools.git
cd InventoryTools

# 2. 添加原项目作为 upstream（仅用于同步更新，不推送）
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git

# 3. 初始化子模块
git submodule update --init --recursive

# 4. 验证远程配置
git remote -v
# 预期输出：
# origin    https://github.com/你的用户名/InventoryTools.git (fetch)
# origin    https://github.com/你的用户名/InventoryTools.git (push)
# upstream  https://github.com/RoefQwQ/InventoryTools.git (fetch)
# upstream  https://github.com/RoefQwQ/InventoryTools.git (push)
```

### 3.3 创建国服专用分支

```bash
# 创建国服本地化分支
git checkout -b cn-server/main

# 推送分支到你的 Fork
git push -u origin cn-server/main
```

### 3.4 仓库结构规划

```
你的 Fork (你的用户名/InventoryTools)
│
├── main                    # 同步上游的主分支（尽量保持纯净）
│
├── cn-server/main          # 国服本地化主分支 ⭐ 主要工作分支
│   ├── 本地化字符串资源
│   ├── 国服适配修改
│   └── 中文插件元数据
│
└── feature/xxx             # 功能开发分支（从 cn-server/main 切出）
```

---

## 4. 本地化实施步骤

### 4.1 创建本地化基础设施

#### 4.1.1 创建资源文件目录

在 `InventoryTools/` 项目下创建：

```
InventoryTools/
└── Localization/
    ├── Resources/
    │   ├── Strings.resx          # 英文（默认/回退）
    │   ├── Strings.zh.resx       # 简体中文
    │   └── Strings.Designer.cs   # 自动生成的访问器
    └── LocalizationService.cs    # 本地化服务
```

#### 4.1.2 实现 LocalizationService

```csharp
// InventoryTools/Localization/LocalizationService.cs
using System;
using System.Globalization;
using System.Resources;
using Dalamud.Plugin;

namespace InventoryTools.Localization
{
    public class LocalizationService
    {
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public LocalizationService()
        {
            _resourceManager = new ResourceManager(
                "InventoryTools.Localization.Resources.Strings",
                typeof(LocalizationService).Assembly);
            
            // 默认使用简体中文
            _currentCulture = new CultureInfo("zh-CN");
        }

        public string this[string key] => GetString(key);

        public string GetString(string key)
        {
            var result = _resourceManager.GetString(key, _currentCulture);
            return result ?? key; // 如果找不到，返回 key 本身
        }

        public void SetCulture(string cultureName)
        {
            _currentCulture = new CultureInfo(cultureName);
        }
    }
}
```

#### 4.1.3 注册服务

在 `InventoryToolsPlugin.cs` 的 `ConfigureServices` 中添加：

```csharp
services.AddSingleton<LocalizationService>();
```

### 4.2 翻译 Localizers 目录

#### 4.2.1 改造 RoleLocalizer.cs

**改造前：**
```csharp
public string Format(RoleType instance)
{
    switch (instance)
    {
        case RoleType.Tank: return "Tank";
        case RoleType.DPSMelee: return "DPS (Melee)";
        // ...
    }
}
```

**改造后：**
```csharp
public class RoleLocalizer : ILocalizer<RoleType>
{
    private readonly LocalizationService _l10n;

    public RoleLocalizer(LocalizationService l10n)
    {
        _l10n = l10n;
    }

    public string Format(RoleType instance)
    {
        return instance switch
        {
            RoleType.Tank => _l10n["Role_Tank"],
            RoleType.DPSMelee => _l10n["Role_DPSMelee"],
            RoleType.DPSRanged => _l10n["Role_DPSRanged"],
            RoleType.Healer => _l10n["Role_Healer"],
            RoleType.Crafting => _l10n["Role_Crafting"],
            RoleType.Gathering => _l10n["Role_Gathering"],
            RoleType.Other => _l10n["Role_Other"],
            _ => _l10n["Role_Unknown"]
        };
    }
}
```

#### 4.2.2 添加资源字符串

在 `Strings.zh.resx` 中添加：

```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
  <data name="Role_Tank" xml:space="preserve">
    <value>防护职业</value>
  </data>
  <data name="Role_DPSMelee" xml:space="preserve">
    <value>进攻职业（近战）</value>
  </data>
  <data name="Role_DPSRanged" xml:space="preserve">
    <value>进攻职业（远程）</value>
  </data>
  <data name="Role_Healer" xml:space="preserve">
    <value>治疗职业</value>
  </data>
  <data name="Role_Crafting" xml:space="preserve">
    <value>能工巧匠</value>
  </data>
  <data name="Role_Gathering" xml:space="preserve">
    <value>大地使者</value>
  </data>
  <data name="Role_Other" xml:space="preserve">
    <value>其他</value>
  </data>
  <data name="Role_Unknown" xml:space="preserve">
    <value>未知</value>
  </data>
</root>
```

### 4.3 翻译 ItemLocalizer.cs（容器名称）

```xml
<!-- Strings.zh.resx 中添加 -->
<data name="Item_Bag1" xml:space="preserve">
  <value>物品栏 1</value>
</data>
<data name="Item_Bag2" xml:space="preserve">
  <value>物品栏 2</value>
</data>
<data name="Item_Bag3" xml:space="preserve">
  <value>物品栏 3</value>
</data>
<data name="Item_Bag4" xml:space="preserve">
  <value>物品栏 4</value>
</data>
<data name="Item_SaddlebagLeft" xml:space="preserve">
  <value>陆行鸟鞍囊（左）</value>
</data>
<data name="Item_SaddlebagRight" xml:space="preserve">
  <value>陆行鸟鞍囊（右）</value>
</data>
<data name="Item_PremiumSaddlebagLeft" xml:space="preserve">
  <value>陆行鸟追加鞍囊（左）</value>
</data>
<data name="Item_PremiumSaddlebagRight" xml:space="preserve">
  <value>陆行鸟追加鞍囊（右）</value>
</data>
<data name="Item_ArmoryBody" xml:space="preserve">
  <value>兵装库 - 身体</value>
</data>
<data name="Item_ArmoryHead" xml:space="preserve">
  <value>兵装库 - 头部</value>
</data>
<!-- ... 其他兵装库部位 ... -->
<data name="Item_GlamourChest" xml:space="preserve">
  <value>投影台</value>
</data>
<data name="Item_Currency" xml:space="preserve">
  <value>金币</value>
</data>
<data name="Item_Armoire" xml:space="preserve">
  <value>收藏柜</value>
</data>
<data name="Item_Empty" xml:space="preserve">
  <value>空</value>
</data>
<data name="Item_HQ" xml:space="preserve">
  <value>（高品质）</value>
</data>
<data name="Item_Collectible" xml:space="preserve">
  <value>（收藏品）</value>
</data>
<data name="Item_NQ" xml:space="preserve">
  <value>（普通品质）</value>
</data>
```

### 4.4 翻译插件元数据

创建 `InventoryTools/InventoryTools.zh.json`：

```json
{
  "Author": "Critical_Impact",
  "Name": "Allagan Tools",
  "Punchline": "在最终幻想14中轻松整理物品、定位物品位置、规划制作，并搜索怪物、副本、飞空艇和潜水艇！",
  "Description": "Allagan Tools 的主要功能是追踪你所有角色、雇员和部队仓库中的物品。通过筛选系统，你永远不会丢失物品或不知道它们应该被整理到哪里。\n\n虽然这是它的主要功能，但它还有许多其他功能，包括：\n\t- 制作规划\n\t- 市场板集成\n\t- 在搜索/整理和选择制作材料时高亮物品栏中的物品\n\t- 包含商店和来源信息等的物品窗口\n\t- 可完全搜索的副本/怪物/飞空艇/潜水艇窗口\n\t- 可设置复杂搜索的筛选系统\n\t- 鼠标悬停时显示物品位置的提示框集成",
  "Tags": ["物品栏", "整理", "筛选"],
  "InternalName": "InventoryTools",
  "RepoUrl": "https://github.com/你的用户名/InventoryTools",
  "LoadPriority": 0,
  "CanUnloadAsync": false,
  "IconUrl": "https://raw.githubusercontent.com/你的用户名/InventoryTools/cn-server/main/InventoryTools/Images/icon.png",
  "ImageUrls": [
    "https://raw.githubusercontent.com/你的用户名/InventoryTools/cn-server/main/InventoryTools/Images/banner1.png"
  ]
}
```

### 4.5 术语表

创建 `docs/GLOSSARY.md` 确保翻译一致性：

| 英文 | 简体中文 | 备注 |
|------|---------|------|
| Inventory | 物品栏 | |
| Retainer | 雇员 | |
| Free Company | 部队 | |
| Crafting | 制作 | 也译作"能工巧匠"（职业分类时）|
| Gathering | 采集 | 也译作"大地使者"（职业分类时）|
| Armory | 兵装库 | |
| Saddlebag | 陆行鸟鞍囊 | |
| Glamour Chest | 投影台 | |
| Armoire | 收藏柜 | |
| Currency | 金币 | |
| HQ | 高品质 | High Quality |
| NQ | 普通品质 | Normal Quality |
| Collectible | 收藏品 | |
| Desynthesis | 分解 | |
| Reduction | 精选 | |
| Marketboard | 市场板 | |
| Venture | 探险 | |
| Zodiac | 黄道武器 | 古武系列 |
| Anima | 元灵武器 | |
| Eurekan | 优雷卡武器 | |
| Resistance | 抵抗武器 | |
| Manderville | 曼德维尔武器 | |
| Phantom | 幻影武器 | |
| Mastercraft | 工匠武器 | |
| Skysteel | 天钢工具 | |
| Splendorous | 辉煌工具 | |
| Cosmic | 宇宙工具 | |

---

## 5. 构建与打包

### 5.1 环境要求

- .NET SDK（根据 `global.json` 中的版本）
- Visual Studio 2022 或 JetBrains Rider（推荐）
- Dalamud SDK（通过 NuGet 自动获取）

### 5.2 构建步骤

```bash
# 1. 进入项目目录
cd InventoryTools

# 2. 还原 NuGet 包
dotnet restore InventoryTools.sln

# 3. 构建 Release 版本
dotnet build InventoryTools.sln -c Release

# 4. 构建输出位置
# InventoryTools/bin/Release/InventoryTools.dll
```

### 5.3 打包为插件

Dalamud 插件的标准结构：

```
InventoryTools/
├── InventoryTools.dll          # 主程序集
├── InventoryTools.json         # 插件元数据
├── InventoryTools.pdb          # 调试符号（可选）
├── Images/                     # 图标资源
│   ├── icon.png
│   └── ...
└── Localization/               # 本地化资源
    └── Resources/
        ├── Strings.resources
        └── Strings.zh.resources
```

#### 5.3.1 手动打包

```powershell
# PowerShell 打包脚本
$pluginName = "InventoryTools"
$buildPath = "InventoryTools/bin/Release"
$outputPath = "dist/$pluginName"

# 创建输出目录
New-Item -ItemType Directory -Force -Path $outputPath

# 复制必要文件
Copy-Item "$buildPath/$pluginName.dll" $outputPath
Copy-Item "$buildPath/$pluginName.json" $outputPath
Copy-Item "$buildPath/$pluginName.pdb" $outputPath
Copy-Item -Recurse "$buildPath/Images" $outputPath

# 压缩为 zip
Compress-Archive -Path $outputPath -DestinationPath "dist/$pluginName.zip" -Force

Write-Host "打包完成: dist/$pluginName.zip"
```

#### 5.3.2 使用 DalamudPackager

项目已配置 `DalamudPackager.targets`，构建时会自动打包：

```bash
# Release 构建会自动生成插件包
dotnet build -c Release

# 输出位置（根据 targets 配置）
# 通常在 bin/Release/ 下的某个子目录
```

### 5.4 验证构建

```bash
# 检查 DLL 是否正确生成
ls InventoryTools/bin/Release/net8.0/InventoryTools.dll

# 检查元数据 JSON
ls InventoryTools/bin/Release/net8.0/InventoryTools.json
```

---

## 6. 远程连接配置

### 6.1 作为自定义插件源

你可以将你的 Fork 仓库配置为 Dalamud 的自定义插件源，这样可以直接从 GitHub 安装/更新插件。

#### 6.1.1 创建插件仓库索引

在你的 Fork 中创建 `repo.json`：

```json
{
  "$schema": "https://raw.githubusercontent.com/goatcorp/DalamudPlugins/api9/repo.json",
  "Author": "你的用户名",
  "Name": "国服 Allagan Tools 插件源",
  "Description": "Allagan Tools 国服中文本地化版本",
  "InternalName": "InventoryToolsCN",
  "AssemblyVersion": "1.0.0.0",
  "RepoUrl": "https://github.com/你的用户名/InventoryTools",
  "ApplicableVersion": "any",
  "DalamudApiLevel": 9,
  "LoadPriority": 0,
  "ImageUrls": [],
  "IconUrl": "",
  "Punchline": "Allagan Tools 国服中文版"
}
```

#### 6.1.2 创建插件清单

创建 `plugins/InventoryTools/latest.zip` 的目录结构，并更新清单文件。

### 6.2 手动安装方式

#### 6.2.1 开发插件目录

```bash
# 1. 构建插件
dotnet build -c Release

# 2. 复制到 Dalamud 开发插件目录
$source = "InventoryTools/bin/Release/net8.0/*"
$dest = "$env:APPDATA\XIVLauncherCN\devPlugins\InventoryTools"

New-Item -ItemType Directory -Force -Path $dest
Copy-Item -Path $source -Destination $dest -Recurse -Force

Write-Host "插件已复制到开发目录"
```

#### 6.2.2 在 Dalamud 中加载

1. 打开游戏，进入 Dalamud 设置
2. 启用 "开发插件" 选项
3. 添加开发插件目录路径
4. 重启游戏或重新加载插件

### 6.3 GitHub Raw 直连

你可以通过 GitHub Raw 直接提供插件文件下载链接：

```
# 插件元数据
https://raw.githubusercontent.com/你的用户名/InventoryTools/cn-server/main/InventoryTools/InventoryTools.json

# 插件 DLL（需要打包后上传）
https://github.com/你的用户名/InventoryTools/releases/download/v1.0.0/InventoryTools.zip
```

---

## 7. 发布与更新流程

### 7.1 版本管理

使用语义化版本号：`主版本.次版本.修订号`

```bash
# 创建版本标签
git tag -a v1.0.0 -m "国服初版发布"
git push origin v1.0.0
```

### 7.2 GitHub Releases 发布

1. 访问你的 Fork 仓库页面
2. 点击 **Releases** → **Create a new release**
3. 选择标签（如 v1.0.0）
4. 填写发布说明
5. 上传构建好的 `InventoryTools.zip`
6. 发布

### 7.3 更新流程

```bash
# 1. 同步上游更新
git checkout main
git fetch upstream
git merge upstream/main
git push origin main

# 2. 合并到国服分支
git checkout cn-server/main
git merge main

# 3. 解决冲突（如有）
# ...

# 4. 重新构建
dotnet build -c Release

# 5. 提交并推送
git add .
git commit -m "chore: sync upstream and rebuild for CN server"
git push origin cn-server/main

# 6. 创建新版本标签
git tag -a v1.0.1 -m "更新至上游最新版本"
git push origin v1.0.1
```

### 7.4 自动化脚本

创建 `scripts/build-and-release.ps1`：

```powershell
param(
    [Parameter(Mandatory=$true)]
    [string]$Version
)

# 颜色输出
function Write-Color($Text, $Color) {
    Write-Host $Text -ForegroundColor $Color
}

Write-Color "=== Allagan Tools 国服构建脚本 ===" Cyan

# 1. 检查分支
$currentBranch = git branch --show-current
if ($currentBranch -ne "cn-server/main") {
    Write-Color "错误：请在 cn-server/main 分支上运行此脚本" Red
    exit 1
}

# 2. 同步上游
Write-Color "正在同步上游更新..." Yellow
git fetch upstream
git merge upstream/main --no-edit

# 3. 构建
Write-Color "正在构建 Release 版本..." Yellow
dotnet clean InventoryTools.sln
dotnet build InventoryTools.sln -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Color "构建失败！" Red
    exit 1
}

# 4. 打包
Write-Color "正在打包..." Yellow
$distPath = "dist/v$Version"
New-Item -ItemType Directory -Force -Path $distPath

$buildPath = "InventoryTools/bin/Release/net8.0"
Copy-Item "$buildPath/InventoryTools.dll" $distPath
Copy-Item "$buildPath/InventoryTools.json" $distPath
Copy-Item "$buildPath/InventoryTools.pdb" $distPath
Copy-Item -Recurse "$buildPath/Images" $distPath

Compress-Archive -Path $distPath -DestinationPath "dist/InventoryTools-v$Version.zip" -Force

# 5. 提交
Write-Color "正在提交更改..." Yellow
git add .
git commit -m "release: v$Version for CN server"
git push origin cn-server/main

# 6. 打标签
Write-Color "创建版本标签..." Yellow
git tag -a "v$Version" -m "Release v$Version for FFXIV CN server"
git push origin "v$Version"

Write-Color "=== 构建完成 ===" Green
Write-Color "版本: v$Version" Green
Write-Color "输出: dist/InventoryTools-v$Version.zip" Green
Write-Color "请前往 GitHub Releases 页面发布此版本" Yellow
```

---

## 8. 常见问题

### 8.1 构建失败

**问题**：`dotnet build` 报错，找不到 Dalamud 引用

**解决**：
```bash
# 确保子模块已初始化
git submodule update --init --recursive

# 清理并重新还原
dotnet clean
dotnet restore
```

### 8.2 插件加载失败

**问题**：Dalamud 无法加载插件

**排查**：
1. 检查 `InventoryTools.json` 中的 `InternalName` 是否正确
2. 确认 DLL 和 JSON 文件在同一目录
3. 检查 Dalamud 日志（`%APPDATA%\XIVLauncherCN\dalamud.log`）

### 8.3 中文字符显示乱码

**问题**：插件界面中文显示为方块或乱码

**解决**：
1. 确保资源文件保存为 UTF-8 编码
2. 检查 ImGui 字体配置是否支持中文
3. 可能需要修改字体加载代码，使用中文字体

### 8.4 同步上游冲突

**问题**：合并 upstream 时出现冲突

**解决**：
```bash
# 查看冲突文件
git status

# 手动编辑解决冲突
# ...

# 标记为已解决
git add .
git commit -m "merge: resolve upstream conflicts"
```

### 8.5 子模块更新

**问题**：CriticalCommonLib 或 OtterGui 有更新

**解决**：
```bash
# 更新所有子模块
git submodule update --recursive --remote

# 提交子模块更新
git add CriticalCommonLib OtterGui
git commit -m "chore: update submodules"
```

---

## 附录：快速参考

### 常用 Git 命令

```bash
# 初始化
git clone https://github.com/你的用户名/InventoryTools.git
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git
git submodule update --init --recursive

# 日常开发
git checkout cn-server/main
git pull upstream main
git checkout -b feature/xxx

# 提交
git add .
git commit -m "描述"
git push origin feature/xxx

# 同步上游
git fetch upstream
git checkout main
git merge upstream/main
git checkout cn-server/main
git merge main
```

### 构建命令

```bash
# 还原包
dotnet restore

# Debug 构建
dotnet build

# Release 构建
dotnet build -c Release

# 清理
dotnet clean
```

### 目录结构速查

```
InventoryTools/
├── .github/                    # GitHub Actions 配置
├── CriticalCommonLib/          # 子模块（不要直接修改）
├── OtterGui/                   # 子模块（不要直接修改）
├── InventoryTools/             # 主插件项目
│   ├── Localization/           # ⭐ 新增：本地化资源
│   │   ├── Resources/
│   │   │   ├── Strings.resx
│   │   │   └── Strings.zh.resx
│   │   └── LocalizationService.cs
│   ├── Localizers/             # 现有本地化器（需要改造）
│   ├── UI/                     # 界面代码
│   ├── Images/                 # 图标资源
│   ├── InventoryTools.csproj   # 项目文件
│   └── InventoryTools.json     # 插件元数据
├── InventoryTools.sln          # 解决方案文件
├── global.json                 # .NET SDK 版本
└── README.md
```
