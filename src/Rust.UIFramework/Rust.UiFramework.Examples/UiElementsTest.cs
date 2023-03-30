using Network;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Oxide.Plugins.UiElementsTestExtensions;
//UiElementsTest created with PluginMerge v(1.0.5.0) by MJSU @ https://github.com/dassjosh/Plugin.Merge
namespace Oxide.Plugins
{
    [Info("UI Elements Test", "MJSU", "1.0.0")]
    [Description("Tests all UI elements")]
    public partial class UiElementsTest : RustPlugin
    {
        #region UiElementsTest.Plugin.cs
        #region Fields
        private readonly Hash<ulong, PlayerState> _playerStates = new Hash<ulong, PlayerState>();
        
        public static UiElementsTest _ins;
        
        private enum DropDownExample
        {
            Value1,
            Value2,
            Value3,
            Value4,
        }
        
        private enum PageDropdownExample
        {
            Value1,
            Value2,
            Value3,
            Value5,
            Value6,
            Value7,
            Value8,
            Value9,
            Value10,
            Value11,
            Value12,
            Value13,
            Value14,
            Value15,
            Value16,
            Value17,
            Value18,
            Value19,
            Value20,
            Value21,
            Value22,
        }
        
        private readonly List<DropdownMenuData> _exampleList = Enum.GetValues(typeof(DropDownExample)).Cast<DropDownExample>().Select(e => new DropdownMenuData(e.ToString(), e.ToString(), false)).ToList();
        private readonly List<DropdownMenuData> _pageExampleList = Enum.GetValues(typeof(PageDropdownExample)).Cast<PageDropdownExample>().Select(e => new DropdownMenuData(e.ToString(), e.ToString(), false)).ToList();
        #endregion
        
        #region Setup & Loading
        private void Init()
        {
            _ins = this;
        }
        
        private void Unload()
        {
            UiBuilder.DestroyUi(UiName);
            UiBuilder.DestroyUi(UiModal);
            UiBuilder.DestroyUi(UiClose);
            UiBuilder.DestroyUi(UiSkin);
            UiBuilder.DestroyUi(UiLootBug);
            UiBuilder.DestroyUi("CI_Crafting_Bottom");
            _ins = null;
        }
        #endregion
        
        #region Chat Command
        [ChatCommand("et")]
        private void ElementsChatCommand(BasePlayer player, string cmd, string[] args)
        {
            CreateUi(player);
        }
        
        [ChatCommand("ets")]
        private void ElementsSkinChatCommand(BasePlayer player, string cmd, string[] args)
        {
            CreateSkinTest(player);
        }
        
        [ChatCommand("ett")]
        private void Elements1SkinChatCommand(BasePlayer player, string cmd, string[] args)
        {
            var json =
            "[{\"name\":\"CI_Crafting_Bottom\",\"parent\":\"Overlay\",\"components\":[{\"type\":\"UnityEngine.UI.Image\",\"material\":\"assets/content/ui/uibackgroundblur-ingamemenu.mat\",\"color\":\"0.035 0.039 0.039 1.000\"},{\"type\":\"RectTransform\",\"anchormin\":\"0.5 0\",\"anchormax\":\"0.5 0\",\"offsetmin\":\"190 110\",\"offsetmax\":\"572 420\"},{\"type\":\"NeedsCursor\"}]},{\"name\":\"CI_Crafting_Result\",\"parent\":\"CI_Crafting_Bottom\",\"components\":[{\"type\":\"UnityEngine.UI.Image\",\"material\":\"assets/content/ui/uibackgroundblur-ingamemenu.mat\",\"color\":\"0.102 0.106 0.106 0.500\"},{\"type\":\"RectTransform\",\"anchormin\":\"0 0\",\"anchormax\":\"1 1\",\"offsetmin\":\"1 1\",\"offsetmax\":\"-1 -1\"}]}]";
            
            CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(player.net.connection), null, "AddUI", json);
        }
        #endregion
        
        #region Hooks
        // private void OnLootEntity(BasePlayer player, BaseEntity entity)
        // {
            //     if (!player.IsAdmin)
            //     {
                //         return;
            //     }
            //
            //     CreateLootBugUi(player);
        // }
        //
        // private void OnLootEntityEnd(BasePlayer player, BaseCombatEntity entity)
        // {
            //     UiBuilder.DestroyUi(player, UiLootBug);
        // }
        #endregion
        
        #region UI
        private const string UiName = nameof(UiElementsTest) + "_Main";
        private const string UiClose = nameof(UiElementsTest) + "_OutsideClose";
        private const string UiModal = nameof(UiElementsTest) + "_Modal";
        private const string UiSkin = nameof(UiElementsTest) + "_Skin";
        private const string UiLootBug = nameof(UiElementsTest) + "_LootBug";
        
        private readonly UiOffset _containerSize = new UiOffset(800, 600);
        private readonly UiPosition _titleBarPos = new UiPosition(0, 0.95f, 1, 1);
        private readonly UiPosition _titleTextPos = new UiPosition(.25f, 0, 0.75f, 1);
        private readonly UiPosition _closeButtonPos = new UiPosition(.8f, 0, .9f, 1);
        private readonly UiPosition _closeCmdButtonPos = new UiPosition(.9f, 0, 1f, 1);
        private readonly UiPosition _mainBodyPosition = new UiPosition(0, .1f, 1f, .89f);
        private readonly UiPosition _paginator = new UiPosition(0.025f, 0.025f, 0.975f, 0.09f);
        
        private readonly UiPosition _numberPickerPos = new UiPosition(0.025f, 0.1f, 0.4f, 0.2f);
        private readonly UiPosition _inputNumberPickerPos = new UiPosition(0.6f, 0.4f, 0.975f, 0.5f);
        
        private readonly GridPosition _grid = new GridPositionBuilder(6, 6).SetPadding(0.01f).Build();
        private readonly GridPosition _pagination = new GridPositionBuilder(11, 1).SetPadding(0.0025f).Build();
        
        private readonly int[] _numberPickerIncrements = { 1, 5, 25 };
        
        private const int FontSize = 14;
        private const int TitleFontSize = 16;
        
        private readonly CachedUiBuilder _outsideClose = UiBuilder.CreateOutsideClose(nameof(UiElementsCloseAll), UiClose).ToCachedBuilder();
        
        private void CreateUi(BasePlayer player)
        {
            _outsideClose.AddUi(player);
            
            PlayerState state = new PlayerState();
            _playerStates[player.userID] = state;
            CreateUi(player, state);
        }
        
        private void CreateUi(BasePlayer player, PlayerState state)
        {
            //Initialize the builder
            UiBuilder builder = UiBuilder.Create(UiPosition.MiddleMiddle, _containerSize, UiColors.Body, UiName);
            
            //UI Grabs control of the mouse
            builder.NeedsMouse();
            
            //UI Grabs control of the keyboard
            builder.NeedsKeyboard();
            
            //Create a panel for the title bar
            UiPanel titlePanel = builder.Panel(builder.Root, _titleBarPos, UiColors.BodyHeader);
            
            //Create the Title Bar Title and parent it to the titlePanel
            builder.Label(titlePanel, _titleTextPos, Title, TitleFontSize, UiColors.Text);
            
            //Create a Text Close Button that closes the UI on the client side without using a server command
            UiButton close = builder.CloseButton(titlePanel, _closeButtonPos, UiColors.CloseButton, UiName);
            builder.Label(close, UiPosition.HorizontalPaddedFull, "<b>X</b>", FontSize, UiColors.Text);
            
            //Create a Text Close Button that closes the UI using a server command
            builder.TextButton(titlePanel, _closeCmdButtonPos, "<b>X</b>", FontSize, UiColors.Text, UiColors.CloseButton, nameof(UiElementsCloseCommand));
            
            //Sections represents an invisible UI element used to parent UI elements to it
            UiSection body = builder.Section(builder.Root, _mainBodyPosition);
            
            //We reset the grid to it's initial position
            _grid.Reset();
            
            //We create a label with a background color
            builder.LabelBackground(body, _grid, "This is a label", FontSize, UiColors.Text, UiColors.PanelSecondary);
            
            //Move the grid to the next column
            _grid.MoveCols(1);
            
            //We create a panel
            builder.Panel(body, _grid, UiColors.PanelTertiary);
            _grid.MoveCols(1);
            
            //Creates a button that displays text
            builder.TextButton(body, _grid, "Text Button", FontSize, UiColors.Text, UiColors.ButtonPrimary, string.Empty);
            _grid.MoveCols(1);
            
            //Creates a button that displays an image from the web
            builder.WebImageButton(body, _grid, UiColor.White, "https://cdn.icon-icons.com/icons2/1381/PNG/512/rust_94773.png", string.Empty);
            _grid.MoveCols(1);
            
            //Creates a button that shows an item icon
            builder.ItemIconButton(body, _grid, UiColors.ButtonSecondary, 963906841, string.Empty);
            _grid.MoveCols(1);
            
            //Displays a web image
            builder.WebImage(body, _grid, "https://community.cloudflare.steamstatic.com/economy/image/6TMcQ7eX6E0EZl2byXi7vaVKyDk_zQLX05x6eLCFM9neAckxGDf7qU2e2gu64OnAeQ7835Ja5WrMfDY0jhyo8DEiv5daMKk6r70yQoJpxfiC/360fx360f");
            _grid.MoveCols(1);
            
            //Displays an item icon with the given skin ID
            builder.ItemIcon(body, _grid, 963906841, 2320435219);
            _grid.MoveCols(1);
            
            //Create a label and add a countdown timer to it.
            var countdownLabel = builder.LabelBackground(body, _grid, "Time Left: %TIME_LEFT%", FontSize, UiColor.White, UiColors.PanelSecondary);
            builder.Countdown(countdownLabel.Label, 100, 0, 1, string.Empty);
            
            //Adds a text outline to the countdownLabel
            builder.Outline(countdownLabel.Background, UiColors.Rust.Red, new Vector2(0.5f, -0.5f));
            _grid.MoveCols(1);
            
            //Creates an input field for the user to type in
            var input1 = builder.InputBackground(body, _grid, default(UiOffset), state.Input1Text, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsUpdateInput1), mode: InputMode.NeedsKeyboard | InputMode.AutoFocus);
            
            _grid.MoveCols(1);
            
            //Adds a border around the body UI
            builder.Border(body, UiColors.Rust.Red);
            
