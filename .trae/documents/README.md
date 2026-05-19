# InventoryTools 国服本地化项目文档

## 文档结构

```
.trae/documents/
├── README.md                           # 本文档（索引）
├── localization_progress_review.md     # 进度审查与优化计划（主文档）
├── architecture/
│   └── localization_architecture.md    # 本地化架构设计
├── progress/
│   ├── index.md                        # 进度文档索引
│   ├── sa1_localizer_analysis.md       # SA1: Localizer 分析
│   ├── sa1_group1_reform.md            # SA1-Group1: ILocalizer 改造方案
│   ├── sa1_group2_analysis.md          # SA1-Group2: 已有注入分析
│   ├── sa1_group3_reform.md            # SA1-Group3: 非接口改造方案
│   ├── sa1_service_design.md           # SA1-Service: 服务设计
│   ├── sa2_resources.md                # SA2: 资源文件方案
│   ├── sa3_metadata.md                 # SA3: 元数据本地化
│   └── sa4_font_config.md              # SA4: 字体配置方案
├── guides/
│   ├── build_guide.md                  # 国服构建指南
│   └── git_connect_guide.md            # Git 连接配置指南
└── reference/
    └── terminology.md                  # 术语表
```

## 文档分类说明

### 根目录
- **localization_progress_review.md**: 项目的主文档，包含完整的进度审查、问题分析和优化计划

### architecture/ - 架构设计
本地化系统的架构设计文档，描述技术方案和组件关系。

### progress/ - 进度文档
各阶段（SA1-SA5）的详细方案设计文档，按功能模块分类。

### guides/ - 操作指南
实际操作的步骤指南，包括构建、配置等。

### reference/ - 参考资料
术语表、API 参考等参考资料。

## 项目基本信息

| 项目 | 值 |
|------|-----|
| 项目名称 | Allagan Tools (InventoryTools) 国服中文本地化 |
| 目标版本 | v15.0.5 (Dalamud API 15) |
| 上游仓库 | https://github.com/Critical-Impact/InventoryTools |
| Fork 仓库 | https://github.com/RoefQwQ/InventoryTools |
| 工作分支 | cn-localization |

## 快速链接

- [进度审查与优化计划](localization_progress_review.md) - 查看项目整体进度
- [本地化架构设计](architecture/localization_architecture.md) - 了解技术架构
- [进度文档索引](progress/index.md) - 查看各阶段详细方案
- [术语表](reference/terminology.md) - 统一术语翻译
