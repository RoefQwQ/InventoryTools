# InventoryTools 国服本地化项目 - 进度审查与优化计划

## 一、项目概述

**项目名称**: Allagan Tools (InventoryTools) 国服中文本地化
**目标版本**: v15.0.5 (Dalamud API 15)
**仓库**: RoefQwQ/InventoryTools (Fork from Critical-Impact/InventoryTools)
**工作分支**: cn-localization

---

## 二、已执行工作盘点

### 2.1 基础设施搭建 ✅

| 任务 | 状态 | 文件/位置 | 备注 |
|------|------|-----------|------|
| Git 仓库连接配置 | ✅ 完成 | `docs/GIT_CONNECT_GUIDE.md` | origin 指向个人 Fork |
| .gitignore 配置 | ✅ 完成 | `.gitignore` | docs/ 目录已排除 |
| 子模块初始化 | ✅ 完成 | `CriticalCommonLib/`, `OtterGui/` | 已克隆 |
| 国服构建指南 | ✅ 完成 | `docs/CN_SERVER_BUILD_GUIDE.md` v2.0 | 已修正技术错误 |

### 2.2 核心本地化架构 ✅

| 任务 | 状态 | 文件 | 备注 |
|------|------|------|------|
| ILocalizationService 接口 | ✅ 已创建 | `InventoryTools/Services/ILocalizationService.cs` | 5 个方法 |
| LocalizationService 实现 | ✅ 已创建 | `InventoryTools/Services/LocalizationService.cs` | 使用 ResourceManager |
| Autofac 注册 | ✅ 已修改 | `InventoryToolsPlugin.cs` 第200行 | 在 Singleton 区域注册 |

### 2.3 SA2-SA5 实际执行验收 ✅

#### SA2: 中文资源文件 (Strings.zh.resx)
- **状态**: ⚠️ 部分完成
- **文件**: `InventoryTools/Strings.zh.resx` (根目录，非标准位置)
- **问题**: 
  - 文件位于项目根目录而非 `Localization/Resources/` 子目录
  - 与 LocalizationService 中 ResourceManager 路径不匹配
  - 需要验证实际内容完整性

#### SA3: 插件元数据本地化
- **状态**: ✅ 已完成
- **文件**: `InventoryTools/InventoryTools.zh.json`
- **内容验证**:
  - Name: "亚拉戈工具箱 (Allagan Tools)"
  - Punchline: 中文宣传语
  - Description: 完整中文描述（保留所有功能列表）
  - Tags: ["物品栏", "整理", "筛选"]
  - AssemblyVersion: "15.0.5"
  - DalamudApiLevel: 15

#### SA4: 中文字体配置
- **状态**: ✅ 已完成
- **文件**: `InventoryTools/Services/ChineseFontService.cs`
- **注册**: `InventoryToolsPlugin.cs` 第181行已注册为 HostedService
- **功能**: 
  - 支持 Noto Sans CJK SC 字体加载
  - 配置项: ChineseFontEnabled, ChineseFontSize, ChineseFontPath
  - PushFont/PopFont/CreateFontScope API
  - 完整异常处理和回退机制

#### SA5: LocalizationService + Autofac 注册
- **状态**: ✅ 已完成
- **文件**: 
  - `InventoryTools/Services/ILocalizationService.cs`
  - `InventoryTools/Services/LocalizationService.cs`
- **注册**: `InventoryToolsPlugin.cs` 第201行
- **验证**: 代码结构正确，但编译未验证（网络问题）

### 2.4 方案设计文档 ✅

