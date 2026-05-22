using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using CriticalCommonLib.Models;
using Dalamud.Configuration;
using Dalamud.Interface.Colors;
using InventoryTools.Attributes;
using InventoryTools.Converters;
using InventoryTools.Logic;
using InventoryTools.Logic.Editors;
using InventoryTools.Logic.Filters;
using InventoryTools.Logic.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OtterGui.Classes;

namespace InventoryTools
{
    [Serializable]
    public class InventoryToolsConfiguration : IPluginConfiguration, IConfigurable<bool?>, IConfigurable<int?>, IConfigurable<Enum?>, IConfigurable<Dictionary<Type, bool>>, IConfigurable<Vector4?>, IConfigurable<uint?>
    {
        [JsonIgnore]
        public bool IsDirty { get; set; }

        private bool _colorRetainerList = true;

        private bool _displayCrossCharacter = true;
        private bool _displayTooltip = true;

        private Vector4 _highlightColor = new (0.007f, 0.008f,
            0.007f, 0.212f);

        private Vector4 _destinationHighlightColor = new Vector4(0.321f, 0.239f, 0.03f, 1f);

        private Vector4 _retainerListColor = ImGuiColors.HealerGreen;

        private string _highlightWhen = "When Searching";
        private bool _invertHighlighting = true;
        private bool _invertDestinationHighlighting;
        private bool _invertTabHighlighting;
        private bool _highlightDestination;
        private bool _highlightDestinationEmpty;

        private bool _isVisible;
        private bool _showItemNumberRetainerList = true;
        private bool _historyEnabled;

        private Vector4 _tabHighlightColor = new (0.007f, 0.008f,
            0.007f, 0.2f);

        public List<FilterConfiguration> FilterConfigurations = new();

        public Dictionary<ulong, Character> SavedCharacters = new();

        public bool InventoriesMigrated { get; set; } = false;
        public bool InventoriesMigratedToCsv { get; set; } = false;

        private HashSet<string>? _openWindows = new();
        private Dictionary<string, Vector2>? _savedWindowPositions = new();
        private List<InventoryChangeReason> _historyTrackReasons = new();
        private HashSet<string>? _windowsIgnoreEscape = new HashSet<string>();
        private Dictionary<string, bool>? _booleanSettings = new();
        private Dictionary<string, int>? _integerSettings = new();
        private Dictionary<string, uint>? _uintegerSettings = new();
        private Dictionary<string, Vector4>? _vector4Settings = new();
        private Dictionary<string, Enum>? _enumSettings = new();
        private Dictionary<string, Dictionary<Type, bool>>? _typeDictionarySettings = new();
        private Dictionary<string, List<string>>? _listSettings = new();

        public void ClearDirtyFlags()
        {
            this.IsDirty = false;
            foreach (var filter in FilterConfigurations)
            {
                filter.ConfigurationDirty = false;
                filter.TableConfigurationDirty = false;
            }
        }

        public HashSet<string> WindowsIgnoreEscape
        {
            get => _windowsIgnoreEscape ??= new();
            set
            {
                _windowsIgnoreEscape = value;
                IsDirty = true;
            }
        }

        public bool HistoryEnabled
        {
            get => _historyEnabled;
            set
            {
                _historyEnabled = value;
                IsDirty = true;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                IsDirty = true;
            }
        }

        public List<InventoryChangeReason> HistoryTrackReasons
        {
            get
            {
                return _historyTrackReasons;
            }
            set
            {
                _historyTrackReasons = value;
                IsDirty = true;
            }
        }

        public void AddWindowToIgnoreEscape(Type windowType)
        {
            WindowsIgnoreEscape.Add(windowType.Name);
            IsDirty = true;
        }

        public void RemoveWindowFromIgnoreEscape(Type windowType)
        {
            WindowsIgnoreEscape.Remove(windowType.Name);
            IsDirty = true;
        }

        public void SetWindowIgnoreEscape(Type windowType, bool ignoreEscape)
        {
            if (ignoreEscape)
            {
                AddWindowToIgnoreEscape(windowType);
            }
            else
            {
                RemoveWindowFromIgnoreEscape(windowType);
            }
        }

        public bool DoesWindowIgnoreEscape(Type windowName)
        {
            return WindowsIgnoreEscape.Contains(windowName.Name);
        }

