using System;
using System.Diagnostics;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine;

namespace Oxide.Plugins;

[Info("BorderRadiusTester", "MJSU", "1.0.0")]
[Description("A plugin to test border radius")]
public class BorderRadiusTester : RustPlugin, IUiFrameworkPlugin
{
    private readonly UiPlayerStore _playerStore = GetLibrary<UiPlayerStore>();
    private readonly UiCommands _commands = GetLibrary<UiCommands>();
    public UiPluginPool PluginPool { get; set; }

    private void Init()
    {
        _playerStore.RegisterStore(this, static playerId => new UiState(playerId));
        (_, _closeCommand) = _commands.RegisterCommand(this, CloseUi);
        (_, _subtract) = _commands.RegisterCommand(this, Subtract);
        (_, _add) = _commands.RegisterCommand(this, Add);
        (_, _unitChanged) = _commands.RegisterCommand<InputArg>(this, OnUnitChanged);
        (_, _toggleAntialiasing) = _commands.RegisterCommand(this, ToggleAntialiasing);
        (_, _subtractEdgeWidth) = _commands.RegisterCommand(this, SubtractEdgeWidth);
        (_, _addEdgeWidth) = _commands.RegisterCommand(this, AddEdgeWidth);
        (_, _edgeWidthChanged) = _commands.RegisterCommand<InputArg>(this, EdgeWidthChanged);
        (_, _subtractSize) = _commands.RegisterCommand(this, SubtractSize);
        (_, _addSize) = _commands.RegisterCommand(this, AddSize);
        (_, _sizeChanged) = _commands.RegisterCommand<InputArg>(this, SizeChanged);
    }

    private void Unload()
    {
        BaseBuilder.DestroyUi(UiName);
    }

    [ChatCommand("br")]
    private void BorderRadiusCommand(BasePlayer player, string command, string[] args)
    {
        CreateUi(player);
    }

    private void CreateUi(BasePlayer player)
    {
        UiState state = _playerStore.GetOrCreateStore<UiState>(this, player);
        CreateUi(player, state);
    }

    private const string UiName = $"{nameof(BorderRadiusTester)}UI";
    private ICommandBuilder _closeCommand;
    private ICommandBuilder _subtract;
    private ICommandBuilder _add;
    private ICommandBuilder<InputArg> _unitChanged;
    private ICommandBuilder _toggleAntialiasing;
    private ICommandBuilder _subtractEdgeWidth;
    private ICommandBuilder _addEdgeWidth;
    private ICommandBuilder<InputArg> _edgeWidthChanged;
    private ICommandBuilder _addSize;
    private ICommandBuilder _subtractSize;
    private ICommandBuilder<InputArg> _sizeChanged;

    private readonly UiOffset _padding = new UiPadding(2).ToOffset();

    private void CreateUi(BasePlayer player, UiState state)
    {
        UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(600, 400), UiColors.Form.Panel);
        builder.NeedsKeyboard().NeedsMouse();
        builder.SetCurrentFont(UiFont.RobotoMonoRegular);

        UiPanel header = builder.Panel(builder.Root, new UiPosition(0, 0.90f, 1, 1), default, UiColors.BodyHeader);
        builder.Label(header, UiPosition.Full, default, "Border Radius Tester", 14, UiColors.Text);
        (_, UiImage sprite) = builder.SpriteButton(header, new UiPosition(0.95f, 0, 1, 1), default, UiColors.CloseButton, UiSprites.Icons.Close, _closeCommand.Build(), spriteColor: UiColors.White);
        sprite.SetOffsetPadding(new UiPadding(4));

        UiSection body = builder.Section(builder.Root, new UiPosition(0, 0, 1, 0.90f));

        UiPanel panel = builder.Panel(body, new UiPosition(0, 0, .66f, 1f), _padding, UiColors.Rust.Red);

        Stopwatch sw = Stopwatch.StartNew();
        panel.AddBorderRadius(state.Size, state.BorderRadius, state.Antialiasing, state.EdgeWidth);
        panel.SetMaterial(UiMaterials.Content.Textures.Background);
        sw.Stop();

        Puts($"{panel.Png}");

        builder.Label(panel, new UiPosition(0, 0, 1, 0.1f), default, $"Generation Time: {TimeSpan.FromTicks(sw.ElapsedTicks).TotalMilliseconds}ms", 12, UiColors.Text);

        UiPanel controls = builder.Panel(body, new UiPosition(0.66f, 0.1f, 1f, 1f), _padding, UiColors.Rust.Panel);

        DirectionalLayoutComponent layout = builder.DirectionalLayout(controls, LayoutDirection.Vertical, 1f, TextAnchor.UpperCenter, childControlHeight: true, childControlWidth: true);

        CreateSize(builder, layout, state);
        CreateIncrementer(builder, layout, state);
        CreateSwitch(builder, layout, state);
        CreateEdgeWidth(builder, layout, state);

        //Puts(builder.GetJsonString());