| 文档 | 状态 | 内容 |
|------|------|------|
| `PROGRESS_SA1_ANALYSIS.md` | ✅ | Localizers/ 目录结构分析（13 个文件分类） |
| `PROGRESS_SA1_GROUP1.md` | ✅ | 无注入 ILocalizer 改造方案（6 个文件） |
| `PROGRESS_SA1_GROUP2.md` | ✅ | 已有注入 ILocalizer 分析（无需改造） |
| `PROGRESS_SA1_GROUP3.md` | ✅ | 非接口 Localizer 改造方案（4 个文件） |
| `PROGRESS_SA1_SERVICE.md` | ✅ | LocalizationService + Autofac 注册方案 |
| `PROGRESS_SA2.md` | ✅ | Strings.zh.resx 完整内容（185 个键） |
| `PROGRESS_SA3.md` | ✅ | InventoryTools.zh.json 元数据本地化 |
| `PROGRESS_SA4.md` | ✅ | 中文字体配置方案（ImGui） |

---

## 三、待执行工作

### 3.1 代码改造（高优先级）

#### Group 1: 无注入 ILocalizer<T>（6 个文件）
- [ ] `RoleLocalizer.cs` - 8 个资源键
- [ ] `RelicWeaponTypeLocalizer.cs` - 46 个资源键
- [ ] `RelicWeaponCategoryLocalizer.cs` - 6 个资源键
- [ ] `RelicToolTypeLocalizer.cs` - 34 个资源键
- [ ] `RelicToolCategoryLocalizer.cs` - 5 个资源键
- [ ] `ChocoboItemSourceTypeLocalizer.cs` - 3 个资源键

#### Group 3: 非接口 Localizer（4 个文件）
- [ ] `ItemLocalizer.cs` - 57 个资源键（最复杂）
- [ ] `IngredientPreferenceLocalizer.cs` - 7 个资源键
- [ ] `CraftItemLocalizer.cs` - 3 个资源键
- [ ] `CraftGroupingLocalizer.cs` - 无需改造（预留注入）

### 3.2 资源文件创建（高优先级）- SA2 修复
- [ ] 移动 `Strings.zh.resx` 到 `InventoryTools/Localization/Resources/Strings.zh.resx`
- [ ] 创建 `InventoryTools/Localization/Resources/Strings.resx`（英文回退）
- [ ] 验证资源文件内容完整性（185 个键）
- [ ] 配置 `.csproj` 嵌入资源
- [ ] 更新 LocalizationService 中 ResourceManager 路径（如果需要）

### 3.3 中文字体配置（中优先级）- SA4 已完成 ✅
- [x] 创建 `ChineseFontService.cs`
- [x] 配置 `InventoryToolsConfiguration` 扩展
- [ ] 添加字体文件到 `Fonts/` 目录（已下载，待确认位置）
- [x] 在 `InventoryToolsPlugin.cs` 注册服务

### 3.4 插件元数据（低优先级）- SA3 已完成 ✅
- [x] 创建 `InventoryTools/InventoryTools.zh.json`
- [x] 验证所有字段完整

---

## 四、已发现问题

### 4.1 严重问题

#### 问题 1: Subagent 并行执行失败
- **现象**: 多个 subagent 同时写入同一文件导致内容丢失
- **影响**: `PROGRESS_SA1_GROUP1.md` 和 `PROGRESS_SA1_GROUP3.md` 内容不完整
- **根因**: 所有 subagent 被指示写入独立文件，但部分任务返回结果被截断
- **解决**: 改为串行执行或确保每个 subagent 只写自己的文件

#### 问题 2: 编译环境网络问题
- **现象**: `dotnet build` 时 NuGet 包下载超时/SSL 错误
- **影响**: 无法验证代码编译
- **根因**: 网络环境不稳定或 NuGet 源访问受限
- **解决**: 
  - 方案 A: 配置国内 NuGet 镜像源
  - 方案 B: 使用 `dotnet restore --source` 指定备用源
  - 方案 C: 离线构建（预下载包）

