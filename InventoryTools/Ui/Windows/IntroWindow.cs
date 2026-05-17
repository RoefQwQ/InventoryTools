using System.Numerics;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic;
using Dalamud.Interface.Utility.Raii;
using InventoryTools.Mediator;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Ui
{
    public class IntroWindow : GenericWindow
    {
        private readonly ILocalizationService _localizationService;

        public IntroWindow(ILogger<IntroWindow> logger, MediatorService mediator, ImGuiService imGuiService, InventoryToolsConfiguration configuration, ILocalizationService localizationService, string name = "Intro Window") : base(logger, mediator, imGuiService, configuration, name)
        {
            _localizationService = localizationService;
        }
        public override void Initialize()
        {
            WindowName = _localizationService["Window_Intro_Title"];
            Flags =
                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoScrollbar;
            Key = "intro";
        }


        public override void Invalidate()
        {
        }

        public override FilterConfiguration? SelectedConfiguration => null;
        public override string GenericKey { get; } = "intro";
        public override string GenericName => _localizationService["Window_Intro_GenericName"];
        public override bool DestroyOnClose => true;

        public override void DrawWindow()
        {
            using (var leftChild = ImRaii.Child("Left", new Vector2(200, 0)))
            {
                if (leftChild.Success)
                {
                    ImGui.SetCursorPosY(40);
                    ImGui.Image(ImGuiService.GetImageTexture("icon-hor").Handle, new Vector2(200, 200) * ImGui.GetIO().FontGlobalScale);
                }
            }
            ImGui.SameLine();
            using (var rightChild = ImRaii.Child("Right", new Vector2(0, 0), false, ImGuiWindowFlags.NoScrollbar))
            {
                if (rightChild.Success)
                {
                    using (var textChild = ImRaii.Child("Text", new Vector2(0, -32)))
                    {
                        if (textChild.Success)
                        {
                            ImGui.TextWrapped(_localizationService["Window_Intro_Welcome"]);
                            ImGui.TextWrapped(
                                _localizationService["Window_Intro_Description"]);
                            using (ImRaii.PushIndent())
                            {
                                ImGui.Bullet();
                                ImGui.Text(_localizationService["Window_Intro_Feature_Inventory"]);
                                ImGui.Bullet();
                                ImGui.Text(_localizationService["Window_Intro_Feature_Craft"]);
                                ImGui.Bullet();
                                ImGui.Text(_localizationService["Window_Intro_Feature_Info"]);
                            }

                            ImGui.TextWrapped(
                                _localizationService["Window_Intro_HelpText1"]);
                            ImGui.TextWrapped(
                                _localizationService["Window_Intro_HelpText2"]);
                            ImGui.TextWrapped(
                                _localizationService["Window_Intro_HelpText3"]);
                        }
                    }

                    using (var buttonsChild = ImRaii.Child("Buttons", new Vector2(0, 32)))
                    {
                        if (buttonsChild.Success)
                        {
                            if (ImGui.Button(_localizationService["Window_Intro_Button_Close"]))
                            {
                                Close();
                            }

                            ImGui.SameLine(0, 4);
                            if (ImGui.Button(_localizationService["Window_Intro_Button_CloseOpenMain"]))
                            {
                                Close();
                                MediatorService.Publish(new OpenGenericWindowMessage(typeof(FiltersWindow)));
                            }
                        }
                    }
                }
            }
        }

        public override Vector2? DefaultSize { get; } = new Vector2(800, 300);
        public override Vector2? MaxSize { get; } = new Vector2(800, 300);
        public override Vector2? MinSize { get; } = new Vector2(800, 300);
        public override bool SaveState => false;
    }
}