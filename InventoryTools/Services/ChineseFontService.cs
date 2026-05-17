using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Services;

/// <summary>
/// 中文字体加载服务，负责在 ImGui 字体构建阶段加载 Noto Sans CJK SC 字体。
/// </summary>
public class ChineseFontService : IHostedService, IDisposable
{
    private readonly ILogger<ChineseFontService> _logger;
    private readonly IUiBuilder _uiBuilder;
    private readonly InventoryToolsConfiguration _configuration;
    private readonly IDalamudPluginInterface _pluginInterface;

    private ImFontPtr? _chineseFont;
    private bool _disposed;

    public ChineseFontService(
        ILogger<ChineseFontService> logger,
        IUiBuilder uiBuilder,
        InventoryToolsConfiguration configuration,
        IDalamudPluginInterface pluginInterface)
    {
        _logger = logger;
        _uiBuilder = uiBuilder;
        _configuration = configuration;
        _pluginInterface = pluginInterface;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Starting ChineseFontService");
        _uiBuilder.BuildFonts += OnBuildFonts;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Stopping ChineseFontService");
        _uiBuilder.BuildFonts -= OnBuildFonts;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 中文字体是否已成功构建。
    /// </summary>
    public bool IsFontAvailable => _chineseFont.HasValue && _chineseFont.Value.NativePtr != IntPtr.Zero;

    /// <summary>
    /// 获取中文字体指针。
    /// </summary>
    public ImFontPtr? ChineseFont => _chineseFont;

    /// <summary>
    /// 触发字体重建（配置变更时调用）。
    /// </summary>
    public void RebuildFonts()
    {
        if (_configuration.ChineseFontEnabled)
        {
            _logger.LogInformation("Triggering font rebuild...");
            _uiBuilder.RebuildFonts();
        }
    }

    private void OnBuildFonts()
    {
        if (!_configuration.ChineseFontEnabled)
        {
            _logger.LogDebug("Chinese font is disabled in configuration.");
            _chineseFont = null;
            return;
        }

        var fontPath = ResolveFontPath();
        if (string.IsNullOrEmpty(fontPath))
        {
            _logger.LogWarning("Chinese font path is not configured and default font was not found.");
            _chineseFont = null;
            return;
        }

        if (!File.Exists(fontPath))
        {
            _logger.LogWarning("Chinese font file not found at path: {FontPath}", fontPath);
            _chineseFont = null;
            return;
        }

        try
        {
            var fontSize = _configuration.ChineseFontSize > 0
                ? _configuration.ChineseFontSize
                : 16.0f;

            var io = ImGui.GetIO();
            var glyphRanges = io.Fonts.GetGlyphRangesChineseFull();

            unsafe
            {
                var fontPtr = io.Fonts.AddFontFromFileTTF(
                    fontPath,
                    fontSize * ImGuiHelpers.GlobalScale,
                    null,
                    glyphRanges);

                if (fontPtr.NativePtr != IntPtr.Zero)
                {
                    _chineseFont = fontPtr;
                    _logger.LogInformation(
                        "Chinese font loaded successfully from {FontPath} with size {FontSize}",
                        fontPath,
                        fontSize);
                }
                else
                {
                    _logger.LogError("Failed to build Chinese font from {FontPath}", fontPath);
                    _chineseFont = null;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load Chinese font from {FontPath}", fontPath);
            _chineseFont = null;
        }
    }

    private string? ResolveFontPath()
    {
        if (!string.IsNullOrEmpty(_configuration.ChineseFontPath))
        {
            return _configuration.ChineseFontPath;
        }

        var pluginDir = _pluginInterface.AssemblyLocation.Directory?.FullName;
        if (!string.IsNullOrEmpty(pluginDir))
        {
            return Path.Combine(pluginDir, "Fonts", "NotoSansCJKsc-Regular.otf");
        }

        return null;
    }

    /// <summary>
    /// 在 ImGui 渲染循环中压入中文字体。
    /// </summary>
    public void PushFont()
    {
        if (!IsFontAvailable)
        {
            return;
        }

        try
        {
            ImGui.PushFont(_chineseFont!.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to push Chinese font.");
        }
    }

    /// <summary>
    /// 弹出中文字体（与 PushFont 配对使用）。
    /// </summary>
    public void PopFont()
    {
        if (!IsFontAvailable)
        {
            return;
        }

        try
        {
            ImGui.PopFont();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to pop Chinese font.");
        }
    }

    /// <summary>
    /// 创建一个字体作用域，用于 using 语句自动 Push/Pop 字体。
    /// </summary>
    public IDisposable? CreateFontScope()
    {
        if (!IsFontAvailable)
        {
            return null;
        }

        return new ChineseFontScope(this);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _uiBuilder.BuildFonts -= OnBuildFonts;
        _chineseFont = null;
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 中文字体作用域，用于 using 语句。
    /// </summary>
    private class ChineseFontScope : IDisposable
    {
        private readonly ChineseFontService _service;
        private bool _disposed;

        public ChineseFontScope(ChineseFontService service)
        {
            _service = service;
            _service.PushFont();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _service.PopFont();
            _disposed = true;
        }
    }
}
