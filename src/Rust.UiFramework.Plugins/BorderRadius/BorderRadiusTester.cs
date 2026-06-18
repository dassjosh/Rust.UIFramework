using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Oxide.Core;
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
using Oxide.Ext.UiFramework.Types.Results;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Plugins;

[Info("BorderRadiusTester", "MJSU", "1.0.0")]
[Description("A plugin to test border radius")]
public class BorderRadiusTester : RustPlugin, IUiFrameworkPlugin
{
    private readonly UiPlayerStore _playerStore = GetLibrary<UiPlayerStore>();
    private readonly UiImageStorage _imageStorage = GetLibrary<UiImageStorage>();
    private readonly UiCommands _commands = GetLibrary<UiCommands>();
    public UiPluginPool PluginPool { get; set; }

    private readonly string _exportFolder = Path.Combine(Interface.Oxide.DataDirectory, nameof(BorderRadiusTester), "Images");

    private void Init()
    {
        _playerStore.RegisterStore(this, static playerId => new UiState(playerId));
        _radius = RegisterIncrementalCommands(SubtractRadius, AddRadius, OnRadiusChanged);
        _edgeWidth = RegisterIncrementalCommands(SubtractEdgeWidth, AddEdgeWidth, EdgeWidthChanged);
        _size = RegisterIncrementalCommands(SubtractSize, AddSize, SizeChanged);
        _borderWidth = RegisterIncrementalCommands(SubtractBorderWidth, AddBorderWidth, BorderWidthChanged);
        _dashLength = RegisterIncrementalCommands(SubtractDashLength, AddDashLength, DashLengthChanged);
        _gapLength = RegisterIncrementalCommands(SubtractDashGap, AddDashGap, DashGapChanged);

        (_, _closeCommand) = _commands.RegisterCommand(this, CloseUi);
        (_, _openExportUi) = _commands.RegisterCommand(this, OpenExportUi);
        (_, _closeExportUi) = _commands.RegisterCommand(this, CloseExportUi);
        (_, _exportImage) = _commands.RegisterCommand(this, ExportImage);
        (_, _setMode) = _commands.RegisterCommand<bool>(this, SetMode);
        (_, _setUrl) = _commands.RegisterCommand<InputArg>(this, SetUrl);
        (_, _toggleAntialiasing) = _commands.RegisterCommand(this, ToggleAntialiasing);
        (_, _toggleBorder) = _commands.RegisterCommand(this, ToggleBorder);
        (_, _toggleDashedBorder) = _commands.RegisterCommand(this, ToggleDashedBorder);
        (_, _fillColorChanged) = _commands.RegisterCommand<InputArg>(this, FillColorChanged);
        (_, _transparentColorChanged) = _commands.RegisterCommand<InputArg>(this, TransparentColorChanged);
        (_, _borderColorChanged) = _commands.RegisterCommand<InputArg>(this, BorderColorChanged);
        (_, _exportNameChanged) = _commands.RegisterCommand<InputArg>(this, ExportNameChanged);
    }

    private void Unload()
    {
        BaseBuilder.DestroyUi(UiName);
        BaseBuilder.DestroyUi(ExportUiName);
        BaseBuilder.DestroyUi(ExportedUiName);
    }

    [ChatCommand("br")]
    private void BorderRadiusCommand(BasePlayer player, string command, string[] args)
    {
        CreateUi(player).Forget();
    }

