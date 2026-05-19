# Checklist

## 阶段 0: 资源文件架构修复

- [ ] Strings.resx 中无中文值（除 FFXIV 专有名词外）
- [ ] Strings.zh.resx 保留所有中文翻译值
- [ ] 两个文件的键完全一致（无缺失、无多余）
- [ ] 67 个代码引用但缺失的键已补充到 .resx
- [ ] 235 个未使用的资源键已清理
- [ ] 28 个含空格的非标准键名已重命名
- [ ] Setting_ 类键名与代码引用一致
- [ ] 编译通过，无资源键缺失警告

## 阶段 1: Column/Filter 本地化

- [ ] 所有 108 个 Column 类已添加 ILocalizationService 依赖注入
- [ ] 所有 Column 类的 Name 属性改为从资源键读取
- [ ] 所有 107 个 Filter 类已添加 ILocalizationService 依赖注入
- [ ] 所有 Filter 类的 Name 属性改为从资源键读取
- [ ] Strings.resx 已添加 215 个 Column/Filter 资源键（英文值）
- [ ] Strings.zh.resx 已添加 215 个 Column/Filter 资源键（中文值）
- [ ] 编译通过，无错误
- [ ] 测试通过

## 阶段 2: Extensions/Settings 本地化

- [ ] SettingCategoryExtensions.cs 中 17 个分类名已本地化
- [ ] SettingSubCategoryExtension.cs 中 30 个子分类名已本地化
- [ ] 相关资源键已添加到 .resx 文件
- [ ] 编译通过，无错误
- [ ] 设置页面分类显示正确

## 阶段 3: UI 窗口和页面本地化（新发现）

- [ ] Ui/Windows/FiltersWindow.cs 中 ~65 个字符串已本地化
- [ ] Services/ImGuiMenuService.cs 中 ~50 个菜单项已本地化
- [ ] Ui/Pages/CharacterRetainerPage.cs 中 ~30 个标签已本地化
- [ ] 其他 Ui/Pages/ 文件中 ~30 个字符串已本地化
- [ ] Logic/ItemRenderers/ 中 ~80 个渲染器名称已本地化
- [ ] Tooltips/ 中 ~20 个字符串已本地化
- [ ] Highlighting/ 中 ~15 个字符串已本地化
- [ ] Lists/ 中 ~10 个字符串已本地化
- [ ] 相关资源键已添加到 .resx 文件
- [ ] 编译通过，无错误
- [ ] 窗口和页面显示正确

## 阶段 4: Compendium/Feature 本地化

- [ ] 所有 20 个 CompendiumType 类已添加 ILocalizationService 依赖注入
- [ ] 所有 CompendiumType 类的 Description 属性改为从资源键读取
- [ ] 所有 7 个 Feature 类已添加 ILocalizationService 依赖注入
- [ ] 所有 Feature 类的 Name 和 Description 属性改为从资源键读取
- [ ] 相关资源键已添加到 .resx 文件
- [ ] 编译通过，无错误
- [ ] 测试通过

## 阶段 5: CriticalCommonLib 本地化

- [ ] EnumExtensions.cs 中 ~100 个 FormattedName() 字符串已本地化
- [ ] 其他 4 个 Extensions/ FormattedName 文件中 ~46 个字符串已本地化（新发现）
- [ ] CraftList.cs 中 33 个操作提示已本地化
- [ ] Models/ 中 ~25 个用户可见字符串已本地化（新发现）
- [ ] ChatUtilities.cs 中 6 个剪贴板提示已本地化（新发现）
- [ ] 其他 CriticalCommonLib 文件中 ~62 个用户可见字符串已本地化
- [ ] FormattedName() 方法签名已更新（接受 ILocalizationService 参数）
- [ ] 所有调用 FormattedName() 的地方已更新
- [ ] 相关资源键已添加到 .resx 文件
- [ ] 编译通过，无错误
- [ ] 枚举名称显示正确

## 阶段 6: OtterGui 本地化

