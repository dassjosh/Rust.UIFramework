using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Plugins.UiCommandExample;

[Info("UiCommandExample", "MJSU", "1.0.0")]
[Description("UiCommandExample")]
public class UiCommandExample : RustPlugin
{
    private readonly UiCommands _uiCommands = GetLibrary<UiCommands>();
    private readonly UiPlayerStore _playerStore = GetLibrary<UiPlayerStore>();

    private ICommandBuilder<UiState, MyCustomArg, int, bool, BuildingPrivlidge, DateTime> _doTheThingBuilder;
    
    private void Init()
    {
        _doTheThingBuilder = _uiCommands.RegisterCommand<UiState, MyCustomArg, int, bool, BuildingPrivlidge, DateTime>(this, HandleDoTheThing);
        _uiCommands.RegisterPlayerCooldownCallback(this, (player, method, cooldown, remaining, errorMessage) => //[Optional]
        {
            //Show UI about being on cooldown
        });
        _uiCommands.RegisterNoPermissionCallback(this, (player, method, errorMessage) => //[Optional]
        {
            //Show UI about not having permission
        });
        _uiCommands.RegisterValidationFailedCallback(this, (player, method) => //[Optional]
        {
            //Handle command protection failing
        });
        _uiCommands.RegisterCustomParser(this, new MyCustomArgHandler());
    }

    public void CreateUi(BasePlayer player)
    {
        UiState state = _playerStore.GetOrCreateStore<UiState>(this, player);
        CreateUi(player, state, player.GetBuildingPrivilege());
    }
    
    public void CreateUi(BasePlayer player, UiState state, BuildingPrivlidge priv)
    {
        UiBuilder builder = UiBuilder.Create(UiPosition.Full, default, UiColors.White, "UiName");
        builder.Button(builder.Root, UiPosition.Full, default, UiColors.Clear, _doTheThingBuilder.Build(state, new MyCustomArg(ulong.MaxValue), 1, false, priv, DateTime.Now));
        builder.AddUi(player);
    }
    
    [UiCommand]
    [UiProtection(ProtectionType.Simple, 3600f)]
    [UiCooldown(1f, "You are on cooldown")]
    [UiPermission(permissions: new[] {"test.admin"}, PermissionMode.RequireAll, "You don't have permission")]
    public void HandleDoTheThing(BasePlayer player, UiState state, MyCustomArg custom, int a, bool b, BuildingPrivlidge id, DateTime c)
    {
        
    }

    public class MyCustomArg
    {
        public ulong Id { get; set; }
        
        public MyCustomArg(ulong id)
        {
            Id = id;
        }
    }

    private class MyCustomArgHandler : IArgHandler<MyCustomArg>
    {
        private readonly Hash<ulong, MyCustomArg> _classes = new();
        
        public MyCustomArg Read(ReadOnlySpan<char> arg)
        {
            ulong id = ulong.Parse(arg);
            if (!_classes.TryGetValue(id, out MyCustomArg custom))
            {
                _classes[id] = custom = new MyCustomArg(id);
            }

            return custom;
        }

        public void Write(UiArgWriter writer, MyCustomArg arg)
        {
            writer.Append(arg.Id);
        }
    }

    public class UiState : IPlayerStore
    {
        public ulong PlayerId { get; set; }
    }
}