    private async UniTask CreateUi(BasePlayer player)
    {
        UiState state = _playerStore.GetOrCreateStore<UiState>(this, player);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    public IncrementalCommandData RegisterIncrementalCommands( Func<ExecutionData, UniTask> subtract, Func<ExecutionData, UniTask> add, Func<ExecutionData, InputArg, UniTask> changed)
    {
        return new IncrementalCommandData(
            _commands.RegisterCommand(this, subtract).Item2,
            _commands.RegisterCommand(this, add).Item2,
            _commands.RegisterCommand(this, changed).Item2
        );
    }

    public record IncrementalCommandData(ICommandBuilder Subtract, ICommandBuilder Add, ICommandBuilder<InputArg> Changed);

    private const string UiName = $"{nameof(BorderRadiusTester)}UI";
    private const string ExportUiName = $"{nameof(BorderRadiusTester)}ExportUI";
    private const string ExportedUiName = $"{nameof(BorderRadiusTester)}ExportedUI";
    private IncrementalCommandData _radius;
    private IncrementalCommandData _edgeWidth;
    private IncrementalCommandData _size;
    private IncrementalCommandData _borderWidth;
    private IncrementalCommandData _dashLength;
    private IncrementalCommandData _gapLength;

    private ICommandBuilder _closeCommand;
    private ICommandBuilder _openExportUi;
    private ICommandBuilder _closeExportUi;
    private ICommandBuilder _exportImage;
    private ICommandBuilder _closeExportedUi;
    private ICommandBuilder _toggleAntialiasing;
    private ICommandBuilder _toggleBorder;
    private ICommandBuilder _toggleDashedBorder;
    private ICommandBuilder<InputArg> _fillColorChanged;
    private ICommandBuilder<InputArg> _transparentColorChanged;
    private ICommandBuilder<InputArg> _borderColorChanged;
    private ICommandBuilder<InputArg> _exportNameChanged;
    private ICommandBuilder<bool> _setMode;
    private ICommandBuilder<InputArg> _setUrl;

    private readonly UiOffset _padding = new UiPadding(2).ToOffset();

    private void CreateUi(BasePlayer player, UiState state)
    {
        UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(700, 500), UiColors.Form.Panel);
        builder.NeedsKeyboard().NeedsMouse();
        builder.SetCurrentFont(UiFont.RobotoMonoRegular);

        UiPanel header = builder.Panel(builder.Root, new UiPosition(0, 0.95f, 1, 1), default, UiColors.BodyHeader);
        builder.Label(header, UiPosition.Full, default, "Border Radius Tester", 14, UiColors.Text);
        builder.SpriteButton(header, new UiPosition(0, 0, 0.05f, 1), _padding, UiColors.ButtonPrimary, UiSprites.Icons.Download, _openExportUi.Build());
        builder.SpriteButton(header, new UiPosition(0.95f, 0, 1, 1), _padding, UiColors.CloseButton, UiSprites.Icons.Close, _closeCommand.Build(), spriteColor: UiColors.White);

        UiSection body = builder.Section(builder.Root, new UiPosition(0, 0, 1, 0.95f));

        var panel = builder.Panel(body, new UiPosition(0, 0, 0.66f, 1f), new UiPadding(5).ToOffset(), UiColors.White);
        panel.SetPng(state.Image);
        panel.SetMaterial(UiMaterials.Content.Textures.Background);

        bool isImageMode = state.Builder.UseInputImage;

        UiPanel controls = builder.Panel(body, new UiPosition(0.66f, 0, 1f, 1f), _padding, UiColors.Rust.Panel);

        UiPanel modeControls = builder.Panel(controls, new UiPosition(0f, 0.9f, 1f, 1f), _padding, UiColors.Rust.Panel);
        UiPanel leftControls = builder.Panel(controls, new UiPosition(0f, 0f, 0.5f, 0.9f), _padding, UiColors.Rust.Panel);
        UiPanel rightControls = builder.Panel(controls, new UiPosition(0.5f, 0f, 1f, 0.9f), _padding, UiColors.Rust.Panel);

        DirectionalLayoutComponent modeLayout = builder.DirectionalLayout(modeControls, LayoutDirection.Horizontal, 1f, TextAnchor.MiddleCenter, childControlHeight: true, childControlWidth: true);
        DirectionalLayoutComponent leftLayout = builder.DirectionalLayout(leftControls, LayoutDirection.Vertical, 1f, TextAnchor.UpperCenter, childControlHeight: true, childControlWidth: true);
        DirectionalLayoutComponent rightLayout = builder.DirectionalLayout(rightControls, LayoutDirection.Vertical, 1f, TextAnchor.UpperCenter, childControlHeight: true, childControlWidth: true);

        CreateToggleMode(builder, modeLayout, isImageMode);

        if (isImageMode)
        {
            CreateTextInput(builder, leftLayout, _setUrl, "Image URL", state.Builder.InputImage);
        }

        if (!isImageMode)
        {
            CreateColor(builder, leftLayout, _fillColorChanged, "Fill Color", state.Builder.Fill);
        }

        CreateColor(builder, leftLayout, _transparentColorChanged, "Transparent Color", state.Builder.Transparent);

        if (!isImageMode)
        {
            CreateIncrementer(builder, leftLayout, _size, "Size", state.Builder.Size.ToString());
        }

        CreateIncrementer(builder, leftLayout, _radius, "Border Radius", state.Builder.Radius.TopLeft.ToString());

        CreateSwitch(builder, leftLayout, _toggleAntialiasing, "Anti Aliasing", state.Builder.AntiAlias);
        CreateIncrementer(builder, leftLayout, _edgeWidth, "Edge Width", state.Builder.EdgeWidth.ToString("0.00"));

        CreateSwitch(builder, rightLayout, _toggleBorder, "Border", state.Builder.EnableBorder);
        CreateIncrementer(builder, rightLayout, _borderWidth, "Border Width", state.Builder.BorderWidth.ToString("0.0"));
        CreateColor(builder, rightLayout, _borderColorChanged, "Border Color", state.Builder.BorderColor);

        CreateSwitch(builder, rightLayout, _toggleDashedBorder, "Dashed Border", state.Builder.EnableDashedBorder);
        CreateIncrementer(builder, rightLayout, _dashLength, "Dash Length", state.Builder.DashLength.ToString("0.0"));
        CreateIncrementer(builder, rightLayout, _gapLength, "Gap Length", state.Builder.GapLength.ToString("0.0"));

        //Puts(builder.GetJsonString());

        builder.AddUi(player);
    }

