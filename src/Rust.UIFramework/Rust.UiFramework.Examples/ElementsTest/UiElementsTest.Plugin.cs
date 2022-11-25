//Requires System.Buffers

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Network;
using Oxide.Core;
using Oxide.Plugins.UiMergeFrameworkExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Plugins
{
    [Info("UI Elements Test", "MJSU", "1.0.0")]
    [Description("Tests all UI elements")]
    public partial class UiElementsTest : RustPlugin
    {
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
            _outsideClose.CacheJson();
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

        private readonly UiBuilder _outsideClose = UiBuilder.CreateOutsideClose(nameof(UiElementsCloseAll), UiClose);
        
        private void CreateUi(BasePlayer player)
        {
            _outsideClose.AddUiCached(player);
            
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
    }
}