        public int SelectedConfigurationPage { get; set; }
        public bool ShowFilterTab { get; set; } = true;
        public bool SwitchFiltersAutomatically { get; set; } = true;
        private bool _tooltipDisplayAmountOwned = true;
        private bool _tooltipDisplayHeader = false;
        private bool _tooltipCurrentCharacter = false;
        private bool _tooltipAddCharacterNameOwned = false;
        private bool _tooltipWhitelistBlacklist = false;
        private int _tooltipLocationLimit = 10;
        private int _tooltipHeaderLines = 0;
        private int _tooltipFooterLines = 0;
        private TooltipLocationDisplayMode _tooltipLocationDisplayMode = TooltipLocationDisplayMode.CharacterCategoryQuantityQuality;
        private TooltipAmountOwnedSort _tooltipAmountOwnedSort = TooltipAmountOwnedSort.Alphabetically;
        private WindowLayout _filtersLayout = WindowLayout.Tabs;
        private uint? _tooltipColor;
        private HashSet<uint> _tooltipWhitelistCategories = new();
        private List<InventorySearchScope> _tooltipSearchScope = new();

        [Vector4Default("0.007, 0.008,0.007, 0.212")]
        public Vector4 HighlightColor
        {
            get => _highlightColor;
            set
            {
                _highlightColor = value;
                IsDirty = true;
            }
        }

        [Vector4Default("0.321, 0.239, 0.03, 1")]
        public Vector4 DestinationHighlightColor
        {
            get => _destinationHighlightColor;
            set
            {
                _destinationHighlightColor = value;
                IsDirty = true;
            }
        }

        public Vector4 RetainerListColor
        {
            get => _retainerListColor;
            set
            {
                _retainerListColor = value;
                IsDirty = true;
            }
        }

        [Vector4Default("0.007, 0.008,0.007, 0.2")]
        public Vector4 TabHighlightColor
        {
            get => _tabHighlightColor;
            set
            {
                _tabHighlightColor = value;
                IsDirty = true;
            }
        }

        public bool DisplayCrossCharacter
        {
            get => _displayCrossCharacter;
            set
            {
                _displayCrossCharacter = value;
                IsDirty = true;
            }
        }

        public bool DisplayTooltip
        {
            get => _displayTooltip;
            set
            {
                _displayTooltip = value;
                IsDirty = true;
            }
        }

        public bool TooltipDisplayAmountOwned
        {
            get => _tooltipDisplayAmountOwned;
            set
            {
                _tooltipDisplayAmountOwned = value;
                IsDirty = true;
            }
        }

        public bool TooltipDisplayHeader
        {
            get => _tooltipDisplayHeader;
            set
            {
                _tooltipDisplayHeader = value;
                IsDirty = true;
            }
        }

        public bool TooltipCurrentCharacter
        {
            get => _tooltipCurrentCharacter;
            set
            {
                _tooltipCurrentCharacter = value;
                IsDirty = true;
            }
        }

        public bool TooltipAddCharacterNameOwned
        {
            get => _tooltipAddCharacterNameOwned;
            set
            {
                _tooltipAddCharacterNameOwned = value;
                IsDirty = true;
            }
        }

        public bool TooltipWhitelistBlacklist
        {
            get => _tooltipWhitelistBlacklist;
            set
            {
                _tooltipWhitelistBlacklist = value;
                IsDirty = true;
            }
        }

        [DefaultValue(10)]
        public int TooltipLocationLimit
        {
            get => _tooltipLocationLimit;
            set
            {
                _tooltipLocationLimit = value;
                IsDirty = true;
            }
        }

        [DefaultValue(0)]
        public int TooltipHeaderLines
        {
            get => _tooltipHeaderLines;
            set
            {
                _tooltipHeaderLines = value;
                IsDirty = true;
            }
        }

        [DefaultValue(0)]
        public int TooltipFooterLines
        {
            get => _tooltipFooterLines;
            set
            {
                _tooltipFooterLines = value;
                IsDirty = true;
            }
        }

        public TooltipLocationDisplayMode TooltipLocationDisplayMode
        {
            get => _tooltipLocationDisplayMode;
            set
            {
                _tooltipLocationDisplayMode = value;
                IsDirty = true;
            }
        }

        public TooltipAmountOwnedSort TooltipAmountOwnedSort
        {
            get => _tooltipAmountOwnedSort;
            set
            {
                _tooltipAmountOwnedSort = value;
                IsDirty = true;
            }
        }

        public HashSet<uint> TooltipWhitelistCategories
        {
            get => _tooltipWhitelistCategories ??= new HashSet<uint>();
            set
            {
                _tooltipWhitelistCategories = value;
                IsDirty = true;
            }
        }

