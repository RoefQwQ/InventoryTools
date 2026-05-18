using System;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Services;

/// <summary>
/// 中文字体服务（当前未启用）
/// 
/// 设计说明：
/// - 当前实现为空壳，因为 Dalamud 已提供原生 CJK 字符支持
/// - 如果未来需要自定义字体功能（如特殊字体、字体切换等），可以在此实现
/// - 启用时需要在 InventoryToolsPlugin.cs 中注册为托管服务
/// 
/// 扩展指南：
/// 1. 实现 PushFont/PopFont/CreateFontScope 的具体逻辑
/// 2. 在需要的地方注入并使用此服务
/// 3. 在 InventoryToolsPlugin.cs 中取消注释托管服务注册
/// </summary>
public class ChineseFontService
{
    private readonly ILogger<ChineseFontService> _logger;

    public ChineseFontService(ILogger<ChineseFontService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 推入自定义字体（当前未实现）
    /// </summary>
    public void PushFont()
    {
        // 未来实现：切换到自定义字体
        _logger.LogDebug("PushFont called - not implemented");
    }

    /// <summary>
    /// 弹出自定义字体（当前未实现）
    /// </summary>
    public void PopFont()
    {
        // 未来实现：恢复到默认字体
        _logger.LogDebug("PopFont called - not implemented");
    }

    /// <summary>
    /// 创建字体作用域（当前未实现）
    /// </summary>
    /// <returns>字体作用域的 IDisposable 对象，当前返回 null</returns>
    public IDisposable? CreateFontScope()
    {
        // 未来实现：创建自动管理的字体作用域
        _logger.LogDebug("CreateFontScope called - not implemented");
        return null;
    }
}
