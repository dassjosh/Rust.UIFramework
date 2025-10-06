using System.Linq;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;
using Random = Oxide.Core.Random;

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

    private void OnServerInitialized()
    {
        NextTick(() =>
        {
            BasePlayer player = BasePlayer.activePlayerList.FirstOrDefault();
            UpdateableTestsCommand(player, null, null);
        });
    }
    
    private void Unload()
    {
        UiBuilder.DestroyUi(UiName);
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
        
        public Timer UpdateTimer { get; set; }

        public void StartTimer()
        {
            UpdateTimer = _ins.timer.Every(1f, () =>
            {
               
            });
        }
        
        public void StopTimer()
        {
            UpdateTimer?.Destroy();
        }
    }

    private const string UiName = "UiUpdateableTests";
    //private readonly GridPosition _grid = new GridPositionBuilder(3, 3).SetPadding(0.01f).Build();
    private readonly UiColor[] _colors = {UiColors.Red, UiColors.Orange, UiColors.Yellow, UiColors.Green, UiColors.Blue, UiColors.Purple, UiColors.Gray, UiColors.Black, UiColors.White};
    private ICommandBuilder<UiState> _close;

    private void CreateUi(BasePlayer player, UiState state)
    {
        state.Player = player;
        UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(400, 400), UiColors.Panel);
        builder.NeedsMouse();
        UiPanel header = builder.Panel(builder.Root, new UiPosition(0, 0.95f, 1, 1), default, UiColors.PanelSecondary);
        builder.TextButton(header, new UiPosition(0.95f, 0f, 1, 1), default, "X", 14, UiColors.White, UiColors.Rust.Red, _close.Build(state));

        UiPanel body = builder.Panel(builder.Root, new UiPosition(0, 0, 1, 0.945f), default, UiColors.PanelTertiary).SetPadding(new UiPadding(10));

        UiScrollView scroll = builder.ScrollView(body, UiPosition.Full, default, ScrollRect.MovementType.Clamped);
        scroll.AddVerticalScrollBar();
        scroll.UpdateContentTransform(UiPosition.Full, default, new Vector2(0f, 1f));
        
        (UiSection scrollContent, GridLayoutComponent layout) = builder.GridLayout(scroll);
        layout.SetCellSize(new Vector2(150f, 150f)).SetSpacing(new Vector2(10f, 10f)).SetPadding(new UiPadding(20, 20));
        
        //UpdatableBuilder updateable = state.Builder = UpdatableBuilder.Create(this);

        for (int i = 0; i < 25; i++)
        {
            UiPanel panel = builder.Panel(layout, new UiColor(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
            //builder.LayoutElement(panel);
            //builder.ContentSizeFitter(panel);
            //state.Panels.Add(panel.ToUpdatable(updateable));
        }
        
        Puts($"A-{builder.GetJsonString()}");
        builder.AddUi(player);
        //state.StartTimer();
    }

    [UiCommand]
    private void CloseCommand(BasePlayer player, UiState state)
    {
        state.StopTimer();
        UiBuilder.DestroyUi(UiName);
    }
}