        public List<InventorySearchScope> TooltipSearchScope
        {
            get => _tooltipSearchScope ??= new List<InventorySearchScope>();
            set
            {
                _tooltipSearchScope = value;
                IsDirty = true;
            }
        }

        public WindowLayout FiltersLayout
        {
            get => _filtersLayout;
            set
            {
                _filtersLayout = value;
                IsDirty = true;
            }
        }

        public bool ColorRetainerList
        {
            get => _colorRetainerList;
            set
            {
                _colorRetainerList = value;
                IsDirty = true;
            }
        }

        public bool ShowItemNumberRetainerList
        {
            get => _showItemNumberRetainerList;
            set
            {
                _showItemNumberRetainerList = value;
                IsDirty = true;
            }
        }

        public bool InvertHighlighting
        {
            get => _invertHighlighting;
            set
            {
                _invertHighlighting = value;
                IsDirty = true;
            }
        }

        public bool InvertDestinationHighlighting
        {
            get => _invertDestinationHighlighting;
            set
            {
                _invertDestinationHighlighting = value;
                IsDirty = true;
            }
        }

        public bool InvertTabHighlighting
        {
            get => _invertTabHighlighting;
            set
            {
                _invertTabHighlighting = value;
                IsDirty = true;
            }
        }

        [Obsolete("Remove with API14")]
        public string HighlightWhen
        {
            get => _highlightWhen;
            set
            {
                _highlightWhen = value;
                IsDirty = true;
            }
        }

        public HighlightWhen HighlightWhenEnum
        {
            get => _highlightWhenEnum;
            set
            {
                _highlightWhenEnum = value;
                IsDirty = true;
            }
        }

        public bool HighlightDestination
        {
            get => _highlightDestination;
            set
            {
                _highlightDestination = value;
                IsDirty = true;
            }
        }

        public bool HighlightDestinationEmpty
        {
            get => _highlightDestinationEmpty;
            set
            {
                _highlightDestinationEmpty = value;
                IsDirty = true;
            }
        }

        public string? ActiveUiFilter { get; set; } = null;

        public string? ActiveBackgroundFilter { get; set; }

        public bool SaveBackgroundFilter { get; set; } = false;

        public bool FirstRun { get; set; } = true;

        public int SelectedHelpPage { get; set; }
        #if DEBUG
        public int SelectedDebugPage { get; set; }
        #endif
        public bool AutoSave { get; set; } = true;
        public int AutoSaveMinutes { get; set; } = 10;
        public int InternalVersion { get; set; } = 0;
        public int Version { get; set; }

        public uint? TooltipColor
        {
            get => _tooltipColor;
            set => _tooltipColor = value;
        }

        public ConcurrentDictionary<string,ModifiableHotkey> Hotkeys
        {
            get
            {
                if (_hotkeys == null)
                {
                    _hotkeys = new ConcurrentDictionary<string, ModifiableHotkey>();
                }
                return _hotkeys;
            }
            set
            {
                _hotkeys = value;
            }
        }

        private ConcurrentDictionary<string,ModifiableHotkey>? _hotkeys;
        private HighlightWhen _highlightWhenEnum;

        public ModifiableHotkey? GetHotkey(string hotkey)
        {
            if(Hotkeys.TryGetValue(hotkey, out var modifiableHotkey))
            {
                return modifiableHotkey;
            }

            return null;
        }

