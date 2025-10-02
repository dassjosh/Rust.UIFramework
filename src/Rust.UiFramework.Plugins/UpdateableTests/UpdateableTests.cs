using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Oxide.Plugins;

namespace Oxide.Plugins;

[Info("Updateable Tests", "MJSU", "1.0.0")]
[Description("Tests the updateable system")]
public class UpdateableTests : RustPlugin, IUiFrameworkPlugin
{
    private readonly UiPlayerStore _store = GetLibrary<UiPlayerStore>();
    private readonly UiCommands _commands = GetLibrary<UiCommands>();
    
    public UiPluginPool PluginPool { get; set; }
    
    private static UpdateableTests _ins;
    
    private void Init()
    {
        _ins = this;
       _close = _commands.RegisterCommand<UiState>(this, CloseCommand);
    }

    private void Unload()
    {
        _ins = null;
    }
    
    [ChatCommand("ut")]
    private void UpdateableTestsCommand(BasePlayer player, string cmd, string[] args)
    {
        CreateUi(player, _store.GetOrCreateStore<UiState>(this, player));
    }

    private class UiState : IPlayerStore
    {
        public ulong PlayerId { get; set; }
        public BasePlayer Player { get; set; }
        
        public UpdatableBuilder Builder { get; set; }
        public List<UiUpdatable<UiPanel>> Panels { get; set; } = new();
        
        public Timer UpdateTimer { get; set; }

        public void StartTimer()
        {
            UpdateTimer = _ins.timer.Every(1f, () =>
            {
                UiUpdatable<UiPanel> panel = Panels.GetRandom();
                panel.Current.SetEnabled(!panel.Current.Enabled);
                _ins.Puts($"B-{Builder.GetJsonString()}");
                Builder.AddUi(Player);
            });
        }
        
        public void StopTimer()
        {
            UpdateTimer?.Destroy();
        }
    }

    private const string UiName = "UiUpdateableTests";
    private readonly GridPosition _grid = new GridPositionBuilder(3, 3).SetPadding(0.01f).Build();
    private readonly UiColor[] _colors = {UiColors.Red, UiColors.Orange, UiColors.Yellow, UiColors.Green, UiColors.Blue, UiColors.Purple, UiColors.Gray, UiColors.Black, UiColors.White};
    private ICommandBuilder<UiState> _close;

    private void CreateUi(BasePlayer player, UiState state)
    {
        state.Player = player;
        UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(400, 400), UiColors.Panel);
        builder.NeedsMouse();
        UiPanel header = builder.Panel(builder.Root, new UiPosition(0, 0.95f, 1, 1), default, UiColors.PanelSecondary);
        builder.TextButton(header, new UiPosition(0.95f, 0f, 1, 1), default, "X", 14, UiColors.White, UiColors.Rust.Red, _close.Build(state));

        UiPanel body = builder.Panel(builder.Root, new UiPosition(0, 0, 1, 0.945f), default, UiColors.PanelTertiary);

        builder.Section(body, new UiPosition(0.25f, 0.25f, 0.25f, 0.25f));
            
        _grid.Reset();

        UpdatableBuilder updateable = state.Builder = UpdatableBuilder.Create(this);
        
        foreach (UiColor color in _colors)
        {
            state.Panels.Add(builder.Panel(body, _grid, default, color).ToUpdatable(updateable));
            _grid.MoveCols(1);
        }
        Puts($"A-{builder.GetJsonString()}");
        builder.AddUi(player);
        state.StartTimer();
    }

    [UiCommand]
    private void CloseCommand(BasePlayer player, UiState state)
    {
        state.StopTimer();
        UiBuilder.DestroyUi(UiName);
    }
}