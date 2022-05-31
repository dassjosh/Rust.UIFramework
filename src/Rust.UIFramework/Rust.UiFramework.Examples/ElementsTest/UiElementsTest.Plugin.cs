//Requires System.Buffers

using System.Text;
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

        private static UiElementsTest _ins;
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
        #endregion

        #region UI
        private const string UiName = nameof(UiElementsTest) + "_Main";
        private const string UiClose = nameof(UiElementsTest) + "_OutsideClose";
        private const string UiModal = nameof(UiElementsTest) + "_Modal";
        private const string UiSkin = nameof(UiElementsTest) + "_Skin";

        private readonly UiOffset _containerSize = new UiOffset(800, 600);
        private readonly UiPosition _titleBarPos = new UiPosition(0, 0.95f, 1, 1);
        private readonly UiPosition _titleTextPos = new UiPosition(.25f, 0, 0.75f, 1);
        private readonly UiPosition _closeButtonPos = new UiPosition(.8f, 0, .9f, 1);
        private readonly UiPosition _closeCmdButtonPos = new UiPosition(.9f, 0, 1f, 1);
        private readonly UiPosition _mainBodyPosition = new UiPosition(0, .1f, 1f, .89f);
        private readonly UiPosition _paginator = new UiPosition(0.025f, 0.025f, 0.975f, 0.09f);

        private readonly UiPosition _numberPickerPos = new UiPosition(0.025f, 0.4f, 0.4f, 0.5f);
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
            UiBuilder builder = UiBuilder.Create(UiColors.Body, UiPosition.MiddleMiddle, _containerSize, UiName);

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
            UiLabel countdownLabel = builder.LabelBackground(body, _grid, "Time Left: %TIME_LEFT%", FontSize, UiColor.White, UiColors.PanelSecondary);
            builder.Countdown(countdownLabel, 100, 0, 1, string.Empty);

            //Adds a text outline to the countdownLabel
            builder.TextOutline(countdownLabel, UiColors.Rust.Red, new Vector2(0.5f, -0.5f));
            _grid.MoveCols(1);

            //Creates an input field for the user to type in
            UiInput input1 = builder.InputBackground(body, _grid, state.Input1Text, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsUpdateInput1));

            //Blocks keyboard input when the input field is selected
            input1.SetRequiresKeyboard();
            _grid.MoveCols(1);

            //Adds a border around the body UI
            builder.Border(body, UiColors.Rust.Red);

            //Creates a checkbox
            builder.Checkbox(body, _grid, state.Checkbox, FontSize, UiColors.Text, UiColors.PanelSecondary, nameof(UiElementsToggleCheckbox));
            _grid.MoveCols(1);

            //Creates a number picker
            builder.SimpleNumberPicker(body, _numberPickerPos, state.NumberPicker, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, nameof(UiElementsNumberPicker));
            _grid.MoveCols(1);

            //Creates a number picker where the user can type into as well
            builder.IncrementalNumberPicker(body, _inputNumberPickerPos, state.InputPicker, _numberPickerIncrements, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, nameof(UiElementsInputNumberPicker));
            _grid.MoveCols(1);

            //Creates a paginator
            UiSection paginatorSection = builder.Section(body, _paginator);
            builder.Paginator(paginatorSection, _pagination, state.Page, 3, FontSize, UiColors.Text, UiColors.ButtonSecondary, UiColors.ButtonPrimary, nameof(UiElementsPage));
            
            builder.TextureImage(body, _grid, "assets/icons/change_code.png", UiColor.White);
            _grid.MoveCols(1);
            
            //Creates a button to open a modal
            builder.TextButton(body, _grid, "Open Modal", FontSize, UiColors.Text, UiColors.ButtonPrimary, nameof(UiElementsOpenModal));
            _grid.MoveCols(1);
            
            builder.DestroyAndAddUi(player);

            LogToFile("Main", string.Empty, this);
            LogToFile("Main", Encoding.UTF8.GetString(builder.GetBytes()), this);
        }

        private void CreateModalUi(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.CreateModal(UiModal, UiColor.WithAlpha(UiColors.Panel, 1f), new UiOffset(400, 300));
            UiPanel panel = builder.Root as UiPanel;
            panel.SetSpriteMaterialImage(UiConstants.Sprites.RoundedBackground2, null, Image.Type.Sliced);

            builder.TextButton(builder.Root, new UiPosition(.9f, .9f, 1f, 1f), "<b>X</b>", 14, UiColors.Text, UiColor.Clear, nameof(UiElementsCloseModal));
            
            //builder.Border(builder.Root, UiColors.Rust.Red, 2);
            
            builder.DestroyAndAddUi(player);
            
            LogToFile("Modal", string.Empty, this);
            LogToFile("Modal", Encoding.UTF8.GetString(builder.GetBytes()), this);
        }

        private static readonly GridPosition Skin = new GridPositionBuilder(2, 1).SetPadding(0.025f).Build();
        
        private void CreateSkinTest(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.Create(UiColors.Body, UiPosition.MiddleMiddle, new UiOffset(400, 300), UiSkin);
            builder.NeedsMouse();
            
            Skin.Reset();

            builder.ItemIcon(builder.Root, Skin, 963906841, 2084257363);
            Skin.MoveCols(1);
            
            builder.ItemIcon(builder.Root, Skin, 963906841, 2320435219ul);
            Skin.MoveCols(1);
            
            UiButton close = builder.CloseButton(builder.Root, _closeButtonPos, UiColors.CloseButton, UiSkin);
            builder.Label(close, UiPosition.HorizontalPaddedFull, "<b>X</b>", FontSize, UiColors.Text);

            builder.DestroyAndAddUi(player);
            
            LogToFile("Skin", string.Empty, this);
            LogToFile("Skin", Encoding.UTF8.GetString(builder.GetBytes()), this);
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
            UiBuilder.DestroyUi(player, UiName);
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
            state.Input1Text = arg.GetString(0);

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
            state.InputPicker = arg.GetInt(0);

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
        #endregion

        #region Classes
        private class PlayerState
        {
            public int Page;
            public string Input1Text = "Default";
            public bool Checkbox = true;
            public int NumberPicker;
            public int InputPicker;
        }
        #endregion
    }
}