            //Creates a checkbox
            builder.Checkbox(body, _grid, default(UiOffset), state.Checkbox, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsToggleCheckbox));
            _grid.MoveCols(1);
            
            //Creates a number picker
            builder.NumberPicker(body, _numberPickerPos, default(UiOffset), state.NumberPicker, FontSize, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, UiColors.ButtonSecondary.WithAlpha(0.5f), nameof(UiElementsNumberPicker), "", "");
            _grid.MoveCols(1);
            
            //Creates a number picker where the user can type into as well
            //builder.IncrementalNumberPicker(body, _inputNumberPickerPos, default(UiOffset), state.InputPicker, _numberPickerIncrements, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, UiColors.ButtonSecondary.WithAlpha(0.5f), nameof(UiElementsInputNumberPicker));
            _grid.MoveCols(1);
            
            //Creates a paginator
            UiSection paginatorSection = builder.Section(body, _paginator);
            builder.Paginator(paginatorSection, _pagination, state.Page, 3, FontSize, UiColors.Text, UiColors.ButtonSecondary, UiColors.ButtonPrimary, nameof(UiElementsPage));
            
            builder.TextureImage(body, _grid, "assets/icons/change_code.png", UiColor.White);
            _grid.MoveCols(1);
            
            //Creates a button to open a modal
            builder.TextButton(body, _grid, "Open Modal", FontSize, UiColors.Text, UiColors.ButtonPrimary, nameof(UiElementsOpenModal));
            _grid.MoveCols(1);
            
            //Creates a drop down that when clicked opens a drop down menu
            builder.Dropdown(body, _grid, default(UiOffset), EnumCache<DropDownExample>.ToString(state.DropDownExample), FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsOpenDropdown));
            _grid.MoveCols(1);
            
            //Creates a drop down that supports a scroll bar
            builder.Dropdown(body, _grid, default(UiOffset), EnumCache<PageDropdownExample>.ToString(state.PageDropDownExample), FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsOpenPageDropdown));
            _grid.MoveCols(1);
            
            //Creates a drop down that supports a scroll bar
            builder.TimePicker(body, _grid, default(UiOffset), state.Time.AsDateTime(), FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsOpenTimePicker));
            _grid.MoveCols(1);
            
            builder.DatePicker(body, _grid, default(UiOffset), state.Date, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsOpenDatePicker));
            _grid.MoveCols(1);
            
            //builder.ColorPicker(body, _grid, default(UiOffset), state.Color, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsOpenColorPicker));
            
            // string imagePath = "Assets/Content/UI/gameui/cardgames/deck/clubs/2_clubs.";
            // builder.ImageSprite(body, _grid, imagePath + "png").SetSpriteMaterialImage(imagePath + "png", null, Image.Type.Sliced);
            // _grid.MoveCols(1);
            // builder.ImageSprite(body, _grid, imagePath + "psd", UiColor.Red);
            // _grid.MoveCols(1);
            // builder.ImageSprite(body, _grid, imagePath + "tga");
            // _grid.MoveCols(1);
            // builder.ImageSprite(body, _grid, imagePath + "jpg");
            // _grid.MoveCols(1);
            
            builder.AddUi(player);
            
            
            LogToFile("Main", string.Empty, this); //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            LogToFile("Main", builder.GetJsonString(), this); //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            
            builder.Dispose();
        }
        
        private void CreateModalUi(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.CreateModal(new UiOffset(400, 300), UiColors.Panel.WithAlpha(1f), UiModal);
            UiPanel panel = builder.Root as UiPanel;
            panel.SetSpriteMaterialImage(UiConstants.Sprites.RoundedBackground2, null, Image.Type.Sliced);
            
            builder.TextButton(builder.Root, new UiPosition(.9f, .9f, 1f, 1f), "<b>X</b>", 14, UiColors.Text, UiColor.Clear, nameof(UiElementsCloseModal));
            
            //builder.Border(builder.Root, UiColors.Rust.Red, 2);
            
            builder.AddUi(player);
            
            LogToFile("Modal", string.Empty, this); //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            LogToFile("Modal", builder.GetJsonString(), this); //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            
            builder.Dispose();
        }
        
        private static readonly GridPosition Skin = new GridPositionBuilder(2, 1).SetPadding(0.025f).Build();
        
        private void CreateSkinTest(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.Create(UiPosition.MiddleMiddle, new UiOffset(400, 300), UiColors.Body, UiSkin);
            builder.NeedsMouse();
            
            Skin.Reset();
            
            builder.ItemIcon(builder.Root, Skin, 963906841, 2084257363);
            Skin.MoveCols(1);
            
            builder.ItemIcon(builder.Root, Skin, 963906841, 2320435219ul);
            Skin.MoveCols(1);
            
            UiButton close = builder.CloseButton(builder.Root, _closeButtonPos, UiColors.CloseButton, UiSkin);
            builder.Label(close, UiPosition.HorizontalPaddedFull, "<b>X</b>", FontSize, UiColors.Text);
            
            builder.AddUi(player);
            
            //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            LogToFile("Skin", string.Empty, this);
            LogToFile("Skin", builder.GetJsonString(), this);
            
            builder.Dispose();
        }
        
        private void CreateLootBugUi(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.Create(UiPosition.MiddleMiddle, new UiOffset(100, 100), UiColor.Clear, UiLootBug);
            
            // UiInput bugged = builder.InputBackground(builder.Root, new UiPosition(0, 0.55f, 1, 1), "", 14, UiColors.Text, UiColors.Rust.Red, "");
            //
            // UiInput working = builder.InputBackground(builder.Root, new UiPosition(0, 0f, 1, 0.45f), "", 14, UiColors.Text, UiColors.Rust.Green, "");
            // working.SetRequiresKeyboard(false);
            
            builder.AddUi(player);
            
            LogToFile("LootBug", builder.GetJsonString(), this); //This code is for debugging purposes only. DO NOT USE IN PRODUCTION!!
            
            builder.Dispose();
        }
        #endregion
        
        #region UI Commands
        [ConsoleCommand(nameof(UiElementsCloseAll))]
        private void UiElementsCloseAll(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            UiBuilder.DestroyUi(player, UiName);
            UiBuilder.DestroyUi(player, UiClose);
            UiBuilder.DestroyUi(player, UiModal);
        }
        
        [ConsoleCommand(nameof(UiElementsCloseCommand))]
        private void UiElementsCloseCommand(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            UiBuilder.DestroyUi(player, UiName);
            UiBuilder.DestroyUi(player, UiClose);
        }
        
        [ConsoleCommand(nameof(UiElementsUpdateInput1))]
        private void UiElementsUpdateInput1(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            string update = arg.GetString(0);
            if (update == state.Input1Text)
            {
                return;
            }
            state.Input1Text = update;
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsToggleCheckbox))]
        private void UiElementsToggleCheckbox(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            state.Checkbox = !state.Checkbox;
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsNumberPicker))]
        private void UiElementsNumberPicker(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            state.NumberPicker = arg.GetInt(0);
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsInputNumberPicker))]
        private void UiElementsInputNumberPicker(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            int update = arg.GetInt(0);
            if (update == state.InputPicker)
            {
                return;
            }
            state.InputPicker = update;
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsPage))]
        private void UiElementsPage(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            state.Page = arg.GetInt(0);
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsOpenModal))]
        private void UiElementsOpenModal(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            CreateModalUi(player);
        }
        
        [ConsoleCommand(nameof(UiElementsCloseModal))]
        private void UiElementsCloseModal(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            UiBuilder.DestroyUi(player, UiModal);
        }
        
        [ConsoleCommand(nameof(UiElementsOpenDropdown))]
        private void UiElementsOpenDropdown(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            string parent = arg.GetString(0);
            PlayerState state = _playerStates[player.userID];
            UiDropdownMenu menu = UiBuilder.DropdownMenu(parent, _exampleList, FontSize + state.NumberPicker, UiColors.Text, UiColors.PanelTertiary, nameof(UiElementsSelectDropdownValue), minWidth: 0);
            menu.Builder.AddUi(player);
            menu.Dispose();
        }
        
        [ConsoleCommand(nameof(UiElementsOpenPageDropdown))]
        private void UiElementsOpenPageDropdown(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            string parent = arg.GetString(0);
            PlayerState state = _playerStates[player.userID];
            state.MenuDropdownPage = arg.GetInt(1);
            
            var menu = UiBuilder.DropdownMenu(parent, _pageExampleList, FontSize + state.NumberPicker, UiColors.Text, UiColors.PanelTertiary, nameof(UiElementsSelectPagedDropdownValue), $"{nameof(UiElementsOpenPageDropdown)} {parent}",
            state.MenuDropdownPage, 6, minWidth: 0);
            menu.Builder.AddUi(player);
            menu.Dispose();
        }
        
        [ConsoleCommand(nameof(UiElementsSelectDropdownValue))]
        private void UiElementsSelectDropdownValue(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            string value = arg.GetString(0);
            
            PlayerState state = _playerStates[player.userID];
            state.DropDownExample = (DropDownExample)Enum.Parse(typeof(DropDownExample), value);
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsSelectPagedDropdownValue))]
        private void UiElementsSelectPagedDropdownValue(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            string value = arg.GetString(0);
            
            PlayerState state = _playerStates[player.userID];
            state.PageDropDownExample = (PageDropdownExample)Enum.Parse(typeof(PageDropdownExample), value);
            
            CreateUi(player, state);
        }
        
        [ConsoleCommand(nameof(UiElementsOpenTimePicker))]
        private void UiElementsOpenTimePicker(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            
            //Interface.Oxide.LogDebug($"UiElementsOpenTimePicker: {arg.FullString}");
            
            string parent = arg.GetString(0);
            int adjustSeconds = arg.GetInt(1);
            if (adjustSeconds != 0)
            {
                state.Time.Update(adjustSeconds);
                //Interface.Oxide.LogDebug($"New Time: {state.Time}");
            }
            
            var menu = UiBuilder.TimePickerMenu(parent, state.Time, FontSize + state.NumberPicker, UiColors.Text, UiColors.PanelTertiary, $"{nameof(UiElementsOpenTimePicker)} {parent}", TimePickerDisplayMode.All, ClockMode.Hour12);
            menu.Builder.AddUi(player);
            menu.Dispose();
        }
        
        [ConsoleCommand(nameof(UiElementsOpenDatePicker))]
        private void UiElementsOpenDatePicker(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            
            //Interface.Oxide.LogDebug($"UiElementsOpenTimePicker: {arg.FullString}");
            
            string parent = arg.GetString(0);
            state.Date = arg.GetDateTime(1, state.Date);
            
            //var sw = Stopwatch.StartNew();
            
            UiCalenderPicker menu = UiBuilder.DateCalenderMenu(parent, state.Date, FontSize + state.NumberPicker, UiColors.Text, UiColors.PanelTertiary, UiColors.PanelTertiary.Darken(.15f), UiColors.Rust.Red,$"{nameof(UiElementsOpenDatePicker)} {parent}", PopoverPosition.Left);
            menu.Builder.AddUi(player);
            menu.Dispose();
            
            //sw.Stop();
            //Puts($"Took: {TimeSpan.FromTicks(sw.ElapsedTicks).TotalMilliseconds}ms");
        }
        
        [ConsoleCommand(nameof(UiElementsOpenColorPicker))]
        private void UiElementsOpenColorPicker(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }
            
            PlayerState state = _playerStates[player.userID];
            
            //Interface.Oxide.LogDebug($"UiElementsOpenTimePicker: {arg.FullString}");
            
            string parent = arg.GetString(0);
            state.Date = arg.GetDateTime(1, state.Date);
            
            var sw = Stopwatch.StartNew();
            
            //Puts($"{UiColors.PanelTertiary} {UiColors.PanelTertiary.ToGrayScale()}");
            
            // UiColorPickerMenu menu = UiBuilder.ColorPickerMenu(parent, state.Color, FontSize + state.NumberPicker, UiColors.Text, UiColors.PanelTertiary.Lighten(.15f), UiColors.PanelTertiary, UiColors.PanelTertiary.Darken(.15f), UiColors.PanelTertiary.Lighten(.15f),$"{nameof(UiElementsOpenDatePicker)} {parent}", ColorPickerMode.RGBA, PopoverPosition.Bottom);
            // menu.Builder.DestroyAndAddUi(player);
            // menu.Dispose();
            
            sw.Stop();
            Puts($"Took: {TimeSpan.FromTicks(sw.ElapsedTicks).TotalMilliseconds}ms");
        }
        #endregion
        
        #region Classes
        private class PlayerState
        {
            public int Page;
            public string Input1Text = "Default";
            public bool Checkbox = true;
            public int NumberPicker;
            public int InputPicker;
            public DropDownExample DropDownExample;
            public PageDropdownExample PageDropDownExample;
            public TimePickerData Time = new TimePickerData(DateTime.Now);
            public DateTime Date = DateTime.Now;
            public int MenuDropdownPage;
            public UiColor Color = UiColor.Red;
        }
        #endregion
        #endregion

    }

    //Framework Rust UI Framework v(1.4.4) by MJSU
    //UI Framework for Rust
    #region Merged Framework Rust UI Framework
    public partial class UiElementsTest
    {
        #region Plugin\UiFramework.Methods.cs
        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            UiFrameworkArrayPool<byte>.Clear();
            UiFrameworkArrayPool<char>.Clear();
            UiColorCache.OnUnload();
            UiNameCache.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
        #endregion
        public class UiConstants
        {
            public static class RpcFunctions
            {
                public const string AddUiFunc = "AddUI";
                public const string DestroyUiFunc = "DestroyUI";
            }
            
            public static class Materials
            {
                public const string InGameBlur = "assets/content/ui/uibackgroundblur-ingamemenu.mat";
                public const string NoticeBlur = "assets/content/ui/uibackgroundblur-notice.mat";
                public const string BackgroundBlur = "assets/content/ui/uibackgroundblur.mat";
                public const string Icon = "assets/icons/iconmaterial.mat";
            }
            
            public static class Sprites
            {
                public const string Default = "Assets/Content/UI/UI.Background.Tile.psd";
                public const string Transparent = "Assets/Content/Textures/Generic/fulltransparent.tga";
                public const string RoundedBackground1 = "Assets/Content/UI/UI.Rounded.tga";
                public const string RoundedBackground2 = "Assets/Content/UI/UI.Background.Rounded.png";
                public const string GradientUp = "Assets/Content/UI/UI.Gradient.Up.psd";
                public const string BackgroundTransparentLinear = "Assets/Content/UI/UI.Background.Transparent.Linear.png";
                public const string BackgroundTransparentLinearLtr = "Assets/Content/UI/UI.Background.Transparent.LinearLTR.png";
                public const string White = "Assets/Content/UI/UI.White.tga";
                public const string Circle = "Assets/Icons/circle_closed_white.png";
                public const string CircleToEdge = "Assets/Icons/circle_closed_white_toEdge.png";
                public const string Box = "Assets/Content/UI/UI.Box.tga";
                public const string BoxSharp = "Assets/Content/UI/UI.Box.Sharp.tga";
            }
        }
        public static class UiHelpers
        {
            public static int CalculateMaxPage(int count, int perPage)
            {
                int maxPage = count / perPage;
                if (count % perPage == 0)
                {
                    maxPage -= 1;
                }
                
                return maxPage;
            }
            
            public static int TextOffsetWidth(int length, int fontSize, float padding = 0)
            {
                return Mathf.CeilToInt(length * fontSize * 0.5f + padding * 2) + 1;
                //return (int)(length * fontSize * 1f) + 1;
            }
            
            public static int TextOffsetHeight(int fontSize, float padding = 0)
            {
                return Mathf.CeilToInt(fontSize * 1.25f + padding * 2);
            }
        }
        public abstract class BaseUiBuilder : BasePoolable
        {
            protected string RootName;
            
            public string GetRootName() => RootName;
            
            public abstract byte[] GetBytes();
            
            /// <summary>
            /// Warning this is only recommend to use for debugging purposes
            /// </summary>
            /// <returns></returns>
            public string GetJsonString() => Encoding.UTF8.GetString(GetBytes());
            
            #region Add UI
            public void AddUi(BasePlayer player)
            {
                if (player == null) throw new ArgumentNullException(nameof(player));
                AddUi(new SendInfo(player.Connection));
            }
            
            public void AddUi(Connection connection)
            {
                if (connection == null) throw new ArgumentNullException(nameof(connection));
                AddUi(new SendInfo(connection));
            }
            
            public void AddUi(List<Connection> connections)
            {
                if (connections == null) throw new ArgumentNullException(nameof(connections));
                AddUi(new SendInfo(connections));
            }
            
            public void AddUi()
            {
                AddUi(new SendInfo(Net.sv.connections));
            }
            
            protected abstract void AddUi(SendInfo send);
            
            protected void AddUi(SendInfo send, JsonFrameworkWriter writer)
            {
                NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
                if (write != null)
                {
                    writer.WriteToNetwork(write);
                    write.Send(send);
                }
            }
            
            protected void AddUi(SendInfo send, byte[] bytes)
            {
                NetWrite write = ClientRPCStart(UiConstants.RpcFunctions.AddUiFunc);
                if (write != null)
                {
                    write.BytesWithSize(bytes);
                    write.Send(send);
                }
            }
            
            private static NetWrite ClientRPCStart(string funcName)
            {
                if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null)
                {
                    return null;
                }
                
                NetWrite write = Net.sv.StartWrite();
                write.PacketID(Message.Type.RPCMessage);
                write.UInt32(CommunityEntity.ServerInstance.net.ID);
                write.UInt32(StringPool.Get(funcName));
                write.UInt64(0UL);
                return write;
            }
            #endregion
            
            #region Destroy UI
            public void DestroyUi(BasePlayer player)
            {
                if (!player) throw new ArgumentNullException(nameof(player));
                DestroyUi(player, RootName);
            }
            
            public void DestroyUi(Connection connection)
            {
                if (connection == null) throw new ArgumentNullException(nameof(connection));
                DestroyUi(new SendInfo(connection), RootName);
            }
            
            public void DestroyUi(List<Connection> connections)
            {
                if (connections == null) throw new ArgumentNullException(nameof(connections));
                DestroyUi(new SendInfo(connections), RootName);
            }
            
            public void DestroyUi()
            {
                DestroyUi(RootName);
            }
            
            public static void DestroyUi(BasePlayer player, string name)
            {
                if (player == null) throw new ArgumentNullException(nameof(player));
                DestroyUi(new SendInfo(player.Connection), name);
            }
            
            public static void DestroyUi(string name)
            {
                DestroyUi(new SendInfo(Net.sv.connections), name);
            }
            
            public static void DestroyUi(SendInfo send, string name)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(send, null, UiConstants.RpcFunctions.DestroyUiFunc, name);
            }
            #endregion
        }
        public partial class UiBuilder
        {
            #region Add Components
            public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
            {
                component.Parent = parent.Name;
                component.Name = UiNameCache.GetComponentName(RootName, _components.Count);
                _components.Add(component);
            }
            
            public void AddControl(BaseUiControl control)
            {
                _controls.Add(control);
            }
            
            private void AddAnchor(BaseUiComponent component, BaseUiComponent parent)
            {
                if (_anchors == null)
                {
                    _anchors = UiFrameworkPool.GetList<BaseUiComponent>();
                }
                
                component.Parent = parent.Name;
                component.Name = UiNameCache.GetAnchorName(RootName, _anchors.Count);
                
                _anchors.Add(component);
            }
            #endregion
            
            #region Section
            public UiSection Section(BaseUiComponent parent, UiPosition pos, UiOffset offset = default(UiOffset))
            {
                UiSection section = UiSection.Create(pos, offset);
                AddComponent(section, parent);
                return section;
            }
            #endregion
            
            #region Panel
            public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color)
            {
                UiPanel panel = UiPanel.Create(pos, offset, color);
                AddComponent(panel, parent);
                return panel;
            }
            
            public UiPanel Panel(BaseUiComponent parent, UiPosition pos, UiColor color) => Panel(parent, pos, default(UiOffset), color);
            #endregion
            
            #region Button
            public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string command)
            {
                UiButton button = UiButton.CreateCommand(pos, offset, color, command);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton CommandButton(BaseUiComponent parent, UiPosition pos, UiColor color, string command) => CommandButton(parent, pos, default(UiOffset), color, command);
            
            public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor color, string close)
            {
                UiButton button = UiButton.CreateClose(pos, offset, color, close);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton CloseButton(BaseUiComponent parent, UiPosition pos, UiColor color, string close) => CloseButton(parent, pos, default(UiOffset), color, close);
            #endregion
            
            #region Image
            public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite, UiColor color)
            {
                UiImage image = UiImage.CreateSpriteImage(pos, offset, color, sprite);
                AddComponent(image, parent);
                return image;
            }
            
            public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, UiOffset offset, string sprite) => ImageSprite(parent, pos, offset, sprite, UiColor.White);
            public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite, UiColor color) => ImageSprite(parent, pos, default(UiOffset), sprite, color);
            public UiImage ImageSprite(BaseUiComponent parent, UiPosition pos, string sprite) => ImageSprite(parent, pos, sprite, UiColor.White);
            #endregion
            
            #region Item Icon
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId, UiColor color)
            {
                UiItemIcon image = UiItemIcon.Create(pos, offset, color, itemId, skinId);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, ulong skinId) => ItemIcon(parent, pos, offset, itemId, skinId, UiColor.White);
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId, UiColor color) => ItemIcon(parent, pos, offset, itemId, 0, color);
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, UiOffset offset, int itemId) => ItemIcon(parent, pos, offset, itemId, UiColor.White);
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, ulong skinId) => ItemIcon(parent, pos, default(UiOffset), itemId, skinId);
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId, UiColor color) => ItemIcon(parent, pos, default(UiOffset), itemId, color);
            public UiItemIcon ItemIcon(BaseUiComponent parent, UiPosition pos, int itemId) => ItemIcon(parent, pos, default(UiOffset), itemId);
            #endregion
            
            #region Raw Image
            public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url, UiColor color)
            {
                if (!url.StartsWith("http"))
                {
                    throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use {nameof(ImageFileStorage)} instead");
                }
                
                UiRawImage image = UiRawImage.CreateUrl(pos, offset, color, url);
                AddComponent(image, parent);
                return image;
            }
            
            public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string url) => WebImage(parent, pos, offset, url, UiColor.White);
            public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url, UiColor color) => WebImage(parent, pos, default(UiOffset), url, color);
            public UiRawImage WebImage(BaseUiComponent parent, UiPosition pos, string url) => WebImage(parent, pos, url, UiColor.White);
            
            public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture, UiColor color)
            {
                UiRawImage image = UiRawImage.CreateTexture(pos, offset, color, texture);
                AddComponent(image, parent);
                return image;
            }
            
            public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string texture) => TextureImage(parent, pos, offset, texture, UiColor.White);
            public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture, UiColor color) => TextureImage(parent, pos, default(UiOffset), texture, color);
            public UiRawImage TextureImage(BaseUiComponent parent, UiPosition pos, string texture) => TextureImage(parent, pos, texture, UiColor.White);
            
            public UiRawImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png, UiColor color)
            {
                uint _;
                if (!uint.TryParse(png, out _))
                {
                    throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
                }
                
                UiRawImage image = UiRawImage.CreateFileImage(pos, offset, color, png);
                AddComponent(image, parent);
                return image;
            }
            
            public UiRawImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png, UiColor color) => ImageFileStorage(parent, pos, default(UiOffset), png, color);
            public UiRawImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, UiOffset offset, string png) => ImageFileStorage(parent, pos, offset, png, UiColor.White);
            public UiRawImage ImageFileStorage(BaseUiComponent parent, UiPosition pos, string png) => ImageFileStorage(parent, pos, default(UiOffset), png, UiColor.White);
            #endregion
            
            #region Label
            public UiLabel Label(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int size, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = UiLabel.Create(pos, offset, textColor, text, size, _font, align);
                AddComponent(label, parent);
                return label;
            }
            
            public UiLabel Label(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, TextAnchor align = TextAnchor.MiddleCenter) => Label(parent, pos, default(UiOffset), text, fontSize, textColor, align);
            
            public UiLabelBackground LabelBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabelBackground control = UiLabelBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, align);
                AddControl(control);
                return control;
            }
            
            public UiLabelBackground LabelBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter) => LabelBackground(parent, pos, default(UiOffset), text, fontSize, textColor, backgroundColor, align);
            #endregion
            
            #region Input
            public UiInput Input(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor,  string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                UiInput input = UiInput.Create(pos, offset, textColor, text, fontSize, command, _font, align, charsLimit, mode, lineType);
                AddComponent(input, parent);
                return input;
            }
            
            public UiInput Input(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
            => Input(parent, pos, default(UiOffset), text, fontSize, textColor, command, align, charsLimit, mode, lineType);
            #endregion
            
            #region Countdown
            public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
            {
                label.AddCountdown(startTime, endTime, step, command);
                return label;
            }
            #endregion
            
            #region Outline
            public T Outline<T>(T outline, UiColor color) where T : BaseUiOutline
            {
                outline.AddElementOutline(color);
                return outline;
            }
            
            public T Outline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha = false) where T : BaseUiOutline
            {
                outline.AddElementOutline(color, distance, useGraphicAlpha);
                return outline;
            }
            #endregion
            
            #region Anchor
            public UiSection Anchor(BaseUiComponent parent, UiPosition pos, UiOffset offset = default(UiOffset))
            {
                UiSection section = UiSection.Create(pos, offset);
                AddAnchor(section, parent);
                return section;
            }
            #endregion
        }
        public partial class UiBuilder
        {
            #region Buttons
            public UiButton TextButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                Label(button, UiPosition.HorizontalPaddedFull, text, textSize, textColor , align);
                return button;
            }
            
            public UiButton TextButton(BaseUiComponent parent, UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string command, TextAnchor align = TextAnchor.MiddleCenter)
            => TextButton(parent, pos, default(UiOffset), text, textSize, textColor, buttonColor, command, align);
            
            public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string png, string command)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                ImageFileStorage(button, UiPosition.Full, png);
                return button;
            }
            
            public UiButton ImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string png, string command) => ImageFileStorageButton(parent, pos, default(UiOffset), buttonColor, png, command);
            
            public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string sprite, string command)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                ImageSprite(button, UiPosition.Full, sprite);
                return button;
            }
            
            public UiButton ImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string sprite, string command) => ImageSpriteButton(parent, pos, default(UiOffset), buttonColor, sprite, command);
            
            public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string url, string command)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                WebImage(button, UiPosition.Full, url);
                return button;
            }
            
            public UiButton WebImageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string url, string command) => WebImageButton(parent, pos, default(UiOffset), buttonColor, url, command);
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, string command)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                ItemIcon(button, UiPosition.Full, itemId);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, command);
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string command)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, command);
                ItemIcon(button, UiPosition.Full, itemId, skinId);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string command) => ItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, skinId, command);
            
            public UiButton CloseTextButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
                Label(button, UiPosition.HorizontalPaddedFull, text, textSize, textColor , align);
                return button;
            }
            
            public UiButton CloseTextButton(BaseUiComponent parent, UiPosition pos, string text, int textSize, UiColor textColor, UiColor buttonColor, string close, TextAnchor align = TextAnchor.MiddleCenter)
            => CloseTextButton(parent, pos, default(UiOffset), text, textSize, textColor, buttonColor, close, align);
            
            public UiButton CloseImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string png, string close)
            {
                UiButton button = CommandButton(parent, pos, offset, buttonColor, close);
                ImageFileStorage(button, UiPosition.Full, png);
                return button;
            }
            
            public UiButton CloseImageFileStorageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string png, string close) => CloseImageFileStorageButton(parent, pos, default(UiOffset), buttonColor, png, close);
            
            public UiButton CloseImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string sprite, string close)
            {
                UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
                ImageSprite(button, UiPosition.Full, sprite);
                return button;
            }
            
            public UiButton CloseImageSpriteButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string sprite, string close) => CloseImageSpriteButton(parent, pos, default(UiOffset), buttonColor, sprite, close);
            
            public UiButton CloseWebImageButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, string url, string close)
            {
                UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
                WebImage(button, UiPosition.Full, url);
                return button;
            }
            
            public UiButton CloseWebImageButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, string url, string close) => CloseWebImageButton(parent, pos, default(UiOffset), buttonColor, url, close);
            
            public UiButton CloseItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, string close)
            {
                UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
                ItemIcon(button, UiPosition.Full, itemId);
                return button;
            }
            
            public UiButton CloseItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, string close) => CloseItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, close);
            
            public UiButton CloseItemIconButton(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor buttonColor, int itemId, ulong skinId, string close)
            {
                UiButton button = CloseButton(parent, pos, offset, buttonColor, close);
                ItemIcon(button, UiPosition.Full, itemId, skinId);
                return button;
            }
            
            public UiButton CloseItemIconButton(BaseUiComponent parent, UiPosition pos, UiColor buttonColor, int itemId, ulong skinId, string close) => CloseItemIconButton(parent, pos, default(UiOffset), buttonColor, itemId, skinId, close);
            #endregion
            
            #region Input Background
            public UiInputBackground InputBackground(BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                UiInputBackground control = UiInputBackground.Create(this, parent, pos, offset, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
                AddControl(control);
                return control;
            }
            
            public UiInputBackground InputBackground(BaseUiComponent parent, UiPosition pos, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine) =>
            InputBackground(parent, pos, UiOffset.None, text, fontSize, textColor, backgroundColor, command, align, charsLimit, mode, lineType);
            #endregion
            
            #region Checkbox
            public UiCheckbox Checkbox(BaseUiComponent parent, UiPosition pos, UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
            {
                UiCheckbox checkbox = UiCheckbox.CreateCheckbox(this, parent, pos, offset, isChecked, textSize, textColor, backgroundColor, command);
                AddControl(checkbox);
                return checkbox;
            }
            #endregion
            
            #region ProgressBar
            public UiProgressBar ProgressBar(BaseUiComponent parent, UiPosition pos, UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
            {
                UiProgressBar control = UiProgressBar.Create(this, parent, pos, offset, percentage, barColor, backgroundColor);
                AddControl(control);
                return control;
            }
            #endregion
            
            #region Button Groups
            public UiButtonGroup ButtonGroup(BaseUiComponent parent, UiPosition pos, UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
            {
                UiButtonGroup control = UiButtonGroup.Create(this, parent, pos, offset, buttons, textSize, textColor, buttonColor, currentButtonColor, command);
                AddControl(control);
                return control;
            }
            
            public UiButtonGroup NumericButtonGroup(BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor currentButtonColor, string command)
            {
                UiButtonGroup control = UiButtonGroup.CreateNumeric(this, parent, pos, offset, value, minValue, maxValue, textSize, textColor, buttonColor, currentButtonColor, command);
                AddControl(control);
                return control;
            }
            #endregion
            
            #region Number Picker
            public UiNumberPicker NumberPicker(BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, string incrementCommand, string decrementCommand, int minValue = int.MinValue, int maxValue = int.MaxValue, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, InputMode mode = InputMode.Default, NumberPickerMode numberMode = NumberPickerMode.LeftRight, string numberFormat = null)
            {
                UiNumberPicker control = UiNumberPicker.Create(this, parent, pos, offset, value, fontSize, buttonFontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, command, incrementCommand, decrementCommand, minValue, maxValue, buttonWidth, align, mode, numberMode, numberFormat);
                AddControl(control);
                return control;
            }
            
            public UiIncrementalNumberPicker<T> IncrementalNumberPicker<T>(BaseUiComponent parent, UiPosition pos, UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, T minValue , T maxValue, InputMode mode = InputMode.Default, float buttonWidth = 0.1f, TextAnchor align = TextAnchor.MiddleRight, string incrementFormat = "0", string numberFormat = null)  where T : struct, IConvertible, IFormattable, IComparable<T>
            {
                UiIncrementalNumberPicker<T> control = UiIncrementalNumberPicker<T>.Create(this, parent, pos, offset, value, increments, fontSize, textColor, backgroundColor, buttonColor, disabledButtonColor, command, align, mode, minValue, maxValue, buttonWidth, incrementFormat, numberFormat);
                AddControl(control);
                return control;
            }
            #endregion
            
            #region Paginator
            public UiPaginator Paginator(BaseUiComponent parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
            {
                UiPaginator control = UiPaginator.Create(this, parent, grid, currentPage, maxPage, fontSize, textColor, buttonColor, activePageColor, command);
                AddControl(control);
                return control;
            }
            #endregion
            
            #region Scroll Bar
            public UiScrollBar ScrollBar(BaseUiComponent parent, UiPosition position, UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction = ScrollbarDirection.Vertical, string sprite = UiConstants.Sprites.RoundedBackground2)
            {
                UiScrollBar control = UiScrollBar.Create(this, parent, position, offset, currentPage, maxPage, barColor, backgroundColor, command, direction, sprite);
                AddControl(control);
                return control;
            }
            #endregion
            
            #region Dropdown
            public UiDropdown Dropdown(BaseUiComponent parent, UiPosition pos, UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
            {
                UiDropdown control = UiDropdown.Create(this, parent, pos, offset, displayValue, fontSize, textColor, backgroundColor, openCommand);
                AddControl(control);
                return control;
            }
            
            public static UiDropdownMenu DropdownMenu(string parentName, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand, string pageCommand = null, int page = 0, int maxValuesPerPage = 100, int minWidth = 100,
            PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                UiDropdownMenu control = UiDropdownMenu.Create(parentName, items, fontSize, textColor, backgroundColor, selectedCommand, pageCommand, page, maxValuesPerPage, minWidth, position, menuSprite);
                return control;
            }
            #endregion
            
            #region Time Picker
            public UiTimePicker TimePicker(BaseUiComponent parent, UiPosition pos, UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "hh:mm:ss tt")
            {
                var control = UiTimePicker.Create(this, parent, pos, offset, time, fontSize, textColor, backgroundColor, openCommand, displayFormat);
                AddControl(control);
                return control;
            }
            
            public static UiTimePickerMenu TimePickerMenu(string parentName, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, TimePickerDisplayMode displayMode = TimePickerDisplayMode.All, ClockMode clockMode = ClockMode.Hour12,
            PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                UiTimePickerMenu picker = UiTimePickerMenu.Create(parentName, time, fontSize, textColor, backgroundColor, changeCommand, displayMode, clockMode, position, menuSprite);
                return picker;
            }
            #endregion
            
            #region Date Picker
            public UiDatePicker DatePicker(BaseUiComponent parent, UiPosition pos, UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
            {
                UiDatePicker picker = UiDatePicker.Create(this, parent, pos,offset, date, fontSize, textColor, backgroundColor, openCommand);
                return picker;
            }
            
            public static UiCalenderPicker DateCalenderMenu(string parentName, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, PopoverPosition position, string menuSprite = UiConstants.Sprites.RoundedBackground2, string buttonSprite = UiConstants.Sprites.RoundedBackground1)
            {
                UiCalenderPicker picker = UiCalenderPicker.Create(parentName, date, fontSize, textColor, backgroundColor, buttonColor, selectedDateColor, changeCommand, position, menuSprite, buttonSprite);
                return picker;
            }
            #endregion
            
            #region Color Picker
            // public UiColorPicker ColorPicker(BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor selectedColor, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
            // {
                //     UiColorPicker control = UiColorPicker.Create(this, parent, pos, offset, selectedColor, fontSize, textColor, backgroundColor, openCommand);
                //     AddControl(control);
                //     return control;
            // }
            //
            // public static UiColorPickerMenu ColorPickerMenu(string parentName, UiColor selectedColor, int fontSize, UiColor textColor, UiColor buttonColor, UiColor backgroundColor, UiColor pickerBackgroundColor, UiColor pickerDisabledColor, string command, ColorPickerMode mode, PopoverPosition position, string menuSprite = UiConstants.Sprites.RoundedBackground2, InputMode inputMode = InputMode.NeedsKeyboard)
            // {
                //     UiColorPickerMenu picker = UiColorPickerMenu.Create(parentName, selectedColor, fontSize, textColor, buttonColor, backgroundColor, pickerBackgroundColor, pickerDisabledColor, command, mode, position, menuSprite, inputMode);
                //     return picker;
            // }
            #endregion
            
            #region Border
            public UiBorder Border(BaseUiComponent parent, UiColor color, int width = 1, BorderMode border = BorderMode.All)
            {
                UiBorder control = UiBorder.Create(this, parent, color, width, border);
                AddControl(control);
                return control;
            }
            #endregion
        }
        public partial class UiBuilder
        {
            public static UiBuilder Create(UiPosition pos, string name, string parent) => Create(pos, default(UiOffset), name, parent);
            public static UiBuilder Create(UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), name, UiLayerCache.GetLayer(parent));
            public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, name, UiLayerCache.GetLayer(parent));
            public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, string parent) => Create(UiSection.Create(pos, offset), name, parent);
            public static UiBuilder Create(UiPosition pos, UiColor color, string name, string parent) => Create(pos, default(UiOffset), color, name, parent);
            public static UiBuilder Create(UiPosition pos, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), color, name, UiLayerCache.GetLayer(parent));
            public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, color, name, UiLayerCache.GetLayer(parent));
            public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, string parent) => Create(UiPanel.Create(pos, offset, color), name, parent);
            public static UiBuilder Create(BaseUiComponent root, string name, UiLayer parent = UiLayer.Overlay) => Create(root, name, UiLayerCache.GetLayer(parent));
            public static UiBuilder Create(BaseUiComponent root, string name, string parent)
            {
                UiBuilder builder = Create();
                builder.SetRoot(root, name, parent);
                return builder;
            }
            
            public static UiBuilder Create()
            {
                return UiFrameworkPool.Get<UiBuilder>();
            }
            
            /// <summary>
            /// Creates a UiBuilder that is designed to be a popup modal
            /// </summary>
            /// <param name="modalSize">Dimensions of the modal</param>
            /// <param name="modalColor">Modal form color</param>
            /// <param name="name">Name of the UI</param>
            /// <param name="layer">Layer the UI is on</param>
            /// <returns></returns>
            public static UiBuilder CreateModal(UiOffset modalSize, UiColor modalColor, string name, UiLayer layer = UiLayer.Overlay)
            {
                return CreateModal(modalSize, modalColor, new UiColor(0, 0, 0, 0.5f), name, layer, UiConstants.Materials.InGameBlur);
            }
            
            /// <summary>
            /// Creates a UiBuilder that is designed to be a popup modal
            /// </summary>
            /// <param name="modalSize">Dimensions of the modal</param>
            /// <param name="modalColor">Modal form color</param>
            /// <param name="name">Name of the UI</param>
            /// <param name="layer">Layer the UI is on</param>
            /// <param name="modalBackgroundColor">Color of the fullscreen background</param>
            /// <param name="backgroundMaterial">Material of the full screen background</param>
            /// <returns></returns>
            public static UiBuilder CreateModal(UiOffset modalSize, UiColor modalColor, UiColor modalBackgroundColor, string name, UiLayer layer = UiLayer.Overlay, string backgroundMaterial = null)
            {
                UiPanel backgroundBlur = UiPanel.Create(UiPosition.Full, default(UiOffset), modalBackgroundColor);
                backgroundBlur.SetMaterial(backgroundMaterial);
                
                UiBuilder builder = Create(backgroundBlur, name, layer);
                
                UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, modalSize, modalColor);
                builder.AddComponent(modal, backgroundBlur);
                builder.OverrideRoot(modal);
                return builder;
            }
            
            /// <summary>
            /// Creates a UI builder that when created before your main UI will run a command if the user click outside of the UI window
            /// </summary>
            /// <param name="command">Command to run when the button is clicked</param>
            /// <param name="name">Name of the UI</param>
            /// <param name="layer">Layer the UI is on</param>
            /// <returns></returns>
            public static UiBuilder CreateOutsideClose(string command, string name, UiLayer layer = UiLayer.Overlay)
            {
                UiBuilder builder = Create(UiButton.CreateCommand(UiPosition.Full, default(UiOffset), UiColor.Clear, command), name, layer);
                builder.NeedsMouse();
                return builder;
            }
            
            /// <summary>
            /// Creates a UI builder that will hold mouse input so the mouse doesn't reset position when updating other UI's
            /// </summary>
            /// <param name="name">Name of the UI</param>
            /// <param name="layer">Layer the UI is on</param>
            /// <returns></returns>
            public static UiBuilder CreateMouseLock(string name, UiLayer layer = UiLayer.Overlay)
            {
                UiBuilder builder = Create(UiPosition.None, UiColor.Clear, name, UiLayerCache.GetLayer(layer));
                builder.NeedsMouse();
                return builder;
            }
            
            public static UiPopover Popover(string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                UiPopover control = UiPopover.Create(parentName, size, backgroundColor, position, menuSprite);
                return control;
            }
        }
        public partial class UiBuilder : BaseUiBuilder
        {
            public BaseUiComponent Root;
            
            private bool _needsMouse;
            private bool _needsKeyboard;
            private bool _autoDestroy = true;
            
            private string _font;
            
            private List<BaseUiComponent> _components;
            private List<BaseUiControl> _controls;
            private List<BaseUiComponent> _anchors;
            
            private static string _globalFont;
            
            #region Constructor
            static UiBuilder()
            {
                SetGlobalFont(UiFont.RobotoCondensedRegular);
            }
            
            public void EnsureCapacity(int capacity)
            {
                if (_components.Capacity < capacity)
                {
                    _components.Capacity = capacity;
                }
            }
            
            public void SetRoot(BaseUiComponent component, string name, string parent)
            {
                Root = component;
                component.Parent = parent;
                component.Name = name;
                _components.Add(component);
                RootName = name;
            }
            
            public void OverrideRoot(BaseUiComponent component)
            {
                Root = component;
            }
            
            public void NeedsMouse(bool enabled = true)
            {
                _needsMouse = enabled;
            }
            
            public void NeedsKeyboard(bool enabled = true)
            {
                _needsKeyboard = enabled;
            }
            
            public void EnableAutoDestroy(bool enabled = true)
            {
                _autoDestroy = enabled;
            }
            
            public void SetCurrentFont(UiFont font)
            {
                _font = UiFontCache.GetUiFont(font);
            }
            
            public static void SetGlobalFont(UiFont font)
            {
                _globalFont = UiFontCache.GetUiFont(font);
            }
            
            // public T GetUi<T>(string name) where T : BaseUiComponent
            // {
                //     return (T)_componentLookup[name];
            // }
            #endregion
            
            #region Decontructor
            ~UiBuilder()
            {
                Dispose();
                //Need this because there is a global GC class that causes issues
                //ReSharper disable once RedundantNameQualifier
                System.GC.SuppressFinalize(this);
            }
            
            protected override void EnterPool()
            {
                FreeComponents();
                
                Root = null;
                _needsKeyboard = false;
                _needsMouse = false;
                _font = null;
                RootName = null;
                _autoDestroy = true;
            }
            
            private void FreeComponents()
            {
                if (_components != null)
                {
                    int count = _components.Count;
                    for (int index = 0; index < count; index++)
                    {
                        _components[index].Dispose();
                    }
                    
                    UiFrameworkPool.FreeList(_components);
                }
                
                if (_controls != null)
                {
                    int count = _controls.Count;
                    for (int index = 0; index < count; index++)
                    {
                        _controls[index].Dispose();
                    }
                    
                    UiFrameworkPool.FreeList(_controls);
                }
                
                if (_anchors != null)
                {
                    int count = _anchors.Count;
                    for (int index = 0; index < count; index++)
                    {
                        _anchors[index].Dispose();
                    }
                    
                    UiFrameworkPool.FreeList(_anchors);
                }
            }
            
            protected override void LeavePool()
            {
                _components = UiFrameworkPool.GetList<BaseUiComponent>();
                _controls = UiFrameworkPool.GetList<BaseUiControl>();
                _font = _globalFont;
            }
            #endregion
            
            #region JSON
            public int WriteBuffer(byte[] buffer)
            {
                JsonFrameworkWriter writer = CreateWriter();
                int bytes = writer.WriteTo(buffer);
                writer.Dispose();
                return bytes;
            }
            
            public JsonFrameworkWriter CreateWriter()
            {
                JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
                
                writer.WriteStartArray();
                
                if (_components == null)
                {
                    throw new Exception("Components List is null. Was UiBuilder not created from pool?");
                }
                
                if (_controls == null)
                {
                    throw new Exception("Controls List is null. Was UiBuilder not created from pool?");
                }
                
                int count;
                if (_controls.Count != 0)
                {
                    count = _controls.Count;
                    for (int index = 0; index < count; index++)
                    {
                        BaseUiControl control = _controls[index];
                        control.RenderControl(this);
                    }
                }
                
                _components[0].WriteRootComponent(writer, _needsMouse, _needsKeyboard, _autoDestroy);
                
                count = _components.Count;
                for (int index = 1; index < count; index++)
                {
                    _components[index].WriteComponent(writer);
                }
                
                
                if (_anchors != null)
                {
                    count = _anchors.Count;
                    for (int index = 0; index < count; index++)
                    {
                        _anchors[index].WriteComponent(writer);
                    }
                }
                
                writer.WriteEndArray();
                return writer;
            }
            
            public CachedUiBuilder ToCachedBuilder()
            {
                CachedUiBuilder cached = CachedUiBuilder.CreateCachedBuilder(this);
                if (!Disposed)
                {
                    Dispose();
                }
                return cached;
            }
            
            public override byte[] GetBytes()
            {
                JsonFrameworkWriter writer = CreateWriter();
                byte[] bytes = writer.ToArray();
                writer.Dispose();
                return bytes;
            }
            
            protected override void AddUi(SendInfo send)
            {
                JsonFrameworkWriter writer = CreateWriter();
                AddUi(send, writer);
                writer.Dispose();
                if (!Disposed)
                {
                    Dispose();
                }
            }
            #endregion
        }
        public static class EnumCache<T>
        {
            private static readonly Dictionary<T, string> CachedStrings = new Dictionary<T, string>();
            
            static EnumCache()
            {
                foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
                {
                    CachedStrings[value] = value.ToString();
                }
            }
            
            public static string ToString(T value)
            {
                return CachedStrings[value];
            }
        }
        public static class StringCache<T> where T : IFormattable
        {
            private static readonly Dictionary<T, string> Cache = new Dictionary<T, string>();
            private static readonly Dictionary<string, Dictionary<T, string>>  FormatCache = new Dictionary<string, Dictionary<T, string>>();
            
            public static string ToString(T value)
            {
                string text;
                if (!Cache.TryGetValue(value, out text))
                {
                    text = value.ToString();
                    Cache[value] = text;
                }
                
                return text;
            }
            
            public static string ToString(T value, string format)
            {
                Dictionary<T, string> values;
                if (!FormatCache.TryGetValue(format, out values))
                {
                    values = new Dictionary<T, string>();
                    FormatCache[format] = values;
                }
                
                string text;
                if (!values.TryGetValue(value, out text))
                {
                    text = value.ToString(format, NumberFormatInfo.CurrentInfo);
                    values[value] = text;
                }
                
                return text;
            }
        }
        public static class UiColorCache
        {
            private const string Format = "0.####";
            private const char Space = ' ';
            
            private static readonly Dictionary<uint, string> ColorCache = new Dictionary<uint, string>();
            
            public static void WriteColor(JsonBinaryWriter writer, UiColor uiColor)
            {
                string color;
                if (!ColorCache.TryGetValue(uiColor.Value, out color))
                {
                    color = GetColor(uiColor);
                    ColorCache[uiColor.Value] = color;
                }
                
                writer.Write(color);
            }
            
            private static string GetColor(Color color)
            {
                StringBuilder builder = UiFrameworkPool.GetStringBuilder();
                builder.Append(color.r.ToString(Format));
                builder.Append(Space);
                builder.Append(color.g.ToString(Format));
                builder.Append(Space);
                builder.Append(color.b.ToString(Format));
                if (color.a < 1f)
                {
                    builder.Append(Space);
                    builder.Append(color.a.ToString(Format));
                }
                
                return builder.ToStringAndFree();
            }
            
            public static void OnUnload()
            {
                ColorCache.Clear();
            }
        }
        public static class UiFontCache
        {
            private const string DroidSansMono = "droidsansmono.ttf";
            private const string PermanentMarker = "permanentmarker.ttf";
            private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
            private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
            private const string PressStart2PRegular = "PressStart2P-Regular.ttf";
            
            private static readonly Dictionary<UiFont, string> Fonts = new Dictionary<UiFont, string>
            {
                [UiFont.DroidSansMono] = DroidSansMono,
                [UiFont.PermanentMarker] = PermanentMarker,
                [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
                [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
                [UiFont.PressStart2PRegular] = PressStart2PRegular,
            };
            
            public static string GetUiFont(UiFont font)
            {
                return Fonts[font];
            }
        }
        public static class UiLayerCache
        {
            private const string Overall = "Overall";
            private const string Overlay = "Overlay";
            private const string Hud = "Hud";
            private const string HudMenu = "Hud.Menu";
            private const string Under = "Under";
            
            private static readonly Dictionary<UiLayer, string> Layers = new Dictionary<UiLayer, string>
            {
                [UiLayer.Overall] = Overall,
                [UiLayer.Overlay] = Overlay,
                [UiLayer.Hud] = Hud,
                [UiLayer.HudMenu] = HudMenu,
                [UiLayer.Under] = Under,
            };
            
            public static string GetLayer(UiLayer layer)
            {
                return Layers[layer];
            }
        }
        public static class UiNameCache
        {
            private static readonly Dictionary<string, List<string>> ComponentNameCache = new Dictionary<string, List<string>>();
            private static readonly Dictionary<string, List<string>> AnchorNameCache = new Dictionary<string, List<string>>();
            
            public static string GetComponentName(string baseName, int index) => GetName(ComponentNameCache, baseName, "_", index);
            public static string GetAnchorName(string baseName, int index) => GetName(AnchorNameCache, baseName, "_anchor_", index);
            
            private static string GetName(Dictionary<string, List<string>> cache, string baseName, string splitter, int index)
            {
                List<string> names;
                if (!cache.TryGetValue(baseName, out names))
                {
                    names = new List<string>();
                    cache[baseName] = names;
                }
                
                if (index >= names.Count)
                {
                    for (int i = names.Count; i <= index; i++)
                    {
                        names.Add(string.Concat(baseName, splitter, index.ToString()));
                    }
                }
                
                return names[index];
            }
            
            public static void OnUnload()
            {
                ComponentNameCache.Clear();
                AnchorNameCache.Clear();
            }
        }
        public static class VectorCache
        {
            private const string Format = "0.####";
            private const char Space = ' ';
            private const short PositionRounder = 10000;
            
            private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
            
            static VectorCache()
            {
                for (ushort i = 0; i <= PositionRounder; i++)
                {
                    PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
                }
            }
            
            public static void WritePosition(JsonBinaryWriter writer, Vector2 pos)
            {
                WriteFromCache(writer, pos.x);
                writer.Write(Space);
                WriteFromCache(writer, pos.y);
            }
            
            private static void WriteFromCache(JsonBinaryWriter writer, float pos)
            {
                if (pos >= 0f && pos <= 1f)
                {
                    writer.Write(PositionCache[(ushort)(pos * PositionRounder)]);
                }
                else
                {
                    string value;
                    if (!PositionCache.TryGetValue((ushort)(pos * PositionRounder), out value))
                    {
                        value = pos.ToString(Format);
                        PositionCache[(ushort)(pos * PositionRounder)] = value;
                    }
                    
                    writer.Write(value);
                }
            }
            
            public static void WriteVector(JsonBinaryWriter writer, Vector2 pos)
            {
                string formattedPos;
                if (!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out formattedPos))
                {
                    formattedPos = pos.x.ToString(Format);
                    PositionCache[(ushort)(pos.x * PositionRounder)] = formattedPos;
                }
                
                writer.Write(formattedPos);
                writer.Write(Space);
                
                if (!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out formattedPos))
                {
                    formattedPos = pos.y.ToString(Format);
                    PositionCache[(ushort)(pos.y * PositionRounder)] = formattedPos;
                }
                
                writer.Write(formattedPos);
            }
            
            public static void WriteOffset(JsonBinaryWriter writer, Vector2 pos)
            {
                writer.Write(StringCache<short>.ToString((short)Math.Round(pos.x)));
                writer.Write(Space);
                writer.Write(StringCache<short>.ToString((short)Math.Round(pos.y)));
            }
        }
        [JsonConverter(typeof(UiColorConverter))]
        public struct UiColor : IEquatable<UiColor>
        {
            #region Fields
            public readonly uint Value;
            public readonly Color Color;
            #endregion
            
            #region Static Colors
            public static readonly UiColor Black =  "#000000";
            public static readonly UiColor White = "#FFFFFF";
            public static readonly UiColor Silver =  "#C0C0C0";
            public static readonly UiColor Gray = "#808080";
            public static readonly UiColor Red = "#FF0000";
            public static readonly UiColor Maroon = "#800000";
            public static readonly UiColor Orange = "#FFA500";
            public static readonly UiColor Yellow = "#FFEB04";
            public static readonly UiColor Olive = "#808000";
            public static readonly UiColor Lime = "#00FF00";
            public static readonly UiColor Green = "#008000";
            public static readonly UiColor Teal = "#008080";
            public static readonly UiColor Cyan = "#00FFFF";
            public static readonly UiColor Blue = "#0000FF";
            public static readonly UiColor Navy = "#000080";
            public static readonly UiColor Magenta = "#FF00FF";
            public static readonly UiColor Purple = "#800080";
            public static readonly UiColor Clear = "#00000000";
            #endregion
            
            #region Constructors
            public UiColor(Color color)
            {
                Color = color;
                Value = ((uint)(color.r * 255) << 24) | ((uint)(color.g * 255) << 16) | ((uint)(color.b * 255) << 8) | (uint)(color.a * 255);
            }
            
            public UiColor(int red, int green, int blue, int alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
            {
                
            }
            
            public UiColor(byte red, byte green, byte blue, byte alpha = 255) : this(red / 255f, green / 255f, blue / 255f, alpha / 255f)
            {
                
            }
            
            public UiColor(float red, float green, float blue, float alpha = 1f) : this(new Color(Mathf.Clamp01(red), Mathf.Clamp01(green), Mathf.Clamp01(blue), Mathf.Clamp01(alpha)))
            {
                
            }
            #endregion
            
            #region Operators
            public static implicit operator UiColor(string value) => ParseHexColor(value);
            public static implicit operator UiColor(Color value) => new UiColor(value);
            public static implicit operator Color(UiColor value) => value.Color;
            public static bool operator ==(UiColor lhs, UiColor rhs) => lhs.Value == rhs.Value;
            public static bool operator !=(UiColor lhs, UiColor rhs) => !(lhs == rhs);
            
            public bool Equals(UiColor other)
            {
                return Value == other.Value;
            }
            
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is UiColor && Equals((UiColor)obj);
            }
            
            public override int GetHashCode()
            {
                return (int)Value;
            }
            
            public override string ToString()
            {
                return $"{Color.r} {Color.g} {Color.b} {Color.a}";
            }
            #endregion
            
            #region Formats
            public string ToHexRGB()
            {
                return ColorUtility.ToHtmlStringRGB(Color);
            }
            
            public string ToHexRGBA()
            {
                return ColorUtility.ToHtmlStringRGBA(Color);
            }
            
            public string ToHtmlColor()
            {
                return $"#{ColorUtility.ToHtmlStringRGBA(Color)}";
            }
            #endregion
            
            #region Parsing
            /// <summary>
            /// Valid Rust Color Formats
            /// 0 0 0
            /// 0.0 0.0 0.0 0.0
            /// 1.0 1.0 1.0
            /// 1.0 1.0 1.0 1.0
            /// </summary>
            /// <param name="color"></param>
            public static UiColor ParseRustColor(string color)
            {
                return new UiColor(ColorEx.Parse(color));
            }
            
            /// <summary>
            /// <a href="https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html">Unity ColorUtility.TryParseHtmlString API reference</a>
            /// Valid Hex Color Formats
            /// #RGB
            /// #RRGGBB
            /// #RGBA
            /// #RRGGBBAA
            /// </summary>
            /// <param name="hexColor"></param>
            /// <returns></returns>
            /// <exception cref="UiFrameworkException"></exception>
            public static UiColor ParseHexColor(string hexColor)
            {
                #if BENCHMARKS
                Color colorValue = Color.black;
                #else
                Color colorValue;
                if (!ColorUtility.TryParseHtmlString(hexColor, out colorValue))
                {
                    throw new UiFrameworkException($"Invalid Color: '{hexColor}' Hex colors must start with a '#'");
                }
                #endif
                
                return new UiColor(colorValue);
            }
            #endregion
        }
        public class UiColorConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((UiColor)value).ToHtmlColor());
            }
            
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                    if (Nullable.GetUnderlyingType(objectType) != null)
                    {
                        return null;
                    }
                    
                    return default(UiColor);
                    
                    case JsonToken.String:
                    return UiColor.ParseHexColor(reader.Value.ToString());
                }
                
                return default(UiColor);
            }
            
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(UiColor);
            }
        }
        public static class UiColors
        {
            public static class BootStrap
            {
                public static readonly UiColor Blue = "#007bff";
                public static readonly UiColor Indigo = "#6610f2";
                public static readonly UiColor Purple = "#6f42c1";
                public static readonly UiColor Pink = "#e83e8c";
                public static readonly UiColor Red = "#dc3545";
                public static readonly UiColor Orange = "#fd7e14";
                public static readonly UiColor Yellow = "#ffc107";
                public static readonly UiColor Green = "#28a745";
                public static readonly UiColor Teal = "#20c997";
                public static readonly UiColor Cyan = "#17a2b8";
                public static readonly UiColor White = "#fff";
                public static readonly UiColor Gray = "#6c757d";
                public static readonly UiColor DarkGray = "#343a40";
                public static readonly UiColor Primary = "#007bff";
                public static readonly UiColor Secondary = "#6c757d";
                public static readonly UiColor Success = "#28a745";
                public static readonly UiColor Info = "#17a2b8";
                public static readonly UiColor Warning = "#ffc107";
                public static readonly UiColor Danger = "#dc3545";
                public static readonly UiColor Light = "#f8f9fa";
                public static readonly UiColor Dark = "#343a40";
            }
            
            public static class Material
            {
                public static readonly UiColor MatPrimary = "#dfe6e9";
                public static readonly UiColor MatPrimaryLight = "#b2bec3";
                public static readonly UiColor MatPrimaryDark = "#636e72";
                public static readonly UiColor MatSecondary = "#2d3436";
                public static readonly UiColor MatSecondaryLight = "#74b9ff";
                public static readonly UiColor MatSecondaryDark = "#0984e3";
                public static readonly UiColor MatTextOnPrimary = "#0984e3";
                public static readonly UiColor MatTextOnSecondary = "#0984e3";
            }
            
            public static class Supreme
            {
                public static readonly UiColor Lime = "#acfa58";
                public static readonly UiColor SilverText = "#e3e3e3";
                public static readonly UiColor K1lly0usRed = "#ce422b";
            }
            
            public static class MJSU
            {
                public static readonly UiColor Orange = "#de8732";
            }
            
            public static class Rust
            {
                public static readonly UiColor Red = "#cd4632";
                public static readonly UiColor Green = "#8cc83c";
                public static readonly UiColor Panel = "#CCCCCC";
                
                public static class Ui
                {
                    public static readonly UiColor Panel = "#A6A6A60F";
                    public static readonly UiColor Header = "#DBDBDB33";
                    public static readonly UiColor PanelButton = "#A6A6A60F";
                    public static readonly UiColor OkButton = "#6A804266";
                    public static readonly UiColor Button = "#BFBFBF1A";
                    public static readonly UiColor PanelText = "#E8dED4";
                    public static readonly UiColor PanelButtonText = "#C4C4C4";
                    public static readonly UiColor OkButtonText = "#9BB46E";
                    public static readonly UiColor ButtonText = "#E8DED4CC";
                }
                
                public static class Chat
                {
                    public static readonly UiColor Player = "#55AAFF";
                    public static readonly UiColor Developer = "#FFAA55";
                    public static readonly UiColor Admin = "#AAFF55";
                }
                
                public static class Steam
                {
                    public static readonly UiColor InGame = "#A2DB40";
                    public static readonly UiColor Online = "#60CBF2";
                    public static readonly UiColor Normal = "#F7EBE1";
                }
            }
            
            public static class Form
            {
                public static readonly UiColor Body = "#00001F";
                public static readonly UiColor Header = "#00001F";
                public static readonly UiColor Text = UiColor.White;
                public static readonly UiColor Panel = "#2B2B2B";
                public static readonly UiColor PanelSecondary = "#3f3f3f";
                public static readonly UiColor PanelTertiary = "#525252";
                public static readonly UiColor ButtonPrimary = Rust.Red;
                public static readonly UiColor ButtonSecondary = "#666666";
            }
            
            #region UI Colors
            public static readonly UiColor Body = Form.Body.WithAlpha("B2");
            public static readonly UiColor BodyHeader = Form.Header;
            public static readonly UiColor Text = Form.Text.WithAlpha("80");
            public static readonly UiColor Panel = Form.Panel;
            public static readonly UiColor PanelSecondary = Form.PanelSecondary;
            public static readonly UiColor PanelTertiary = Form.PanelTertiary;
            public static readonly UiColor CloseButton = Form.ButtonPrimary;
            public static readonly UiColor ButtonPrimary = Form.ButtonPrimary;
            public static readonly UiColor ButtonSecondary = Form.ButtonSecondary;
            #endregion
        }
        public struct UiCommand : IDisposable
        {
            public readonly string Command;
            public List<string> Args;
            public bool IsEmpty => string.IsNullOrEmpty(Command) && (Args == null || Args.Count == 0);
            private readonly bool _disposable;
            
            public UiCommand(string command, bool disposable = true)
            {
                Command = command;
                Args = null;
                _disposable = disposable;
            }
            
            public static UiCommand Create(string command)
            {
                return new UiCommand(command);
            }
            
            public static UiCommand Create<T0>(string command, T0 arg0, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1>(string command, T0 arg0, T1 arg1, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2>(string command, T0 arg0, T1 arg1, T2 arg2, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4, T5>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                cmd.AddArg(arg5);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                cmd.AddArg(arg5);
                cmd.AddArg(arg6);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                cmd.AddArg(arg5);
                cmd.AddArg(arg6);
                cmd.AddArg(arg7);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                cmd.AddArg(arg5);
                cmd.AddArg(arg6);
                cmd.AddArg(arg7);
                cmd.AddArg(arg8);
                return cmd;
            }
            
            public static UiCommand Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string command, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, bool disposable = true)
            {
                UiCommand cmd = new UiCommand(command, disposable);
                cmd.Args = UiFrameworkPool.GetList<string>();;
                cmd.AddArg(arg0);
                cmd.AddArg(arg1);
                cmd.AddArg(arg2);
                cmd.AddArg(arg3);
                cmd.AddArg(arg4);
                cmd.AddArg(arg5);
                cmd.AddArg(arg6);
                cmd.AddArg(arg7);
                cmd.AddArg(arg8);
                cmd.AddArg(arg9);
                return cmd;
            }
            
            public void AddArg<T>(T arg)
            {
                Args.Add(arg as string ?? arg.ToString());
            }
            
            public void Dispose()
            {
                if (_disposable && Args != null)
                {
                    UiFrameworkPool.FreeList(Args);
                }
            }
            
            public static implicit operator UiCommand(string command) => new UiCommand(command);
        }
        public abstract class BaseColorComponent : BaseComponent
        {
            public UiColor Color;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.AddField(JsonDefaults.Color.ColorName, Color);
            }
        }
        public abstract class BaseComponent : BasePoolable
        {
            public abstract void WriteComponent(JsonFrameworkWriter writer);
        }
        public abstract class BaseFadeInComponent : BaseColorComponent
        {
            public float FadeIn;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                FadeIn = 0;
            }
        }
        public abstract class BaseImageComponent : BaseFadeInComponent
        {
            public string Sprite;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.AddField(JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
                writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Sprite = null;
                Material = null;
            }
        }
        public abstract class BaseTextComponent : BaseFadeInComponent
        {
            public int FontSize = JsonDefaults.BaseText.FontSize;
            public string Font;
            public TextAnchor Align;
            public string Text;
            public VerticalWrapMode VerticalOverflow;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.AddTextField(JsonDefaults.BaseText.TextName, Text);
                writer.AddField(JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
                writer.AddField(JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
                writer.AddField(JsonDefaults.BaseText.AlignName, Align);
                writer.AddField(JsonDefaults.BaseText.VerticalOverflowName, VerticalOverflow);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                FontSize = JsonDefaults.BaseText.FontSize;
                Font = null;
                Align = TextAnchor.UpperLeft;
                Text = null;
                VerticalOverflow = VerticalWrapMode.Truncate;
            }
        }
        public class ButtonComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Button";
            
            public string Command;
            public string Close;
            public Image.Type ImageType;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
                writer.AddField(JsonDefaults.Image.ImageType, ImageType);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Command = null;
                Close = null;
                ImageType = Image.Type.Simple;
            }
        }
        public class CountdownComponent : BaseComponent
        {
            private const string Type = "Countdown";
            
            public int StartTime;
            public int EndTime;
            public int Step;
            public string Command;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
                writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
                writer.AddField(JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
                writer.AddField(JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                StartTime = JsonDefaults.Countdown.StartTimeValue;
                EndTime = JsonDefaults.Countdown.EndTimeValue;
                Step = JsonDefaults.Countdown.StepValue;
                Command = null;
            }
        }
        public class ImageComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Image";
            
            public string Png;
            public Image.Type ImageType;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.Image.PngName, Png, JsonDefaults.Common.NullValue);
                writer.AddField(JsonDefaults.Image.ImageType, ImageType);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Png = JsonDefaults.Common.NullValue;
                ImageType = Image.Type.Simple;
            }
        }
        public class InputComponent : BaseTextComponent
        {
            private const string Type = "UnityEngine.UI.InputField";
            
            public int CharsLimit;
            public string Command;
            public InputMode Mode;
            public InputField.LineType LineType;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
                writer.AddField(JsonDefaults.Input.LineTypeName, LineType);
                
                if (HasMode(InputMode.ReadOnly))
                {
                    writer.AddField(JsonDefaults.Input.ReadOnlyName, true, false);
                }
                else
                {
                    writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                }
                
                if (HasMode(InputMode.Password))
                {
                    writer.AddKeyField(JsonDefaults.Input.PasswordName);
                }
                
                if (HasMode(InputMode.NeedsKeyboard))
                {
                    writer.AddKeyField(JsonDefaults.Input.NeedsKeyboardName);
                }
                
                if (HasMode(InputMode.HudNeedsKeyboard))
                {
                    writer.AddKeyField(JsonDefaults.Input.NeedsHudKeyboardName);
                }
                
                if (HasMode(InputMode.AutoFocus))
                {
                    writer.AddKeyField(JsonDefaults.Input.AutoFocusName);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            public bool HasMode(InputMode mode)
            {
                return (Mode & mode) == mode;
            }
            
            public void SetMode(InputMode mode, bool enabled)
            {
                if (enabled)
                {
                    Mode |= mode;
                }
                else
                {
                    Mode &= ~mode;
                }
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                CharsLimit = JsonDefaults.Input.CharacterLimitValue;
                Command = null;
                Mode = default(InputMode);
                LineType = default(InputField.LineType);
            }
        }
        public class ItemIconComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Image";
            
            public int ItemId;
            public ulong SkinId;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddFieldRaw(JsonDefaults.ItemIcon.ItemIdName, ItemId);
                writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinId, default(ulong));
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                ItemId = default(int);
                SkinId = default(ulong);
            }
        }
        public class OutlineComponent : BaseColorComponent
        {
            private const string Type = "UnityEngine.UI.Outline";
            
            public Vector2 Distance = JsonDefaults.Outline.Distance;
            public bool UseGraphicAlpha;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.FpDistance);
                if (UseGraphicAlpha)
                {
                    writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void LeavePool()
            {
                Distance = JsonDefaults.Outline.Distance;
                UseGraphicAlpha = false;
            }
        }
        public class RawImageComponent : BaseFadeInComponent
        {
            private const string Type = "UnityEngine.UI.RawImage";
            
            public string Url;
            public string Png;
            public string Texture;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                writer.AddField(JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
                writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                if (!string.IsNullOrEmpty(Url))
                {
                    writer.AddFieldRaw(JsonDefaults.Image.UrlName, Url);
                }
                
                if (!string.IsNullOrEmpty(Png))
                {
                    writer.AddFieldRaw(JsonDefaults.Image.PngName, Png);
                }
                
                base.WriteComponent(writer);
                
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Url = null;
                Texture = null;
                Material = null;
            }
        }
        public class TextComponent : BaseTextComponent
        {
            private const string Type = "UnityEngine.UI.Text";
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
        }
        public abstract class BaseUiControl : BasePoolable
        {
            private bool _hasRendered;
            
            protected static T CreateControl<T>() where T : BaseUiControl, new()
            {
                return UiFrameworkPool.Get<T>();
            }
            
            public void RenderControl(UiBuilder builder)
            {
                if (!_hasRendered)
                {
                    Render(builder);
                    _hasRendered = true;
                }
            }
            
            protected virtual void Render(UiBuilder builder)
            {
                
            }
            
            protected override void EnterPool()
            {
                _hasRendered = false;
            }
        }
        public class UiBorder : BaseUiControl
        {
            public UiPanel Left;
            public UiPanel Right;
            public UiPanel Top;
            public UiPanel Bottom;
            
            public static UiBorder Create(UiBuilder builder, BaseUiComponent parent, UiColor color, int width = 1, BorderMode border = BorderMode.All)
            {
                UiBorder control = CreateControl<UiBorder>();
                //If width is 0 nothing is displayed so don't try to render
                if (width == 0)
                {
                    return control;
                }
                
                bool top = HasBorderFlag(border, BorderMode.Top);
                bool left = HasBorderFlag(border, BorderMode.Left);
                bool bottom = HasBorderFlag(border, BorderMode.Bottom);
                bool right = HasBorderFlag(border, BorderMode.Right);
                
                if (width > 0)
                {
                    int tbMin = left ? -width : 0;
                    int tbMax = right ? width : 0;
                    int lrMin = top ? -width : 0;
                    int lrMax = bottom ? width : 0;
                    
                    if (top)
                    {
                        control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width), color);
                    }
                    
                    if (left)
                    {
                        control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax), color);
                    }
                    
                    if (bottom)
                    {
                        control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0), color);
                    }
                    
                    if (right)
                    {
                        control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax), color);
                    }
                }
                else
                {
                    int tbMin = left ? width : 0;
                    int tbMax = right ? -width : 0;
                    int lrMin = top ? width : 0;
                    int lrMax = bottom ? -width : 0;
                    
                    if (top)
                    {
                        control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(tbMin, width, tbMax, 0), color);
                    }
                    
                    if (left)
                    {
                        control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(0, lrMin, -width, lrMax), color);
                    }
                    
                    if (bottom)
                    {
                        control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(tbMin, 0, tbMax, -width), color);
                    }
                    
                    if (right)
                    {
                        control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(width, lrMin, 0, lrMax), color);
                    }
                }
                
                return control;
            }
            
            private static bool HasBorderFlag(BorderMode mode, BorderMode flag)
            {
                return (mode & flag) != 0;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Left = null;
                Top = null;
                Right = null;
                Bottom = null;
            }
        }
        public class UiButtonGroup : BaseUiControl
        {
            public UiSection Base;
            public List<UiButton> Buttons;
            
            public static UiButtonGroup Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
            {
                UiButtonGroup control = CreateControl<UiButtonGroup>();
                control.Base = builder.Section(parent, pos, offset);
                
                float buttonSize = 1f / (buttons.Count + 1);
                for (int i = 0; i < buttons.Count; i++)
                {
                    ButtonGroupData button = buttons[i];
                    
                    UiPosition buttonPos = UiPosition.Full.SliceHorizontal(buttonSize * i, buttonSize * (i + 1));
                    control.Buttons.Add(builder.TextButton(control.Base, buttonPos, button.DisplayName, textSize, textColor, button.IsActive ? activeButtonColor : buttonColor, $"{command} {button.CommandArgs}"));
                }
                
                return control;
            }
            
            public static UiButtonGroup CreateNumeric(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
            {
                List<ButtonGroupData> data = UiFrameworkPool.GetList<ButtonGroupData>();
                for (int i = minValue; i <= maxValue; i++)
                {
                    string num = StringCache<int>.ToString(i);
                    data.Add(new ButtonGroupData(num, num, i == value));
                }
                
                UiButtonGroup control = Create(builder, parent, pos, offset, data, textSize, textColor, buttonColor, activeButtonColor, command);
                UiFrameworkPool.FreeList(data);
                
                return control;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Buttons = UiFrameworkPool.GetList<UiButton>();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.FreeList(Buttons);
                Base = null;
            }
        }
        public class UiCheckbox : BaseUiControl
        {
            private const string DefaultCheckmark = "<b>✓</b>";
            
            public bool IsChecked;
            public string Checkmark = DefaultCheckmark;
            public UiButton Button;
            public UiLabel Label;
            
            public static UiCheckbox CreateCheckbox(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, string command)
            {
                UiCheckbox control = CreateControl<UiCheckbox>();
                control.IsChecked = isChecked;
                control.Button = builder.CommandButton(parent, pos, offset, backgroundColor, command);
                control.Label = builder.Label(control.Button, UiPosition.Full, string.Empty, textSize, textColor);
                control.Button.AddElementOutline(UiColor.Black.WithAlpha(0.75f));
                return control;
            }
            
            protected override void Render(UiBuilder builder)
            {
                if (IsChecked)
                {
                    Label.Text.Text = Checkmark;
                }
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                IsChecked = false;
                Button = null;
                Label = null;
                Checkmark = DefaultCheckmark;
            }
        }
        public class UiDatePicker : BaseUiControl
        {
            public UiSection Anchor;
            public UiButton Command;
            public UiLabel Text;
            public UiLabel Icon;
            
            public static UiDatePicker Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "MM/dd/yyyy")
            {
                UiDatePicker control = CreateControl<UiDatePicker>();
                
                control.Anchor = builder.Anchor(parent, pos, offset);
                control.Command = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Name}");
                control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), date.ToString(displayFormat), fontSize, textColor, TextAnchor.MiddleLeft);
                control.Icon = builder.Label(control.Command, UiPosition.Right, new UiOffset(-fontSize - 4, 0, -4 , 0), "⏱", fontSize, textColor);
                
                return control;
            }
        }
        public class UiDropdown : BaseUiControl
        {
            public UiSection Anchor;
            public UiButton Command;
            public UiLabel Text;
            public UiLabel Icon;
            
            public static UiDropdown Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, string displayValue, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
            {
                UiDropdown control = CreateControl<UiDropdown>();
                control.Anchor = builder.Anchor(parent, pos);
                control.Command = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Name}");
                control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), displayValue, fontSize, textColor, TextAnchor.MiddleLeft);
                control.Icon = builder.Label(control.Command, UiPosition.Right, new UiOffset(-UiHelpers.TextOffsetWidth(1, fontSize) - 4, 0, -4 , 0), "▼", fontSize, textColor);
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Anchor = null;
                Command = null;
                Text = null;
                Icon = null;
            }
        }
        public class UiInputBackground : BaseUiControl
        {
            public UiInput Input;
            public UiPanel Background;
            
            public static UiInputBackground Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                UiInputBackground control = CreateControl<UiInputBackground>();
                control.Background = builder.Panel(parent,  pos, offset, backgroundColor);
                control.Input = builder.Input(control.Background, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Input = null;
                Background = null;
            }
        }
        public class UiLabelBackground : BaseUiControl
        {
            public UiLabel Label;
            public UiPanel Background;
            
            public static UiLabelBackground Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabelBackground control = CreateControl<UiLabelBackground>();
                control.Background = builder.Panel(parent, pos, offset, backgroundColor);
                control.Label = builder.Label(control.Background, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, align);
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Label = null;
                Background = null;
            }
        }
        public class UiPaginator : BaseUiControl
        {
            public UiButton FirstPage;
            public UiButton PreviousPage;
            public List<UiButton> PageButtons;
            public UiButton NextPage;
            public UiButton LastPage;
            
            public static UiPaginator Create(UiBuilder builder, BaseUiComponent parent, GridPosition grid, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, string command)
            {
                UiPaginator control = CreateControl<UiPaginator>();
                grid.Reset();
                
                int totalButtons = (int)Math.Round(grid.NumCols, 0);
                int pageButtons = totalButtons - 5;
                
                int startPage = Math.Max(currentPage - pageButtons / 2, 0);
                int endPage = Math.Min(maxPage, startPage + pageButtons);
                if (endPage - startPage != pageButtons)
                {
                    startPage = Math.Max(endPage - pageButtons, 0);
                    if (endPage - startPage != pageButtons)
                    {
                        grid.MoveCols((pageButtons - endPage - startPage) / 2f);
                    }
                }
                
                control.FirstPage = builder.TextButton(parent, grid, "<<<", fontSize, textColor, buttonColor, $"{command} 0");
                grid.MoveCols(1);
                control.PreviousPage = builder.TextButton(parent, grid, "<", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(Math.Max(0, currentPage - 1))}");
                grid.MoveCols(1);
                
                for (int i = startPage; i <= endPage; i++)
                {
                    control.PageButtons.Add(builder.TextButton(parent, grid, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, $"{command} {StringCache<int>.ToString(i)}"));
                    grid.MoveCols(1);
                }
                
                control.NextPage = builder.TextButton(parent, grid, ">", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(Math.Min(maxPage, currentPage + 1))}");
                grid.MoveCols(1);
                control.LastPage = builder.TextButton(parent, grid, ">>>", fontSize, textColor, buttonColor, $"{command} {StringCache<int>.ToString(maxPage)}");
                
                return control;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                PageButtons = UiFrameworkPool.GetList<UiButton>();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                FirstPage = null;
                PreviousPage = null;
                UiFrameworkPool.FreeList(PageButtons);
                NextPage = null;
                LastPage = null;
            }
        }
        public class UiPicker : BaseUiControl
        {
            public UiButton Previous;
            public UiLabel Value;
            public UiButton Next;
            
            public static UiPicker Create(UiBuilder builder, UiOffset pos, string value, int fontSize, UiColor textColor, UiColor backgroundColor, float height, string incrementCommand, string decrementCommand)
            {
                UiPicker control = CreateControl<UiPicker>();
                
                UiOffset slice = pos.SliceVertical(0, (int)height * 2);
                control.Next =  builder.TextButton(builder.Root, UiPosition.BottomLeft, slice, "˅", fontSize, textColor, backgroundColor, decrementCommand);
                slice = slice.MoveY(height);
                control.Value = builder.Label(builder.Root, UiPosition.BottomLeft, slice, value, fontSize, textColor);
                slice = slice.MoveY(height);
                control.Previous = builder.TextButton(builder.Root, UiPosition.BottomLeft, slice, "˄", fontSize, textColor, backgroundColor, incrementCommand);
                
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Previous = null;
                Value = null;
                Next = null;
            }
        }
        public class UiProgressBar : BaseUiControl
        {
            public UiPanel BackgroundPanel;
            public UiPanel BarPanel;
            
            public static UiProgressBar Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, float percentage, UiColor barColor, UiColor backgroundColor)
            {
                UiProgressBar control = CreateControl<UiProgressBar>();
                control.BackgroundPanel = builder.Panel(parent, pos, offset, backgroundColor);
                control.BarPanel = builder.Panel(control.BackgroundPanel, UiPosition.Full.SliceHorizontal(0, percentage), barColor);
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                BackgroundPanel = null;
                BarPanel = null;
            }
        }
        public class UiScrollBar : BaseUiControl
        {
            public UiPanel Background;
            public UiPanel ScrollBar;
            public List<UiButton> ScrollButtons;
            
            public static UiScrollBar Create(UiBuilder builder, BaseUiComponent parent, UiPosition position, UiOffset offset, int currentPage, int maxPage, UiColor barColor, UiColor backgroundColor, string command, ScrollbarDirection direction, string sprite)
            {
                UiScrollBar control = CreateControl<UiScrollBar>();
                
                control.Background = builder.Panel(parent, position, offset, backgroundColor);
                control.Background.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                float buttonSize = 1f / (maxPage + 1);
                for (int i = 0; i <= maxPage; i++)
                {
                    float min = buttonSize * i;
                    float max = buttonSize * (i + 1);
                    UiPosition pagePosition = direction == ScrollbarDirection.Horizontal ? UiPosition.Full.SliceHorizontal(min, max) : new UiPosition(0, 1 - max, 1, 1 - min);
                    
                    if (i != currentPage)
                    {
                        UiButton button = builder.CommandButton(control.Background, pagePosition, backgroundColor, $"{command} {StringCache<int>.ToString(i)}");
                        button.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                        control.ScrollButtons.Add(button);
                    }
                    else
                    {
                        control.ScrollBar = builder.Panel(control.Background, pagePosition, barColor);
                        control.ScrollBar.SetSpriteMaterialImage(sprite, null, Image.Type.Sliced);
                    }
                }
                
                return control;
            }
            
            public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
            {
                Background.SetSpriteMaterialImage(sprite, material, type);
                ScrollBar.SetSpriteMaterialImage(sprite, material, type);
                for (int index = 0; index < ScrollButtons.Count; index++)
                {
                    UiButton button = ScrollButtons[index];
                    button.SetSpriteMaterialImage(sprite, material, type);
                }
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                ScrollButtons = UiFrameworkPool.GetList<UiButton>();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Background = null;
                ScrollBar = null;
                UiFrameworkPool.FreeList(ScrollButtons);
            }
        }
        public class UiTimePicker : BaseUiControl
        {
            public UiSection Anchor;
            public UiButton Command;
            public UiLabel Text;
            public UiLabel Icon;
            
            public static UiTimePicker Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, DateTime time, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand, string displayFormat = "hh:mm:ss tt")
            {
                UiTimePicker control = CreateControl<UiTimePicker>();
                
                control.Anchor = builder.Anchor(parent, pos, offset);
                control.Command = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Name}");
                control.Text = builder.Label(control.Command, UiPosition.Full, new UiOffset(5, 0, 0, 0), time.ToString(displayFormat), fontSize, textColor, TextAnchor.MiddleLeft);
                control.Icon = builder.Label(control.Command, UiPosition.Right, new UiOffset(-fontSize - 4, 0, -4 , 0), "⏱", fontSize, textColor);
                
                return control;
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Anchor = null;
                Command = null;
                Text = null;
                Icon = null;
            }
        }
        [Flags]
        public enum BorderMode : byte
        {
            Top = 1 << 0,
            Left = 1 << 1,
            Bottom = 1 << 2,
            Right = 1 << 3,
            All = Top | Left | Bottom | Right
        }
        public enum ClockMode
        {
            Hour12,
            Hour24
        }
        public enum ColorPickerMode
        {
            RGB,
            RGBA
        }
        [Flags]
        public enum DatePickerDisplayMode : byte
        {
            Day = 1 << 0,
            Month = 1 << 1,
            Year = 1 << 2,
            All = Day | Month | Year
        }
        public enum DatePickerDisplayOrder
        {
            MonthDayYear,
            YearMonthDay,
            DayMonthYear,
        }
        [Flags]
        public enum InputMode : byte
        {
            Default = 0,
            ReadOnly = 1 << 0,
            NeedsKeyboard = 1 << 1,
            HudNeedsKeyboard = 1 << 2,
            Password = 1 << 3,
            AutoFocus = 1 << 4
        }
        public enum NumberPickerMode
        {
            LeftRight,
            UpDown
        }
        public enum PopoverPosition
        {
            Top,
            Left,
            Right,
            Bottom
        }
        public enum ScrollbarDirection
        {
            Vertical,
            Horizontal
        }
        [Flags]
        public enum TimePickerDisplayMode : byte
        {
            Hours = 1 << 0,
            Minutes = 1 << 1,
            Seconds = 1 << 2,
            HoursMinutes = Hours | Minutes,
            All = Hours | Minutes | Seconds
        }
        public enum UiFont : byte
        {
            /// <summary>
            /// droidsansmono.ttf
            /// </summary>
            DroidSansMono,
            
            /// <summary>
            /// permanentmarker.ttf
            /// </summary>
            PermanentMarker,
            
            /// <summary>
            /// robotocondensed-bold.ttf
            /// </summary>
            RobotoCondensedBold,
            
            /// <summary>
            /// robotocondensed-regular.ttf
            /// </summary>
            RobotoCondensedRegular,
            
            /// <summary>
            /// PressStart2P-Regular.ttf
            /// </summary>
            PressStart2PRegular
        }
        public enum UiLayer : byte
        {
            Overall,
            Overlay,
            HudMenu,
            Hud,
            Under
        }
        public class UiFrameworkException : Exception
        {
            public UiFrameworkException(string message) : base(message) { }
        }
        public static class GenericMath
        {
            public static T Add<T>(T left, T right) where T : struct
            {
                Type type = typeof(T);
                if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) + GenericsUtil.Cast<T, int>(right));
                if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) + GenericsUtil.Cast<T, float>(right));
                if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) + GenericsUtil.Cast<T, double>(right));
                if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) + GenericsUtil.Cast<T, long>(right));
                if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) + GenericsUtil.Cast<T, uint>(right));
                if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) + GenericsUtil.Cast<T, ulong>(right));
                
                throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
            }
            
            public static T Subtract<T>(T left, T right) where T : struct
            {
                Type type = typeof(T);
                if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) - GenericsUtil.Cast<T, int>(right));
                if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) - GenericsUtil.Cast<T, float>(right));
                if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) - GenericsUtil.Cast<T, double>(right));
                if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) - GenericsUtil.Cast<T, long>(right));
                if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) - GenericsUtil.Cast<T, uint>(right));
                if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) - GenericsUtil.Cast<T, ulong>(right));
                
                throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
            }
            
            public static T Multiply<T>(T left, T right) where T : struct
            {
                Type type = typeof(T);
                if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) * GenericsUtil.Cast<T, int>(right));
                if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) * GenericsUtil.Cast<T, float>(right));
                if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) * GenericsUtil.Cast<T, double>(right));
                if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) * GenericsUtil.Cast<T, long>(right));
                if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) * GenericsUtil.Cast<T, uint>(right));
                if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) * GenericsUtil.Cast<T, ulong>(right));
                
                throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
            }
            
            public static T Divide<T>(T left, T right) where T : struct
            {
                Type type = typeof(T);
                if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) / GenericsUtil.Cast<T, int>(right));
                if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) / GenericsUtil.Cast<T, float>(right));
                if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) / GenericsUtil.Cast<T, double>(right));
                if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) / GenericsUtil.Cast<T, long>(right));
                if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) / GenericsUtil.Cast<T, uint>(right));
                if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) / GenericsUtil.Cast<T, ulong>(right));
                
                throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
            }
        }
        public class JsonBinaryWriter : BasePoolable
        {
            private const int SegmentSize = 2048;
            
            private List<SizedArray<byte>> _segments;
            private int _charIndex;
            private int _size;
            private char[] _charBuffer;
            
            public void Write(char character)
            {
                _charBuffer[_charIndex] = character;
                _charIndex++;
                if (_charIndex >= SegmentSize)
                {
                    Flush();
                }
            }
            
            public void Write(string text)
            {
                int length = text.Length;
                char[] buffer = _charBuffer;
                int charIndex = _charIndex;
                for (int i = 0; i < length; i++)
                {
                    buffer[charIndex + i] = text[i];
                }
                _charIndex += length;
                if (_charIndex >= SegmentSize)
                {
                    Flush();
                }
            }
            
            private void Flush()
            {
                if (_charIndex == 0)
                {
                    return;
                }
                
                byte[] segment = UiFrameworkArrayPool<byte>.Shared.Rent(SegmentSize * 2);
                int size = Encoding.UTF8.GetBytes(_charBuffer, 0, _charIndex, segment, 0);
                _segments.Add(new SizedArray<byte>(segment, size));
                _size += size;
                _charIndex = 0;
            }
            
            public int WriteToArray(byte[] bytes)
            {
                Flush();
                int writeIndex = 0;
                for (int i = 0; i < _segments.Count; i++)
                {
                    SizedArray<byte> segment = _segments[i];
                    Buffer.BlockCopy(segment.Array, 0, bytes, writeIndex, segment.Size);
                    writeIndex += segment.Size;
                }
                
                return _size;
            }
            
            public void WriteToNetwork(NetWrite write)
            {
                Flush();
                write.UInt32((uint)_size);
                for (int i = 0; i < _segments.Count; i++)
                {
                    SizedArray<byte> segment = _segments[i];
                    write.Write(segment.Array, 0, segment.Size);
                }
            }
            
            public byte[] ToArray()
            {
                Flush();
                byte[] bytes = new byte[_size];
                WriteToArray(bytes);
                return bytes;
            }
            
            protected override void LeavePool()
            {
                _segments = UiFrameworkPool.GetList<SizedArray<byte>>();
                if (_segments.Capacity < 100)
                {
                    _segments.Capacity = 100;
                }
                _charBuffer = UiFrameworkArrayPool<char>.Shared.Rent(SegmentSize * 2);
            }
            
            protected override void EnterPool()
            {
                for (int index = 0; index < _segments.Count; index++)
                {
                    byte[] bytes = _segments[index].Array;
                    UiFrameworkArrayPool<byte>.Shared.Return(bytes);
                }
                
                UiFrameworkArrayPool<char>.Shared.Return(_charBuffer);
                UiFrameworkPool.FreeList(_segments);
                _charBuffer = null;
                _size = 0;
                _charIndex = 0;
            }
        }
        public static class JsonDefaults
        {
            public static class Common
            {
                public const string ComponentTypeName = "type";
                public const string ComponentName = "name";
                public const string ParentName = "parent";
                public const string FadeInName = "fadeIn";
                public const string FadeOutName = "fadeOut";
                public const float FadeOut = 0;
                public const float FadeIn = 0;
                public const string RectTransformName = "RectTransform";
                public const string NullValue = null;
                public const string NeedsCursorValue = "NeedsCursor";
                public const string NeedsKeyboardValue = "NeedsKeyboard";
                public const string AutoDestroy = "destroyUi";
                public const string CommandName = "command";
                public static readonly Vector2 Min = new Vector2(0, 0);
                public static readonly Vector2 Max = new Vector2(1, 1);
            }
            
            public static class Position
            {
                public const string AnchorMinName = "anchormin";
                public const string AnchorMaxName = "anchormax";
            }
            
            public static class Offset
            {
                public const string OffsetMinName = "offsetmin";
                public const string OffsetMaxName = "offsetmax";
                public const string DefaultOffsetMax = "0 0";
            }
            
            public static class Color
            {
                public const string ColorName = "color";
                public const uint ColorValue = 0xFFFFFFFF;
            }
            
            public static class BaseImage
            {
                public const string SpriteName = "sprite";
                public const string MaterialName = "material";
                public const string Sprite = "Assets/Content/UI/UI.Background.Tile.psd";
                public const string Material = "Assets/Icons/IconMaterial.mat";
            }
            
            public static class RawImage
            {
                public const string TextureValue = "Assets/Icons/rust.png";
            }
            
            public static class BaseText
            {
                public const int FontSize = 14;
                public const string FontValue = "RobotoCondensed-Bold.ttf";
                public const string FontName = "font";
                public const string TextName = "text";
                public const string FontSizeName = "fontSize";
                public const string AlignName = "align";
                public const string VerticalOverflowName = "verticalOverflow";
            }
            
            public static class Outline
            {
                public const string DistanceName = "distance";
                public const string UseGraphicAlphaName = "useGraphicAlpha";
                public static readonly Vector2 FpDistance = new Vector2(1.0f, -1.0f);
                public static readonly Vector2 Distance = new Vector2(0.5f, -0.5f);
            }
            
            public static class Button
            {
                public const string CloseName = "close";
            }
            
            public static class Image
            {
                public const string PngName = "png";
                public const string UrlName = "url";
                public const string ImageType = "imagetype";
            }
            
            public static class ItemIcon
            {
                public const string ItemIdName = "itemid";
                public const string SkinIdName = "skinid";
            }
            
            public static class Input
            {
                public const string CharacterLimitName = "characterLimit";
                public const int CharacterLimitValue = 0;
                public const string PasswordName = "password";
                public const string ReadOnlyName = "readOnly";
                public const string LineTypeName = "lineType";
                public const string NeedsKeyboardName = "needsKeyboard";
                public const string NeedsHudKeyboardName = "hudMenuInput";
                public const string AutoFocusName = "autofocus";
            }
            
            public static class Countdown
            {
                public const string StartTimeName = "startTime";
                public const int StartTimeValue = 0;
                public const string EndTimeName = "endTime";
                public const int EndTimeValue = 0;
                public const string StepName = "step";
                public const int StepValue = 1;
                public const string CountdownCommandName = "command";
            }
        }
        public class JsonFrameworkWriter : BasePoolable
        {
            private const char QuoteChar = '\"';
            private const char ArrayStartChar = '[';
            private const char ArrayEndChar = ']';
            private const char ObjectStartChar = '{';
            private const char ObjectEndChar = '}';
            private const char CommaChar = ',';
            private const string Separator = "\":";
            private const string PropertyComma = ",\"";
            
            private bool _propertyComma;
            private bool _objectComma;
            
            private JsonBinaryWriter _writer;
            
            private void Init()
            {
                _writer = UiFrameworkPool.Get<JsonBinaryWriter>();
            }
            
            public static JsonFrameworkWriter Create()
            {
                JsonFrameworkWriter writer = UiFrameworkPool.Get<JsonFrameworkWriter>();
                writer.Init();
                return writer;
            }
            
            private void OnDepthIncrease()
            {
                if (_objectComma)
                {
                    _writer.Write(CommaChar);
                    _objectComma = false;
                }
                
                _propertyComma = false;
            }
            
            private void OnDepthDecrease()
            {
                _objectComma = true;
            }
            
            #region Field Handling
            public void AddFieldRaw(string name, string value)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
            
            public void AddFieldRaw(string name, int value)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
            
            public void AddField(string name, string value, string defaultValue)
            {
                if (value != null && value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddField(string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddPosition(string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WritePosition(value);
                }
            }
            
            public void AddOffset(string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteOffset(value);
                }
            }
            
            public void AddField(string name, TextAnchor value)
            {
                if (value != TextAnchor.UpperLeft)
                {
                    WritePropertyName(name);
                    WriteValue(EnumCache<TextAnchor>.ToString(value));
                }
            }
            
            public void AddField(string name, InputField.LineType value)
            {
                if (value != InputField.LineType.SingleLine)
                {
                    WritePropertyName(name);
                    WriteValue(EnumCache<InputField.LineType>.ToString(value));
                }
            }
            
            public void AddField(string name, Image.Type value)
            {
                if (value != Image.Type.Simple)
                {
                    WritePropertyName(name);
                    WriteValue(EnumCache<Image.Type>.ToString(value));
                }
            }
            
            public void AddField(string name, VerticalWrapMode value)
            {
                if (value != VerticalWrapMode.Truncate)
                {
                    WritePropertyName(name);
                    WriteValue(EnumCache<VerticalWrapMode>.ToString(value));
                }
            }
            
            public void AddField(string name, int value, int defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddField(string name, float value, float defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddField(string name, ulong value, ulong defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddField(string name, bool value, bool defaultValue)
            {
                if (value != defaultValue)
                {
                    WritePropertyName(name);
                    WriteValue(value);
                }
            }
            
            public void AddField(string name, UiColor color)
            {
                if (color.Value != JsonDefaults.Color.ColorValue)
                {
                    WritePropertyName(name);
                    WriteValue(color);
                }
            }
            
            public void AddKeyField(string name)
            {
                WritePropertyName(name);
                WriteBlankValue();
            }
            
            public void AddTextField(string name, string value)
            {
                WritePropertyName(name);
                WriteTextValue(value);
            }
            
            public void AddMouse()
            {
                WriteStartObject();
                AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
                WriteEndObject();
            }
            
            public void AddKeyboard()
            {
                WriteStartObject();
                AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
                WriteEndObject();
            }
            #endregion
            
            #region Writing
            
            #endregion
            public void WriteStartArray()
            {
                OnDepthIncrease();
                _writer.Write(ArrayStartChar);
            }
            
            public void WriteEndArray()
            {
                _writer.Write(ArrayEndChar);
                OnDepthDecrease();
            }
            
            public void WriteStartObject()
            {
                OnDepthIncrease();
                _writer.Write(ObjectStartChar);
            }
            
            public void WriteEndObject()
            {
                _writer.Write(ObjectEndChar);
                OnDepthDecrease();
            }
            
            public void WritePropertyName(string name)
            {
                if (_propertyComma)
                {
                    _writer.Write(PropertyComma);
                }
                else
                {
                    _propertyComma = true;
                    _writer.Write(QuoteChar);
                }
                
                _writer.Write(name);
                _writer.Write(Separator);
            }
            
            public void WriteValue(bool value)
            {
                _writer.Write(value ? '1' : '0');
            }
            
            public void WriteValue(int value)
            {
                _writer.Write(StringCache<int>.ToString(value));
            }
            
            public void WriteValue(float value)
            {
                _writer.Write(StringCache<float>.ToString(value));
            }
            
            public void WriteValue(ulong value)
            {
                _writer.Write(StringCache<ulong>.ToString(value));
            }
            
            public void WriteValue(string value)
            {
                _writer.Write(QuoteChar);
                _writer.Write(value);
                _writer.Write(QuoteChar);
            }
            
            public void WriteBlankValue()
            {
                _writer.Write(QuoteChar);
                _writer.Write(QuoteChar);
            }
            
            public void WriteTextValue(string value)
            {
                _writer.Write(QuoteChar);
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        char character = value[i];
                        if (character == '\"')
                        {
                            _writer.Write("\\\"");
                        }
                        else
                        {
                            _writer.Write(character);
                        }
                    }
                }
                _writer.Write(QuoteChar);
            }
            
            public void WriteValue(Vector2 pos)
            {
                _writer.Write(QuoteChar);
                VectorCache.WriteVector(_writer, pos);
                _writer.Write(QuoteChar);
            }
            
            public void WritePosition(Vector2 pos)
            {
                _writer.Write(QuoteChar);
                VectorCache.WritePosition(_writer, pos);
                _writer.Write(QuoteChar);
            }
            
            public void WriteOffset(Vector2 offset)
            {
                _writer.Write(QuoteChar);
                VectorCache.WriteOffset(_writer, offset);
                _writer.Write(QuoteChar);
            }
            
            public void WriteValue(UiColor color)
            {
                _writer.Write(QuoteChar);
                UiColorCache.WriteColor(_writer, color);
                _writer.Write(QuoteChar);
            }
            
            protected override void EnterPool()
            {
                _objectComma = false;
                _propertyComma = false;
                _writer.Dispose();
            }
            
            public override string ToString()
            {
                return _writer.ToString();
            }
            
            public int WriteTo(byte[] buffer)
            {
                return _writer.WriteToArray(buffer);
            }
            
            public void WriteToNetwork(NetWrite write)
            {
                _writer.WriteToNetwork(write);
            }
            
            public byte[] ToArray()
            {
                return _writer.ToArray();
            }
        }
        public struct SizedArray<T>
        {
            public readonly T[] Array;
            public readonly int Size;
            
            public SizedArray(T[] array, int size)
            {
                Array = array;
                Size = size;
            }
        }
        public abstract class BaseOffset
        {
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            private readonly UiOffset _initialState;
            
            protected BaseOffset(float xMin, float yMin, float xMax, float yMax)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMin + xMax;
                YMax = yMin + yMax;
                _initialState = new UiOffset(XMin, YMin, XMax, YMax);
            }
            
            public UiOffset ToOffset()
            {
                return new UiOffset(XMin, YMin, XMax, YMax);
            }
            
            public void Reset()
            {
                XMin = _initialState.Min.x;
                YMin = _initialState.Min.y;
                XMax = _initialState.Max.x;
                YMax = _initialState.Max.y;
            }
            
            public static implicit operator UiOffset(BaseOffset offset) => offset.ToOffset();
        }
        public class GridOffset : BaseOffset
        {
            public readonly int NumCols;
            public readonly int NumRows;
            public readonly float Width;
            public readonly float Height;
            
            public GridOffset(float xMin, float yMin, float xMax, float yMax, int numCols, int numRows, float width, float height) : base(xMin, yMin, xMax, yMax)
            {
                NumCols = numCols;
                NumRows = numRows;
                Width = width;
                Height = height;
            }
            
            public void MoveCols(int cols)
            {
                MoveCols((float)cols);
            }
            
            public void MoveCols(float cols)
            {
                float distance = cols / NumCols * Width;
                XMin += distance;
                XMax += distance;
                
                if (XMax > Width)
                {
                    XMin -= Width;
                    XMax -= Width;
                    MoveRows(1);
                }
            }
            
            public void MoveRows(int rows)
            {
                float distance = rows / (float)NumRows * Height;
                YMin -= distance;
                YMax -= distance;
            }
        }
        public class GridOffsetBuilder
        {
            private readonly int _numCols;
            private readonly int _numRows;
            private readonly UiOffset _area;
            private readonly float _width;
            private readonly float _height;
            private int _rowHeight = 1;
            private int _rowOffset;
            private int _colWidth = 1;
            private int _colOffset;
            private int _xPad;
            private int _yPad;
            
            public GridOffsetBuilder(int size, UiOffset area) : this(size, size, area)
            {
                
            }
            
            public GridOffsetBuilder(int numCols, int numRows, UiOffset area)
            {
                if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
                if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
                _numCols = numCols;
                _numRows = numRows;
                _area = area;
                _width = area.Max.x - area.Min.x;
                _height = area.Max.y - area.Min.y;
            }
            
            public GridOffsetBuilder SetRowHeight(int height)
            {
                if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
                _rowHeight = height;
                return this;
            }
            
            public GridOffsetBuilder SetRowOffset(int offset)
            {
                if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
                _rowOffset = offset;
                return this;
            }
            
            public GridOffsetBuilder SetColWidth(int width)
            {
                if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
                _colWidth = width;
                return this;
            }
            
            public GridOffsetBuilder SetColOffset(int offset)
            {
                if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
                _colOffset = offset;
                return this;
            }
            
            public GridOffsetBuilder SetPadding(int padding)
            {
                _xPad = padding;
                _yPad = padding;
                return this;
            }
            
            public GridOffsetBuilder SetPadding(int xPad, int yPad)
            {
                _xPad = xPad;
                _yPad = yPad;
                return this;
            }
            
            public GridOffsetBuilder SetRowPadding(int padding)
            {
                _xPad = padding;
                return this;
            }
            
            public GridOffsetBuilder SetColPadding(int padding)
            {
                _yPad = padding;
                return this;
            }
            
            public GridOffset Build()
            {
                float xMin = _area.Min.x;
                float yMin = _area.Max.y - _rowHeight / (float)_numRows * _height;
                float xMax = _colWidth / (float)_numCols * _width;
                float yMax = _rowHeight / (float)_numRows * _height;
                
                if (_colOffset != 0)
                {
                    int size = (int)(_colOffset / (float)_numCols * _width);
                    xMin += size;
                    xMax += size;
                }
                
                if (_rowOffset != 0)
                {
                    int size = (int)(_rowOffset / (float)_numRows * _height);
                    yMin += size;
                    yMax += size;
                }
                
                xMin += _xPad;
                xMax -= _xPad * 2;
                yMin += _yPad;
                yMax -= _yPad * 2;
                
                return new GridOffset(xMin, yMin, xMax, yMax, _numCols, _numRows, _width, _height);
            }
        }
        public struct UiOffset
        {
            public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
            public static readonly UiOffset Scaled = new UiOffset(1280, 720);
            
            public readonly Vector2 Min;
            public readonly Vector2 Max;
            
            public UiOffset(int width, int height) : this(-width / 2f, -height / 2f, width / 2f, height / 2f) { }
            
            public UiOffset(float xMin, float yMin, float xMax, float yMax)
            {
                Min = new Vector2(xMin, yMin);
                Max = new Vector2(xMax, yMax);
            }
            
            public static UiOffset CreateRect(int x, int y, int width, int height)
            {
                return new UiOffset(x, y, x + width, y + height);
            }
            
            public Vector2 Size => Max - Min;
            public float Width => Max.x - Min.x;
            public float Height => Max.y - Min.y;
            
            public override string ToString()
            {
                return $"({Min.x:0}, {Min.y:0}) ({Max.x:0}, {Max.y:0}) WxH:({Width} x {Height})";
            }
        }
        public abstract class BasePool<T> : IPool<T> where T : class
        {
            private readonly T[] _pool;
            private int _index;
            
            /// <summary>
            /// Base Pool Constructor
            /// </summary>
            /// <param name="maxSize">Max Size of the pool</param>
            protected BasePool(int maxSize)
            {
                _pool = new T[maxSize];
                UiFrameworkPool.AddPool(this);
            }
            
            /// <summary>
            /// Returns an element from the pool if it exists else it creates a new one
            /// </summary>
            /// <returns></returns>
            public T Get()
            {
                T item = null;
                if (_index < _pool.Length)
                {
                    item = _pool[_index];
                    _pool[_index] = null;
                    _index++;
                }
                
                if (item == null)
                {
                    item = CreateNew();
                }
                
                OnGetItem(item);
                return item;
            }
            
            /// <summary>
            /// Creates new type of T
            /// </summary>
            /// <returns>Newly created type of T</returns>
            protected abstract T CreateNew();
            
            /// <summary>
            /// Frees an item back to the pool
            /// </summary>
            /// <param name="item">Item being freed</param>
            public void Free(T item) => Free(ref item);
            
            /// <summary>
            /// Frees an item back to the pool
            /// </summary>
            /// <param name="item">Item being freed</param>
            private void Free(ref T item)
            {
                if (item == null)
                {
                    return;
                }
                
                if (!OnFreeItem(ref item))
                {
                    return;
                }
                
                if (_index != 0)
                {
                    _index--;
                    _pool[_index] = item;
                }
                
                item = null;
            }
            
            /// <summary>
            /// Called when an item is retrieved from the pool
            /// </summary>
            /// <param name="item">Item being retrieved</param>
            protected virtual void OnGetItem(T item) { }
            
            /// <summary>
            /// Returns if an item can be freed to the pool
            /// </summary>
            /// <param name="item">Item to be freed</param>
            /// <returns>True if can be freed; false otherwise</returns>
            protected virtual bool OnFreeItem(ref T item) => true;
            
            public void Clear()
            {
                for (int index = 0; index < _pool.Length; index++)
                {
                    _pool[index] = null;
                    _index = 0;
                }
            }
        }
        public abstract class BasePoolable : IDisposable
        {
            internal bool Disposed;
            
            /// <summary>
            /// Returns if the object should be pooled.
            /// This field is set to true when leaving the pool.
            /// If the object instantiated using new() outside the pool it will be false
            /// </summary>
            private bool _shouldPool;
            private IPool<BasePoolable> _pool;
            
            internal void OnInit(IPool<BasePoolable> pool)
            {
                _pool = pool;
            }
            
            internal void EnterPoolInternal()
            {
                EnterPool();
                _shouldPool = false;
                Disposed = true;
            }
            
            internal void LeavePoolInternal()
            {
                _shouldPool = true;
                Disposed = false;
                LeavePool();
            }
            
            /// <summary>
            /// Called when the object is returned to the pool.
            /// Can be overriden in child classes to cleanup used data
            /// </summary>
            protected virtual void EnterPool()
            {
                
            }
            
            /// <summary>
            /// Called when the object leaves the pool.
            /// Can be overriden in child classes to set the initial object state
            /// </summary>
            protected virtual void LeavePool()
            {
                
            }
            
            public void Dispose()
            {
                if (!_shouldPool)
                {
                    return;
                }
                
                if (Disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                
                _pool.Free(this);
            }
        }
        public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
        {
            public static readonly IPool<Hash<TKey, TValue>> Instance;
            
            static HashPool()
            {
                Instance = new HashPool<TKey, TValue>();
            }
            
            private HashPool() : base(128) { }
            
            protected override Hash<TKey, TValue> CreateNew() => new Hash<TKey, TValue>();
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
            {
                item.Clear();
                return true;
            }
        }
        public interface IPool
        {
            void Clear();
        }
        public interface IPool<T> : IPool
        {
            /// <summary>
            /// Returns the Pooled type or a new instance if pool is empty.
            /// </summary>
            /// <returns></returns>
            T Get();
            
            /// <summary>
            /// Returns the pooled type back to the pool
            /// </summary>
            /// <param name="item"></param>
            void Free(T item);
        }
        public class ListPool<T> : BasePool<List<T>>
        {
            public static readonly IPool<List<T>> Instance;
            
            static ListPool()
            {
                Instance = new ListPool<T>();
            }
            
            private ListPool() : base(128) { }
            
            protected override List<T> CreateNew() => new List<T>();
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref List<T> item)
            {
                item.Clear();
                return true;
            }
        }
        public class ObjectPool<T> : BasePool<BasePoolable> where T : BasePoolable, new()
        {
            public static readonly IPool<BasePoolable> Instance = new ObjectPool<T>();
            
            private ObjectPool() : base(1024) { }
            
            protected override BasePoolable CreateNew()
            {
                T obj = new T();
                obj.OnInit(this);
                return obj;
            }
            
            protected override void OnGetItem(BasePoolable item)
            {
                item.LeavePoolInternal();
            }
            
            protected override bool OnFreeItem(ref BasePoolable item)
            {
                if (item.Disposed)
                {
                    return false;
                }
                
                item.EnterPoolInternal();
                return true;
            }
        }
        public class StringBuilderPool : BasePool<StringBuilder>
        {
            public static readonly IPool<StringBuilder> Instance;
            
            static StringBuilderPool()
            {
                Instance = new StringBuilderPool();
            }
            
            private StringBuilderPool() : base(64) { }
            
            protected override StringBuilder CreateNew() => new StringBuilder();
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref StringBuilder item)
            {
                item.Length = 0;
                return true;
            }
        }
        public static class UiFrameworkPool
        {
            private static readonly Hash<Type, IPool> Pools = new Hash<Type, IPool>();
            
            /// <summary>
            /// Returns a pooled object of type T
            /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
            /// </summary>
            /// <typeparam name="T">Type to be returned</typeparam>
            /// <returns>Pooled object of type T</returns>
            public static T Get<T>() where T : BasePoolable, new()
            {
                return (T)ObjectPool<T>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a <see cref="BasePoolable"/> back into the pool
            /// </summary>
            /// <param name="value">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            internal static void Free<T>(T value) where T : BasePoolable, new()
            {
                ObjectPool<T>.Instance.Free(value);
            }
            
            /// <summary>
            /// Returns a pooled <see cref="List{T}"/>
            /// </summary>
            /// <typeparam name="T">Type for the list</typeparam>
            /// <returns>Pooled List</returns>
            public static List<T> GetList<T>()
            {
                return ListPool<T>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a pooled <see cref="Hash{TKey, TValue}"/>
            /// </summary>
            /// <typeparam name="TKey">Type for the key</typeparam>
            /// <typeparam name="TValue">Type for the value</typeparam>
            /// <returns>Pooled Hash</returns>
            public static Hash<TKey, TValue> GetHash<TKey, TValue>()
            {
                return HashPool<TKey, TValue>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a pooled <see cref="StringBuilder"/>
            /// </summary>
            /// <returns>Pooled <see cref="StringBuilder"/></returns>
            public static StringBuilder GetStringBuilder()
            {
                return StringBuilderPool.Instance.Get();
            }
            
            /// <summary>
            /// Free's a pooled <see cref="List{T}"/>
            /// </summary>
            /// <param name="list">List to be freed</param>
            /// <typeparam name="T">Type of the list</typeparam>
            public static void FreeList<T>(List<T> list)
            {
                ListPool<T>.Instance.Free(list);
            }
            
            /// <summary>
            /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
            /// </summary>
            /// <param name="hash">Hash to be freed</param>
            /// <typeparam name="TKey">Type for key</typeparam>
            /// <typeparam name="TValue">Type for value</typeparam>
            public static void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash)
            {
                HashPool<TKey, TValue>.Instance.Free(hash);
            }
            
            /// <summary>
            /// Frees a <see cref="StringBuilder"/> back to the pool
            /// </summary>
            /// <param name="sb">StringBuilder being freed</param>
            public static void FreeStringBuilder(StringBuilder sb)
            {
                StringBuilderPool.Instance.Free(sb);
            }
            
            public static void AddPool<TType>(BasePool<TType> pool) where TType : class
            {
                Pools[typeof(TType)] = pool;
            }
            
            public static void OnUnload()
            {
                foreach (IPool pool in Pools.Values)
                {
                    pool.Clear();
                }
                
                Pools.Clear();
            }
        }
        public abstract class BasePosition
        {
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            private readonly UiPosition _initialState;
            
            protected BasePosition(float xMin, float yMin, float xMax, float yMax)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMax;
                YMax = yMax;
                _initialState = new UiPosition(XMin, YMin, XMax, YMax);
            }
            
            public UiPosition ToPosition()
            {
                return new UiPosition(XMin, YMin, XMax, YMax);
            }
            
            public void Reset()
            {
                XMin = _initialState.Min.x;
                YMin = _initialState.Min.y;
                XMax = _initialState.Max.x;
                YMax = _initialState.Max.y;
            }
            
            public override string ToString()
            {
                return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
            }
            
            public static implicit operator UiPosition(BasePosition pos) => pos.ToPosition();
        }
        public class GridPosition : BasePosition
        {
            public readonly float NumCols;
            public readonly float NumRows;
            
            public GridPosition(float xMin, float yMin, float xMax, float yMax, float numCols, float numRows) : base(xMin, yMin, xMax, yMax)
            {
                NumCols = numCols;
                NumRows = numRows;
            }
            
            public void MoveCols(int cols)
            {
                XMin += cols / NumCols;
                XMax += cols / NumCols;
                
                if (XMax > 1)
                {
                    XMin -= 1;
                    XMax -= 1;
                    MoveRows(1);
                }
            }
            
            public void MoveCols(float cols)
            {
                XMin += cols / NumCols;
                XMax += cols / NumCols;
                
                if (XMax > 1)
                {
                    XMin -= 1;
                    XMax -= 1;
                    MoveRows(1);
                }
            }
            
            public void MoveRows(int rows)
            {
                YMin -= rows / NumRows;
                YMax -= rows / NumRows;
            }
        }
        public class GridPositionBuilder
        {
            private readonly float _numCols;
            private readonly float _numRows;
            private int _rowHeight = 1;
            private int _rowOffset;
            private int _colWidth = 1;
            private int _colOffset;
            private float _xPad;
            private float _yPad;
            
            public GridPositionBuilder(int size) : this(size, size)
            {
            }
            
            public GridPositionBuilder(int numCols, int numRows)
            {
                if (numCols <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
                if (numRows <= 0) throw new ArgumentOutOfRangeException(nameof(numCols));
                _numCols = numCols;
                _numRows = numRows;
            }
            
            public GridPositionBuilder SetRowHeight(int height)
            {
                if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
                _rowHeight = height;
                return this;
            }
            
            public GridPositionBuilder SetRowOffset(int offset)
            {
                if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
                _rowOffset = offset;
                return this;
            }
            
            public GridPositionBuilder SetColWidth(int width)
            {
                if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
                _colWidth = width;
                return this;
            }
            
            public GridPositionBuilder SetColOffset(int offset)
            {
                if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
                _colOffset = offset;
                return this;
            }
            
            public GridPositionBuilder SetPadding(float padding)
            {
                _xPad = padding;
                _yPad = padding;
                return this;
            }
            
            public GridPositionBuilder SetPadding(float xPad, float yPad)
            {
                _xPad = xPad;
                _yPad = yPad;
                return this;
            }
            
            public GridPositionBuilder SetRowPadding(float padding)
            {
                _xPad = padding;
                return this;
            }
            
            public GridPositionBuilder SetColPadding(float padding)
            {
                _yPad = padding;
                return this;
            }
            
            public GridPosition Build()
            {
                float xMin = 0;
                float yMin = 1 - _rowHeight / _numRows;
                float xMax = _colWidth / _numCols;
                float yMax = 1;
                
                if (_colOffset != 0)
                {
                    float size = _colOffset / _numCols;
                    xMin += size;
                    xMax += size;
                }
                
                if (_rowOffset != 0)
                {
                    float size = _rowOffset / _numRows;
                    yMin -= size;
                    yMax -= size;
                }
                
                xMin += _xPad;
                xMax -= _xPad;
                yMin += _yPad;
                yMax -= _yPad;
                
                return new GridPosition(xMin, yMin, xMax, yMax, _numCols, _numRows);
            }
        }
        public struct UiPosition
        {
            public static readonly UiPosition None = new UiPosition(0, 0, 0, 0);
            public static readonly UiPosition Full = new UiPosition(0, 0, 1, 1);
            public static readonly UiPosition HorizontalPaddedFull = Full.SliceHorizontal(0.01f, 0.99f);
            public static readonly UiPosition VerticalPaddedFull = Full.SliceVertical(0.01f, 0.99f);
            public static readonly UiPosition TopLeft = new UiPosition(0, 1, 0, 1);
            public static readonly UiPosition MiddleLeft = new UiPosition(0, .5f, 0, .5f);
            public static readonly UiPosition BottomLeft = new UiPosition(0, 0, 0, 0);
            public static readonly UiPosition TopMiddle = new UiPosition(.5f, 1, .5f, 1);
            public static readonly UiPosition MiddleMiddle = new UiPosition(.5f, .5f, .5f, .5f);
            public static readonly UiPosition BottomMiddle = new UiPosition(.5f, 0, .5f, 0);
            public static readonly UiPosition TopRight = new UiPosition(1, 1, 1, 1);
            public static readonly UiPosition MiddleRight = new UiPosition(1, .5f, 1, .5f);
            public static readonly UiPosition BottomRight = new UiPosition(1, 0, 1, 0);
            
            public static readonly UiPosition Top = new UiPosition(0, 1, 1, 1);
            public static readonly UiPosition Bottom = new UiPosition(0, 0, 1, 0);
            public static readonly UiPosition Left = new UiPosition(0, 0, 0, 1);
            public static readonly UiPosition Right = new UiPosition(1, 0, 1, 1);
            
            public static readonly UiPosition LeftHalf = new UiPosition(0, 0, 0.5f, 1);
            public static readonly UiPosition TopHalf = new UiPosition(0, 0.5f, 1, 1);
            public static readonly UiPosition RightHalf = new UiPosition(0.5f, 0, 1, 1);
            public static readonly UiPosition BottomHalf = new UiPosition(0, 0, 1, 0.5f);
            
            public readonly Vector2 Min;
            public readonly Vector2 Max;
            
            public UiPosition(float xMin, float yMin, float xMax, float yMax)
            {
                Min = new Vector2(xMin, yMin);
                Max = new Vector2(xMax, yMax);
            }
            
            public override string ToString()
            {
                return $"({Min.x:0.####}, {Min.y:0.####}) ({Max.x:0.####}, {Max.y:0.####})";
            }
        }
        public abstract class BaseUiComponent : BasePoolable
        {
            public string Name;
            public string Parent;
            public float FadeOut;
            public UiPosition Position;
            public UiOffset Offset;
            
            protected static T CreateBase<T>(UiPosition pos, UiOffset offset) where T : BaseUiComponent, new()
            {
                T component = UiFrameworkPool.Get<T>();
                component.Position = pos;
                component.Offset = offset;
                return component;
            }
            
            public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard, bool autoDestroy)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
                writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
                writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                if (autoDestroy)
                {
                    writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Name);
                }
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                
                if (needsMouse)
                {
                    writer.AddMouse();
                }
                
                if (needsKeyboard)
                {
                    writer.AddKeyboard();
                }
                
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            public void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Name);
                writer.AddFieldRaw(JsonDefaults.Common.ParentName, Parent);
                writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            protected virtual void WriteComponents(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
                writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Common.Min);
                writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Common.Max);
                writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.Common.Min);
                writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.Common.Max);
                writer.WriteEndObject();
            }
            
            public void SetFadeOut(float duration)
            {
                FadeOut = duration;
            }
            
            protected override void EnterPool()
            {
                Name = null;
                Parent = null;
                FadeOut = 0;
                Position = default(UiPosition);
                Offset = default(UiOffset);
            }
        }
        public abstract class BaseUiImage : BaseUiOutline
        {
            public ImageComponent Image;
            
            public void SetImageType(Image.Type type)
            {
                Image.ImageType = type;
            }
            
            public void SetSprite(string sprite)
            {
                Image.Sprite = sprite;
            }
            
            public void SetMaterial(string material)
            {
                Image.Material = material;
            }
            
            public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = UnityEngine.UI.Image.Type.Simple)
            {
                Image.Sprite = sprite;
                Image.Material = material;
                Image.ImageType = type;
            }
            
            public void SetFadeIn(float duration)
            {
                Image.FadeIn = duration;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Image.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Image.Dispose();
                Image = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Image = UiFrameworkPool.Get<ImageComponent>();
            }
        }
        public abstract class BaseUiOutline : BaseUiComponent
        {
            public OutlineComponent Outline;
            
            public void AddElementOutline(UiColor color)
            {
                Outline = UiFrameworkPool.Get<OutlineComponent>();
                Outline.Color = color;
            }
            
            public void AddElementOutline(UiColor color, Vector2 distance)
            {
                AddElementOutline(color);
                Outline.Distance = distance;
            }
            
            public void AddElementOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
            {
                AddElementOutline(color, distance);
                Outline.UseGraphicAlpha = useGraphicAlpha;
            }
            
            public void RemoveOutline()
            {
                if (Outline != null)
                {
                    Outline.Dispose();
                    Outline = null;
                }
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Outline?.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                RemoveOutline();
            }
        }
        public class UiButton : BaseUiOutline
        {
            public ButtonComponent Button;
            
            public static UiButton CreateCommand(UiPosition pos, UiOffset offset, UiColor color, string command)
            {
                UiButton button = CreateBase<UiButton>(pos, offset);
                button.Button.Color = color;
                button.Button.Command = command;
                return button;
            }
            
            public static UiButton CreateClose(UiPosition pos, UiOffset offset, UiColor color, string close)
            {
                UiButton button = CreateBase<UiButton>(pos, offset);
                button.Button.Color = color;
                button.Button.Close = close;
                return button;
            }
            
            public void SetFadeIn(float duration)
            {
                Button.FadeIn = duration;
            }
            
            public void SetImageType(Image.Type type)
            {
                Button.ImageType = type;
            }
            
            public void SetSprite(string sprite)
            {
                Button.Sprite = sprite;
            }
            
            public void SetMaterial(string material)
            {
                Button.Material = material;
            }
            
            public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
            {
                Button.Sprite = sprite;
                Button.Material = material;
                Button.ImageType = type;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Button.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Button.Dispose();
                Button = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Button = UiFrameworkPool.Get<ButtonComponent>();
            }
        }
        public class UiImage : BaseUiImage
        {
            public static UiImage CreateSpriteImage(UiPosition pos, UiOffset offset, UiColor color, string sprite)
            {
                UiImage image = CreateBase<UiImage>(pos, offset);
                image.Image.Color = color;
                image.Image.Sprite = sprite;
                return image;
            }
        }
        public class UiInput : BaseUiOutline
        {
            public InputComponent Input;
            
            public static UiInput Create(UiPosition pos, UiOffset offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                UiInput input = CreateBase<UiInput>(pos, offset);
                InputComponent comp = input.Input;
                comp.Text = text;
                comp.FontSize = size;
                comp.Color = textColor;
                comp.Align = align;
                comp.Font = font;
                comp.Command = cmd;
                comp.CharsLimit = charsLimit;
                comp.Mode = mode;
                comp.LineType = lineType;
                return input;
            }
            
            public void SetTextAlign(TextAnchor align)
            {
                Input.Align = align;
            }
            
            public void SetCharsLimit(int limit)
            {
                Input.CharsLimit = limit;
            }
            
            public void SetIsPassword(bool isPassword)
            {
                Input.SetMode(InputMode.Password, isPassword);
            }
            
            public void SetIsReadonly(bool isReadonly)
            {
                Input.SetMode(InputMode.ReadOnly, isReadonly);
            }
            
            public void SetAutoFocus(bool autoFocus)
            {
                Input.SetMode(InputMode.AutoFocus, autoFocus);
            }
            
            /// <summary>
            /// Sets if the input should block keyboard input when focused.
            /// This should not be used when the loot panel / crafting UI is open. Use SetNeedsHudKeyboard instead
            /// </summary>
            /// <param name="needsKeyboard"></param>
            public void SetNeedsKeyboard(bool needsKeyboard)
            {
                Input.SetMode(InputMode.NeedsKeyboard, needsKeyboard);
            }
            
            /// <summary>
            /// Sets if the input should block keyboard input when focused a loot panel / crafting ui is open.
            /// This should not if a loot panel / crafting ui won't be open when displaying the UI.
            /// </summary>
            /// <param name="needsKeyboard"></param>
            public void SetNeedsHudKeyboard(bool needsKeyboard)
            {
                Input.SetMode(InputMode.HudNeedsKeyboard, needsKeyboard);
            }
            
            public void SetLineType(InputField.LineType lineType)
            {
                Input.LineType = lineType;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Input.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Input.Dispose();
                Input = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Input = UiFrameworkPool.Get<InputComponent>();
            }
        }
        public class UiItemIcon : BaseUiOutline
        {
            public ItemIconComponent Icon;
            
            public static UiItemIcon Create(UiPosition pos, UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
            {
                UiItemIcon icon = CreateBase<UiItemIcon>(pos, offset);
                icon.Icon.Color = color;
                icon.Icon.ItemId = itemId;
                icon.Icon.SkinId = skinId;
                return icon;
            }
            
            public void SetFadeIn(float duration)
            {
                Icon.FadeIn = duration;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Icon.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Icon.Dispose();
                Icon = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Icon = UiFrameworkPool.Get<ItemIconComponent>();
            }
        }
        public class UiLabel : BaseUiOutline
        {
            public TextComponent Text;
            public CountdownComponent Countdown;
            
            public static UiLabel Create(UiPosition pos, UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = CreateBase<UiLabel>(pos, offset);
                TextComponent textComp = label.Text;
                textComp.Text = text;
                textComp.FontSize = size;
                textComp.Color = color;
                textComp.Align = align;
                textComp.Font = font;
                return label;
            }
            
            public void AddCountdown(int startTime, int endTime, int step, string command)
            {
                Countdown = UiFrameworkPool.Get<CountdownComponent>();
                Countdown.StartTime = startTime;
                Countdown.EndTime = endTime;
                Countdown.Step = step;
                Countdown.Command = command;
            }
            
            public void SetFadeIn(float duration)
            {
                Text.FadeIn = duration;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Text.WriteComponent(writer);
                Countdown?.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Text.Dispose();
                Text = null;
                Countdown?.Dispose();
                Countdown = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Text = UiFrameworkPool.Get<TextComponent>();
            }
        }
        public class UiPanel : BaseUiImage
        {
            public static UiPanel Create(UiPosition pos, UiOffset offset, UiColor color)
            {
                UiPanel panel = CreateBase<UiPanel>(pos, offset);
                panel.Image.Color = color;
                return panel;
            }
        }
        public class UiRawImage : BaseUiOutline
        {
            public RawImageComponent RawImage;
            
            public static UiRawImage CreateUrl(UiPosition pos, UiOffset offset, UiColor color, string url)
            {
                UiRawImage image = CreateBase<UiRawImage>(pos, offset);
                image.RawImage.Color = color;
                image.RawImage.Url = url;
                return image;
            }
            
            public static UiRawImage CreateTexture(UiPosition pos, UiOffset offset, UiColor color, string icon)
            {
                UiRawImage image = CreateBase<UiRawImage>(pos, offset);
                image.RawImage.Color = color;
                image.RawImage.Texture = icon;
                return image;
            }
            
            public static UiRawImage CreateFileImage(UiPosition pos, UiOffset offset, UiColor color, string png)
            {
                UiRawImage image = CreateBase<UiRawImage>(pos, offset);
                image.RawImage.Color = color;
                image.RawImage.Png = png;
                return image;
            }
            
            public void SetMaterial(string material)
            {
                RawImage.Material = material;
            }
            
            public void SetFadeIn(float duration)
            {
                RawImage.FadeIn = duration;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                RawImage.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                RawImage.Dispose();
                RawImage = null;
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                RawImage = UiFrameworkPool.Get<RawImageComponent>();
            }
        }
        public class UiSection : BaseUiComponent
        {
            public static UiSection Create(UiPosition pos, UiOffset offset)
            {
                UiSection panel = CreateBase<UiSection>(pos, offset);
                return panel;
            }
        }
        public class CachedUiBuilder : BaseUiBuilder
        {
            private readonly byte[] _cachedJson;
            
            private CachedUiBuilder(UiBuilder builder)
            {
                _cachedJson = builder.GetBytes();
                RootName = builder.GetRootName();
            }
            
            internal static CachedUiBuilder CreateCachedBuilder(UiBuilder builder) => new CachedUiBuilder(builder);
            
            public override byte[] GetBytes() => _cachedJson;
            
            protected override void AddUi(SendInfo send) => AddUi(send, GetBytes());
        }
        public struct ButtonGroupData
        {
            public readonly string DisplayName;
            public readonly string CommandArgs;
            public readonly bool IsActive;
            
            public ButtonGroupData(string displayName, string commandArgs, bool isActive = false)
            {
                DisplayName = displayName;
                CommandArgs = commandArgs;
                IsActive = isActive;
            }
        }
        public struct DropdownMenuData
        {
            public readonly string DisplayName;
            public readonly string CommandArgs;
            public readonly bool IsActive;
            
            public DropdownMenuData(string displayName, string commandArgs, bool isActive = false)
            {
                DisplayName = displayName;
                CommandArgs = commandArgs;
                IsActive = isActive;
            }
        }
        public struct TimePickerData
        {
            public byte Hour;
            public byte Minute;
            public byte Second;
            
            public TimePickerData(DateTime time)
            {
                Hour = (byte)time.Hour;
                Minute = (byte)time.Minute;
                Second = (byte)time.Second;
            }
            
            public TimePickerData(int hour, int minute, int second)
            {
                Hour = (byte)hour;
                Minute = (byte)minute;
                Second = (byte)second;
            }
            
            public void Update(int seconds)
            {
                int abs = Math.Abs(seconds);
                if (abs == 1)
                {
                    Second += (byte)seconds;
                }
                else if (abs == 60)
                {
                    Minute += (byte)(seconds / 60);
                }
                else
                {
                    Hour += (byte)(seconds / 3600);
                }
            }
            
            public DateTime AsDateTime()
            {
                DateTime now = DateTime.Now;
                return new DateTime(now.Year, now.Month, now.Day, Hour, Minute, Second);
            }
            
            public DateTime AsDateTime(DateTime time)
            {
                return new DateTime(time.Year, time.Month, time.Day, Hour, Minute, Second);
            }
        }
        public abstract class BaseNumberPicker<T> : BaseUiControl where T : struct, IConvertible, IFormattable, IComparable<T>
        {
            public UiPanel Background;
            public UiInput Input;
            
            protected void CreateLeftRightPicker(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, T value, int fontSize, UiColor textColor, UiColor backgroundColor, string command, InputMode mode, float buttonWidth, TextAnchor align, string numberFormat)
            {
                Background = builder.Panel(parent, pos, offset, backgroundColor);
                string displayValue = string.IsNullOrEmpty(numberFormat) ? StringCache<T>.ToString(value) : StringCache<T>.ToString(value, numberFormat);
                Input = builder.Input(Background, UiPosition.Full.SliceHorizontal(buttonWidth, 1 - buttonWidth), displayValue, fontSize, textColor, command, align, mode: mode);
            }
            
            protected void CreateUpDownPicker(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, T value, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align, InputMode mode, string numberFormat)
            {
                Background = builder.Panel(parent, pos, offset, backgroundColor);
                string displayValue = string.IsNullOrEmpty(numberFormat) ? StringCache<T>.ToString(value) : StringCache<T>.ToString(value, numberFormat);
                Input = builder.Input(Background, UiPosition.HorizontalPaddedFull, displayValue, fontSize, textColor, command, mode: mode, align: align);
            }
            
            protected override void EnterPool()
            {
                Background = null;
                Input = null;
            }
        }
        public class UiIncrementalNumberPicker<T> : BaseNumberPicker<T> where T : struct, IConvertible, IFormattable, IComparable<T>
        {
            public List<UiButton> Subtracts;
            public List<UiButton> Adds;
            
            public static UiIncrementalNumberPicker<T> Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, T value, IList<T> increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, TextAnchor align, InputMode mode, T minValue, T maxValue, float buttonWidth, string incrementFormat, string numberFormat)
            {
                UiIncrementalNumberPicker<T> control = CreateControl<UiIncrementalNumberPicker<T>>();
                int incrementCount = increments.Count;
                
                control.CreateLeftRightPicker(builder, parent, pos, offset, value, fontSize, textColor, backgroundColor, command, mode, buttonWidth * incrementCount, align, numberFormat);
                List<UiButton> subtracts = control.Subtracts;
                List<UiButton> adds = control.Adds;
                UiPanel background = control.Background;
                
                for (int i = 0; i < incrementCount; i++)
                {
                    T increment = increments[i];
                    string incrementValue = StringCache<T>.ToString(increment);
                    UiPosition subtractSlice = UiPosition.Full.SliceHorizontal(i * buttonWidth, (i + 1) * buttonWidth);
                    UiPosition addSlice = UiPosition.Full.SliceHorizontal(1 - (buttonWidth * incrementCount) + i * buttonWidth, 1 - (buttonWidth * incrementCount) + (i + 1) * buttonWidth);
                    
                    string displayIncrement = StringCache<T>.ToString(increment, incrementFormat);
                    
                    if (GenericMath.Subtract(value, increment).CompareTo(minValue) >= 0)
                    {
                        subtracts.Add(builder.TextButton(background, subtractSlice,  $"-{displayIncrement}", fontSize, textColor, buttonColor, $"{command} -{incrementValue}"));
                    }
                    else
                    {
                        subtracts.Add(builder.TextButton(background, subtractSlice, $"-{displayIncrement}", fontSize, textColor, disabledButtonColor, string.Empty));
                    }
                    
                    if (GenericMath.Add(value, increment).CompareTo(maxValue) <= 0)
                    {
                        adds.Add(builder.TextButton(background, addSlice, displayIncrement, fontSize, textColor, buttonColor, $"{command} {incrementValue}"));
                    }
                    else
                    {
                        adds.Add(builder.TextButton(background, addSlice, displayIncrement, fontSize, textColor, disabledButtonColor, string.Empty));
                    }
                }
                
                return control;
            }
            
            protected override void LeavePool()
            {
                Subtracts = UiFrameworkPool.GetList<UiButton>();
                Adds = UiFrameworkPool.GetList<UiButton>();
            }
            
            protected override void EnterPool()
            {
                UiFrameworkPool.FreeList(Subtracts);
                UiFrameworkPool.FreeList(Adds);
            }
        }
        public class UiNumberPicker : BaseNumberPicker<int>
        {
            public UiButton Subtract;
            public UiButton Add;
            
            public static UiNumberPicker Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int fontSize, int buttonFontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor disabledButtonColor, string command, string incrementCommand, string decrementCommand, int minValue, int maxValue, float buttonWidth, TextAnchor align, InputMode mode, NumberPickerMode numberMode, string numberFormat)
            {
                UiNumberPicker control = CreateControl<UiNumberPicker>();
                
                if (numberMode == NumberPickerMode.LeftRight)
                {
                    control.CreateLeftRightPicker(builder, parent, pos, offset, value, fontSize, textColor, backgroundColor, command, mode, buttonWidth, align, numberFormat);
                    UiPosition subtractPosition = UiPosition.Full.SliceHorizontal(0, buttonWidth);
                    UiPosition addPosition = UiPosition.Full.SliceHorizontal(1 - buttonWidth, 1);
                    control.CreateAdd(builder, value, maxValue, addPosition, default(UiOffset), "+", buttonFontSize, textColor, buttonColor, disabledButtonColor, incrementCommand);
                    control.CreateSubtract(builder, value, minValue, subtractPosition, default(UiOffset), "-", buttonFontSize, textColor, buttonColor, disabledButtonColor, decrementCommand);
                }
                else
                {
                    int width = UiHelpers.TextOffsetWidth(1, buttonFontSize, 4);
                    UiOffset pickerOffset = offset.SliceHorizontal(0, width);
                    control.CreateUpDownPicker(builder, parent, pos, pickerOffset, value, fontSize, textColor, backgroundColor, command, align, mode, numberFormat);
                    UiOffset buttonOffset = new UiOffset(0, 0, width, 0);
                    control.CreateAdd(builder, value, maxValue, new UiPosition(1, 0.5f, 1, 1), buttonOffset, "<b>˄</b>", buttonFontSize, textColor, buttonColor, disabledButtonColor, incrementCommand);
                    control.CreateSubtract(builder, value, minValue, new UiPosition(1, 0, 1, 0.5f), buttonOffset, "<b>˅</b>", buttonFontSize, textColor, buttonColor, disabledButtonColor, decrementCommand);
                }
                
                return control;
            }
            
            private void CreateSubtract(UiBuilder builder, int value, int minValue, UiPosition position, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor buttonColor, UiColor disabledButtonColor, string command)
            {
                if (value > minValue)
                {
                    Subtract = builder.TextButton(Background, position, offset, text, fontSize, textColor, buttonColor, command);
                }
                else
                {
                    Subtract = builder.TextButton(Background, position, offset, text, fontSize, textColor.MultiplyAlpha(0.5f), disabledButtonColor, null);
                }
            }
            
            private void CreateAdd(UiBuilder builder, int value, int maxValue, UiPosition position, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor buttonColor, UiColor disabledButtonColor, string command)
            {
                if (value < maxValue)
                {
                    Add = builder.TextButton(Background, position, offset, text, fontSize, textColor, buttonColor,  command);
                }
                else
                {
                    Add = builder.TextButton(Background, position, offset, text, fontSize, textColor.MultiplyAlpha(0.5f), disabledButtonColor, null);
                }
            }
            
            protected override void EnterPool()
            {
                Subtract = null;
                Add = null;
            }
        }
        public abstract class BasePopoverControl : BaseUiControl
        {
            public UiBuilder Builder;
            public UiButton OutsideClose;
            public UiPanel PopoverBackground;
            
            public static BasePopoverControl CreateBuilder(BasePopoverControl control, string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                string name = $"{parentName}_Popover";
                
                UiBuilder builder = UiBuilder.Create(UiSection.Create(UiPosition.Full, default(UiOffset)), name, parentName);
                
                UiPosition anchor = GetPopoverPosition(position);
                UiOffset offset = GetPopoverOffset(position, size);
                
                control.Builder = builder;
                control.OutsideClose = builder.CloseButton(builder.Root, UiPosition.Full, new UiOffset(-1000, -1000, 1000, 1000), UiColor.Clear, name);
                control.PopoverBackground = builder.Panel(builder.Root, anchor, offset, backgroundColor);
                control.PopoverBackground.SetSpriteMaterialImage(menuSprite, null, Image.Type.Sliced);
                control.PopoverBackground.AddElementOutline(UiColor.Black.WithAlpha(0.75f));
                builder.OverrideRoot(control.PopoverBackground);
                
                return control;
            }
            
            public static UiPosition GetPopoverPosition(PopoverPosition position)
            {
                switch (position)
                {
                    case PopoverPosition.Top:
                    case PopoverPosition.Left:
                    return UiPosition.TopLeft;
                    
                    case PopoverPosition.Right:
                    return UiPosition.TopRight;
                    
                    case PopoverPosition.Bottom:
                    return UiPosition.BottomLeft;
                    
                    default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
                }
            }
            
            public static UiOffset GetPopoverOffset(PopoverPosition position, Vector2Int size)
            {
                switch (position)
                {
                    case PopoverPosition.Top:
                    return new UiOffset(0, 1, 1 + size.x, size.y);
                    
                    case PopoverPosition.Left:
                    return new UiOffset(-size.x, -size.y - 1, 0, -1);
                    
                    case PopoverPosition.Right:
                    return new UiOffset(0, -size.y - 1, size.x, -1);
                    
                    case PopoverPosition.Bottom:
                    return new UiOffset(1, -size.y, 1 + size.x, 0);
                    
                    default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
                }
            }
            
            protected override void EnterPool()
            {
                Builder = null;
                OutsideClose = null;
                PopoverBackground = null;
            }
        }
        public class UiCalenderPicker : BasePopoverControl
        {
            public UiButton PreviousYear;
            public UiButton PreviousMonth;
            public UiButton NextYear;
            public UiButton NextMonth;
            public List<UiButton> DateButtons;
            
            private const int MenuPadding = 4;
            private const int ItemPadding = 2;
            private const int HorizontalButtonPadding = 5;
            private const int VerticalButtonPadding = 3;
            
            private int _width;
            private int _height;
            
            private DateTime _firstOfTheMonth;
            private DateTime _previousYear;
            private DateTime _previousMonth;
            private DateTime _nextMonth;
            private DateTime _nextYear;
            
            private int _daysInMonth;
            
            private const int NumColumns = 7;
            private int _numRows;
            private int _maxDays;
            
            private int _textHeight;
            private int _textWidth1;
            private int _textWidth2;
            private int _textWidth3;
            
            private string _yearText;
            private string _monthLabelText;
            
            private static readonly string[] DayOfWeekNames = { "Su", "M", "Tu", "W", "Th", "F", "Sa" };
            
            public static UiCalenderPicker Create(string parentName, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, PopoverPosition position, string menuSprite, string buttonSprite)
            {
                UiCalenderPicker control = CreateControl<UiCalenderPicker>();
                
                //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(Create)} Num Rows: {control.NumRows} Max Days: {control.MaxDays}");
                
                control.CalculateDates(date);
                control.CalculateSize(fontSize);
                
                CreateBuilder(control, parentName, new Vector2Int(control._width + MenuPadding * 2, control._height + MenuPadding * 2), backgroundColor, position, menuSprite);
                UiBuilder builder = control.Builder;
                
                control.CreateHeader(builder, fontSize, textColor, buttonColor, changeCommand, buttonSprite);
                control.CreateDayOfWeekHeader(builder, fontSize, textColor);
                control.CreateCalender(builder, fontSize, textColor, buttonColor, selectedDateColor, changeCommand, buttonSprite);
                return control;
            }
            
            public void CalculateDates(DateTime date)
            {
                _firstOfTheMonth = new DateTime(date.Year, date.Month, 1);
                _previousYear = _firstOfTheMonth.AddYears(-1);
                _previousMonth = _firstOfTheMonth.AddMonths(-1);
                _nextMonth = _firstOfTheMonth.AddMonths(1);
                _nextYear = _firstOfTheMonth.AddYears(1);
                _daysInMonth = DateTime.DaysInMonth(_firstOfTheMonth.Year, _firstOfTheMonth.Month);
                _yearText = StringCache<int>.ToString(_firstOfTheMonth.Year);
                _monthLabelText = StringCache<DateTime>.ToString(_firstOfTheMonth, "MMM");
                _numRows = GetWeekRows(date.Year, date.Month);
                _maxDays = _numRows * NumColumns;
            }
            
            public void CalculateSize(int fontSize)
            {
                _textHeight = UiHelpers.TextOffsetHeight(fontSize, VerticalButtonPadding);
                _textWidth1 = UiHelpers.TextOffsetWidth(1, fontSize, HorizontalButtonPadding);
                _textWidth2 = UiHelpers.TextOffsetWidth(2, fontSize, HorizontalButtonPadding);
                _textWidth3 = UiHelpers.TextOffsetWidth(3, fontSize, HorizontalButtonPadding);
                
                _width = NumColumns * _textWidth2 + ItemPadding * (NumColumns - 1);
                _height = (_numRows + 2) * _textHeight + ItemPadding * (_numRows + 1);
                //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(CalculateSize)} {Width} x {Height}");
            }
            
            public void CreateHeader(UiBuilder builder, int fontSize, UiColor textColor, UiColor buttonColor, string changeCommand, string buttonSprite)
            {
                UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, _textWidth3,  -MenuPadding);
                //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(CreateHeader)} {pos.ToString()}");
                PreviousYear = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, "<<<", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_previousYear)}");
                PreviousYear.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                pos = pos.MoveXPadded(ItemPadding);
                
                pos = pos.SetWidth(_textWidth1);
                PreviousMonth = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, "<", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_previousMonth)}");
                PreviousMonth.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                
                float width = UiHelpers.TextOffsetWidth(_monthLabelText.Length + 5, fontSize, HorizontalButtonPadding);
                pos = new UiOffset(-width / 2, -MenuPadding - _textHeight, width / 2, -MenuPadding);
                builder.Label(builder.Root, UiPosition.TopMiddle, pos, $"{_monthLabelText} {_yearText}", fontSize, textColor);
                
                pos = new UiOffset(-MenuPadding - _textWidth3, -MenuPadding - _textHeight, -MenuPadding, -MenuPadding);
                NextMonth = builder.TextButton(builder.Root, UiPosition.TopRight, pos, ">>>", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_nextYear)}");
                NextMonth.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                pos = pos.MoveXPadded(-ItemPadding);
                
                pos = pos.SetX(pos.Max.x - _textWidth1, pos.Max.x);
                NextYear = builder.TextButton(builder.Root, UiPosition.TopRight, pos, ">", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_nextMonth)}");
                NextYear.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
            }
            
            public void CreateDayOfWeekHeader(UiBuilder builder, int fontSize, UiColor textColor)
            {
                UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, MenuPadding + _textWidth2,  -MenuPadding);
                pos = pos.MoveYPadded(-ItemPadding);
                
                for (int i = 0; i < 7; i++)
                {
                    builder.Label(builder.Root, UiPosition.TopLeft, pos, DayOfWeekNames[i], fontSize, textColor);
                    pos = pos.MoveXPadded(ItemPadding);
                }
            }
            
            public void CreateCalender(UiBuilder builder, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, string buttonSprite)
            {
                UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, MenuPadding + _textWidth2,  -MenuPadding);
                pos = pos.MoveYPadded(-ItemPadding);
                pos = pos.MoveYPadded(-ItemPadding);
                
                int offset = 0;
                int dayOfWeek = (int)_firstOfTheMonth.DayOfWeek;
                for (int i = 0; i < dayOfWeek; i++)
                {
                    DateTime date = _firstOfTheMonth.AddDays(-dayOfWeek + i);
                    UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor.MultiplyAlpha(0.5f), buttonColor, $"{changeCommand} {GetCommandArg(date)}");
                    button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                    DateButtons.Add(button);
                    pos = pos.MoveXPadded(ItemPadding);
                    offset++;
                }
                
                for (int i = 0; i < _daysInMonth; i++)
                {
                    if (offset != 0 && offset % 7 == 0)
                    {
                        pos = pos.SetX(MenuPadding, MenuPadding + _textWidth2);
                        pos = pos.MoveYPadded(-ItemPadding);
                    }
                    
                    DateTime date = _firstOfTheMonth.AddDays(i);
                    UiColor color = date.Date == DateTime.Now.Date ? selectedDateColor : buttonColor;
                    
                    UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor, color, $"{changeCommand} {GetCommandArg(date)}");
                    button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                    DateButtons.Add(button);
                    pos = pos.MoveXPadded(ItemPadding);
                    offset++;
                }
                
                for (int i = 0; offset < _maxDays; i++)
                {
                    if (offset != 0 && offset % 7 == 0)
                    {
                        pos = pos.SetX(MenuPadding, MenuPadding + _textWidth2);
                        pos = pos.MoveYPadded(-ItemPadding);
                    }
                    
                    DateTime date = _nextMonth.AddDays(i);
                    UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor.MultiplyAlpha(0.5f), buttonColor, $"{changeCommand} {GetCommandArg(date)}");
                    button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                    DateButtons.Add(button);
                    pos = pos.MoveXPadded(ItemPadding);
                    offset++;
                }
            }
            
            public int GetWeekRows(int year, int month)
            {
                DateTime firstDayOfMonth = new DateTime(year, month, 1);
                DateTime lastDayOfMonth = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                Calendar calendar = CultureInfo.CurrentCulture.Calendar;
                int lastWeek = calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                int firstWeek = calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(GetWeekRows)} First: {firstDayOfMonth} {firstWeek} Last: {lastDayOfMonth} {lastWeek}");
                return lastWeek - firstWeek + 1;
            }
            
            public string GetCommandArg(DateTime date)
            {
                return $"{StringCache<int>.ToString(date.Year)}/{StringCache<int>.ToString(date.Month)}/{StringCache<int>.ToString(date.Day)}";
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                DateButtons = UiFrameworkPool.GetList<UiButton>();
            }
            
            protected override void EnterPool()
            {
                _yearText = null;
                _monthLabelText = null;
                _width = 0;
                _height = 0;
                _numRows = 0;
                _maxDays = 0;
                
                _textHeight = 0;
                _textWidth1 = 0;
                _textWidth2 = 0;
                _textWidth3 = 0;
                _daysInMonth = 0;
                
                PreviousYear = null;
                PreviousMonth = null;
                NextYear = null;
                NextMonth = null;
                UiFrameworkPool.FreeList(DateButtons);
            }
        }
        public class UiDatePickerMenu : BasePopoverControl
        {
            public UiPicker Year;
            public UiPicker Month;
            public UiPicker Day;
            
            public const int MenuPadding = 5;
            public const int ItemPadding = 3;
            
            private string _yearText;
            private string _monthLabelText;
            private string _monthValueText;
            private string _dayText;
            
            private int _yearWidth;
            private int _monthWidth;
            private int _dayWidth;
            
            private int _width;
            private int _height;
            
            public static UiDatePickerMenu Create(string parentName, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order, PopoverPosition position, string menuSprite)
            {
                UiDatePickerMenu control = CreateControl<UiDatePickerMenu>();
                
                control._width = control.PopulateVariables(displayMode, date, fontSize);
                control._height = UiHelpers.TextOffsetHeight(fontSize) * 3;
                
                Vector2Int size = new Vector2Int(control._width + MenuPadding * 2 + 1, control._height + MenuPadding * 2);
                CreateBuilder(control, parentName, size, backgroundColor, position, menuSprite);
                
                UiBuilder builder = control.Builder;
                
                control.CreatePickers(builder, date, fontSize, textColor, backgroundColor, changeCommand, displayMode, order);
                
                return control;
            }
            
            public int PopulateVariables(DatePickerDisplayMode displayMode, DateTime date, int fontSize)
            {
                int width = 0;
                if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Year))
                {
                    _yearText = StringCache<int>.ToString(date.Year);
                    _yearWidth = UiHelpers.TextOffsetWidth(_yearText.Length, fontSize);
                    width += _yearWidth;
                }
                
                if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Month))
                {
                    if (width != 0)
                    {
                        width += ItemPadding;
                    }
                    
                    _monthLabelText = date.ToString("MMM");
                    _monthValueText = StringCache<int>.ToString(date.Month);
                    _monthWidth = UiHelpers.TextOffsetWidth(_monthLabelText.Length, fontSize);
                    width += _monthWidth;
                }
                
                if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Day))
                {
                    if (width != 0)
                    {
                        width += ItemPadding;
                    }
                    
                    _dayText = StringCache<int>.ToString(date.Day);
                    _dayWidth = UiHelpers.TextOffsetWidth(_dayText.Length, fontSize);
                    width += _dayWidth;
                }
                
                width += MenuPadding * 2;
                
                return width;
            }
            
            public void CreatePickers(UiBuilder builder, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order)
            {
                UiOffset offset = new UiOffset(MenuPadding, MenuPadding, MenuPadding, _height);
                
                switch (order)
                {
                    case DatePickerDisplayOrder.MonthDayYear:
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    if (CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_dayWidth + ItemPadding);
                    CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
                    case DatePickerDisplayOrder.YearMonthDay:
                    if (CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_yearWidth + ItemPadding);
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
                    case DatePickerDisplayOrder.DayMonthYear:
                    if (CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_dayWidth + ItemPadding);
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
                }
            }
            
            public bool CreateYearPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
            {
                if (_yearWidth == 0)
                {
                    return false;
                }
                
                string increment = $"{changeCommand} {StringCache<int>.ToString(value.Year + 1)}/{_monthValueText}/{_dayText}";
                string decrement = $"{changeCommand} {StringCache<int>.ToString(value.Year - 1)}/{_monthValueText}/{_dayText}";
                Year = UiPicker.Create(builder, pos, _yearText, fontSize, textColor, backgroundColor, _height, increment, decrement);
                return true;
            }
            
            public bool CreateMonthPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
            {
                if (_monthWidth == 0)
                {
                    return false;
                }
                
                string increment = $"{changeCommand} {_yearText}/{StringCache<int>.ToString(value.Month % 12 + 1)}/{_dayText}";
                string decrement = $"{changeCommand} {_yearText}/{StringCache<int>.ToString(value.Month == 1 ? 12 : value.Month - 1)}/{_dayText}";
                Month = UiPicker.Create(builder, pos, _monthLabelText, fontSize, textColor, backgroundColor, _height, increment, decrement);
                return true;
            }
            
            public bool CreateDayPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
            {
                if (_dayWidth == 0)
                {
                    return false;
                }
                
                int numDays = DateTime.DaysInMonth(value.Year, value.Month);
                string increment = $"{changeCommand} {_yearText}/{_monthValueText}/{StringCache<int>.ToString(value.Day % numDays + 1)}";
                string decrement = $"{changeCommand} {_yearText}/{_monthValueText}/{StringCache<int>.ToString(value.Day == 1 ? numDays : value.Day - 1)}";
                Day = UiPicker.Create(builder, pos, _dayText, fontSize, textColor, backgroundColor, _height, increment, decrement);
                return true;
            }
            
            private bool HasDatePickerDisplayModeFlag(DatePickerDisplayMode mode, DatePickerDisplayMode flag)
            {
                return (mode & flag) != 0;
            }
            
            protected override void EnterPool()
            {
                Year = null;
                Month = null;
                Day = null;
                _yearText = null;
                _monthLabelText = null;
                _monthValueText = null;
                _dayText = null;
                _yearWidth = 0;
                _monthWidth = 0;
                _dayWidth = 0;
                _width = 0;
                _height = 0;
            }
        }
        public class UiDropdownMenu : BasePopoverControl
        {
            public UiSection ScrollBarSection;
            public UiScrollBar ScrollBar;
            public List<UiDropdownMenuItem> Items;
            
            public static UiDropdownMenu Create(string parentName, List<DropdownMenuData> items, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand, string pageCommand = null, int page = 0, int maxValuesPerPage = 100, int minWidth = 100,
            PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                const int itemPadding = 4;
                const int menuPadding = 5;
                
                UiDropdownMenu control = CreateControl<UiDropdownMenu>();
                
                int itemCount = Math.Min(items.Count, maxValuesPerPage);
                int width = Math.Max(minWidth, control.GetWidth(items, fontSize));
                int itemHeight = UiHelpers.TextOffsetHeight(fontSize);
                int height = itemCount * (itemHeight + itemPadding) + menuPadding * 2;
                int maxPage = UiHelpers.CalculateMaxPage(items.Count, maxValuesPerPage);
                
                Vector2Int size = new Vector2Int(width, height);
                CreateBuilder(control, parentName, size, backgroundColor, position, menuSprite);
                
                UiBuilder builder = control.Builder;
                
                UiOffset buttonPos = new UiOffset(menuPadding, -itemHeight - menuPadding, width + menuPadding, -menuPadding);
                if (maxPage > 0)
                {
                    buttonPos = buttonPos.SliceHorizontal(0, 10);
                    control.ScrollBarSection = builder.Section(builder.Root, UiPosition.Right, new UiOffset(-10, 5, -3, -5));
                    control.ScrollBar = builder.ScrollBar(control.ScrollBarSection, UiPosition.Full, default(UiOffset), page, maxPage, UiColors.ButtonPrimary, UiColors.PanelSecondary, pageCommand);
                    control.ScrollBar.SetSpriteMaterialImage(UiConstants.Sprites.RoundedBackground1, null, Image.Type.Sliced);
                }
                
                for (int i = page * maxValuesPerPage; i < page * maxValuesPerPage + itemCount; i++)
                {
                    control.Items.Add(UiDropdownMenuItem.Create(builder, buttonPos, items[i], fontSize, textColor, backgroundColor, selectedCommand));
                    buttonPos = buttonPos.MoveY(-itemHeight - itemPadding);
                }
                
                return control;
            }
            
            public virtual int GetWidth(List<DropdownMenuData> items, int fontSize)
            {
                int width = 0;
                int count = items.Count;
                for (int i = 0; i < count; i++)
                {
                    DropdownMenuData item = items[i];
                    int valueWidth = UiHelpers.TextOffsetWidth(item.DisplayName.Length, fontSize);
                    if (valueWidth > width)
                    {
                        width = valueWidth;
                    }
                }
                
                return width;
            }
        }
        public class UiDropdownMenuItem : BaseUiControl
        {
            public UiButton Button;
            public UiLabel Label;
            
            public static UiDropdownMenuItem Create(UiBuilder builder, UiOffset position, DropdownMenuData item, int fontSize, UiColor textColor, UiColor backgroundColor, string selectedCommand)
            {
                UiDropdownMenuItem control = CreateControl<UiDropdownMenuItem>();
                
                control.Button = builder.CommandButton(builder.Root, UiPosition.TopLeft, position, backgroundColor, $"{selectedCommand} {item.CommandArgs}");
                control.Label = builder.Label(control.Button, UiPosition.Full, item.DisplayName, fontSize, textColor);
                
                return control;
            }
        }
        public class UiPopover : BasePopoverControl
        {
            public static UiPopover Create(string parentName, Vector2Int size, UiColor backgroundColor, PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                UiPopover control = CreateControl<UiPopover>();
                CreateBuilder(control, parentName, size, backgroundColor, position, menuSprite);
                return control;
            }
        }
        public class UiTimePickerMenu : BasePopoverControl
        {
            public UiPicker Hour;
            public UiPicker Minute;
            public UiPicker Second;
            public UiPicker AmPm;
            
            public static UiTimePickerMenu Create(string parentName, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, TimePickerDisplayMode displayMode = TimePickerDisplayMode.All, ClockMode clockMode = ClockMode.Hour12,
            PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
            {
                const int menuPadding = 5;
                const int itemPadding = 3;
                
                UiTimePickerMenu control = CreateControl<UiTimePickerMenu>();
                
                int numPickers = GetPickerCount(displayMode, clockMode);
                
                int segmentWidth = UiHelpers.TextOffsetWidth(2, fontSize);
                int width = menuPadding * 2 + (numPickers - 2) * itemPadding + numPickers * segmentWidth;
                int height = UiHelpers.TextOffsetHeight(fontSize) * 3;
                Interface.Oxide.LogDebug($"{nameof(UiTimePickerMenu)}.{nameof(Create)} {width} x {height}");
                
                Vector2Int size = new Vector2Int(width + menuPadding * 2 + 1, height + menuPadding * 2);
                CreateBuilder(control, parentName, size, backgroundColor, position, menuSprite);
                
                UiBuilder builder = control.Builder;
                
                UiOffset offset = new UiOffset(menuPadding, menuPadding, segmentWidth + menuPadding * 2, height);
                //Interface.Oxide.LogDebug(offset.ToString());
                
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Hours))
                {
                    control.Hour = CreateTimePickerTimeSegment(builder, offset, time.Hour, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Hours, clockMode, changeCommand);
                    offset = offset.MoveX(segmentWidth + itemPadding);
                    //Interface.Oxide.LogDebug($"Hour:{offset.ToString()}");
                }
                
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Minutes))
                {
                    control.Minute = CreateTimePickerTimeSegment(builder, offset, time.Minute, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Minutes, clockMode, changeCommand);
                    offset = offset.MoveX(segmentWidth + itemPadding);
                    //Interface.Oxide.LogDebug($"Minute:{offset.ToString()}");
                }
                
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Seconds))
                {
                    control.Second = CreateTimePickerTimeSegment(builder, offset, time.Second, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Seconds, clockMode, changeCommand);
                    offset = offset.MoveX(segmentWidth + itemPadding);
                    //Interface.Oxide.LogDebug($"Second:{offset.ToString()}");
                }
                
                if (clockMode == ClockMode.Hour12)
                {
                    control.AmPm = CreateTimePickerAmPmSegment(builder, offset, time.Hour, fontSize, textColor, backgroundColor, changeCommand);
                }
                
                return control;
            }
            
            private static int GetPickerCount(TimePickerDisplayMode displayMode, ClockMode clockMode)
            {
                int numPickers = 0;
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Hours))
                {
                    numPickers++;
                }
                
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Minutes))
                {
                    numPickers++;
                }
                
                if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Seconds))
                {
                    numPickers++;
                }
                
                if (clockMode == ClockMode.Hour12)
                {
                    numPickers++;
                }
                
                return numPickers;
            }
            
            public static UiPicker CreateTimePickerTimeSegment(UiBuilder builder, UiOffset pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, TimePickerDisplayMode mode, ClockMode clockMode, string changeCommand)
            {
                float height = pos.Height / 3f;
                string timeAmount = StringCache<int>.ToString(mode == TimePickerDisplayMode.Hours ? 3600 : mode == TimePickerDisplayMode.Minutes ? 60 : 1);
                if (clockMode == ClockMode.Hour12 && mode == TimePickerDisplayMode.Hours)
                {
                    if (value > 12)
                    {
                        value -= 12;
                    }
                    
                    if (value <= 0)
                    {
                        value = 1;
                    }
                }
                
                string valueText = mode == TimePickerDisplayMode.Hours ? StringCache<int>.ToString(value) : value.ToString("00");
                
                return UiPicker.Create(builder, pos, valueText, fontSize, textColor, backgroundColor, height, $"{changeCommand} {timeAmount}", $"{changeCommand} -{timeAmount}");
            }
            
            public static UiPicker CreateTimePickerAmPmSegment(UiBuilder builder, UiOffset pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
            {
                float height = pos.Height / 3f;
                string command = value >= 12 ? $"{changeCommand} -43200" : $"{changeCommand} 43200";
                return UiPicker.Create(builder, pos, value >= 12 ? "PM" : "AM", fontSize, textColor, backgroundColor, height, command, command);
            }
            
            private static bool HasTimePickerDisplayModeFlag(TimePickerDisplayMode mode, TimePickerDisplayMode flag)
            {
                return (mode & flag) != 0;
            }
            
            protected override void EnterPool()
            {
                Hour = null;
                Minute = null;
                Second = null;
                AmPm = null;
            }
        }
        public class UiFrameworkArrayPool<T>
        {
            public static readonly UiFrameworkArrayPool<T> Shared;
            
            private const int DefaultMaxArrayLength = 1024 * 16;
            private const int DefaultMaxNumberOfArraysPerBucket = 50;
            
            private readonly Bucket[] _buckets;
            
            static UiFrameworkArrayPool()
            {
                Shared = new UiFrameworkArrayPool<T>();
            }
            
            private UiFrameworkArrayPool() : this(DefaultMaxArrayLength, DefaultMaxNumberOfArraysPerBucket) { }
            
            private UiFrameworkArrayPool(int maxArrayLength, int maxArraysPerBucket)
            {
                if (maxArrayLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxArrayLength));
                if (maxArraysPerBucket <= 0) throw new ArgumentOutOfRangeException(nameof(maxArraysPerBucket));
                
                maxArrayLength = Mathf.Clamp(maxArrayLength, 16, DefaultMaxArrayLength);
                
                _buckets = new Bucket[SelectBucketIndex(maxArrayLength) + 1];
                for (int i = 0; i < _buckets.Length; i++)
                {
                    _buckets[i] = new Bucket(GetMaxSizeForBucket(i), maxArraysPerBucket);
                }
            }
            
            public T[] Rent(int minimumLength)
            {
                if (minimumLength < 0)  throw new ArgumentOutOfRangeException(nameof(minimumLength));
                
                if (minimumLength == 0)
                {
                    return Array.Empty<T>();
                }
                
                T[] array;
                int bucketIndex = SelectBucketIndex(minimumLength);
                int index = bucketIndex;
                do
                {
                    array = _buckets[index].Rent();
                    if (array != null)
                    {
                        return array;
                    }
                    
                    index++;
                }
                while (index < _buckets.Length && index != bucketIndex + 2);
                array = new T[_buckets[bucketIndex].BufferLength];
                return array;
            }
            
            public void Return(T[] array)
            {
                if (array == null) throw new ArgumentNullException(nameof(array));
                if (array.Length == 0)
                {
                    return;
                }
                
                int num = SelectBucketIndex(array.Length);
                if (num < _buckets.Length)
                {
                    _buckets[num].Return(array);
                }
            }
            
            private static int GetMaxSizeForBucket(int binIndex)
            {
                return 16 << (binIndex & 31);
            }
            
            private static int SelectBucketIndex(int bufferSize)
            {
                uint num = (uint)(bufferSize - 1 >> 4);
                int num1 = 0;
                if (num > 255)
                {
                    num >>= 8;
                    num1 += 8;
                }
                if (num > 15)
                {
                    num >>= 4;
                    num1 += 4;
                }
                if (num > 3)
                {
                    num >>= 2;
                    num1 += 2;
                }
                if (num > 1)
                {
                    num >>= 1;
                    num1++;
                }
                return (int)(num1 + num);
            }
            
            private void ClearInternal()
            {
                for (int index = 0; index < _buckets.Length; index++)
                {
                    _buckets[index].Clear();
                }
            }
            
            public static void Clear()
            {
                Shared.ClearInternal();
            }
            
            private sealed class Bucket
            {
                internal readonly int BufferLength;
                
                private readonly T[][] _buffers;
                
                private int _index;
                
                internal Bucket(int bufferLength, int numberOfBuffers)
                {
                    _buffers = new T[numberOfBuffers][];
                    BufferLength = bufferLength;
                }
                
                internal T[] Rent()
                {
                    if (_index < _buffers.Length)
                    {
                        T[] array = _buffers[_index];
                        _buffers[_index] = null;
                        _index++;
                        if (array != null)
                        {
                            return array;
                        }
                    }
                    return new T[BufferLength];
                }
                
                internal void Return(T[] array)
                {
                    if (array.Length != BufferLength)  throw new ArgumentException("Buffer not from pool", nameof(array));
                    if (_index != 0)
                    {
                        _index--;
                        _buffers[_index] = array;
                    }
                }
                
                public void Clear()
                {
                    for (int index = 0; index < _buffers.Length; index++)
                    {
                        _buffers[index] = null;
                    }
                    
                    _index = 0;
                }
            }
        }
    }
    #endregion

}

