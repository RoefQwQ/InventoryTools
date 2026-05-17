namespace InventoryTools.Services;

public interface ILocalizationService
{
    string this[string key] { get; }

    string GetString(string key);

    string GetString(string key, params object[] args);

    string CurrentCulture { get; }

    void SetCulture(string cultureName);
}
