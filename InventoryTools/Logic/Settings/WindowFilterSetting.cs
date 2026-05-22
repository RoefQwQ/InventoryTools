using System.Collections.Generic;
using System.Linq;
using InventoryTools.Logic.Settings.Abstract;
using InventoryTools.Services;
using InventoryTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Settings
{
    public class WindowFilterSetting : ChoiceSetting<string>
    {
        private readonly IListService _listService;

        public WindowFilterSetting(ILogger<WindowFilterSetting> logger, ImGuiService imGuiService, ILocalizationService localizationService, IListService listService) : base(logger, imGuiService, localizationService)
        {
            _listService = listService;
            Name = localizationService.GetString("Setting_WindowFilter_Name");
            HelpText = localizationService.GetString("Setting_WindowFilter_HelpText");
        }
        public override string DefaultValue { get; set; } = "";
        public override string CurrentValue(InventoryToolsConfiguration configuration)
        {
            return configuration.ActiveUiFilter ?? "";
        }

        public override void UpdateFilterConfiguration(InventoryToolsConfiguration configuration, string newValue)
        {
            if (newValue == "")
            {
                _listService.ClearActiveUiList();
            }
            else
            {
                _listService.SetActiveUiListByKey(newValue);
            }
        }

        public override string Key { get; set; } = "WindowFilter";
        public override string Name { get; set; } = "窗口列表高亮";

        public override string HelpText { get; set; } =
            "当任何Allagan Tools窗口可见时，要高亮显示的列表。";

        public override SettingCategory SettingCategory { get; set; } = SettingCategory.Lists;
        public override SettingSubCategory SettingSubCategory { get; } = SettingSubCategory.ActiveLists;

        public override Dictionary<string, string> Choices
        {
            get
            {
                var filterItems = new Dictionary<string, string> {{"", "无"}};
                foreach (var config in _listService.Lists.Where(c => !c.CraftListDefault))
                {
                    filterItems.Add(config.Key, config.Name);
                }
                return filterItems;
            }
        }
        public override string Version => "1.7.0.0";
    }
}