    private void CreateToggleMode(UiBuilder builder, UiReference parent, bool enableInputImage)
    {
        builder.TextButton(parent, "Fill Mode", 14, UiColors.Text, !enableInputImage ? UiColors.ButtonPrimary : UiColors.ButtonSecondary, _setMode.Build(false));
        builder.TextButton(parent, "Image Mode", 14, UiColors.Text, enableInputImage ? UiColors.ButtonPrimary : UiColors.ButtonSecondary, _setMode.Build(true));
    }

    private void CreateColor(UiBuilder builder, UiReference parent, ICommandBuilder<InputArg> command, string name, UiColor value)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 50);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, $"<b>{name}</b>", 12, UiColors.Text, TextAnchor.MiddleCenter);
        builder.Panel(panel, new UiPosition(0, 0, 0.2f, .67f), _padding, value);
        builder.Input(panel, new UiPosition(0.2f, 0, 1f, .67f), default, value.ToHtmlColor(), 14, UiColors.Text, command);
    }

    private void CreateIncrementer(UiBuilder builder, UiReference parent, IncrementalCommandData data, string name, string value)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 50);

        builder.Label(panel, new UiPosition(0, 0.67f, 1f, 1f), _padding, $"<b>{name}</b>", 12, UiColors.Text, TextAnchor.MiddleCenter);
        builder.SpriteButton(panel, new UiPosition(0, 0, 0.2f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Subtract, data.Subtract.Build(), spriteColor: UiColors.Text);
        builder.Input(panel, new UiPosition(0.2f, 0, .8f, .67f), default, value, 14, UiColors.Text, data.Changed.Build(InputArg.Empty));
        builder.SpriteButton(panel, new UiPosition(0.8f, 0, 1f, .67f), _padding, UiColors.ButtonSecondary, UiSprites.Icons.Add, data.Add.Build(), spriteColor: UiColors.Text);
    }

    private void CreateSwitch(UiBuilder builder, UiReference parent, ICommandBuilder command, string name, bool value)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 30);

        builder.Label(panel, new UiPosition(0, 0, 0.5f, 1f), _padding, $"<b>{name}</b>", 12, UiColors.Text, TextAnchor.MiddleCenter);
        UiButton button = builder.Button(panel, new UiPosition(0.5f, 0, 1f, 1f), _padding, UiColors.Clear, command.Build());
        builder.Icon(button, UiPosition.Full, default, value ? Icons.ToggleOn : Icons.ToggleOff, value ? UiColors.Rust.Green : UiColors.Rust.Red);
    }

    private void CreateTextInput(UiBuilder builder, UiReference parent, ICommandBuilder<InputArg> command, string name, string value)
    {
        UiPanel panel = builder.Panel(parent, UiColors.Form.PanelTertiary);
        builder.LayoutElement(panel, 120, 80);

        builder.Label(panel, new UiPosition(0, 0.8f, 1f, 1f), _padding, $"<b>{name}</b>", 12, UiColors.Text, TextAnchor.MiddleCenter);
        builder.Input(panel, new UiPosition(0, 0, 1f, .8f), default, value, 14, UiColors.Text, command, TextAnchor.MiddleLeft, lineType: InputField.LineType.MultiLineSubmit);
    }

    private void CreateExportUi(BasePlayer player, UiState state)
    {
        UiBuilder builder = UiBuilder.CreateModal(this, new UiReference(UiLayer.Overlay, ExportUiName), new UiOffset(250, 250), UiColors.Form.PanelTertiary);

        builder.Label(builder.Root, new UiPosition(0f, 0.8f, 1f, 1f), default, "Export Image File Name", 14, UiColors.Text);
        builder.Input(builder.Root, new UiPosition(0, 0.2f, 1, 0.8f), _padding, state.ExportName, 14, UiColors.Text, UiColors.Panel, _exportNameChanged.Build(InputArg.Empty));
        builder.TextButton(builder.Root, new UiPosition(0, 0, 1, 0.2f), _padding, "Export", 14, UiColors.Text, UiColors.ButtonPrimary, _exportImage.Build());

        builder.AddUi(player);
    }

    private void CreateExportedUi(BasePlayer player, string path)
    {
        Puts(path);
        UiBuilder builder = UiBuilder.CreateModal(this, new UiReference(UiLayer.Overlay, ExportUiName), new UiOffset(300, 300), UiColors.Form.PanelTertiary);

        builder.Label(builder.Root, new UiPosition(0f, 0.8f, 1f, 1f), default, "Image Exported To:", 14, UiColors.Text);
        builder.Input(builder.Root, new UiPosition(0, 0f, 1, 0.8f), _padding, path.Replace('\\', '/'), 14, UiColors.Text, UiColors.Panel, "", TextAnchor.MiddleLeft, lineType: InputField.LineType.MultiLineSubmit);

        builder.AddUi(player);
    }

    public class UiState : IPlayerStore
    {
        public ulong PlayerId { get; }
        public BorderRadiusBuilder Builder { get; } = new BorderRadiusBuilder()
            .SetSize(UiSize2D.Size256)
            .SetFill(UiColors.Rust.Red)
            .SetRadius(new UiBorderRadius(10.Percent()));

        public ImageId Image { get; set; }
        public string ExportName { get; set; } = "image";

        public UiState(ulong playerId)
        {
            PlayerId = playerId;
        }

        public async UniTask<Result<ImageId>> Generate(IUiFrameworkPlugin plugin) => await Builder.GenerateAsync(plugin);
    }

    [UiCommand]
    private void CloseUi(ExecutionData data)
    {
        BasePlayer player = data.Player;
        _playerStore.RemoveStore(this, player);
        BaseBuilder.DestroyUi(player, UiName);
    }

    [UiCommand]
    private void OpenExportUi(ExecutionData data)
    {
        CreateExportUi(data.Player, data.GetStore<UiState>());
    }

    [UiCommand]
    private void CloseExportUi(ExecutionData data)
    {
        BasePlayer player = data.Player;
        BaseBuilder.DestroyUi(player, ExportUiName);
    }

    [UiCommand]
    private async UniTask SetMode(ExecutionData data, bool enableInputImage)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetImage(enableInputImage ? UiImageDefaults.Logo : null);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SetUrl(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        state.Builder.SetImage(arg.Value);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask FillColorChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out UiColor value))
        {
            state.Builder.SetFill(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask TransparentColorChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out UiColor value))
        {
            state.Builder.SetTransparent(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractRadius(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.Radius.TopLeft.Value > 1f)
        {
            state.Builder.SetRadius(state.Builder.Radius - 1f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddRadius(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetRadius(state.Builder.Radius + 1f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask OnRadiusChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(UiUnit.TryParse(arg.Value, out UiUnit unit))
        {
            state.Builder.SetRadius(new UiBorderRadius(unit));
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask ToggleAntialiasing(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetAntiAlias(!state.Builder.AntiAlias);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractEdgeWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.EdgeWidth >= 0.1f)
        {
            state.Builder.SetAntiAliasEdgeWidth(state.Builder.EdgeWidth - 0.1f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddEdgeWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetAntiAliasEdgeWidth(state.Builder.EdgeWidth + 0.1f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask EdgeWidthChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if (arg.TryGetValue(out float value))
        {
            state.Builder.SetAntiAliasEdgeWidth(value);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractSize(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.Size.Height > 1f)
        {
            state.Builder.SetSize(state.Builder.Size / 2f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddSize(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetSize(state.Builder.Size * 2f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SizeChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if(UiSize2D.TryParse(arg.Value, out UiSize2D size, "x"))
        {
            state.Builder.SetSize(size);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask ToggleBorder(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetEnableBorder(!state.Builder.EnableBorder);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractBorderWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.BorderWidth > 1f)
        {
            state.Builder.SetBorderWidth(state.Builder.BorderWidth - 1f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddBorderWidth(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetBorderWidth(state.Builder.BorderWidth + 1f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask BorderWidthChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out float value))
        {
            state.Builder.SetBorderWidth(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask BorderColorChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out UiColor value))
        {
            state.Builder.SetBorderColor(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask ToggleDashedBorder(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetEnableDashedBorder(!state.Builder.EnableDashedBorder);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractDashLength(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.DashLength > 1f)
        {
            state.Builder.SetDashLength(state.Builder.DashLength - 1f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddDashLength(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetDashLength(state.Builder.DashLength + 1f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask DashLengthChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out float value))
        {
            state.Builder.SetDashLength(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask SubtractDashGap(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        if(state.Builder.GapLength > 1f)
        {
            state.Builder.SetGapLength(state.Builder.GapLength - 1f);
            state.Image = await state.Generate(this);
        }
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask AddDashGap(ExecutionData data)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();
        state.Builder.SetGapLength(state.Builder.GapLength + 1f);
        state.Image = await state.Generate(this);
        CreateUi(player, state);
    }

    [UiCommand]
    private async UniTask DashGapChanged(ExecutionData data, InputArg arg)
    {
        BasePlayer player = data.Player;
        UiState state = data.GetStore<UiState>();

        if (arg.TryGetValue(out float value))
        {
            state.Builder.SetGapLength(value);
            state.Image = await state.Generate(this);
        }

        CreateUi(player, state);
    }

    [UiCommand]
    private void ExportNameChanged(ExecutionData data, InputArg arg)
    {
        UiState state = data.GetStore<UiState>();
        if (arg.Value.Length != 0)
        {
            state.ExportName = arg.Value;
        }
    }

    [UiCommand]
    private void ExportImage(ExecutionData data)
    {
        UiState state = data.GetStore<UiState>();
        string path = Path.Combine(_exportFolder, $"{state.ExportName}.png");
        _imageStorage.WriteImage(this, path, state.Image);
        CreateExportedUi(data.Player, path);
    }
}