- [ ] Widgets 中 ~45 个用户可见字符串已本地化
- [ ] Filesystem/Selector 中 ~30 个按钮和菜单文本已本地化
- [ ] SortMode.cs 中 16 个排序模式名称和描述已本地化
- [ ] CopyOnClickSelectable.cs 中 ~11 个复制提示已本地化（新发现）
- [ ] Classes 中 ~12 个提示标签已本地化
- [ ] Backup.cs 中 4 个备份恢复消息已本地化（新发现）
- [ ] ModifiableHotkey.cs 中 "No Key" 已本地化（新发现）
- [ ] YesNoColumn.cs 中 Yes/No 值已本地化
- [ ] 相关资源键已添加到 .resx 文件
- [ ] 编译通过，无错误
- [ ] UI 控件文本显示正确

## 阶段 7: 服务注册和配置修复

- [ ] GenericDecimalFilter 已正确注册到 Autofac
- [ ] GenericDecimalFilter Factory 返回类型错误已修复
- [ ] ConfigurationWizardService 重复注册已删除
- [ ] IngredientPatchSearchFilter 被错误排除问题已修复
- [ ] InventoryTools.json 和 InventoryTools.zh.json 版本号已统一
- [ ] 编译通过，无错误
- [ ] 所有 Filter 正确注册，无重复

## 最终验证

- [ ] Strings.resx 为英文回退文件（无中文值）
- [ ] Strings.zh.resx 为中文翻译文件
- [ ] 所有代码引用的键在 .resx 中都有定义
- [ ] 无重复键
- [ ] 完整编译通过（`dotnet build InventoryTools`）
- [ ] 所有测试通过（`dotnet test InventoryToolsTesting`）
- [ ] 中文环境下 UI 显示正确：
  - [ ] 列标题显示中文
  - [ ] 筛选器名称显示中文
  - [ ] 设置分类显示中文
  - [ ] 百科描述显示中文
  - [ ] 功能描述显示中文
  - [ ] 枚举名称显示中文
  - [ ] 操作提示显示中文
  - [ ] 窗口和页面标签显示中文
  - [ ] 菜单项显示中文
  - [ ] 渲染器名称显示中文
  - [ ] 提示框显示中文
  - [ ] OtterGui 控件文本显示中文

## 资源键命名规范

- [ ] Column 类使用 `Column_{ClassName}` 格式
- [ ] Filter 类使用 `Filter_{ClassName}` 格式
- [ ] Compendium 类使用 `Compendium_{TypeName}_Description` 格式
- [ ] Feature 类使用 `Feature_{FeatureName}_Name` / `Feature_{FeatureName}_Description` 格式
- [ ] 枚举使用 `Enum_{EnumType}_{Value}` 格式
- [ ] 设置分类使用 `SettingCategory_{Name}` 格式
- [ ] 设置子分类使用 `SettingSubCategory_{Name}` 格式
- [ ] OtterGui 使用 `OtterGui_{Component}_{Key}` 格式
- [ ] UI 窗口使用 `Window_{WindowName}_{Key}` 格式
- [ ] 菜单使用 `Menu_{MenuName}_{Key}` 格式
- [ ] 渲染器使用 `Renderer_{RendererName}` 格式
- [ ] 提示框使用 `Tooltip_{Key}` 格式

## 翻译质量检查

- [ ] FFXIV 术语使用官方中文翻译（参考 terminology.md）
- [ ] 技术术语保持一致（如"筛选器"、"列表"、"制作清单"）
- [ ] 翻译简洁明了，适合 UI 显示
- [ ] 避免过度翻译（如 NPC、API 等不翻译）
- [ ] 专有名词保留英文（如 Zodiac、Anima、Eureka 等武器阶段名）

## 不需要汉化的确认

- [ ] InventoryToolsTesting 确认无需汉化（测试项目）
- [ ] InventoryToolsMock 确认无需汉化（开发工具，最低优先级）
- [ ] OtterGuiInternal 确认无需汉化（底层 ImGui 封装）
- [ ] CriticalCommonLib 中 Addons/Agents/Enums/GameStructs/Helpers/Interfaces 确认无需汉化
- [ ] InventoryTools 中 Hotkeys/Host/Boot/Attributes/Converters/Enums/Images 确认无需汉化
