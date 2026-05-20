using System.Numerics;
using AllaganLib.Shared.Extensions;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Extensions;
using InventoryTools.Logic;
using Dalamud.Interface.Utility.Raii;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Ui
{
    public class HelpWindow : GenericWindow
    {
        private readonly InventoryToolsConfiguration _configuration;
        private readonly ILocalizationService _localizationService;

        public HelpWindow(ILogger<HelpWindow> logger, MediatorService mediator, ImGuiService imGuiService, InventoryToolsConfiguration configuration, ILocalizationService localizationService, string name = "Help Window") : base(logger, mediator, imGuiService, configuration, name)
        {
            _configuration = configuration;
            _localizationService = localizationService;
        }
        public override void Initialize()
        {
            WindowName = _localizationService["Window_Help_WindowName"];
            Key = "help";
        }

        public override bool SaveState => false;
        public override Vector2? DefaultSize { get; } = new Vector2(700, 700);
        public override  Vector2? MaxSize { get; } = new Vector2(2000, 2000);
        public override  Vector2? MinSize { get; } = new Vector2(200, 200);
        public override string GenericKey { get; } = "help";
        public override string GenericName => _localizationService["Window_Help_GenericName"];
        public override bool DestroyOnClose => true;


        public override void DrawWindow()
        {
            using (var sideBarChild =
                   ImRaii.Child("SideBar", new Vector2(150, -1) * ImGui.GetIO().FontGlobalScale, true))
            {
                if (sideBarChild.Success)
                {
                    if (ImGui.Selectable(_localizationService["Window_Help_Nav_General"], _configuration.SelectedHelpPage == 0))
                    {
                        _configuration.SelectedHelpPage = 0;
                    }

                    if (ImGui.Selectable(_localizationService["Window_Help_Nav_FilterBasics"], _configuration.SelectedHelpPage == 1))
                    {
                        _configuration.SelectedHelpPage = 1;
                    }

                    if (ImGui.Selectable(_localizationService["Window_Help_Nav_Filtering"], _configuration.SelectedHelpPage == 2))
                    {
                        _configuration.SelectedHelpPage = 2;
                    }

                    if (ImGui.Selectable(_localizationService["Window_Help_Nav_About"], _configuration.SelectedHelpPage == 3))
                    {
                        _configuration.SelectedHelpPage = 3;
                    }
                }
            }

            ImGui.SameLine();

            using (var mainChild = ImRaii.Child("###ivHelpView", new Vector2(-1, -1), true))
            {
                if (mainChild.Success)
                {
                    if (_configuration.SelectedHelpPage == 0)
                    {
                        ImGui.TextWrapped(_localizationService["Window_Help_General_Description"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_General_Inspiration"]);
                        ImGui.NewLine();
                        ImGui.TextUnformatted(_localizationService["Window_Help_General_InventoryTracking"]);
                        ImGui.Separator();
                        ImGui.TextWrapped(_localizationService["Window_Help_General_InventoryTrackingDesc"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_General_InventoryTrackingDesc2"]);
                        ImGui.NewLine();

                        ImGui.TextUnformatted(_localizationService["Window_Help_General_CraftPlanning"]);
                        ImGui.Separator();
                        ImGui.TextWrapped(_localizationService["Window_Help_General_CraftPlanningDesc"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_General_CraftPlanningDesc2"]);
                        ImGui.NewLine();

                        ImGui.TextUnformatted(_localizationService["Window_Help_General_ItemInformation"]);
                        ImGui.Separator();
                        ImGui.TextWrapped(_localizationService["Window_Help_General_ItemInformationDesc"]);
                        ImGui.NewLine();

                        ImGui.TextUnformatted(_localizationService["Window_Help_General_Highlighting"]);
                        ImGui.Separator();
                        ImGui.TextWrapped(_localizationService["Window_Help_General_HighlightingDesc"]);
                        ImGui.NewLine();

                        ImGui.TextUnformatted(_localizationService["Window_Help_General_SeeWiki"]);
                        if (ImGui.Button(_localizationService["Window_Help_General_OpenWiki"]))
                        {
                            "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview".OpenBrowser();
                        }
                    }
                    else if (_configuration.SelectedHelpPage == 1)
                    {
                        ImGui.PushTextWrapPos();
                        ImGui.Text(_localizationService["Window_Help_FilterBasics_ListsCore"]);
                        ImGui.Text(_localizationService["Window_Help_FilterBasics_ThreeTypes"]);
                        ImGui.PopTextWrapPos();
                        ImGui.NewLine();

                        ImGui.Text(_localizationService["Window_Help_FilterBasics_SearchList"]);
                        ImGui.Separator();
                        ImGui.PushTextWrapPos();

                        ImGui.TextUnformatted(_localizationService["Window_Help_FilterBasics_SearchListDesc"]);
                        ImGui.TextUnformatted(_localizationService["Window_Help_FilterBasics_ExampleUsages"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleCraftMaterials"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleHousingItem"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleItemWorth"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleGlamourChest"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleRetainerEquipment"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleArmoire"]);
                        ImGui.PopTextWrapPos();
                        ImGui.NewLine();

                        ImGui.Text(_localizationService["Window_Help_FilterBasics_SortFilter"]);
                        ImGui.Separator();
                        ImGui.PushTextWrapPos();
                        ImGui.TextUnformatted(_localizationService["Window_Help_FilterBasics_SortFilterDesc"]);
                        ImGui.TextUnformatted("示例用法：");
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExamplePutAwayMaterials"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleChocoboSaddlebag"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleFreeCompanyChest"]);
                        ImGui.PopTextWrapPos();

                        ImGui.NewLine();
                        ImGui.Text(_localizationService["Window_Help_FilterBasics_GameItemFilter"]);
                        ImGui.Separator();
                        ImGui.PushTextWrapPos();
                        ImGui.TextUnformatted(_localizationService["Window_Help_FilterBasics_GameItemFilterDesc"]);
                        ImGui.TextUnformatted("示例用法：");
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleGlamours"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleMountsMinions"]);
                        ImGui.BulletText(_localizationService["Window_Help_FilterBasics_ExampleTrackPrices"]);
                        ImGui.PopTextWrapPos();
                    }
                    else if (_configuration.SelectedHelpPage == 2)
                    {
                        ImGui.TextUnformatted(_localizationService["Window_Help_Filtering_AdvancedSyntax"]);
                        ImGui.Separator();
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_AdvancedSyntaxDesc"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_Not"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_LessThan"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_GreaterThan"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_GreaterThanOrEqual"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_LessThanOrEqual"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_Equal"]);
                        ImGui.TextWrapped(_localizationService["Window_Help_Filtering_Operator_Chain"]);
                    }
                    else if (_configuration.SelectedHelpPage == 3)
                    {
                        ImGui.TextUnformatted(_localizationService["Window_Help_About_Title"]);
                        ImGui.TextUnformatted(_localizationService["Window_Help_About_Description"]);
                        ImGui.TextUnformatted(_localizationService["Window_Help_About_Feedback"]);
                        ImGui.TextUnformatted(_localizationService["Window_Help_About_WikiLabel"]);
                        ImGui.SameLine();
                        if (ImGui.Button(_localizationService["Window_Help_About_OpenWiki"] + "##WikiBtn"))
                        {
                            "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview".OpenBrowser();
                        }

                        ImGui.TextUnformatted(_localizationService["Window_Help_About_FoundBug"]);
                        ImGui.SameLine();
                        if (ImGui.Button(_localizationService["Window_Help_About_OpenBug"] + "##BugBtn"))
                        {
                            "https://github.com/Critical-Impact/InventoryTools/issues".OpenBrowser();
                        }
                    }
                }
            }
        }

        public override FilterConfiguration? SelectedConfiguration => null;

        public override void Invalidate()
        {

        }
    }
}