#### 问题 7: SA2 资源文件位置错误 [新增]
- **现象**: `Strings.zh.resx` 创建在项目根目录 `InventoryTools/Strings.zh.resx`
- **影响**: LocalizationService 中 ResourceManager 路径指向 `InventoryTools.Localization.Resources.Strings`，无法找到资源
- **根因**: 未按标准目录结构创建
- **解决**: 
  - 移动文件到 `InventoryTools/Localization/Resources/Strings.zh.resx`
  - 创建对应目录结构
  - 验证 ResourceManager 基名称匹配

#### 问题 8: ~~SA3 元数据文件未创建~~ [已修正]
- **状态**: ✅ 文件已存在 `InventoryTools/InventoryTools.zh.json`
- **说明**: 初次审阅时遗漏检查，文件实际已完成
- **内容**: Name、Punchline、Description、Tags 均已中文化

### 4.2 中等问题

#### 问题 3: 术语一致性
- **现象**: 不同文档中同一术语翻译不一致
- **示例**: 
  - "Glamour Chest" → "投影台" vs "幻化柜"
  - "Armory" → "兵装库" vs "军械库"
- **影响**: 用户体验不一致
- **解决**: 建立统一的术语表并强制执行

#### 问题 4: 资源键命名不统一
- **现象**: 
  - Group 1 使用 `Role_Tank`, `RelicWeaponType_ZodiacBase`
  - Group 3 使用 `Container_Bag1`, `Item_HQ`
- **影响**: 维护困难
- **解决**: 制定命名规范文档

### 4.3 轻微问题

#### 问题 5: 文档分散
- **现象**: 8 个 PROGRESS 文件分散在 docs/ 目录
- **影响**: 查找困难
- **解决**: 合并为单一 PROGRESS.md 或建立索引

#### 问题 6: 字体文件下载
- **现象**: 终端显示已下载 `NotoSansCJKsc-Regular.otf` (16MB)
- **状态**: 未确认是否放入正确位置
- **解决**: 验证文件位置并配置 `.csproj`

---

## 五、优化建议

### 5.1 工作流程优化

#### 建议 1: 串行执行替代并行
```
当前: 5 个 subagent 同时执行 → 部分失败
优化: 按依赖关系串行执行
  1. 先执行 SA5 (LocalizationService) - 基础依赖
  2. 再执行 SA1_GROUP1 (ILocalizer 改造)
  3. 然后 SA1_GROUP3 (非接口改造)
  4. 最后 SA2 (资源文件) + SA4 (字体)
```

#### 建议 2: 统一术语表
```
创建: docs/TERMINOLOGY.md
内容: 所有术语的官方翻译 + 使用场景
维护: 任何翻译工作前必须先查阅
```

#### 建议 3: 自动化验证
```
添加: GitHub Actions 工作流
触发: 每次推送时
任务: 
  - 检查术语一致性
  - 验证资源键完整性
  - 编译检查（如果环境允许）
```

### 5.2 代码质量优化

#### 建议 4: 统一资源键命名规范
```
格式: {Category}_{SubCategory}_{Name}
示例:
  Role_Tank                    (OK)
  RelicWeaponType_ZodiacBase   (OK)
  Container_Bag1               (OK)
  Item_HQ                      (OK)
  
避免:
  Unknown                      (太通用)
  Base                         (重复)
```

#### 建议 5: 添加单元测试
```
测试内容:
  - 所有资源键都有对应的中文翻译
  - LocalizationService 能正确读取
  - 回退机制工作正常
```

### 5.3 文档优化

#### 建议 6: 合并进度文档
```
当前: 8 个分散文件
优化: 
  docs/
  ├── PROGRESS.md              # 主进度文件（索引）
  ├── PROGRESS/
  │   ├── 01-analysis.md       # 原 SA1_ANALYSIS
  │   ├── 02-ilocalizer.md     # 原 SA1_GROUP1
  │   ├── 03-non-interface.md  # 原 SA1_GROUP3
  │   ├── 04-service.md        # 原 SA1_SERVICE
  │   ├── 05-resources.md      # 原 SA2
  │   ├── 06-metadata.md       # 原 SA3
  │   └── 07-font.md           # 原 SA4
```