namespace Oxide.Plugins.UiElementsTestExtensions
{
    using UiFrameworkPool = UiElementsTest.UiFrameworkPool;
    using UiColor = UiElementsTest.UiColor;
    using UiOffset = UiElementsTest.UiOffset;
    using UiPosition = UiElementsTest.UiPosition;

    public static class ArgExt
    {
        public static DateTime GetDateTime(this ConsoleSystem.Arg arg, int iArg, DateTime def)
        {
            string s = arg.GetString(iArg, null);
            if (string.IsNullOrEmpty(s))
            {
                return def;
            }
            
            DateTime date = DateTime.Parse(s);
            return date;
        }
    }
    public static class StringBuilderExt
    {
        /// <summary>
        /// Frees a <see cref="StringBuilder"/> back to the pool returning the created <see cref="string"/>
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> with string and being freed</param>
        public static string ToStringAndFree(this StringBuilder sb)
        {
            string result = sb.ToString();
            UiFrameworkPool.FreeStringBuilder(sb);
            return result;
        }
    }
    public static class UiColorExt
    {
        public static UiColor WithAlpha(this UiColor color, string hex)
        {
            return WithAlpha(color, int.Parse(hex, NumberStyles.HexNumber));
        }
        
        public static UiColor WithAlpha(this UiColor color, int alpha)
        {
            return color.WithAlpha(alpha / 255f);
        }
        
