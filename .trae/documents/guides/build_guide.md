# 国服构建指南

## 前置条件

### 必需软件
- Visual Studio 2022 或 JetBrains Rider
- .NET 8 SDK
- Git

### 游戏数据
- FFXIV 游戏客户端（用于获取 EXD 数据）
- 或使用 GitHub Actions 自动下载

## 构建步骤

### 1. 克隆仓库

```bash
git clone https://github.com/RoefQwQ/InventoryTools.git
cd InventoryTools
git checkout cn-localization
git submodule update --init --recursive
```

### 2. 配置 NuGet 源

如果遇到 NuGet 下载问题，配置国内镜像源：

```xml
<!-- nuget.config -->
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    <add key="tencent" value="https://mirrors.cloud.tencent.com/nuget/" />
    <add key="aliyun" value="https://mirrors.aliyun.com/nuget/" />
  </packageSources>
</configuration>
```

### 3. 还原依赖

```bash
dotnet restore InventoryTools
dotnet restore InventoryToolsTesting
```

### 4. 下载 Dalamud SDK

```bash
# Linux/macOS
wget https://goatcorp.github.io/dalamud-distrib/latest.zip -O latest.zip
mkdir -p $HOME/.xlcore/dalamud/Hooks/dev/
unzip -o latest.zip -d "$HOME/.xlcore/dalamud/Hooks/dev/"

# Windows (PowerShell)
Invoke-WebRequest -Uri "https://goatcorp.github.io/dalamud-distrib/latest.zip" -OutFile "latest.zip"
Expand-Archive -Path "latest.zip" -DestinationPath "$env:USERPROFILE\.xlcore\dalamud\Hooks\dev\" -Force
```

### 5. 下载 EXD 数据

```bash
# 使用 ffxiv-downloader 工具
# 或从游戏目录复制 sqpack/ffxiv/0a0000.* 文件
```

### 6. 构建项目

```bash
dotnet build -c Debug InventoryTools
```

### 7. 运行测试

```bash
dotnet test -c Debug InventoryToolsTesting
```

## 常见问题

### NuGet 下载超时
**问题**: `dotnet restore` 时 SSL 错误或超时

**解决**:
1. 配置国内 NuGet 镜像源（见步骤 2）
2. 使用代理
3. 离线构建（预下载包）

### Dalamud SDK 找不到
**问题**: 编译时提示找不到 Dalamud 引用

**解决**:
1. 确认 `latest.zip` 已解压到正确位置
2. 检查环境变量 `DALAMUD_DIR` 是否设置
3. 重启 IDE

### EXD 数据缺失
**问题**: 测试时提示找不到游戏数据

**解决**:
1. 确认 EXD 文件已下载
2. 设置环境变量 `EXD_DATA_DIR` 指向 EXD 目录
3. 使用 `exd-data/.cachemeta.json` 验证数据完整性

## CI/CD

项目使用 GitHub Actions 进行自动化构建和测试：

- **test.yml**: 运行测试
- **build.yml**: 构建项目
- **pr.yml**: PR 检查
- **release.yml**: 发布版本

## 相关链接

- [Dalamud 开发文档](https://dalamud.dev/)
- [Lumina 文档](https://github.com/NotAdam/Lumina)
- [上游仓库](https://github.com/Critical-Impact/InventoryTools)
