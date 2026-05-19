# Tasks

## 阶段 0: 资源文件架构修复（P0 严重，必须最先执行）

- [ ] Task 0.1: 将 Strings.resx 中 920 个中文值替换为英文回退文本
  - 读取 Strings.resx 中所有包含中文字符的值
  - 为每个中文值提供对应的英文翻译
  - 保留 Strings.zh.resx 中的中文值不变
  - 确保两个文件的键完全一致

- [ ] Task 0.2: 修复 67 个代码引用但 .resx 中缺失的键
  - 主要是 Setting_ 类键名不匹配（代码重构后键名改变）
  - 方案A：修改 .resx 中的键名匹配代码（推荐）
  - 方案B：修改代码中的键名匹配 .resx
  - 缺失键包括：Setting_addToActiveCraftListContextMenu_*、Setting_ItemSearchContextMenu_*、Setting_moreInfoContextMenu_* 等

- [ ] Task 0.3: 清理 235 个未使用的资源键
  - 删除 28 个含空格的非标准命令描述键
  - 删除 4 个 PluginInfo 键（如确认不使用）
  - 删除 18 个 IngredientPreference 键（如确认不使用）
  - 统一 Setting_ 类旧命名键后删除冗余

- [ ] Task 0.4: 修复 28 个含空格的非标准键名
  - 重命名为标准 `Prefix_SubKey` 格式
  - 示例：`Shows the allagan tools items list window.` → `Command_ShowItemList_Description`

- [ ] Task 0.5: 验证资源文件修复
  - 确认 Strings.resx 中无中文值（除专有名词外）
  - 确认 Strings.zh.resx 中保留所有中文值
  - 确认代码引用的键在 .resx 中都有定义
  - 编译通过

## 阶段 1: InventoryTools 主项目 - Column/Filter 本地化（高优先级）

- [ ] Task 1.1: 为 108 个 Column 类添加 ILocalizationService 依赖注入
  - 修改构造函数，注入 ILocalizationService
  - 将 `public override string Name { get; } = "English"` 改为 `public override string Name => _localizationService["Column_{ClassName}"]`
  - 同步处理 FilterKey、FilterDescription 等属性

- [ ] Task 1.2: 为 107 个 Filter 类添加 ILocalizationService 依赖注入
  - 同 Task 1.1 模式
  - 资源键格式：`Filter_{ClassName}`

- [ ] Task 1.3: 更新 Strings.resx 和 Strings.zh.resx
  - 添加 108 个 `Column_*` 资源键
  - 添加 107 个 `Filter_*` 资源键
  - 英文值使用原始硬编码字符串
  - 中文值使用翻译后的字符串

- [ ] Task 1.4: 验证 Column/Filter 本地化
  - 编译项目
  - 运行测试
  - 验证列标题和筛选器名称显示正确

## 阶段 2: InventoryTools 主项目 - Extensions/Settings 本地化（高优先级）

- [ ] Task 2.1: 本地化 SettingCategoryExtensions.cs（17 个分类名）
  - "General" → "通用"、"Visuals" → "视觉"、"Marketboard" → "市场板" 等

- [ ] Task 2.2: 本地化 SettingSubCategoryExtension.cs（30 个子分类名）

- [ ] Task 2.3: 更新 .resx 资源文件
  - 添加 `SettingCategory_*` 和 `SettingSubCategory_*` 资源键

- [ ] Task 2.4: 验证设置分类本地化
  - 编译通过
  - 设置页面分类显示正确

## 阶段 3: InventoryTools 主项目 - UI 窗口和页面本地化（高优先级，新发现）

- [ ] Task 3.1: 本地化 Ui/Windows/FiltersWindow.cs（~65 个字符串）
  - 筛选器窗口中的所有 UI 文本
  - 按钮、标签、提示文本

- [ ] Task 3.2: 本地化 Services/ImGuiMenuService.cs（~50 个菜单项）
  - 右键菜单项
  - 菜单分隔符文本

- [ ] Task 3.3: 本地化 Ui/Pages/CharacterRetainerPage.cs（~30 个标签）
  - 角色和雇员页面的标签文本