        builder.AddUi(player);
    }

    private void CreateSize(UiBuilder builder, UiReference parent, UiState state)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 60);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, "Dimensions", 12, UiColors.Text, TextAnchor.MiddleLeft);
        builder.SpriteButton(panel, new UiPosition(0, 0, 0.2f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Subtract, _subtractSize.Build(), spriteColor: UiColors.Text);
        builder.Input(panel, new UiPosition(0.2f, 0, .8f, .67f), default, state.Size.ToString(), 14, UiColors.Text, _sizeChanged.Build(InputArg.Empty));
        builder.SpriteButton(panel, new UiPosition(0.8f, 0, 1f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Add, _addSize.Build(), spriteColor: UiColors.Text);
    }

    private void CreateIncrementer(UiBuilder builder, UiReference parent, UiState state)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 60);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, "Border Radius", 12, UiColors.Text, TextAnchor.MiddleLeft);
        builder.SpriteButton(panel, new UiPosition(0, 0, 0.2f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Subtract, _subtract.Build(), spriteColor: UiColors.Text);
        builder.Input(panel, new UiPosition(0.2f, 0, .8f, .67f), default, state.Unit.ToString(), 14, UiColors.Text, _unitChanged.Build(InputArg.Empty));
        builder.SpriteButton(panel, new UiPosition(0.8f, 0, 1f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Add, _add.Build(), spriteColor: UiColors.Text);
    }

    private void CreateSwitch(UiBuilder builder, UiReference parent, UiState state)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 60);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, "Antialiasing", 12, UiColors.Text, TextAnchor.MiddleLeft);
        UiButton button = builder.Button(panel, new UiPosition(0, 0, 1f, .67f), _padding, UiColors.Clear, _toggleAntialiasing.Build());
        builder.Icon(button, UiPosition.Full, default, state.Antialiasing ? Icons.ToggleOn : Icons.ToggleOff, state.Antialiasing ? UiColors.Rust.Green : UiColors.Rust.Red);
    }

    private void CreateEdgeWidth(UiBuilder builder, UiReference parent, UiState state)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 60);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, "Edge Width", 12, UiColors.Text, TextAnchor.MiddleLeft);
        builder.SpriteButton(panel, new UiPosition(0, 0, 0.2f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Subtract, _subtractEdgeWidth.Build(), spriteColor: UiColors.Text);
        builder.Input(panel, new UiPosition(0.2f, 0, .8f, .67f), default, state.EdgeWidth.ToString("0.0"), 14, UiColors.Text, _edgeWidthChanged.Build(InputArg.Empty));
        builder.SpriteButton(panel, new UiPosition(0.8f, 0, 1f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Add, _addEdgeWidth.Build(), spriteColor: UiColors.Text);
    }

    public class UiState : IPlayerStore
    {
        public ulong PlayerId { get; }
        public UiDimensions2D Size { get; set; } = new(256, 256);
        public UiUnit Unit { get; set; } = 10.Percent();
        public UiBorderRadius BorderRadius { get; set; } = new(10.Percent());
        public float EdgeWidth { get; set; } = 1f;
        public bool Antialiasing { get; set; } = true;

        public UiState(ulong playerId)
        {
            PlayerId = playerId;
        }
    }

    [UiCommand]
    private void CloseUi(ExecutionData data)
    {
        BasePlayer player = data.Player;
        _playerStore.RemoveStore(this, player);
        BaseBuilder.DestroyUi(player, UiName);
    }

    [UiCommand]
    private void Subtract(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Unit.Value > 1f)
        {
            state.Unit -= 1;
        }
        state.BorderRadius = new UiBorderRadius(state.Unit);
        CreateUi(player, state);
    }

    [UiCommand]
    private void Add(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Unit += 1;
        state.BorderRadius = new UiBorderRadius(state.Unit);
        CreateUi(player, state);
    }

    [UiCommand]
    private void OnUnitChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(UiUnit.TryParse(arg.Value, out UiUnit unit))
        {
            state.Unit = unit;
            state.BorderRadius = new UiBorderRadius(state.Unit);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private void ToggleAntialiasing(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Antialiasing = !state.Antialiasing;
        CreateUi(player, state);
    }

    [UiCommand]
    private void SubtractEdgeWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.EdgeWidth >= 0.1f)
        {
            state.EdgeWidth -= 0.1f;
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private void AddEdgeWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.EdgeWidth += 0.1f;
        CreateUi(player, state);
    }

    [UiCommand]
    private void EdgeWidthChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if (arg.TryGetValue(out float value))
        {
            state.EdgeWidth = value;
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private void SubtractSize(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Size.Height > 1f)
        {
            state.Size -= 1f;
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private void AddSize(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Size += 1f;
        CreateUi(player, state);
    }

    [UiCommand]
    private void SizeChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if(UiDimensions2D.TryParse(arg.Value, out UiDimensions2D size, "x"))
        {
            state.Size = size;
        }

        CreateUi(player, state);
    }
}