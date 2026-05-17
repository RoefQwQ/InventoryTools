using System;
using System.Globalization;
using System.Resources;
using Dalamud.Plugin.Services;

namespace InventoryTools.Services;

public class LocalizationService : ILocalizationService
{
    private readonly ResourceManager _resourceManager;
    private readonly IPluginLog _pluginLog;
    private CultureInfo _currentCulture;

    public LocalizationService(IPluginLog pluginLog)
    {
        _pluginLog = pluginLog;

        _resourceManager = new ResourceManager(
            "InventoryTools.Localization.Resources.Strings",
            typeof(LocalizationService).Assembly);

        _currentCulture = new CultureInfo("zh-CN");

        _pluginLog.Debug("[LocalizationService] Initialized with culture: {Culture}", _currentCulture.Name);
    }

    public string this[string key] => GetString(key);

    public string GetString(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            _pluginLog.Warning("[LocalizationService] Empty key requested");
            return string.Empty;
        }

        try
        {
            var result = _resourceManager.GetString(key, _currentCulture);
            if (result != null)
            {
                return result;
            }

            _pluginLog.Warning("[LocalizationService] Key not found: {Key}", key);
            return key;
        }
        catch (Exception ex)
        {
            _pluginLog.Error(ex, "[LocalizationService] Failed to get string for key: {Key}", key);
            return key;
        }
    }

    public string GetString(string key, params object[] args)
    {
        var format = GetString(key);
        try
        {
            return string.Format(format, args);
        }
        catch (FormatException ex)
        {
            _pluginLog.Error(ex, "[LocalizationService] Format failed for key: {Key}", key);
            return format;
        }
    }

    public string CurrentCulture => _currentCulture.Name;

    public void SetCulture(string cultureName)
    {
        try
        {
            _currentCulture = new CultureInfo(cultureName);
            _pluginLog.Information("[LocalizationService] Culture changed to: {Culture}", cultureName);
        }
        catch (CultureNotFoundException ex)
        {
            _pluginLog.Error(ex, "[LocalizationService] Invalid culture: {Culture}", cultureName);
        }
    }
}