- [ ] Task 3.4: 本地化其他 Ui/Pages/ 文件（~30 个字符串）
  - 其他页面中的标签和提示文本

- [ ] Task 3.5: 本地化 Logic/ItemRenderers/（~80 个渲染器名称）
  - 物品渲染器的显示名称

- [ ] Task 3.6: 本地化 Tooltips/（~20 个字符串）
  - 提示框中的文本

- [ ] Task 3.7: 本地化 Highlighting/（~15 个字符串）
  - 高亮功能中的文本

- [ ] Task 3.8: 本地化 Lists/（~10 个字符串）
  - 列表功能中的文本

- [ ] Task 3.9: 更新 .resx 资源文件
  - 添加约 300 个 UI 相关资源键

- [ ] Task 3.10: 验证 UI 本地化
  - 编译通过
  - 窗口和页面显示正确

## 阶段 4: InventoryTools 主项目 - Compendium/Feature 本地化（中优先级）

- [ ] Task 4.1: 为 20 个 CompendiumType 类添加本地化
  - 注入 ILocalizationService
  - Description 属性改为资源键读取
  - 资源键格式：`Compendium_{TypeName}_Description`

- [ ] Task 4.2: 为 7 个 Feature 类添加本地化
  - 注入 ILocalizationService
  - Name 和 Description 属性改为资源键读取

- [ ] Task 4.3: 更新 .resx 资源文件
  - 添加 20 个 Compendium 和 7 个 Feature 资源键

- [ ] Task 4.4: 验证 Compendium/Feature 本地化
  - 编译通过
  - 百科和功能描述显示正确

## 阶段 5: CriticalCommonLib 本地化（高优先级）

- [ ] Task 5.1: 本地化 EnumExtensions.cs（~100 个字符串）
  - FormattedName() 方法需要注入 ILocalizationService
  - 涉及枚举：InventoryCategory、InventoryType、InventoryChangeReason、CharacterType 等
  - 资源键格式：`Enum_{EnumType}_{Value}`
  - **注意**：FormattedName() 是扩展方法，需要改为接受 ILocalizationService 参数或使用静态服务定位器

- [ ] Task 5.2: 本地化其他 Extensions/ FormattedName 文件（~46 个字符串，新发现）
  - 4 个其他扩展文件中的 FormattedName() 方法
  - 同 Task 5.1 模式

- [ ] Task 5.3: 本地化 CraftList.cs（33 个操作提示）
  - "Retrieve" → "取出"、"Gather" → "采集"、"Buy" → "购买"、"Craft" → "制作" 等

- [ ] Task 5.4: 本地化 Models/ 用户可见字符串（~25 个，新发现）
  - InventoryChange.cs：库存变更描述
  - Character.cs：角色信息显示

- [ ] Task 5.5: 本地化 ChatUtilities.cs（6 个剪贴板提示，新发现）
  - 复制到剪贴板的用户提示消息

- [ ] Task 5.6: 本地化其他 CriticalCommonLib 文件（~62 个用户可见字符串）

- [ ] Task 5.7: 更新 .resx 资源文件
  - 添加约 272 个 CriticalCommonLib 相关资源键

- [ ] Task 5.8: 验证 CriticalCommonLib 本地化
  - 编译通过
  - 枚举名称和操作提示显示正确

## 阶段 6: OtterGui 本地化（中优先级）

- [ ] Task 6.1: 本地化 Widgets 中的用户可见字符串（~45 个）
  - Changelog.cs：变更日志选项和确认按钮
  - ColorPicker.cs：颜色选择器右键菜单
  - HexViewer.cs：Hex 查看器标题和按钮
  - ImGuiResizingTextInput.cs：拖拽提示
  - WidgetUtil.cs：复制提示
  - PaletteColorPicker.cs：默认颜色名

- [ ] Task 6.2: 本地化 Filesystem/Selector 中的按钮和菜单（~30 个）
  - FileSystemSelector.Buttons.cs：Add New Folder、Delete、Expand、Collapse
  - FileSystemSelector.Context.cs：Lock/Unlock、Dissolve Folder、Quick Move、Rename
  - FileSystemSelector.Draw.cs：Filter 占位文本
  - FileSystemSelector.DragDrop.cs：Moving 提示

