using System.Collections.Generic;
using System.Numerics;
using AllaganLib.GameSheets.Sheets;
using AllaganLib.GameSheets.Sheets.Rows;
using CriticalCommonLib;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic;
using InventoryTools.Mediator;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;
using OtterGui;

namespace InventoryTools.Ui
{
    class RetainerTaskWindow : UintWindow
    {
        private readonly RetainerTaskSheet _retainerTaskSheet;
        private readonly ItemSheet _itemSheet;
        private readonly ILocalizationService _localizationService;

        public RetainerTaskWindow(ILogger<RetainerTaskWindow> logger,
            MediatorService mediator,
            ImGuiService imGuiService,
            InventoryToolsConfiguration configuration,
            RetainerTaskSheet retainerTaskSheet,
            ItemSheet itemSheet,
            ILocalizationService localizationService,
            string name = "Retainer Venture") : base(logger,
            mediator,
            imGuiService,
            configuration,
            name)
        {
            _retainerTaskSheet = retainerTaskSheet;
            _itemSheet = itemSheet;
            _localizationService = localizationService;
        }
        public override void Initialize(uint retainerTaskId)
        {
            base.Initialize(retainerTaskId);
            _retainerTaskId = retainerTaskId;
            if (RetainerTask != null)
            {
                Key = "rt_" + retainerTaskId;
                WindowName = "Allagan Tools - " + RetainerTask.FormattedName + " - Venture";
                _drops = RetainerTask.Drops;
            }
            else
            {
                Key = "rt_invalid";
                WindowName = _localizationService["Window_RetainerTask_InvalidTask"];
                _drops = new List<ItemRow>();
            }
        }
        public override bool SaveState => false;
        private uint _retainerTaskId;
        private List<ItemRow> _drops;
        private RetainerTaskRow? RetainerTask => _retainerTaskSheet.GetRowOrDefault(_retainerTaskId);


        public override string GenericKey { get; } = "retainertask";
        public override string GenericName => _localizationService["Window_RetainerTask_GenericName"];
        public override bool DestroyOnClose => true;
        public override void DrawWindow()
        {
            if (RetainerTask == null)
            {
                ImGui.TextUnformatted(_localizationService.GetString("Window_RetainerTask_NotFound", _retainerTaskId));
            }
            else
            {
                ImGui.TextUnformatted(RetainerTask.FormattedName);
                ImGui.TextUnformatted(_localizationService["Window_RetainerTask_Level"] + RetainerTask.Base.RetainerLevel);
                ImGui.TextUnformatted(_localizationService["Window_RetainerTask_Duration"] + RetainerTask.DurationString);
                ImGui.TextUnformatted(_localizationService["Window_RetainerTask_Experience"] + RetainerTask.ExperienceString);
                ImGui.TextUnformatted(_localizationService["Window_RetainerTask_VentureCost"] + RetainerTask.Base.VentureCost);
                ImGui.TextUnformatted(_localizationService["Window_RetainerTask_AverageILvl"] + RetainerTask.Base.RequiredItemLevel);
                ;
                ImGui.Image(ImGuiService.GetIconTexture(65049).Handle, new Vector2(100, 100) * ImGui.GetIO().FontGlobalScale);


                if (ImGui.CollapsingHeader(_localizationService["Window_RetainerTask_RewardsHeader"] + _drops.Count + ")", ImGuiTreeNodeFlags.DefaultOpen | ImGuiTreeNodeFlags.CollapsingHeader))
                {
                    ImGuiStylePtr style = ImGui.GetStyle();
                    float windowVisibleX2 = ImGui.GetWindowPos().X + ImGui.GetWindowContentRegionMax().X;

                    for (var index = 0; index < _drops.Count; index++)
                    {
                        ImGui.PushID("Reward"+index);
                        var drop = _drops[index];

                        if (ImGui.ImageButton(ImGuiService.GetIconTexture(drop.Icon).Handle,
                                new Vector2(32, 32) * ImGui.GetIO().FontGlobalScale, new(0, 0), new(1, 1), 0))
                        {
                            MediatorService.Publish(new OpenUintWindowMessage(typeof(ItemWindow), drop.RowId));
                        }
                        if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled & ImGuiHoveredFlags.AllowWhenOverlapped & ImGuiHoveredFlags.AllowWhenBlockedByPopup & ImGuiHoveredFlags.AllowWhenBlockedByActiveItem & ImGuiHoveredFlags.AnyWindow) && ImGui.IsMouseReleased(ImGuiMouseButton.Right))
                        {
                            ImGui.OpenPopup("RightClickUse" + drop.RowId);
                        }

                        using (var popup = ImRaii.Popup("RightClickUse"+ drop.RowId))
                        {
                            if (popup)
                            {
                                var itemRow = _itemSheet.GetRowOrDefault(drop.RowId);
                                if (itemRow != null)
                                {
                                    MediatorService.Publish(ImGuiService.ImGuiMenuService.DrawRightClickPopup(itemRow));
                                }
                            }
                        }

                        float lastButtonX2 = ImGui.GetItemRectMax().X;
                        float nextButtonX2 = lastButtonX2 + style.ItemSpacing.X + 32;
                        ImGuiUtil.HoverTooltip(drop.NameString);
                        if (index + 1 < _drops.Count && nextButtonX2 < windowVisibleX2)
                        {
                            ImGui.SameLine();
                        }

                        ImGui.PopID();
                    }
                }

                #if DEBUG
                if (ImGui.CollapsingHeader("Debug"))
                {
                    ImGui.TextUnformatted("Duty ID: " + _retainerTaskId);
                    Utils.PrintOutObject(RetainerTask, 0, new List<string>());
                }
                #endif

            }
        }

        public override void Invalidate()
        {

        }
        public override FilterConfiguration? SelectedConfiguration => null;
        public override Vector2? DefaultSize { get; } = new Vector2(500, 800);
        public override Vector2? MaxSize => new (800, 1500);
        public override Vector2? MinSize => new (100, 100);
    }
}