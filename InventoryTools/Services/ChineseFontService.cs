using System;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Services;

public class ChineseFontService : IHostedService
{
    private readonly ILogger<ChineseFontService> _logger;

    public ChineseFontService(ILogger<ChineseFontService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("ChineseFontService started (Dalamud native CJK support)");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("ChineseFontService stopped");
        return Task.CompletedTask;
    }

    public void PushFont()
    {
    }

    public void PopFont()
    {
    }

    public IDisposable? CreateFontScope()
    {
        return null;
    }
}
