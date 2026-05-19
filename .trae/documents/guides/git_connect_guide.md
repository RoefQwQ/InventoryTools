# Git 连接配置指南

## 仓库结构

```
上游仓库: https://github.com/Critical-Impact/InventoryTools
    ↓ Fork
个人仓库: https://github.com/RoefQwQ/InventoryTools
    ↓ Clone
本地仓库: E:\Program Files (x86)\claude code\InventoryTools
```

## 配置步骤

### 1. 克隆个人 Fork

```bash
git clone https://github.com/RoefQwQ/InventoryTools.git
cd InventoryTools
```

### 2. 添加上游远程

```bash
git remote add upstream https://github.com/Critical-Impact/InventoryTools.git
```

### 3. 验证远程配置

```bash
git remote -v
# 应该显示:
# origin    https://github.com/RoefQwQ/InventoryTools.git (fetch)
# origin    https://github.com/RoefQwQ/InventoryTools.git (push)
# upstream  https://github.com/Critical-Impact/InventoryTools.git (fetch)
# upstream  https://github.com/Critical-Impact/InventoryTools.git (push)
```

### 4. 初始化子模块

```bash
git submodule update --init --recursive
```

## 日常操作

### 同步上游更新

```bash
# 1. 获取上游最新代码
git fetch upstream

# 2. 切换到主分支
git checkout main

# 3. 合并上游更新
git merge upstream/main

# 4. 推送到个人仓库
git push origin main
```

### 创建本地化分支

```bash
# 从 main 分支创建本地化分支
git checkout -b cn-localization

# 推送到个人仓库
git push -u origin cn-localization
```

### 提交本地化更改

```bash
# 1. 添加更改
git add .

# 2. 提交（使用规范的提交信息）
git commit -m "feat(localization): 添加中文翻译资源文件"

# 3. 推送到本地化分支
git push origin cn-localization
```

## 分支策略

```
main (上游同步)
    ↓
cn-localization (本地化工作)
    ↓
feature/xxx (具体功能分支，可选)
```

## 注意事项

1. **不要直接修改 main 分支**: main 分支用于同步上游更新
2. **定期同步上游**: 每周至少同步一次，避免冲突积累
3. **使用规范的提交信息**: 遵循 Conventional Commits 规范
4. **备份重要更改**: 在大规模重构前创建备份分支

## 常见问题

### 合并冲突
**问题**: 同步上游时出现冲突

**解决**:
1. 使用 `git status` 查看冲突文件
2. 手动解决冲突（保留本地化更改）
3. 使用 `git add` 标记已解决的文件
4. 使用 `git commit` 完成合并

### 子模块更新
**问题**: 子模块版本不一致

**解决**:
```bash
git submodule update --init --recursive
```

### 推送被拒绝
**问题**: 推送时提示需要先拉取

**解决**:
```bash
git pull --rebase origin cn-localization
git push origin cn-localization
```

## 相关链接

- [GitHub Fork 文档](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks)
- [Git 子模块文档](https://git-scm.com/book/en/v2/Git-Tools-Submodules)
- [Conventional Commits](https://www.conventionalcommits.org/)
