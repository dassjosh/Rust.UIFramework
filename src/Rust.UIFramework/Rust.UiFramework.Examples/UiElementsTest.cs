using Network;
using Newtonsoft.Json;
using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Color = UnityEngine.Color;
using Net = Network.Net;

//UiElementsTest created with PluginMerge v(1.0.4.0) by MJSU @ https://github.com/dassjosh/Plugin.Merge
namespace Oxide.Plugins
{
    [Info("UI Elements Test", "MJSU", "1.0.0")]
    [Description("Tests all UI elements")]
    public partial class UiElementsTest : RustPlugin
    {
        #region UiElementsTest.Plugin.cs
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
        #endregion
        
        #region UI
        private const string UiName = nameof(UiElementsTest) + "_Main";
        private const string UiModal = nameof(UiElementsTest) + "_Modal";
        
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
            builder.ItemIcon(body, 963906841, 2563674658ul, _grid);
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
        #endregion

    }

    //Framework Rust UI Framework v(1.3.0) by MJSU
    //UI Framework for Rust
    #region Merged Framework Rust UI Framework
    public partial class UiElementsTest
    {
        #region Plugin\UiFramework.Methods.cs
        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            UiBuilder.DestroyUi(player, name);
        }
        
        public void DestroyUi(List<Connection> connections, string name)
        {
            UiBuilder.DestroyUi(connections, name);
        }
        
        private void DestroyUiAll(string name)
        {
            UiBuilder.DestroyUi(name);
        }
        #endregion
        
        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
        #endregion
        public class UiConstants
        {
            public static class UiFonts
            {
                private const string DroidSansMono = "droidsansmono.ttf";
                private const string PermanentMarker = "permanentmarker.ttf";
                private const string RobotoCondensedBold = "robotocondensed-bold.ttf";
                private const string RobotoCondensedRegular = "robotocondensed-regular.ttf";
                
                private static readonly Hash<UiFont, string> Fonts = new Hash<UiFont, string>
                {
                    [UiFont.DroidSansMono] = DroidSansMono,
                    [UiFont.PermanentMarker] = PermanentMarker,
                    [UiFont.RobotoCondensedBold] = RobotoCondensedBold,
                    [UiFont.RobotoCondensedRegular] = RobotoCondensedRegular,
                };
                
                public static string GetUiFont(UiFont font)
                {
                    return Fonts[font];
                }
            }
            
            public static class UiLayers
            {
                private const string Overall = "Overall";
                private const string Overlay = "Overlay";
                private const string Hud = "Hud";
                private const string HudMenu = "Hud.Menu";
                private const string Under = "Under";
                
                private static readonly Hash<UiLayer, string> Layers = new Hash<UiLayer, string>
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
        }
        public partial class UiBuilder
        {
            public UiButton Checkbox(BaseUiComponent parent, bool isChecked, int textSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd)
            {
                return TextButton(parent, isChecked ? "<b>✓</b>" : string.Empty, textSize, textColor, backgroundColor, pos, cmd);
            }
            
            public UiPanel ProgressBar(BaseUiComponent parent, float percentage, UiColor barColor, UiColor backgroundColor, UiPosition pos)
            {
                UiPanel background = Panel(parent, backgroundColor, pos);
                Panel(parent, barColor, pos.SliceHorizontal(0, Mathf.Clamp01(percentage)));
                return background;
            }
            
            public void SimpleNumberPicker(BaseUiComponent parent, int value, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.1f, bool readOnly = false)
            {
                UiPosition subtractSlice = pos.SliceHorizontal(0, buttonWidth);
                UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth, 1);
                
                TextButton(parent, "-", fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - 1).ToString()}");
                TextButton(parent, "+", fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + 1).ToString()}");
                
                UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(buttonWidth, 1 - buttonWidth), cmd, readOnly: readOnly);
                input.SetRequiresKeyboard();
            }
            
            public void IncrementalNumberPicker(BaseUiComponent parent, int value, int[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.3f, bool readOnly = false)
            {
                int incrementCount = increments.Length;
                float buttonSize = buttonWidth / incrementCount;
                for (int i = 0; i < incrementCount; i++)
                {
                    int increment = increments[i];
                    UiPosition subtractSlice = pos.SliceHorizontal(i * buttonSize, (i + 1) * buttonSize);
                    UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);
                    
                    string incrementDisplay = increment.ToString();
                    TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - increment).ToString()}");
                    TextButton(parent, incrementDisplay, fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + increment).ToString()}");
                }
                
                UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(0.3f, 0.7f), cmd, readOnly: readOnly);
                input.SetRequiresKeyboard();
            }
            
            public void IncrementalNumberPicker(BaseUiComponent parent, float value, float[] increments, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiPosition pos, string cmd, float buttonWidth = 0.3f, bool readOnly = false, string incrementFormat = "0.##")
            {
                int incrementCount = increments.Length;
                float buttonSize = buttonWidth / incrementCount;
                for (int i = 0; i < incrementCount; i++)
                {
                    float increment = increments[i];
                    UiPosition subtractSlice = pos.SliceHorizontal(i * buttonSize, (i + 1) * buttonSize);
                    UiPosition addSlice = pos.SliceHorizontal(1 - buttonWidth + i * buttonSize, 1 - buttonWidth + (i + 1) * buttonSize);
                    
                    string incrementDisplay = increment.ToString(incrementFormat);
                    TextButton(parent, string.Concat("-", incrementDisplay), fontSize, textColor, buttonColor, subtractSlice, $"{cmd} {(value - increment).ToString()}");
                    TextButton(parent, incrementDisplay, fontSize, textColor, buttonColor, addSlice, $"{cmd} {(value + increment).ToString()}");
                }
                
                UiInput input = Input(parent, value.ToString(), fontSize, textColor, backgroundColor, pos.SliceHorizontal(0.3f, 0.7f), cmd, readOnly: readOnly);
                input.SetRequiresKeyboard();
            }
            
            public void Paginator(BaseUiComponent parent, int currentPage, int maxPage, int fontSize, UiColor textColor, UiColor buttonColor, UiColor activePageColor, GridPosition grid, string cmd)
            {
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
                
                TextButton(parent, "<<<", fontSize, textColor, buttonColor, grid, $"{cmd} 0");
                grid.MoveCols(1);
                TextButton(parent, "<", fontSize, textColor, buttonColor, grid, $"{cmd} {Math.Max(0, currentPage - 1).ToString()}");
                grid.MoveCols(1);
                
                for (int i = startPage; i <= endPage; i++)
                {
                    TextButton(parent, (i + 1).ToString(), fontSize, textColor, i == currentPage ? activePageColor : buttonColor, grid, $"{cmd} {i.ToString()}");
                    grid.MoveCols(1);
                }
                
                TextButton(parent, ">", fontSize, textColor, buttonColor, grid, $"{cmd} {Math.Min(maxPage, currentPage + 1).ToString()}");
                grid.MoveCols(1);
                TextButton(parent, ">>>", fontSize, textColor, buttonColor, grid, $"{cmd} {maxPage.ToString()}");
            }
            
            public static UiBuilder CreateModal(UiOffset offset, UiColor modalColor, string name, UiLayer layer = UiLayer.Overlay)
            {
                UiBuilder builder = new UiBuilder();
                UiPanel backgroundBlur = UiPanel.Create(UiPosition.FullPosition, null, new UiColor(0, 0, 0, 0.5f));
                backgroundBlur.AddMaterial(UiConstants.Materials.InGameBlur);
                builder.SetRoot(backgroundBlur, name, UiConstants.UiLayers.GetLayer(layer));
                UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, offset, modalColor);
                builder.AddComponent(modal, backgroundBlur);
                builder.OverrideRoot(modal);
                return builder;
            }
        }
        public partial class UiBuilder : IDisposable
        {
            public BaseUiComponent Root;
            
            private bool _needsMouse;
            private bool _needsKeyboard;
            private bool _disposed;
            
            private string _baseName;
            private string _font;
            private string _cachedJson;
            
            private List<BaseUiComponent> _components;
            //private Hash<string, BaseUiComponent> _componentLookup;
            
            private static string _globalFont;
            
            #region Constructor
            static UiBuilder()
            {
                SetGlobalFont(UiFont.RobotoCondensedRegular);
            }
            
            public UiBuilder(UiPosition pos, string name, string parent) : this(pos, null, name, parent) { }
            
            public UiBuilder(UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) : this(pos, null, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(UiPosition pos, UiOffset? offset, string name, UiLayer parent = UiLayer.Overlay) : this(pos, offset, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(UiPosition pos, UiOffset? offset, string name, string parent) : this(UiSection.Create(pos, offset), name, parent) { }
            
            public UiBuilder(UiColor color, UiPosition pos, string name, string parent) : this(color, pos, null, name, parent) { }
            
            public UiBuilder(UiColor color, UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, null, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(UiColor color, UiPosition pos, UiOffset? offset, string name, UiLayer parent = UiLayer.Overlay) : this(color, pos, offset, name, UiConstants.UiLayers.GetLayer(parent)) { }
            
            public UiBuilder(UiColor color, UiPosition pos, UiOffset? offset, string name, string parent) : this(UiPanel.Create(pos, offset, color), name, parent) { }
            
            public UiBuilder(BaseUiComponent root, string name, string parent) : this()
            {
                SetRoot(root, name, parent);
            }
            
            public UiBuilder()
            {
                _components = UiFrameworkPool.GetList<BaseUiComponent>();
                //_componentLookup = UiFrameworkPool.GetHash<string, BaseUiComponent>();
                _font = _globalFont;
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
                _baseName = name + "_";
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
            
            public void SetCurrentFont(UiFont font)
            {
                _font = UiConstants.UiFonts.GetUiFont(font);
            }
            
            public static void SetGlobalFont(UiFont font)
            {
                _globalFont = UiConstants.UiFonts.GetUiFont(font);
            }
            
            // public T GetUi<T>(string name) where T : BaseUiComponent
            // {
                //     return (T)_componentLookup[name];
            // }
            #endregion
            
            #region Decontructor
            ~UiBuilder()
            {
                DisposeInternal();
            }
            
            public void Dispose()
            {
                DisposeInternal();
                //Need this because there is a global GC class that causes issues
                // ReSharper disable once RedundantNameQualifier
                System.GC.SuppressFinalize(this);
            }
            
            private void DisposeInternal()
            {
                if (_disposed)
                {
                    return;
                }
                
                _disposed = true;
                
                for (int index = 0; index < _components.Count; index++)
                {
                    _components[index].Dispose();
                }
                
                UiFrameworkPool.FreeList(ref _components);
                //UiFrameworkPool.FreeHash(ref _componentLookup);
                Root = null;
            }
            #endregion
            
            #region Add UI
            public void AddComponent(BaseUiComponent component, BaseUiComponent parent)
            {
                component.Parent = parent.Name;
                component.Name = GetComponentName();
                //_componentLookup[component.Name] = component;
                _components.Add(component);
            }
            
            public string GetComponentName()
            {
                return string.Concat(_baseName, _components.Count.ToString());
            }
            
            public UiSection Section(BaseUiComponent parent, UiPosition pos)
            {
                UiSection section = UiSection.Create(pos, null);
                AddComponent(section, parent);
                return section;
            }
            
            public UiPanel Panel(BaseUiComponent parent, UiColor color, UiPosition pos, UiOffset? offset = null)
            {
                UiPanel panel = UiPanel.Create(pos, offset, color);
                AddComponent(panel, parent);
                return panel;
            }
            
            public UiButton EmptyCommandButton(BaseUiComponent parent, UiColor color, UiPosition pos, string cmd)
            {
                UiButton button = UiButton.CreateCommand(pos, null, color, cmd);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton EmptyCloseButton(BaseUiComponent parent, UiColor color, UiPosition pos, string close)
            {
                UiButton button = UiButton.CreateClose(pos, null, color, close);
                AddComponent(button, parent);
                return button;
            }
            
            public UiButton TextButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                Label(button, text, textSize, textColor, UiPosition.FullPosition, align);
                return button;
            }
            
            public UiButton ImageButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                Image(button, png, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton WebImageButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                WebImage(button, url, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                ItemIcon(button, itemId, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton ItemIconButton(BaseUiComponent parent, UiColor buttonColor, int itemId, ulong skinId, UiPosition pos, string cmd)
            {
                UiButton button = EmptyCommandButton(parent, buttonColor, pos, cmd);
                ItemIcon(button, itemId, skinId, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton TextCloseButton(BaseUiComponent parent, string text, int textSize, UiColor textColor, UiColor buttonColor, UiPosition pos, string close, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                Label(button, text, textSize, textColor, UiPosition.FullPosition, align);
                return button;
            }
            
            public UiButton ImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string png, UiPosition pos, string close)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                Image(button, png, UiPosition.FullPosition);
                return button;
            }
            
            public UiButton WebImageCloseButton(BaseUiComponent parent, UiColor buttonColor, string url, UiPosition pos, string close)
            {
                UiButton button = EmptyCloseButton(parent, buttonColor, pos, close);
                WebImage(button, url, UiPosition.FullPosition);
                return button;
            }
            
            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos, UiColor color)
            {
                uint _;
                if (!uint.TryParse(png, out _))
                {
                    throw new UiFrameworkException($"Image PNG '{png}' is not a valid uint. If trying to use a url please use WebImage instead");
                }
                
                UiImage image = UiImage.Create(pos, null, color, png);
                AddComponent(image, parent);
                return image;
            }
            
            public UiImage Image(BaseUiComponent parent, string png, UiPosition pos)
            {
                return Image(parent, png, pos, UiColors.StandardColors.White);
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos, UiColor color)
            {
                UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, UiPosition pos)
            {
                return ItemIcon(parent, itemId, pos, UiColors.StandardColors.White);
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos, UiColor color)
            {
                UiItemIcon image = UiItemIcon.Create(pos, null, color, itemId, skinId);
                AddComponent(image, parent);
                return image;
            }
            
            public UiItemIcon ItemIcon(BaseUiComponent parent, int itemId, ulong skinId, UiPosition pos)
            {
                return ItemIcon(parent, itemId, skinId, pos, UiColors.StandardColors.White);
            }
            
            public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos)
            {
                return WebImage(parent, url, pos, UiColors.StandardColors.White);
            }
            
            public UiRawImage WebImage(BaseUiComponent parent, string url, UiPosition pos, UiColor color)
            {
                if (!url.StartsWith("http"))
                {
                    throw new UiFrameworkException($"WebImage Url '{url}' is not a valid url. If trying to use a png id please use Image instead");
                }
                
                UiRawImage image = UiRawImage.CreateUrl(pos, null, color, url);
                AddComponent(image, parent);
                return image;
            }
            
            public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos)
            {
                return TextureImage(parent, texture, pos, UiColors.StandardColors.White);
            }
            
            public UiRawImage TextureImage(BaseUiComponent parent, string texture, UiPosition pos, UiColor color)
            {
                UiRawImage image = UiRawImage.CreateTexture(pos, null, color, texture);
                AddComponent(image, parent);
                return image;
            }
            
            public UiLabel Label(BaseUiComponent parent, string text, int size, UiColor textColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiLabel label = UiLabel.Create(pos, null, textColor, text, size, _font, align);
                AddComponent(label, parent);
                return label;
            }
            
            public UiLabel LabelBackground(BaseUiComponent parent, string text, int size, UiColor textColor, UiColor backgroundColor, UiPosition pos, TextAnchor align = TextAnchor.MiddleCenter)
            {
                UiPanel panel = Panel(parent, backgroundColor, pos);
                UiLabel label = UiLabel.Create(UiPosition.FullPosition, null, textColor, text, size, _font, align);
                AddComponent(label, panel);
                return label;
            }
            
            public UiLabel Countdown(UiLabel label, int startTime, int endTime, int step, string command)
            {
                label.AddCountdown(startTime, endTime, step, command);
                return label;
            }
            
            public T TextOutline<T>(T outline, UiColor color) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color);
                return outline;
            }
            
            public T TextOutline<T>(T outline, UiColor color, Vector2 distance) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color, distance);
                return outline;
            }
            
            public T TextOutline<T>(T outline, UiColor color, Vector2 distance, bool useGraphicAlpha) where T : BaseUiTextOutline
            {
                outline.AddTextOutline(color, distance, useGraphicAlpha);
                return outline;
            }
            
            public UiInput Input(BaseUiComponent parent, string text, int fontSize, UiColor textColor, UiColor backgroundColor, UiPosition pos, string cmd, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
            {
                parent = Panel(parent, backgroundColor, pos);
                UiInput input = UiInput.Create(UiPosition.FullPosition, null, textColor, text, fontSize, cmd, _font, align, charsLimit, isPassword, readOnly, lineType);
                AddComponent(input, parent);
                return input;
            }
            
            public void Border(BaseUiComponent parent, UiColor color, int width = 1, BorderMode border = BorderMode.Top | BorderMode.Bottom | BorderMode.Left | BorderMode.Right)
            {
                //If width is 0 nothing is displayed so don't try to render
                if (width == 0)
                {
                    return;
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
                        Panel(parent, color, UiPosition.Top, new UiOffset(tbMin, 0, tbMax, width));
                    }
                    
                    if (left)
                    {
                        Panel(parent, color, UiPosition.Left, new UiOffset(-width, lrMin, 0, lrMax));
                    }
                    
                    if (bottom)
                    {
                        Panel(parent, color, UiPosition.Bottom, new UiOffset(tbMin, -width, tbMax, 0));
                    }
                    
                    if (right)
                    {
                        Panel(parent, color, UiPosition.Right, new UiOffset(0, lrMin, width, lrMax));
                    }
                }
                else
                {
                    if (top)
                    {
                        Panel(parent, color, UiPosition.Top, new UiOffset(0, width, 0, 0));
                    }
                    
                    if (left)
                    {
                        Panel(parent, color, UiPosition.Left, new UiOffset(0, 0, -width, 0));
                    }
                    
                    if (bottom)
                    {
                        Panel(parent, color, UiPosition.Bottom, new UiOffset(0, 0, 0, -width));
                    }
                    
                    if (right)
                    {
                        Panel(parent, color, UiPosition.Right, new UiOffset(width, 0, 0, 0));
                    }
                }
            }
            
            private bool HasBorderFlag(BorderMode mode, BorderMode flag)
            {
                return (mode & flag) != 0;
            }
            #endregion
            
            #region JSON
            public string ToJson()
            {
                return JsonCreator.CreateJson(_components, _needsMouse, _needsKeyboard);
            }
            
            public void CacheJson()
            {
                _cachedJson = ToJson();
            }
            #endregion
            
            #region Add UI
            public void AddUi(BasePlayer player)
            {
                AddUi(player.Connection);
            }
            
            public void AddUiCached(BasePlayer player)
            {
                AddUiCached(player.Connection);
            }
            
            public void AddUi(Connection connection)
            {
                AddUi(connection, ToJson());
            }
            
            public void AddUiCached(Connection connection)
            {
                AddUi(connection, _cachedJson);
            }
            
            public void AddUi(List<Connection> connections)
            {
                AddUi(connections, ToJson());
            }
            
            public void AddUiCached(List<Connection> connections)
            {
                AddUi(connections, _cachedJson);
            }
            
            public void AddUi()
            {
                AddUi(ToJson());
            }
            
            public void AddUiCached()
            {
                AddUi(_cachedJson);
            }
            
            public static void AddUi(BasePlayer player, string json)
            {
                AddUi(player.Connection, json);
            }
            
            public static void AddUi(string json)
            {
                AddUi(Net.sv.connections, json);
            }
            
            public static void AddUi(Connection connection, string json)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, UiConstants.RpcFunctions.AddUiFunc, json);
            }
            
            public static void AddUi(List<Connection> connections, string json)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, UiConstants.RpcFunctions.AddUiFunc, json);
            }
            #endregion
            
            #region Destroy Ui
            public void DestroyUi(BasePlayer player)
            {
                DestroyUi(player, Root.Name);
            }
            
            public void DestroyUi(Connection connection)
            {
                DestroyUi(connection, Root.Name);
            }
            
            public void DestroyUi(List<Connection> connections)
            {
                DestroyUi(connections, Root.Name);
            }
            
            public void DestroyUi()
            {
                DestroyUi(Root.Name);
            }
            
            public void DestroyUiImages(BasePlayer player)
            {
                DestroyUiImages(player.Connection);
            }
            
            public void DestroyUiImages()
            {
                DestroyUiImages(Net.sv.connections);
            }
            
            public void DestroyUiImages(Connection connection)
            {
                for (int index = _components.Count - 1; index >= 0; index--)
                {
                    BaseUiComponent component = _components[index];
                    if (component is UiRawImage)
                    {
                        DestroyUi(connection, component.Name);
                    }
                }
            }
            
            public void DestroyUiImages(List<Connection> connections)
            {
                for (int index = _components.Count - 1; index >= 0; index--)
                {
                    BaseUiComponent component = _components[index];
                    if (component is UiRawImage)
                    {
                        DestroyUi(connections, component.Name);
                    }
                }
            }
            
            public static void DestroyUi(BasePlayer player, string name)
            {
                DestroyUi(player.Connection, name);
            }
            
            public static void DestroyUi(string name)
            {
                DestroyUi(Net.sv.connections, name);
            }
            
            public static void DestroyUi(Connection connection, string name)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connection), null, UiConstants.RpcFunctions.DestroyUiFunc, name);
            }
            
            public static void DestroyUi(List<Connection> connections, string name)
            {
                CommunityEntity.ServerInstance.ClientRPCEx(new SendInfo(connections), null, UiConstants.RpcFunctions.DestroyUiFunc, name);
            }
            #endregion
        }
        [JsonConverter(typeof(UiColorConverter))]
        public struct UiColor : IEquatable<UiColor>
        {
            #region Fields
            public readonly uint Value;
            public readonly Color Color;
            #endregion
            
            #region Constructors
            public UiColor(Color color)
            {
                Color = color;
                Value = ((uint)(color.r * 255) << 24) + ((uint)(color.g * 255) << 16) + ((uint)(color.b * 255) << 8) + (uint)(color.a * 255);
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
            
            #region Modifiers
            public static UiColor WithAlpha(UiColor color, string hex)
            {
                return WithAlpha(color, int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
            }
            
            public static UiColor WithAlpha(UiColor color, int alpha)
            {
                return WithAlpha(color, alpha / 255f);
            }
            
            public static UiColor WithAlpha(UiColor color, float alpha)
            {
                return new UiColor(color.Color.WithAlpha(Mathf.Clamp01(alpha)));
            }
            
            public static UiColor Darken(UiColor color, float percentage)
            {
                percentage = Mathf.Clamp01(percentage);
                Color col = color.Color;
                float red = col.r * (1 - percentage);
                float green = col.g * (1 - percentage);
                float blue = col.b * (1 - percentage);
                
                return new UiColor(red, green, blue, col.a);
            }
            
            public static UiColor Lighten(UiColor color, float percentage)
            {
                percentage = Mathf.Clamp01(percentage);
                Color col = color.Color;
                float red = (1 - col.r) * percentage + col.r;
                float green = (1 - col.g) * percentage + col.g;
                float blue = (1 - col.b) * percentage + col.b;
                
                return new UiColor(red, green, blue, col.a);
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
            #endregion
            
            #region Formats
            public string ToHtmlColor()
            {
                return string.Concat("#", ColorUtility.ToHtmlStringRGBA(Color));
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
                #if UiBenchmarks
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
            
            public static class StandardColors
            {
                public static readonly UiColor White = Color.white;
                public static readonly UiColor Silver = "#C0C0C0";
                public static readonly UiColor Gray = Color.gray;
                public static readonly UiColor Black = Color.black;
                public static readonly UiColor Red = Color.red;
                public static readonly UiColor Maroon = "#800000";
                public static readonly UiColor Yellow = Color.yellow;
                public static readonly UiColor Olive = "#808000";
                public static readonly UiColor Lime = "#00FF00";
                public static readonly UiColor Green = Color.green;
                public static readonly UiColor Aqua = "#00FFFF";
                public static readonly UiColor Teal = "#008080";
                public static readonly UiColor Cyan = Color.cyan;
                public static readonly UiColor Blue = Color.blue;
                public static readonly UiColor Navy = "#000080";
                public static readonly UiColor Fuchsia = "#FF00FF";
                public static readonly UiColor Magenta = Color.magenta;
                public static readonly UiColor Purple = "#800080";
                public static readonly UiColor Clear = Color.clear;
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
                public static readonly UiColor Text = StandardColors.White;
                public static readonly UiColor Panel = "#2B2B2B";
                public static readonly UiColor PanelSecondary = "#3f3f3f";
                public static readonly UiColor PanelTertiary = "#525252";
                public static readonly UiColor ButtonPrimary = Rust.Red;
                public static readonly UiColor ButtonSecondary = "#666666";
            }
            
            #region UI Colors
            public static readonly UiColor Body = UiColor.WithAlpha(Form.Body, "F2");
            public static readonly UiColor BodyHeader = Form.Header;
            public static readonly UiColor Text = UiColor.WithAlpha(Form.Text, "80");
            public static readonly UiColor Panel = Form.Panel;
            public static readonly UiColor PanelSecondary = Form.PanelSecondary;
            public static readonly UiColor PanelTertiary = Form.PanelTertiary;
            public static readonly UiColor CloseButton = Form.ButtonPrimary;
            public static readonly UiColor ButtonPrimary = Form.ButtonPrimary;
            public static readonly UiColor ButtonSecondary = Form.ButtonSecondary;
            #endregion
        }
        public class BaseColorComponent : BaseComponent
        {
            public UiColor Color;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.Color.ColorName, Color);
            }
        }
        public abstract class BaseComponent : BasePoolable
        {
            public abstract void WriteComponent(JsonFrameworkWriter writer);
        }
        public class BaseImageComponent : FadeInComponent
        {
            public string Sprite;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Sprite = null;
                Material = null;
            }
        }
        public class BaseTextComponent : FadeInComponent
        {
            public int FontSize = JsonDefaults.BaseText.FontSize;
            public string Font;
            public TextAnchor Align;
            public string Text;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddTextField(writer, JsonDefaults.BaseText.TextName, Text);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
                JsonCreator.AddField(writer, JsonDefaults.BaseText.AlignName, Align);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                FontSize = JsonDefaults.BaseText.FontSize;
                Font = null;
                Align = TextAnchor.UpperLeft;
                Text = null;
            }
        }
        public class ButtonComponent : BaseImageComponent
        {
            private const string Type = "UnityEngine.UI.Button";
            
            public string Command;
            public string Close;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                JsonCreator.AddField(writer, JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Command = null;
                Close = null;
                Sprite = null;
                Material = null;
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
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
                JsonCreator.AddField(writer, JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                StartTime = 0;
                EndTime = 0;
                Step = 0;
                Command = null;
            }
        }
        public abstract class FadeInComponent : BaseColorComponent
        {
            public float FadeIn;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
                base.WriteComponent(writer);
            }
            
            protected override void EnterPool()
            {
                FadeIn = 0;
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
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Image.PngName, Png, null);
                JsonCreator.AddField(writer, JsonDefaults.Image.ImageType, ImageType);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                Png = null;
                ImageType = Image.Type.Simple;
            }
        }
        public class InputComponent : BaseTextComponent
        {
            private const string Type = "UnityEngine.UI.InputField";
            
            public int CharsLimit;
            public string Command;
            public bool IsPassword;
            public bool IsReadyOnly;
            public bool NeedsKeyboard = true;
            public InputField.LineType LineType;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
                JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
                JsonCreator.AddField(writer, JsonDefaults.Input.LineTypeName, LineType);
                
                if (IsPassword)
                {
                    JsonCreator.AddKeyField(writer, JsonDefaults.Input.PasswordName);
                }
                
                if (IsReadyOnly)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Input.ReadOnlyName, true);
                }
                
                if (NeedsKeyboard)
                {
                    JsonCreator.AddKeyField(writer, JsonDefaults.Input.InputNeedsKeyboardName);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                CharsLimit = 0;
                Command = null;
                NeedsKeyboard = true;
                IsPassword = false;
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
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddFieldRaw(writer, JsonDefaults.ItemIcon.ItemIdName, ItemId);
                JsonCreator.AddField(writer, JsonDefaults.ItemIcon.SkinIdName, SkinId, JsonDefaults.ItemIcon.DefaultSkinId);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                ItemId = 0;
                SkinId = JsonDefaults.ItemIcon.DefaultSkinId;
            }
        }
        public class OutlineComponent : BaseColorComponent
        {
            private const string Type = "UnityEngine.UI.Outline";
            
            public Vector2 Distance;
            public bool UseGraphicAlpha;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.DistanceValue);
                if (UseGraphicAlpha)
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Outline.UseGraphicAlphaName, JsonDefaults.Outline.UseGraphicAlphaValue);
                }
                
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
            
            protected override void EnterPool()
            {
                Distance = JsonDefaults.Outline.DistanceValue;
                UseGraphicAlpha = false;
            }
        }
        public class RawImageComponent : FadeInComponent
        {
            private const string Type = "UnityEngine.UI.RawImage";
            
            public string Url;
            public string Texture;
            public string Material;
            
            public override void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
                JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
                if (!string.IsNullOrEmpty(Url))
                {
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.UrlName, Url);
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
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
                base.WriteComponent(writer);
                writer.WriteEndObject();
            }
        }
        [Flags]
        public enum BorderMode : byte
        {
            Top = 1 << 0,
            Left = 1 << 1,
            Bottom = 1 << 2,
            Right = 1 << 3,
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
            RobotoCondensedRegular
        }
        public enum UiLayer : byte
        {
            Overall,
            Overlay,
            Hud,
            HudMenu,
            Under,
        }
        public class UiFrameworkException : Exception
        {
            public UiFrameworkException(string message) : base(message)
            {
                
            }
        }
        public static class EnumExt<T>
        {
            private static readonly Hash<T, string> CachedStrings = new Hash<T, string>();
            
            static EnumExt()
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
        public static class UiColorExt
        {
            private const string Format = "0.####";
            private const string RGBFormat = "{0} ";
            private const string AFormat = "{0}";
            
            private static readonly Hash<uint, string> ColorCache = new Hash<uint, string>();
            
            public static void WriteColor(StringBuilder writer, UiColor uiColor)
            {
                string color = ColorCache[uiColor.Value];
                if (color == null)
                {
                    color = GetColor(uiColor);
                    ColorCache[uiColor.Value] = color;
                }
                
                writer.Append(color);
            }
            
            public static string GetColor(Color color)
            {
                StringBuilder builder = UiFrameworkPool.GetStringBuilder();
                builder.AppendFormat(RGBFormat, color.r.ToString(Format));
                builder.AppendFormat(RGBFormat, color.g.ToString(Format));
                builder.AppendFormat(RGBFormat, color.b.ToString(Format));
                builder.AppendFormat(AFormat, color.a.ToString(Format));
                return UiFrameworkPool.ToStringAndFreeStringBuilder(ref builder);
            }
        }
        public static class VectorExt
        {
            private const string Format = "0.####";
            private const char Space = ' ';
            private const short PositionRounder = 10000;
            
            private static readonly Dictionary<ushort, string> PositionCache = new Dictionary<ushort, string>();
            private static readonly Dictionary<short, string> OffsetCache = new Dictionary<short, string>();
            
            static VectorExt()
            {
                for (ushort i = 0; i <= PositionRounder; i++)
                {
                    PositionCache[i] = (i / (float)PositionRounder).ToString(Format);
                }
            }
            
            public static void WritePos(StringBuilder sb, Vector2 pos)
            {
                sb.Append(PositionCache[(ushort)(pos.x * PositionRounder)]);
                sb.Append(Space);
                sb.Append(PositionCache[(ushort)(pos.y * PositionRounder)]);
            }
            
            public static void WriteVector2(StringBuilder sb, Vector2 pos)
            {
                string formattedPos;
                if (!PositionCache.TryGetValue((ushort)(pos.x * PositionRounder), out formattedPos))
                {
                    formattedPos = pos.x.ToString(Format);
                    PositionCache[(ushort)(pos.x * PositionRounder)] = formattedPos;
                }
                
                sb.Append(formattedPos);
                sb.Append(Space);
                
                if (!PositionCache.TryGetValue((ushort)(pos.y * PositionRounder), out formattedPos))
                {
                    formattedPos = pos.y.ToString(Format);
                    PositionCache[(ushort)(pos.y * PositionRounder)] = formattedPos;
                }
                
                sb.Append(formattedPos);
            }
            
            public static void WritePos(StringBuilder sb, Vector2Short pos)
            {
                string formattedPos;
                if (!OffsetCache.TryGetValue(pos.X, out formattedPos))
                {
                    formattedPos = pos.X.ToString();
                    OffsetCache[pos.X] = formattedPos;
                }
                
                sb.Append(formattedPos);
                sb.Append(Space);
                
                if (!OffsetCache.TryGetValue(pos.Y, out formattedPos))
                {
                    formattedPos = pos.Y.ToString();
                    OffsetCache[pos.Y] = formattedPos;
                }
                
                sb.Append(formattedPos);
            }
        }
        public static class JsonCreator
        {
            public static string CreateJson(List<BaseUiComponent> components, bool needsMouse, bool needsKeyboard)
            {
                JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
                
                writer.WriteStartArray();
                components[0].WriteRootComponent(writer, needsMouse, needsKeyboard);
                
                for (int index = 1; index < components.Count; index++)
                {
                    components[index].WriteComponent(writer);
                }
                
                writer.WriteEndArray();
                
                return writer.ToJson();
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, string value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, int value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddFieldRaw(JsonFrameworkWriter writer, string name, bool value)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(value);
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, string value, string defaultValue)
            {
                if (value != null && value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddPosition(JsonFrameworkWriter writer, string name, Vector2 value, Vector2 defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WritePosition(value);
                }
            }
            
            public static void AddOffset(JsonFrameworkWriter writer, string name, Vector2Short value, Vector2Short defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteOffset(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, TextAnchor value)
            {
                if (value != TextAnchor.UpperLeft)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(EnumExt<TextAnchor>.ToString(value));
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, InputField.LineType value)
            {
                if (value != InputField.LineType.SingleLine)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(EnumExt<InputField.LineType>.ToString(value));
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, Image.Type value)
            {
                if (value != Image.Type.Simple)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(EnumExt<Image.Type>.ToString(value));
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, int value, int defaultValue)
            {
                if (value != defaultValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, float value, float defaultValue)
            {
                if (Math.Abs(value - defaultValue) >= 0.0001)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(value);
                }
            }
            
            public static void AddField(JsonFrameworkWriter writer, string name, UiColor color)
            {
                if (color.Value != JsonDefaults.Color.ColorValue)
                {
                    writer.WritePropertyName(name);
                    writer.WriteValue(color);
                }
            }
            
            public static void AddKeyField(JsonFrameworkWriter writer, string name)
            {
                writer.WritePropertyName(name);
                writer.WriteValue(string.Empty);
            }
            
            public static void AddTextField(JsonFrameworkWriter writer, string name, string value)
            {
                writer.WritePropertyName(name);
                writer.WriteTextValue(value);
            }
            
            public static void AddMouse(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
                writer.WriteEndObject();
            }
            
            public static void AddKeyboard(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
                writer.WriteEndObject();
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
                public const string CommandName = "command";
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
            }
            
            public static class Outline
            {
                public const string DistanceName = "distance";
                public const string UseGraphicAlphaName = "useGraphicAlpha";
                public const string UseGraphicAlphaValue = "True";
                public static readonly Vector2 DistanceValue = new Vector2(1.0f, -1.0f);
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
                public const ulong DefaultSkinId = 0;
            }
            
            public static class Input
            {
                public const string CharacterLimitName = "characterLimit";
                public const int CharacterLimitValue = 0;
                public const string PasswordName = "password";
                public const string ReadOnlyName = "readOnly";
                public const string LineTypeName = "lineType";
                public const string InputNeedsKeyboardName = "needsKeyboard";
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
            
            private StringBuilder _writer;
            
            private void Init()
            {
                _writer = UiFrameworkPool.GetStringBuilder();
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
                    _writer.Append(CommaChar);
                }
                
                _propertyComma = false;
                _objectComma = false;
            }
            
            private void OnDepthDecrease()
            {
                _objectComma = true;
            }
            
            public void WriteStartArray()
            {
                OnDepthIncrease();
                _writer.Append(ArrayStartChar);
            }
            
            public void WriteEndArray()
            {
                _writer.Append(ArrayEndChar);
                OnDepthDecrease();
            }
            
            public void WriteStartObject()
            {
                OnDepthIncrease();
                _writer.Append(ObjectStartChar);
            }
            
            public void WriteEndObject()
            {
                _writer.Append(ObjectEndChar);
                OnDepthDecrease();
            }
            
            public void WritePropertyName(string name)
            {
                if (_propertyComma)
                {
                    _writer.Append(PropertyComma);
                }
                else
                {
                    _propertyComma = true;
                    _writer.Append(QuoteChar);
                }
                
                _writer.Append(name);
                _writer.Append(Separator);
            }
            
            public void WriteValue(bool value)
            {
                _writer.Append(value ? "true" : "false");
            }
            
            public void WriteValue(int value)
            {
                _writer.Append(value.ToString());
            }
            
            public void WriteValue(float value)
            {
                _writer.Append(value.ToString());
            }
            
            public void WriteValue(string value)
            {
                _writer.Append(QuoteChar);
                _writer.Append(value);
                _writer.Append(QuoteChar);
            }
            
            public void WriteTextValue(string value)
            {
                _writer.Append(QuoteChar);
                for (int i = 0; i < value.Length; i++)
                {
                    char character = value[i];
                    if (character == '\"')
                    {
                        _writer.Append("\\\"");
                    }
                    else
                    {
                        _writer.Append(character);
                    }
                }
                _writer.Append(QuoteChar);
            }
            
            public void WriteValue(Vector2 pos)
            {
                _writer.Append(QuoteChar);
                VectorExt.WriteVector2(_writer, pos);
                _writer.Append(QuoteChar);
            }
            
            public void WritePosition(Vector2 pos)
            {
                _writer.Append(QuoteChar);
                VectorExt.WritePos(_writer, pos);
                _writer.Append(QuoteChar);
            }
            
            public void WriteOffset(Vector2Short offset)
            {
                _writer.Append(QuoteChar);
                VectorExt.WritePos(_writer, offset);
                _writer.Append(QuoteChar);
            }
            
            public void WriteValue(UiColor color)
            {
                _writer.Append(QuoteChar);
                UiColorExt.WriteColor(_writer, color);
                _writer.Append(QuoteChar);
            }
            
            protected override void EnterPool()
            {
                UiFrameworkPool.FreeStringBuilder(ref _writer);
            }
            
            public override string ToString()
            {
                return _writer.ToString();
            }
            
            public string ToJson()
            {
                string json = _writer.ToString();
                Dispose();
                return json;
            }
        }
        public class MovableUiOffset
        {
            public int XMin;
            public int YMin;
            public int XMax;
            public int YMax;
            private readonly UiOffset _initialState;
            
            public MovableUiOffset(int x, int y, int width, int height)
            {
                XMin = x;
                YMin = y;
                XMax = x + width;
                YMax = y + height;
                _initialState = new UiOffset(XMin, YMin, XMax, YMax);
            }
            
            public void MoveX(int pixels)
            {
                XMin += pixels;
                XMax += pixels;
            }
            
            public void MoveY(int pixels)
            {
                YMin += pixels;
                YMax += pixels;
            }
            
            public void SetWidth(int width)
            {
                XMax = XMin + width;
            }
            
            public void SetHeight(int height)
            {
                YMax = YMin + height;
            }
            
            public UiOffset ToOffset()
            {
                return new UiOffset(XMin, YMin, XMax, YMax);
            }
            
            public void Reset()
            {
                XMin = _initialState.Min.X;
                YMin = _initialState.Min.Y;
                XMax = _initialState.Max.X;
                YMax = _initialState.Max.Y;
            }
            
            public static implicit operator UiOffset(MovableUiOffset offset) => offset.ToOffset();
        }
        public struct UiOffset
        {
            public static readonly UiOffset None = new UiOffset(0, 0, 0, 0);
            public static readonly UiOffset Scaled = new UiOffset(1280, 720);
            
            public readonly Vector2Short Min;
            public readonly Vector2Short Max;
            
            public UiOffset(int width, int height) : this(-width / 2, -height / 2, width / 2, height / 2) { }
            
            public UiOffset(int xMin, int yMin, int xMax, int yMax)
            {
                Min = new Vector2Short(xMin, yMin);
                Max = new Vector2Short(xMax, yMax);
            }
            
            public override string ToString()
            {
                return $"({Min.X:0.####}, {Min.Y:0.####}) ({Max.X:0.####}, {Max.Y:0.####})";
            }
        }
        public struct Vector2Short : IEquatable<Vector2Short>
        {
            public readonly short X;
            public readonly short Y;
            
            public Vector2Short(short x, short y)
            {
                X = x;
                Y = y;
            }
            
            public Vector2Short(int x, int y) : this((short)x, (short)y) { }
            
            public bool Equals(Vector2Short other)
            {
                return X == other.X && Y == other.Y;
            }
            
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Vector2Short && Equals((Vector2Short)obj);
            }
            
            public override int GetHashCode()
            {
                return (int)X | (Y << 16);
            }
            
            public static bool operator ==(Vector2Short lhs, Vector2Short rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
            
            public static bool operator !=(Vector2Short lhs, Vector2Short rhs) => !(lhs == rhs);
        }
        public abstract class BasePool<T> : IPool<T> where T : class, new()
        {
            private readonly Queue<T> _pool;
            private readonly int _maxSize;
            
            /// <summary>
            /// Base Pool Constructor
            /// </summary>
            /// <param name="maxSize">Max Size of the pool</param>
            protected BasePool(int maxSize)
            {
                _maxSize = maxSize;
                _pool = new Queue<T>(maxSize);
                for (int i = 0; i < maxSize; i++)
                {
                    _pool.Enqueue(new T());
                }
                
                UiFrameworkPool.AddPool(this);
            }
            
            /// <summary>
            /// Returns an element from the pool if it exists else it creates a new one
            /// </summary>
            /// <returns></returns>
            public T Get()
            {
                T item = _pool.Count != 0 ? _pool.Dequeue() : new T();
                OnGetItem(item);
                return item;
            }
            
            /// <summary>
            /// Frees an item back to the pool
            /// </summary>
            /// <param name="item">Item being freed</param>
            public void Free(ref T item)
            {
                if (item == null)
                {
                    return;
                }
                
                if (!OnFreeItem(ref item))
                {
                    return;
                }
                
                if (_pool.Count >= _maxSize)
                {
                    return;
                }
                
                _pool.Enqueue(item);
                
                item = null;
            }
            
            /// <summary>
            /// Called when an item is retrieved from the pool
            /// </summary>
            /// <param name="item">Item being retrieved</param>
            protected virtual void OnGetItem(T item)
            {
                
            }
            
            /// <summary>
            /// Returns if an item can be freed to the pool
            /// </summary>
            /// <param name="item">Item to be freed</param>
            /// <returns>True if can be freed; false otherwise</returns>
            protected virtual bool OnFreeItem(ref T item)
            {
                return true;
            }
            
            public virtual void Clear()
            {
                _pool.Clear();
            }
        }
        public class BasePoolable : IDisposable
        {
            internal bool Disposed;
            
            /// <summary>
            /// Returns if the object should be pooled.
            /// This field is set to true when leaving the pool.
            /// If the object instantiated using new() outside the pool it will be false
            /// </summary>
            private bool _shouldPool;
            
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
            
            /// <summary>
            /// Frees a pooled object that is part of a field on this object
            /// </summary>
            /// <param name="obj">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            protected void Free<T>(ref T obj) where T : BasePoolable, new()
            {
                if (obj != null && obj._shouldPool)
                {
                    UiFrameworkPool.Free(ref obj);
                }
            }
            
            /// <summary>
            /// Frees a pooled list that is part of a field on this object
            /// </summary>
            /// <param name="obj">List to be freed</param>
            /// <typeparam name="T">Type of the list</typeparam>
            protected void FreeList<T>(ref List<T> obj)
            {
                UiFrameworkPool.FreeList(ref obj);
            }
            
            /// <summary>
            /// Disposes the object when used in a using statement
            /// </summary>
            public void Dispose()
            {
                if (_shouldPool)
                {
                    UiFrameworkPool.Free(this);
                }
            }
        }
        public class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>>
        {
            public static IPool<Hash<TKey, TValue>> Instance;
            
            static HashPool()
            {
                Instance = new HashPool<TKey, TValue>();
            }
            
            private HashPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref Hash<TKey, TValue> item)
            {
                item.Clear();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
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
            void Free(ref T item);
        }
        public class ListPool<T> : BasePool<List<T>>
        {
            public static IPool<List<T>> Instance;
            
            static ListPool()
            {
                Instance = new ListPool<T>();
            }
            
            private ListPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref List<T> item)
            {
                item.Clear();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        public class MemoryStreamPool : BasePool<MemoryStream>
        {
            public static IPool<MemoryStream> Instance;
            
            static MemoryStreamPool()
            {
                Instance = new MemoryStreamPool();
            }
            
            private MemoryStreamPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref MemoryStream item)
            {
                item.Position = 0;
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        public class ObjectPool<T> : BasePool<T> where T : BasePoolable, new()
        {
            public static IPool<T> Instance;
            
            static ObjectPool()
            {
                Instance = new ObjectPool<T>();
            }
            
            private ObjectPool() : base(1024) { }
            
            protected override void OnGetItem(T item)
            {
                item.LeavePoolInternal();
            }
            
            protected override bool OnFreeItem(ref T item)
            {
                if (item.Disposed)
                {
                    return false;
                }
                
                item.EnterPoolInternal();
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
            }
        }
        public class StringBuilderPool : BasePool<StringBuilder>
        {
            public static IPool<StringBuilder> Instance;
            
            static StringBuilderPool()
            {
                Instance = new StringBuilderPool();
            }
            
            private StringBuilderPool() : base(256) { }
            
            ///<inheritdoc/>
            protected override bool OnFreeItem(ref StringBuilder item)
            {
                item.Length = 0;
                return true;
            }
            
            public override void Clear()
            {
                base.Clear();
                Instance = null;
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
                return ObjectPool<T>.Instance.Get();
            }
            
            /// <summary>
            /// Returns a <see cref="BasePoolable"/> back into the pool
            /// </summary>
            /// <param name="value">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            public static void Free<T>(ref T value) where T : BasePoolable, new()
            {
                ObjectPool<T>.Instance.Free(ref value);
            }
            
            /// <summary>
            /// Returns a <see cref="BasePoolable"/> back into the pool
            /// </summary>
            /// <param name="value">Object to free</param>
            /// <typeparam name="T">Type of object being freed</typeparam>
            internal static void Free<T>(T value) where T : BasePoolable, new()
            {
                ObjectPool<T>.Instance.Free(ref value);
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
            /// Returns a pooled <see cref="MemoryStream"/>
            /// </summary>
            /// <returns>Pooled <see cref="MemoryStream"/></returns>
            public static MemoryStream GetMemoryStream()
            {
                return MemoryStreamPool.Instance.Get();
            }
            
            /// <summary>
            /// Free's a pooled <see cref="List{T}"/>
            /// </summary>
            /// <param name="list">List to be freed</param>
            /// <typeparam name="T">Type of the list</typeparam>
            public static void FreeList<T>(ref List<T> list)
            {
                ListPool<T>.Instance.Free(ref list);
            }
            
            /// <summary>
            /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
            /// </summary>
            /// <param name="hash">Hash to be freed</param>
            /// <typeparam name="TKey">Type for key</typeparam>
            /// <typeparam name="TValue">Type for value</typeparam>
            public static void FreeHash<TKey, TValue>(ref Hash<TKey, TValue> hash)
            {
                HashPool<TKey, TValue>.Instance.Free(ref hash);
            }
            
            /// <summary>
            /// Frees a <see cref="StringBuilder"/> back to the pool
            /// </summary>
            /// <param name="sb">StringBuilder being freed</param>
            public static void FreeStringBuilder(ref StringBuilder sb)
            {
                StringBuilderPool.Instance.Free(ref sb);
            }
            
            /// <summary>
            /// Frees a <see cref="MemoryStream"/> back to the pool
            /// </summary>
            /// <param name="stream">MemoryStream being freed</param>
            public static void FreeMemoryStream(ref MemoryStream stream)
            {
                MemoryStreamPool.Instance.Free(ref stream);
            }
            
            /// <summary>
            /// Frees a <see cref="StringBuilder"/> back to the pool returning the <see cref="string"/>
            /// </summary>
            /// <param name="sb"><see cref="StringBuilder"/> being freed</param>
            public static string ToStringAndFreeStringBuilder(ref StringBuilder sb)
            {
                string result = sb.ToString();
                StringBuilderPool.Instance.Free(ref sb);
                return result;
            }
            
            public static void AddPool<TType>(BasePool<TType> pool) where TType : class, new()
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
        public class GridPosition : MovablePosition
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
                    MoveRows(-1);
                }
                
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveCols(float cols)
            {
                XMin += cols / NumCols;
                XMax += cols / NumCols;
                
                if (XMax > 1)
                {
                    XMin -= 1;
                    XMax -= 1;
                    MoveRows(-1);
                }
                
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveRows(int rows)
            {
                YMin += rows / NumRows;
                YMax += rows / NumRows;
                
                #if UiDebug
                ValidatePositions();
                #endif
            }
        }
        public class GridPositionBuilder
        {
            private float _xMin;
            private float _yMin;
            private float _xMax;
            private float _yMax;
            
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
                _numCols = numCols;
                _numRows = numRows;
            }
            
            public GridPositionBuilder SetRowHeight(int height)
            {
                _rowHeight = height;
                return this;
            }
            
            public GridPositionBuilder SetRowOffset(int offset)
            {
                _rowOffset = offset;
                return this;
            }
            
            public GridPositionBuilder SetColWidth(int width)
            {
                _colWidth = width;
                return this;
            }
            
            public GridPositionBuilder SetColOffset(int offset)
            {
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
                if (_rowHeight != 0)
                {
                    float size = _rowHeight / _numCols;
                    _xMax += size;
                }
                
                if (_rowOffset != 0)
                {
                    float size = _rowOffset / _numCols;
                    _xMin += size;
                    _xMax += size;
                }
                
                if (_colWidth != 0)
                {
                    float size = _colWidth / _numRows;
                    _yMax += size;
                }
                
                if (_colOffset != 0)
                {
                    float size = _colOffset / _numRows;
                    _yMin += size;
                    _yMax += size;
                }
                
                _xMin += _xPad;
                _xMax -= _xPad;
                float yMin = _yMin; //Need to save yMin before we overwrite it
                _yMin = 1 - _yMax + _yPad;
                _yMax = 1 - yMin - _yPad;
                
                #if UiDebug
                ValidatePositions();
                #endif
                
                return new GridPosition(_xMin, _yMin, _xMax, _yMax, _numCols, _numRows);
            }
        }
        public class MovablePosition
        {
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            private readonly UiPosition _initialState;
            
            public MovablePosition(float xMin, float yMin, float xMax, float yMax)
            {
                XMin = xMin;
                YMin = yMin;
                XMax = xMax;
                YMax = yMax;
                _initialState = new UiPosition(XMin, YMin, XMax, YMax);
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public UiPosition ToPosition()
            {
                return new UiPosition(XMin, YMin, XMax, YMax);
            }
            
            public void Set(float xMin, float yMin, float xMax, float yMax)
            {
                SetX(xMin, xMax);
                SetY(yMin, yMax);
            }
            
            public void SetX(float xMin, float xMax)
            {
                XMin = xMin;
                XMax = xMax;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void SetY(float yMin, float yMax)
            {
                YMin = yMin;
                YMax = yMax;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveX(float delta)
            {
                XMin += delta;
                XMax += delta;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveXPadded(float padding)
            {
                float spacing = (XMax - XMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
                XMin += spacing;
                XMax += spacing;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveY(float delta)
            {
                YMin += delta;
                YMax += delta;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void MoveYPadded(float padding)
            {
                float spacing = (YMax - YMin + Math.Abs(padding)) * (padding < 0 ? -1 : 1);
                YMin += spacing;
                YMax += spacing;
                #if UiDebug
                ValidatePositions();
                #endif
            }
            
            public void Expand(float amount)
            {
                ExpandHorizontal(amount);
                ExpandVertical(amount);
            }
            
            public void ExpandHorizontal(float amount)
            {
                XMin -= amount;
                XMax += amount;
            }
            
            public void ExpandVertical(float amount)
            {
                YMin -= amount;
                YMax += amount;
            }
            
            public void Shrink(float amount)
            {
                Expand(-amount);
            }
            
            public void ShrinkHorizontal(float amount)
            {
                ExpandHorizontal(-amount);
            }
            
            public void ShrinkVertical(float amount)
            {
                ExpandVertical(-amount);
            }
            
            public void Reset()
            {
                XMin = _initialState.Min.x;
                YMin = _initialState.Min.y;
                XMax = _initialState.Max.x;
                YMax = _initialState.Max.y;
            }
            
            #if UiDebug
            protected void ValidatePositions()
            {
                if (XMin < 0 || XMin > 1)
                {
                    PrintError($"[{GetType().Name}] XMin is out or range at: {XMin}");
                }
                
                if (XMax > 1 || XMax < 0)
                {
                    PrintError($"[{GetType().Name}] XMax is out or range at: {XMax}");
                }
                
                if (YMin < 0 || YMin > 1)
                {
                    PrintError($"[{GetType().Name}] YMin is out or range at: {YMin}");
                }
                
                if (YMax > 1 || YMax < 0)
                {
                    PrintError($"[{GetType().Name}] YMax is out or range at: {YMax}");
                }
            }
            
            private void PrintError(string format)
            {
                _ins.PrintError(format);
            }
            #endif
            
            public override string ToString()
            {
                return $"{XMin.ToString()} {YMin.ToString()} {XMax.ToString()} {YMax.ToString()}";
            }
            
            public static implicit operator UiPosition(MovablePosition pos) => pos.ToPosition();
        }
        public struct UiPosition
        {
            public static readonly UiPosition FullPosition = new UiPosition(0, 0, 1, 1);
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
            
            public readonly Vector2 Min;
            public readonly Vector2 Max;
            
            public UiPosition(float xMin, float yMin, float xMax, float yMax)
            {
                Min = new Vector2(Mathf.Clamp01(xMin), Mathf.Clamp01(yMin));
                Max = new Vector2(Mathf.Clamp01(xMax), Mathf.Clamp01(yMax));
            }
            
            public UiPosition Slice(float xMin, float yMin, float xMax, float yMax)
            {
                Vector2 distance = Max - Min;
                return new UiPosition(Min.x + distance.x * xMin, Min.y + distance.y * yMin, Min.x + distance.x * xMax, Min.y + distance.y * yMax);
            }
            
            public UiPosition SliceHorizontal(float xMin, float xMax)
            {
                return new UiPosition(Min.x + (Max.x - Min.x) * xMin, Min.y, Min.x + (Max.x - Min.x) * xMax, Max.y);
            }
            
            public UiPosition SliceVertical(float yMin, float yMax)
            {
                return new UiPosition(Min.x, Min.y + (Max.y - Min.y) * yMin, Max.x, Min.y + (Max.y - Min.y) * yMax);
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
            public UiOffset? Offset;
            
            protected static T CreateBase<T>(UiPosition pos, UiOffset? offset) where T : BaseUiComponent, new()
            {
                T component = UiFrameworkPool.Get<T>();
                component.Position = pos;
                component.Offset = offset;
                return component;
            }
            
            public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                
                if (needsMouse)
                {
                    JsonCreator.AddMouse(writer);
                }
                
                if (needsKeyboard)
                {
                    JsonCreator.AddKeyboard(writer);
                }
                
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            public void WriteComponent(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentName, Name);
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ParentName, Parent);
                JsonCreator.AddField(writer, JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
                
                writer.WritePropertyName("components");
                writer.WriteStartArray();
                WriteComponents(writer);
                writer.WriteEndArray();
                writer.WriteEndObject();
            }
            
            protected virtual void WriteComponents(JsonFrameworkWriter writer)
            {
                writer.WriteStartObject();
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
                JsonCreator.AddPosition(writer, JsonDefaults.Position.AnchorMinName, Position.Min, new Vector2(0, 0));
                JsonCreator.AddPosition(writer, JsonDefaults.Position.AnchorMaxName, Position.Max, new Vector2(1, 1));
                
                if (Offset.HasValue)
                {
                    UiOffset offset = Offset.Value;
                    JsonCreator.AddOffset(writer, JsonDefaults.Offset.OffsetMinName, offset.Min, new Vector2Short(0, 0));
                    JsonCreator.AddOffset(writer, JsonDefaults.Offset.OffsetMaxName, offset.Max, new Vector2Short(1, 1));
                }
                else
                {
                    //Fixes issue with UI going outside of bounds
                    JsonCreator.AddFieldRaw(writer, JsonDefaults.Offset.OffsetMaxName, JsonDefaults.Offset.DefaultOffsetMax);
                }
                
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
                Offset = null;
            }
        }
        public abstract class BaseUiTextOutline : BaseUiComponent
        {
            public OutlineComponent Outline;
            
            public void AddTextOutline(UiColor color)
            {
                Outline = UiFrameworkPool.Get<OutlineComponent>();
                Outline.Color = color;
            }
            
            public void AddTextOutline(UiColor color, Vector2 distance)
            {
                AddTextOutline(color);
                Outline.Distance = distance;
            }
            
            public void AddTextOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
            {
                AddTextOutline(color, distance);
                Outline.UseGraphicAlpha = useGraphicAlpha;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Outline?.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                if (Outline != null)
                {
                    UiFrameworkPool.Free(ref Outline);
                }
            }
        }
        public class UiButton : BaseUiComponent
        {
            public ButtonComponent Button;
            
            public static UiButton CreateCommand(UiPosition pos, UiOffset? offset, UiColor color, string command)
            {
                UiButton button = CreateBase<UiButton>(pos, offset);
                button.Button.Color = color;
                button.Button.Command = command;
                return button;
            }
            
            public static UiButton CreateClose(UiPosition pos, UiOffset? offset, UiColor color, string close)
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
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Button.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Button);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Button = UiFrameworkPool.Get<ButtonComponent>();
            }
        }
        public class UiImage : BaseUiComponent
        {
            public ImageComponent Image;
            
            public static UiImage Create(UiPosition pos, UiOffset? offset, UiColor color, string png)
            {
                UiImage image = CreateBase<UiImage>(pos, offset);
                image.Image.Color = color;
                image.Image.Png = png;
                return image;
            }
            
            public void SetImageType(Image.Type type)
            {
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
                UiFrameworkPool.Free(ref Image);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Image = UiFrameworkPool.Get<ImageComponent>();
            }
        }
        public class UiInput : BaseUiTextOutline
        {
            public InputComponent Input;
            
            public static UiInput Create(UiPosition pos, UiOffset? offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
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
                comp.IsPassword = isPassword;
                comp.IsReadyOnly = readOnly;
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
                Input.IsPassword = isPassword;
            }
            
            public void SetIsReadonly(bool isReadonly)
            {
                Input.IsReadyOnly = isReadonly;
            }
            
            public void SetLineType(InputField.LineType lineType)
            {
                Input.LineType = lineType;
            }
            
            /// <summary>
            /// Sets if the input should block keyboard input when focused.
            /// Default value is true
            /// </summary>
            /// <param name="needsKeyboard"></param>
            public void SetRequiresKeyboard(bool needsKeyboard = true)
            {
                Input.NeedsKeyboard = needsKeyboard;
            }
            
            protected override void WriteComponents(JsonFrameworkWriter writer)
            {
                Input.WriteComponent(writer);
                base.WriteComponents(writer);
            }
            
            protected override void EnterPool()
            {
                base.EnterPool();
                UiFrameworkPool.Free(ref Input);
                if (Outline != null)
                {
                    UiFrameworkPool.Free(ref Outline);
                }
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Input = UiFrameworkPool.Get<InputComponent>();
            }
        }
        public class UiItemIcon : BaseUiComponent
        {
            public ItemIconComponent Icon;
            
            public static UiItemIcon Create(UiPosition pos, UiOffset? offset, UiColor color, int itemId, ulong skinId = 0)
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
                UiFrameworkPool.Free(ref Icon);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Icon = UiFrameworkPool.Get<ItemIconComponent>();
            }
        }
        public class UiLabel : BaseUiTextOutline
        {
            public TextComponent Text;
            public CountdownComponent Countdown;
            
            public static UiLabel Create(UiPosition pos, UiOffset? offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
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
                UiFrameworkPool.Free(ref Text);
                
                if (Countdown != null)
                {
                    UiFrameworkPool.Free(ref Countdown);
                }
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Text = UiFrameworkPool.Get<TextComponent>();
            }
        }
        public class UiPanel : BaseUiComponent
        {
            public ImageComponent Image;
            
            public static UiPanel Create(UiPosition pos, UiOffset? offset, UiColor color)
            {
                UiPanel panel = CreateBase<UiPanel>(pos, offset);
                panel.Image.Color = color;
                return panel;
            }
            
            public void AddSprite(string sprite)
            {
                Image.Sprite = sprite;
            }
            
            public void AddMaterial(string material)
            {
                Image.Material = material;
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
                UiFrameworkPool.Free(ref Image);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                Image = UiFrameworkPool.Get<ImageComponent>();
            }
        }
        public class UiRawImage : BaseUiComponent
        {
            public RawImageComponent RawImage;
            
            public static UiRawImage CreateUrl(UiPosition pos, UiOffset? offset, UiColor color, string url)
            {
                UiRawImage image = CreateBase<UiRawImage>(pos, offset);
                image.RawImage.Color = color;
                image.RawImage.Url = url;
                return image;
            }
            
            public static UiRawImage CreateTexture(UiPosition pos, UiOffset? offset, UiColor color, string icon)
            {
                UiRawImage image = CreateBase<UiRawImage>(pos, offset);
                image.RawImage.Color = color;
                image.RawImage.Texture = icon;
                return image;
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
                UiFrameworkPool.Free(ref RawImage);
            }
            
            protected override void LeavePool()
            {
                base.LeavePool();
                RawImage = UiFrameworkPool.Get<RawImageComponent>();
            }
        }
        public class UiSection : BaseUiComponent
        {
            public static UiSection Create(UiPosition pos, UiOffset? offset)
            {
                UiSection panel = CreateBase<UiSection>(pos, offset);
                return panel;
            }
        }
    }
    #endregion

}
