using UnityEngine;

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

        private void CreateUi(BasePlayer player)
        {
            PlayerState state = new PlayerState();
            _playerStates[player.userID] = state;
            CreateUi(player, state);
        }

        private void CreateUi(BasePlayer player, PlayerState state)
        {
            //Initialize the builder
            UiBuilder builder = new UiBuilder(UiColors.Body, UiPosition.MiddleMiddle, _containerSize, UiName);

            //UI Grabs control of the mouse
            builder.NeedsMouse();

            //UI Grabs control of the keyboard
            //builder.NeedsKeyboard();

            //Create a panel for the title bar
            UiPanel titlePanel = builder.Panel(builder.Root, UiColors.BodyHeader, _titleBarPos);

            //Create the Title Bar Title and parent it to the titlePanel
            builder.Label(titlePanel, Title, TitleFontSize, UiColors.Text, _titleTextPos);

            //Create a Text Close Button that closes the UI on the client side without using a server command
            builder.TextCloseButton(titlePanel, "<b>X</b>", FontSize, UiColors.Text, UiColors.CloseButton, _closeButtonPos, UiName);

            //Create a Text Close Button that closes the UI using a server command
            builder.TextButton(titlePanel, "<b>X</b>", FontSize, UiColors.Text, UiColors.CloseButton, _closeCmdButtonPos, nameof(UiElementsCloseCommand));

            //Sections represents an invisible UI element used to parent UI elements to it
            UiSection body = builder.Section(builder.Root, _mainBodyPosition);

            //We reset the grid to it's initial position
            _grid.Reset();

            //We create a label with a background color
            builder.LabelBackground(body, "This is a label", FontSize, UiColors.Text, UiColors.PanelSecondary, _grid);

            //Move the grid to the next column
            _grid.MoveCols(1);

            //We create a panel
            builder.Panel(body, UiColors.PanelTertiary, _grid);
            _grid.MoveCols(1);

            //Creates a button that displays text
            builder.TextButton(body, "Text Button", FontSize, UiColors.Text, UiColors.ButtonPrimary, _grid, string.Empty);
            _grid.MoveCols(1);

            //Creates a button that displays an image from the web
            builder.WebImageButton(body, UiColors.StandardColors.White, "https://cdn.icon-icons.com/icons2/1381/PNG/512/rust_94773.png", _grid, string.Empty);
            _grid.MoveCols(1);

            //Creates a button that shows an item icon
            builder.ItemIconButton(body, UiColors.ButtonSecondary, 963906841, _grid, string.Empty);
            _grid.MoveCols(1);

            //Displays a web image
            builder.WebImage(body, "https://community.cloudflare.steamstatic.com/economy/image/6TMcQ7eX6E0EZl2byXi7vaVKyDk_zQLX05x6eLCFM9neAckxGDf7qU2e2gu64OnAeQ7835Ja5WrMfDY0jhyo8DEiv5daMKk6r70yQoJpxfiC/360fx360f", _grid);
            _grid.MoveCols(1);

            //Displays an item icon with the given skin ID
            builder.ItemIcon(body, 963906841, 2320435219, _grid);
            _grid.MoveCols(1);

            //Create a label and add a countdown timer to it.
            UiLabel countdownLabel = builder.LabelBackground(body, "Time Left: %TIME_LEFT%", FontSize, UiColors.StandardColors.White, UiColors.PanelSecondary, _grid);
            builder.Countdown(countdownLabel, 100, 0, 1, string.Empty);

            //Adds a text outline to the countdownLabel
            builder.TextOutline(countdownLabel, UiColors.Rust.Red, new Vector2(0.5f, -0.5f));
            _grid.MoveCols(1);

            //Creates an input field for the user to type in
            UiInput input1 = builder.Input(body, state.Input1Text, FontSize, UiColors.Text, UiColors.PanelSecondary, _grid, nameof(UiElementsUpdateInput1));

            //Blocks keyboard input when the input field is selected
            input1.SetRequiresKeyboard();
            _grid.MoveCols(1);

            //Adds a border around the body UI
            builder.Border(body, UiColors.Rust.Red);

            //Creates a checkbox
            builder.Checkbox(body, state.Checkbox, FontSize, UiColors.Text, UiColors.PanelSecondary, _grid, nameof(UiElementsToggleCheckbox));
            _grid.MoveCols(1);

            //Creates a number picker
            builder.SimpleNumberPicker(body, state.NumberPicker, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, _numberPickerPos, nameof(UiElementsNumberPicker), readOnly: true);
            _grid.MoveCols(1);

            //Creates a number picker where the user can type into as well
            builder.IncrementalNumberPicker(body, state.InputPicker, _numberPickerIncrements, FontSize, UiColors.Text, UiColors.Panel, UiColors.ButtonSecondary, _inputNumberPickerPos, nameof(UiElementsInputNumberPicker));
            _grid.MoveCols(1);

            //Creates a paginator
            UiSection paginatorSection = builder.Section(body, _paginator);
            builder.Paginator(paginatorSection, state.Page, 3, FontSize, UiColors.Text, UiColors.ButtonSecondary, UiColors.ButtonPrimary, _pagination, nameof(UiElementsPage));
            
            builder.TextureImage(body, "assets/icons/change_code.png", _grid, UiColors.StandardColors.White);
            _grid.MoveCols(1);
            
            //Creates a button to open a modal
            builder.TextButton(body, "Open Modal", FontSize, UiColors.Text, UiColors.ButtonPrimary, _grid, nameof(UiElementsOpenModal));
            _grid.MoveCols(1);

            builder.DestroyUi(player);
            builder.AddUi(player);

            LogToFile("Main", string.Empty, this);
            LogToFile("Main", builder.ToJson(), this);
        }

        private void CreateModalUi(BasePlayer player)
        {
            UiBuilder builder = UiBuilder.CreateModal(new UiOffset(400, 300), UiColor.WithAlpha(UiColors.Panel, 0.5f), UiModal);
            builder.TextButton(builder.Root, "<b>X</b>", 14, UiColors.Text, UiColors.StandardColors.Clear, new UiPosition(.9f, .9f, 1f, 1f), nameof(UiElementsCloseModal));
            
            builder.Border(builder.Root, UiColors.Rust.Red, 2);
            
            builder.DestroyUi(player);
            builder.AddUi(player);
            
            LogToFile("Modal", string.Empty, this);
            LogToFile("Modal", builder.ToJson(), this);
        }

        private static readonly GridPosition _skin = new GridPositionBuilder(2, 1).SetPadding(0.025f).Build();
        
        private void CreateSkinTest(BasePlayer player)
        {
            UiBuilder builder = new UiBuilder(UiColors.Body, UiPosition.MiddleMiddle, new UiOffset(400, 300), UiSkin);
            builder.NeedsMouse();
            
            _skin.Reset();

            builder.ItemIcon(builder.Root, 963906841, 2084257363, _skin);
            _skin.MoveCols(1);
            
            builder.ItemIcon(builder.Root, 963906841, 2320435219ul, _skin);
            _skin.MoveCols(1);
            
            builder.TextCloseButton(builder.Root, "<b>X</b>", 14, UiColors.Text, UiColors.StandardColors.Clear, new UiPosition(.9f, .9f, 1f, 1f), UiSkin);

            builder.DestroyUi(player);
            builder.AddUi(player);
            
            LogToFile("Skin", string.Empty, this);
            LogToFile("Skin", builder.ToJson(), this);
        }
        #endregion

        #region UI Commands
        [ConsoleCommand(nameof(UiElementsCloseCommand))]
        private void UiElementsCloseCommand(ConsoleSystem.Arg arg)
        {
            BasePlayer player = arg.Player();
            if (!player)
            {
                return;
            }

            UiBuilder.DestroyUi(player, UiName);
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