        public static UiColor WithAlpha(this UiColor color, float alpha)
        {
            return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(alpha)));
        }
        
        public static UiColor MultiplyAlpha(this UiColor color, float alpha)
        {
            return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(color.Color.a * alpha)));
        }
        
        public static UiColor ToGrayScale(this UiColor color)
        {
            float scale = color.Color.grayscale;
            return new UiColor(new Color(scale, scale, scale));
        }
        
        public static UiColor Darken(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = col.r * (1 - percentage);
            float green = col.g * (1 - percentage);
            float blue = col.b * (1 - percentage);
            
            return new UiColor(red, green, blue, col.a);
        }
        
        public static UiColor Lighten(this UiColor color, float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            Color col = color.Color;
            float red = (1 - col.r) * percentage + col.r;
            float green = (1 - col.g) * percentage + col.g;
            float blue = (1 - col.b) * percentage + col.b;
            
            return new UiColor(red, green, blue, col.a);
        }
        
        public static UiColor Lerp(this UiColor start, UiColor end, float value)
        {
            return Color.Lerp(start, end, value);
        }
    }
    public static class UiOffsetExt
    {
        public static UiOffset SetX(this UiOffset pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(xMin, min.y, xMax, max.y);
        }
        
        public static UiOffset SetY(this UiOffset pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, yMin, max.x, yMax);
        }
        
        public static UiOffset SetWidth(this UiOffset pos, float width)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y,  min.x + width, max.y);
        }
        
        public static UiOffset SetHeight(this UiOffset pos, float height)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y, max.x, min.y + height);
        }
        
        public static UiOffset MoveX(this UiOffset pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + delta, min.y, max.x + delta, max.y);
        }
        
        public static UiOffset MoveXPadded(this UiOffset pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiOffset(min.x + spacing, min.y, max.x + spacing, max.y);
        }
        
        public static UiOffset MoveXPaddedWithWidth(this UiOffset pos, float padding, float width)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiOffset(min.x + spacing, min.y, min.x + spacing + width, max.y);
        }
        
        public static UiOffset MoveXPaddedWithHeight(this UiOffset pos, float padding, float height)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiOffset(min.x + spacing, min.y, max.x + spacing, min.y + height);
        }
        
        public static UiOffset MoveY(this UiOffset pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y + delta, max.x, max.y + delta);
        }
        
        public static UiOffset MoveYPadded(this UiOffset pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiOffset(min.x, min.y + spacing, max.x, max.y + spacing);
        }
        
        public static UiOffset MoveYPaddedWithWidth(this UiOffset pos, float padding, float width)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiOffset(min.x, min.y + spacing, min.x + width, max.y + spacing);
        }
        
        public static UiOffset MoveYPaddedWithHeight(this UiOffset pos, float padding, float height)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            int multiplier = padding < 0 ? -1 : 1;
            float spacing = (max.y - min.y + Math.Abs(padding)) * multiplier;
            return new UiOffset(min.x, min.y + spacing, max.x, min.y + spacing + height * multiplier);
        }
        
        public static UiOffset Expand(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
        }
        
        public static UiOffset ExpandHorizontal(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x - amount, min.y, max.x + amount, max.y);
        }
        
        public static UiOffset ExpandVertical(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y - amount, max.x, max.y + amount);
        }
        
        public static UiOffset Shrink(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
        }
        
        public static UiOffset ShrinkHorizontal(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + amount, min.y, max.x - amount, max.y);
        }
        
        public static UiOffset ShrinkVertical(this UiOffset pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y + amount, max.x, max.y - amount);
        }
        
        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="left">Pixels to remove from the left</param>
        /// <param name="bottom">Pixels to remove from the bottom</param>
        /// <param name="right">>Pixels to remove from the right</param>
        /// <param name="top">Pixels to remove from the top</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset Slice(this UiOffset pos, int left, int bottom, int right, int top)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + left, min.y + bottom, max.x - right, max.y - top);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="pos">Offset to slice</param>
        /// <param name="left">Pixels to remove from the left</param>
        /// <param name="right">>Pixels to remove from the right</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset SliceHorizontal(this UiOffset pos, int left, int right)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x + left, min.y, max.x - right,max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="bottom">Pixels to remove from the bottom</param>
        /// <param name="top">Pixels to remove from the top</param>
        /// <returns>Sliced <see cref="UiOffset"/></returns>
        public static UiOffset SliceVertical(this UiOffset pos, int bottom, int top)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiOffset(min.x, min.y + bottom, max.x, max.y - top);
        }
    }
    public static class UiPositionExt
    {
        public static UiPosition SetX(this UiPosition pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(xMin, min.y, xMax, max.y);
        }
        
        public static UiPosition SetY(this UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, yMin, max.x, yMax);
        }
        
        public static UiPosition MoveX(this UiPosition pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + delta, min.y, max.x + delta, max.y);
        }
        
        public static UiPosition MoveXPadded(this UiPosition pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.x - min.x + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiPosition(min.x + spacing, min.y, max.x + spacing, max.y);
        }
        
        public static UiPosition MoveY(this UiPosition pos, float delta)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y + delta, max.x, max.y + delta);
        }
        
        public static UiPosition MoveYPadded(this UiPosition pos, float padding)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            float spacing = (max.y - min.y + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
            return new UiPosition(min.x, min.y + spacing, max.x, max.y + spacing);
        }
        
        public static UiPosition Expand(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x - amount, min.y - amount, max.x + amount, max.y + amount);
        }
        
        public static UiPosition ExpandHorizontal(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x - amount, min.y, max.x + amount, max.y);
        }
        
        public static UiPosition ExpandVertical(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y - amount, max.x, max.y + amount);
        }
        
        public static UiPosition Shrink(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + amount, min.y + amount, max.x - amount, max.y - amount);
        }
        
        public static UiPosition ShrinkHorizontal(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + amount, min.y, max.x - amount, max.y);
        }
        
        public static UiPosition ShrinkVertical(this UiPosition pos, float amount)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y + amount, max.x, max.y - amount);
        }
        
        /// <summary>
        /// Returns a slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition Slice(this UiPosition pos, float xMin, float yMin, float xMax, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            Vector2 distance = max - min;
            return new UiPosition(min.x + distance.x * xMin, min.y + distance.y * yMin, min.x + distance.x * xMax, min.y + distance.y * yMax);
        }
        
        /// <summary>
        /// Returns a horizontal slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="xMin">% of the xMax - xMin distance added to xMin</param>
        /// <param name="xMax">>% of the xMax - xMin distance added to xMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceHorizontal(this UiPosition pos, float xMin, float xMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x + (max.x - min.x) * xMin, min.y, min.x + (max.x - min.x) * xMax, max.y);
        }
        
        /// <summary>
        /// Returns a vertical slice of the position
        /// </summary>
        /// <param name="pos">Position to slice</param>
        /// <param name="yMin">% of the yMax - yMin distance added to yMin</param>
        /// <param name="yMax">% of the yMax - yMin distance added to yMin</param>
        /// <returns>Sliced <see cref="UiPosition"/></returns>
        public static UiPosition SliceVertical(this UiPosition pos, float yMin, float yMax)
        {
            Vector2 min = pos.Min;
            Vector2 max = pos.Max;
            return new UiPosition(min.x, min.y + (max.y - min.y) * yMin, max.x, min.y + (max.y - min.y) * yMax);
        }
    }
}