        public HashSet<string> OpenWindows
        {
            get
            {
                if (_openWindows == null)
                {
                    _openWindows = new HashSet<string>();
                }
                return _openWindows;
            }
            set => _openWindows = value;
        }
        public Dictionary<string, Vector2> SavedWindowPositions
        {
            get
            {
                if (_savedWindowPositions == null)
                {
                    _savedWindowPositions = new Dictionary<string, Vector2>();
                }
                return _savedWindowPositions;
            }
            set => _savedWindowPositions = value;
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        public Dictionary<string, bool> BooleanSettings
        {
            get => _booleanSettings ??= new Dictionary<string, bool>();
            set => _booleanSettings = value;
        }

        public Dictionary<string, int> IntegerSettings
        {
            get => _integerSettings ??= new Dictionary<string, int>();
            set => _integerSettings = value;
        }

        public Dictionary<string, uint> UIntegerSettings
        {
            get => _uintegerSettings ??= new Dictionary<string, uint>();
            set => _uintegerSettings = value;
        }

        [JsonConverter(typeof(EnumDictionaryConverter))]
        public Dictionary<string, Enum> EnumSettings
        {
            get => _enumSettings ??= new Dictionary<string, Enum>();
            set => _enumSettings = value;
        }

        [JsonConverter(typeof(TypeDictionaryConverter))]
        public Dictionary<string, Dictionary<Type, bool>> TypeDictionarySettings
        {
            get => _typeDictionarySettings ??= new Dictionary<string, Dictionary<Type, bool>>();
            set => _typeDictionarySettings = value;
        }

        public Dictionary<string, Vector4> Vector4Settings
        {
            get => _vector4Settings ??= new Dictionary<string, Vector4>();
            set => _vector4Settings = value;
        }

        public Dictionary<string, List<string>> ListSettings
        {
            get => _listSettings ??= new Dictionary<string, List<string>>();
            set => _listSettings = value;
        }


        //Configuration Helpers

        public Dictionary<ulong, Character> GetSavedRetainers()
        {
            return SavedCharacters;
        }

        public List<FilterConfiguration> GetSavedFilters()
        {
            return FilterConfigurations;
        }

        public void MarkReloaded()
        {
            if (!SaveBackgroundFilter)
            {
                ActiveBackgroundFilter = null;
            }
        }

        public bool HasList(string name)
        {
            if (FilterConfigurations.Any(c => c.Name == name))
            {
                return true;
            }

            return false;
        }


        public bool? Get(string key, bool? defaultValue)
        {
            return this.BooleanSettings.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public void Set(string key, int? newValue)
        {
            if (newValue == null)
            {
                this.IntegerSettings.Remove(key);
            }
            else
            {
                this.IntegerSettings[key] = newValue.Value;
            }

            this.IsDirty = true;
        }

        public void Set(string key, bool? newValue)
        {
            if (newValue == null)
            {
                this.BooleanSettings.Remove(key);
            }
            else
            {
                this.BooleanSettings[key] = newValue.Value;
            }

            this.IsDirty = true;
        }

        public int? Get(string key, int? defaultValue)
        {
            return this.IntegerSettings.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public Enum? Get(string key, Enum? defaultValue)
        {
            return this.EnumSettings.GetValueOrDefault(key);
        }

        public void Set(string key, Enum? newValue)
        {
            if (newValue == null)
            {
                this.EnumSettings.Remove(key);
            }
            else
            {
                this.EnumSettings[key] = newValue;
            }

            this.IsDirty = true;
        }

        public Dictionary<Type, bool>? Get(string key, Dictionary<Type, bool>? defaultValue)
        {
            return this.TypeDictionarySettings.GetValueOrDefault(key);
        }

        public void Set(string key, Dictionary<Type, bool>? newValue)
        {
            if (newValue == null)
            {
                this.TypeDictionarySettings.Remove(key);
            }
            else
            {
                this.TypeDictionarySettings[key] = newValue;
            }

            this.IsDirty = true;
        }

        public Vector4? Get(string key, Vector4? defaultValue)
        {
            return this.Vector4Settings.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public void Set(string key, Vector4? newValue)
        {
            if (newValue == null)
            {
                this.Vector4Settings.Remove(key);
            }
            else
            {
                this.Vector4Settings[key] = newValue.Value;
            }

            this.IsDirty = true;
        }

        public List<TEnum>? Get<TEnum>(string key, List<TEnum>? defaultValue) where TEnum : struct, Enum
        {
            if (!this.ListSettings.TryGetValue(key, out var strings)) return defaultValue;
            var result = new List<TEnum>();
            foreach (var s in strings)
                if (Enum.TryParse<TEnum>(s, out var val)) result.Add(val);
            return result;
        }

        public void Set<TEnum>(string key, List<TEnum>? newValue) where TEnum : struct, Enum
        {
            if (newValue == null)
            {
                this.ListSettings.Remove(key);
            }
            else
            {
                this.ListSettings[key] = newValue.ConvertAll(v => v.ToString());
            }

            this.IsDirty = true;
        }

        public uint? Get(string key, uint? defaultValue)
        {
            return this.UIntegerSettings.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public void Set(string key, uint? newValue)
        {
            if (newValue == null)
            {
                this.UIntegerSettings.Remove(key);
            }
            else
            {
                this.UIntegerSettings[key] = newValue.Value;
            }

            this.IsDirty = true;
        }
    }
}