- [ ] Task 6.3: 本地化 SortMode.cs（16 个排序模式名称和描述）

- [ ] Task 6.4: 本地化 CopyOnClickSelectable.cs（~11 个复制提示，新发现）
  - "Click to copy to clipboard." 的 11 处引用

- [ ] Task 6.5: 本地化 Classes 中的提示标签（~12 个）
  - PerformanceTracker.cs：计时器标签
  - Backup.cs：备份恢复消息（4 个，新发现）

- [ ] Task 6.6: 本地化 ModifiableHotkey.cs（1 个，新发现）
  - "No Key" → "未设置"

- [ ] Task 6.7: 本地化 YesNoColumn.cs（2 个值）
  - "Yes"/"No" → "是"/"否"

- [ ] Task 6.8: 更新 .resx 资源文件
  - 添加约 121 个 OtterGui 相关资源键

- [ ] Task 6.9: 验证 OtterGui 本地化
  - 编译通过
  - UI 控件文本显示正确

## 阶段 7: 服务注册和配置修复

- [ ] Task 7.1: 修复 GenericDecimalFilter 缺失注册
  - 在 InventoryToolsPlugin.cs 中添加 GenericDecimalFilter 的 Autofac 注册
  - 修复 Factory 委托返回类型错误（当前错误返回 GenericIntegerFilter）

- [ ] Task 7.2: 修复 ConfigurationWizardService 重复注册
  - 删除 InventoryToolsPlugin.cs 第 215 行的重复注册

- [ ] Task 7.3: 修复 IngredientPatchSearchFilter 被错误排除问题
  - 因继承 GenericDecimalFilter 间接实现了 IGenericFilter，导致被 IFilter 扫描排除
  - 需要调整注册逻辑

- [ ] Task 7.4: 修复版本号和仓库 URL 不一致
  - InventoryTools.json: AssemblyVersion "1.15.0.5"
  - InventoryTools.zh.json: AssemblyVersion "15.0.5"
  - 统一版本号格式

- [ ] Task 7.5: 验证服务注册修复
  - 编译通过
  - 所有 Filter 正确注册
  - 无重复注册

## 阶段 8: 最终验证

- [ ] Task 8.1: 完整编译验证
  - 运行 `dotnet build InventoryTools`
  - 确保无编译错误

- [ ] Task 8.2: 运行测试套件
  - 运行 `dotnet test InventoryToolsTesting`
  - 确保所有测试通过

- [ ] Task 8.3: 资源文件完整性检查
  - 验证 Strings.resx 为英文回退
  - 验证 Strings.zh.resx 为中文翻译
  - 验证所有代码引用的键都有定义
  - 验证无重复键

- [ ] Task 8.4: 中文环境 UI 验证
  - 列标题显示中文
  - 筛选器名称显示中文
  - 设置分类显示中文
  - 百科描述显示中文
  - 枚举名称显示中文
  - 操作提示显示中文
  - 窗口和页面标签显示中文
  - 菜单项显示中文
  - 渲染器名称显示中文
  - 提示框显示中文

# Task Dependencies

```
阶段 0（资源文件修复）→ 阶段 1-6（可并行）→ 阶段 7（服务注册）→ 阶段 8（最终验证）
```

- Task 0.1 → 0.2 → 0.3 → 0.4 → 0.5（串行）
- 阶段 1-6 可以并行执行（均依赖阶段 0 完成）
- 阶段 7 依赖阶段 1-6 完成
- 阶段 8 依赖阶段 7 完成

# 子模块注意事项

CriticalCommonLib 和 OtterGui 是 Git 子模块，修改时需要注意：
1. 需要在 fork 的子模块仓库中创建分支
2. 修改后更新主项目的子模块引用
3. 上游更新时需要 rebase 或 merge
4. 资源键建议统一放在 InventoryTools 的 .resx 中，通过 ILocalizationService 接口跨项目访问
