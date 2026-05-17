# Git 基础操作与 Fork 工作流程指南

## 目录

1. [Git 基础操作原理](#1-git-基础操作原理)
2. [文件上传机制详解](#2-文件上传机制详解)
3. [Fork 操作后的正确工作流程](#3-fork-操作后的正确工作流程)
4. [远程仓库管理](#4-远程仓库管理)
5. [常见问题排查](#5-常见问题排查)

---

## 1. Git 基础操作原理

### 1.1 Git 的核心概念

Git 是一个**分布式版本控制系统**，其核心原理基于以下几点：

#### 1.1.1 工作区、暂存区、本地仓库、远程仓库

```
┌─────────────────────────────────────────────────────────────┐
│                        远程仓库 (Remote)                      │
│              例如: github.com/你的用户名/InventoryTools        │
└─────────────────────────────────────────────────────────────┘
                              ↑
                              │ git push / git fetch
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                       本地仓库 (.git)                         │
│  • 包含完整的项目历史记录                                      │
│  • 所有分支、标签、提交记录                                    │
│  • 存储在 .git 目录中                                         │
└─────────────────────────────────────────────────────────────┘
                              ↑
                              │ git commit
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                       暂存区 (Staging Area)                   │
│  • 通过 git add 添加的文件                                    │
│  • 准备被提交的快照                                           │
│  • 也称为 "Index"                                            │
└─────────────────────────────────────────────────────────────┘
                              ↑
                              │ git add
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                       工作区 (Working Directory)              │
│  • 你实际编辑的文件                                            │
│  • 可见的项目文件                                              │
└─────────────────────────────────────────────────────────────┘
```

#### 1.1.2 Git 对象模型

Git 内部使用四种核心对象来存储数据：

| 对象类型 | 说明 | 类比 |
|---------|------|------|
| **Blob** | 文件内容的二进制表示 | 文件内容快照 |
| **Tree** | 目录结构的快照，包含文件名和对应的 Blob 哈希 | 文件夹 |
| **Commit** | 提交记录，包含作者、时间、提交信息、父提交指针 | 版本节点 |
| **Tag** | 对特定提交的命名引用 | 书签 |

### 1.2 基本命令原理

#### git add

将工作区的修改添加到暂存区（Index）。Git 会计算文件内容的 SHA-1 哈希，创建 Blob 对象。

```bash
# 添加单个文件
git add filename.cs

# 添加所有修改
git add .

# 交互式添加
git add -p
```

#### git commit

将暂存区的内容保存为一个新的 Commit 对象，包含：
- 当前 Tree 对象的哈希
- 父提交的哈希（形成链式历史）
- 作者和提交者信息
- 时间戳
- 提交信息

```bash
# 提交暂存区的内容
git commit -m "提交信息"

# 修改最后一次提交
git commit --amend
```

#### git status

查看工作区、暂存区和本地仓库之间的差异状态。

```bash
git status
```

#### git log

查看提交历史，每个提交包含唯一的 SHA-1 哈希值。

```bash
# 简洁查看
git log --oneline

# 图形化查看分支
git log --graph --oneline --all
```

---

## 2. 文件上传机制详解

### 2.1 推送流程（git push）

```
本地仓库  ──git push──→  远程仓库
   │                        │
   │  1. 打包本地提交对象      │
   │  2. 计算差异              │
   │  3. 传输数据包            │
   │  4. 更新远程引用          │
   │                        │
   ▼                        ▼
```

#### 详细步骤：

1. **本地计算**：Git 计算本地分支与远程分支的差异
2. **打包对象**：将新的 Blob、Tree、Commit 对象打包成数据包
3. **传输数据**：通过 HTTPS 或 SSH 协议发送到远程服务器
4. **服务器验证**：GitHub 验证权限、检查冲突
5. **更新引用**：远程仓库更新分支指针到新的提交

### 2.2 拉取流程（git fetch / git pull）

```bash
# 仅下载远程更新，不合并
git fetch origin

# 下载并合并到当前分支
git pull origin main
```

#### git fetch 原理：
1. 连接远程仓库
2. 下载远程分支的最新 Commit 对象
3. 更新本地的远程跟踪分支（如 `origin/main`）
4. **不修改本地工作分支**

#### git pull = git fetch + git merge

### 2.3 分支管理

```bash
# 查看所有分支
git branch -a

# 创建并切换分支
git checkout -b feature/new-feature

# 切换分支
git checkout main

# 合并分支
git merge feature/new-feature

# 删除本地分支
git branch -d feature/new-feature
```

### 2.4 远程仓库配置

```bash
# 查看远程仓库
git remote -v

# 添加远程仓库
git remote add origin https://github.com/你的用户名/InventoryTools.git

# 添加上游仓库（原项目）
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git

# 修改远程 URL
git remote set-url origin https://github.com/新用户名/新仓库.git
```

---

## 3. Fork 操作后的正确工作流程

### 3.1 什么是 Fork

Fork 是 GitHub 提供的一项功能，它会在**你的个人账号下创建原项目的一个完整副本**。这个副本：
- 拥有独立的 Git 历史记录
- 你可以完全控制（推送、修改、删除）
- 与原项目没有直接的写入关联

```
原项目仓库                    你的 Fork 仓库
┌─────────────┐              ┌─────────────┐
│ RoefQwQ/    │   Fork       │ 你的用户名/  │
│ InventoryTools│  ───────→  │ InventoryTools│
│             │              │             │
│ 你无法推送   │              │ 你可以推送   │
└─────────────┘              └─────────────┘
```

### 3.2 重要原则：不要推送到原项目

**核心规则**：Fork 后，所有修改都应该推送到**你自己的 Fork 仓库**，而不是原项目（upstream）。

#### 为什么？

1. **权限限制**：通常你没有原项目的写入权限，推送会被拒绝
2. **协作规范**：即使你是协作者，也应该通过 Pull Request 提交变更
3. **项目隔离**：Fork 的目的就是让你自由实验而不影响原项目

### 3.3 正确的 Fork 工作流程

#### 步骤 1：Fork 原项目

在 GitHub 网页上操作：
1. 访问 `https://github.com/RoefQwQ/InventoryTools`
2. 点击右上角的 **Fork** 按钮
3. 选择你的个人账号作为目标

#### 步骤 2：克隆你的 Fork

```bash
# 克隆你的 Fork（不是原项目！）
git clone https://github.com/你的用户名/InventoryTools.git
cd InventoryTools

# 添加原项目作为 upstream（用于同步更新）
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git

# 验证配置
git remote -v
# 应该显示：
# origin    https://github.com/你的用户名/InventoryTools.git (fetch)
# origin    https://github.com/你的用户名/InventoryTools.git (push)
# upstream  https://github.com/RoefQwQ/InventoryTools.git (fetch)
# upstream  https://github.com/RoefQwQ/InventoryTools.git (push)
```

#### 步骤 3：创建功能分支

```bash
# 确保在 main 分支上
git checkout main

# 从 main 创建新分支
git checkout -b feature/localization-zh

# 现在你在 feature/localization-zh 分支上工作
```

#### 步骤 4：进行修改并提交

```bash
# 编辑文件...

# 添加修改到暂存区
git add .

# 提交修改
git commit -m "feat: add Simplified Chinese localization

- Localize UI strings
- Add Chinese resource files
- Update plugin metadata"
```

#### 步骤 5：推送到你的 Fork

```bash
# 推送到你的 Fork 的 feature 分支
git push origin feature/localization-zh

# 注意：origin 指向你的 Fork，不是原项目！
```

#### 步骤 6：同步原项目的更新（可选）

```bash
# 获取原项目的最新更新
git fetch upstream

# 切换到你的 main 分支
git checkout main

# 合并 upstream 的更新
git merge upstream/main

# 推送到你的 Fork
git push origin main
```

### 3.4 工作流程图示

```
┌─────────────────────────────────────────────────────────────────┐
│                        GitHub 平台                               │
│  ┌───────────────┐              ┌───────────────────────────┐  │
│  │ 原项目仓库     │              │ 你的 Fork 仓库             │  │
│  │ RoefQwQ/      │   Fork       │ 你的用户名/                │  │
│  │ InventoryTools│ ──────────→  │ InventoryTools            │  │
│  │               │              │                           │  │
│  │ 只读访问       │              │ 读写访问                   │  │
│  └───────────────┘              └───────────────────────────┘  │
│           ↑                              ↑                      │
│           │ fetch upstream               │ push origin          │
└───────────┼──────────────────────────────┼──────────────────────┘
            │                              │
┌───────────┼──────────────────────────────┼──────────────────────┐
│           │        你的本地仓库           │                      │
│           │                              │                      │
│  ┌────────┴────────┐            ┌────────┴────────┐            │
│  │ upstream/main   │            │ origin/main     │            │
│  │ (远程跟踪分支)   │            │ (远程跟踪分支)   │            │
│  └─────────────────┘            └─────────────────┘            │
│           ↑                              ↑                      │
│           │ merge                        │ checkout             │
│  ┌────────┴──────────────────────────────┴────────┐            │
│  │                  main (本地)                    │            │
│  └─────────────────────────────────────────────────┘            │
│                              │                                  │
│                              │ checkout -b                      │
│  ┌───────────────────────────┴───────────────────────────┐     │
│  │              feature/localization-zh (本地)             │     │
│  │              在这里进行所有修改                          │     │
│  └─────────────────────────────────────────────────────────┘     │
└─────────────────────────────────────────────────────────────────┘
```

---

## 4. 远程仓库管理

### 4.1 配置多个远程仓库

```bash
# 查看当前远程配置
git remote -v

# 添加原项目作为 upstream
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git

# 如果需要，也可以添加其他协作者的 Fork
git remote add collaborator https://github.com/协作者用户名/InventoryTools.git
```

### 4.2 切换远程 URL

```bash
# 从 HTTPS 切换到 SSH（推荐，更安全）
git remote set-url origin git@github.com:你的用户名/InventoryTools.git

# 验证
git remote -v
```

### 4.3 推送规则

| 操作 | 目标 | 命令 | 说明 |
|------|------|------|------|
| 推送功能分支 | 你的 Fork | `git push origin feature/xxx` | 正确 |
| 推送 main 分支 | 你的 Fork | `git push origin main` | 正确（同步后） |
| 推送到原项目 | RoefQwQ/ | `git push upstream main` | **错误！会被拒绝** |
| 获取原项目更新 | 原项目 | `git fetch upstream` | 正确 |

---

## 5. 常见问题排查

### 5.1 推送被拒绝

```bash
# 错误：remote: Permission to RoefQwQ/InventoryTools.git denied
# 原因：你尝试推送到原项目，而不是你的 Fork

# 解决方法：确认 remote 配置
git remote -v

# 确保 origin 指向你的 Fork
git remote set-url origin https://github.com/你的用户名/InventoryTools.git
```

### 5.2 合并冲突

```bash
# 当 upstream 有更新，且你也有本地修改时

# 1. 保存当前工作
git stash

# 2. 获取 upstream 更新
git fetch upstream
git checkout main
git merge upstream/main

# 3. 恢复你的工作
git stash pop

# 4. 解决冲突后提交
git add .
git commit -m "merge upstream changes"
```

### 5.3 子模块更新

```bash
# InventoryTools 包含子模块（CriticalCommonLib, OtterGui）
# 初始化子模块
git submodule update --init --recursive

# 更新子模块
git submodule update --recursive --remote
```

### 5.4 撤销错误推送

```bash
# 如果误推了敏感信息

# 1. 从本地历史中删除（谨慎使用！）
git reset --hard HEAD~1

# 2. 强制推送到远程（会覆盖远程历史）
git push origin main --force

# 注意：如果其他人已经拉取了你的提交，强制推送会造成问题
```

---

## 附录：常用命令速查表

```bash
# ===== 初始化 =====
git clone https://github.com/你的用户名/InventoryTools.git
git remote add upstream https://github.com/RoefQwQ/InventoryTools.git
git submodule update --init --recursive

# ===== 日常开发 =====
git checkout main
git pull upstream main          # 同步原项目更新
git push origin main            # 推送到你的 Fork
git checkout -b feature/xxx     # 创建功能分支

# ===== 提交修改 =====
git status                      # 查看状态
git add .                       # 添加所有修改
git commit -m "描述信息"         # 提交
git push origin feature/xxx     # 推送到你的 Fork

# ===== 同步更新 =====
git fetch upstream              # 获取原项目更新
git checkout main
git merge upstream/main         # 合并到本地 main
git push origin main            # 推送到你的 Fork

# ===== 分支管理 =====
git branch                      # 查看本地分支
git branch -a                   # 查看所有分支
git branch -d feature/xxx       # 删除本地分支
git push origin --delete feature/xxx  # 删除远程分支
```