---

## 六、风险评估

| 风险 | 概率 | 影响 | 缓解措施 |
|------|------|------|----------|
| 编译持续失败 | 高 | 高 | 配置国内 NuGet 源 |
| 术语不一致 | 中 | 中 | 建立术语表审查流程 |
| 资源键遗漏 | 中 | 中 | 添加自动化检查 |
| 字体加载失败 | 低 | 中 | 多层回退机制 |
| 上游更新冲突 | 中 | 低 | 定期同步测试 |

---

## 七、下一步行动计划

### 阶段 1: 修复基础问题（1-2 天）
1. [ ] 配置国内 NuGet 镜像源
2. [ ] 验证编译环境
3. [ ] 统一术语表
4. [ ] 整理文档结构

### 阶段 2: 核心改造（3-5 天）
1. [ ] 改造 Group 1（6 个 ILocalizer）
2. [ ] 改造 Group 3（3 个非接口 Localizer）
3. [ ] 创建资源文件
4. [ ] 编译验证

### 阶段 3: 增强功能（2-3 天）
1. [ ] 中文字体配置
2. [ ] 插件元数据本地化
3. [ ] 测试验证

### 阶段 4: 发布准备（1 天）
1. [ ] 打包测试
2. [ ] 文档更新
3. [ ] 创建 Release

---

## 八、总结

### 已完成
- ✅ 基础设施搭建（Git、文档、子模块）
- ✅ 核心架构设计（LocalizationService、Autofac 注册）
- ✅ SA4 中文字体配置（ChineseFontService.cs + 注册）
- ✅ SA5 LocalizationService + Autofac 注册
- ✅ 详细改造方案（8 个文档覆盖所有 Localizer）

### 待完成
- 🔄 SA1 实际代码改造（10 个 Localizer 文件）
- 🔄 SA2 资源文件修复（移动到正确位置 + 验证内容）
- 🔄 编译验证（依赖网络环境修复）

### 关键阻塞
1. **网络问题**: NuGet 包下载失败，需要配置镜像源
2. **SA2 位置错误**: Strings.zh.resx 在根目录而非 Localization/Resources/

---

## 九、审阅记录

### 第一次审阅 (2026-05-17)
**审阅人**: AI Assistant
**范围**: SA2/SA3/SA4/SA5 实际执行状态

#### 审阅方法
1. 文件系统检查：验证实际创建的文件
2. 代码审查：检查文件内容和位置
3. 与方案文档对比：确认执行与方案的一致性

#### 审阅结果
| SA | 预期 | 实际 | 状态 |
|----|------|------|------|
| SA2 | Strings.zh.resx + 目录结构 | 根目录 Strings.zh.resx | ⚠️ 位置错误 |
| SA3 | InventoryTools.zh.json | 文件已创建，内容完整 | ✅ 完成 |
| SA4 | ChineseFontService + 注册 | 文件 + 注册均完成 | ✅ 完成 |
| SA5 | LocalizationService + 注册 | 文件 + 注册均完成 | ✅ 完成 |

#### 发现的新问题
1. **SA2 路径不匹配**: LocalizationService 中 ResourceManager 基名称为 `InventoryTools.Localization.Resources.Strings`，但文件实际在根目录
2. ~~**SA3 完全缺失**: 虽然方案详细，但未创建实际文件~~ **[已修正]** 文件实际已存在
3. **字体文件位置**: NotoSansCJKsc-Regular.otf 已下载但位置未确认

#### 修正措施
- 更新本文档第 2.3 节添加 SA2-SA5 验收详情
- 更新第 3.2/3.3/3.4 节反映实际状态
- 新增问题 7 到第 4.1 节
- 问题 8 标记为已修正
- 更新第 8 节总结

---

*审查日期: 2026-05-17*
*审查人: AI Assistant*
*版本: v1.1*
