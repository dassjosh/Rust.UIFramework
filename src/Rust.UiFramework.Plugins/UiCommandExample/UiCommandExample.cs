using System;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Positions;


namespace Oxide.Plugins;

[Info("Test", "MJSU", "1.0.0")]
[Description("Test")]
public class UiCommandExample : RustPlugin
{
    private readonly UiCommands _uiCommands = GetLibrary<UiCommands>();

    private ICommandBuilder<int, bool, BuildingPrivlidge, DateTime> _doTheThingBuilder;
    
    private void Init()
    {
        _doTheThingBuilder = _uiCommands.RegisterCommand<int, bool, BuildingPrivlidge, DateTime>(this, HandleDoTheThing);
        _uiCommands.RegisterPlayerCooldownCallback(this, (player, method, cooldown, remaining) => //[Optional]
        {
            //Show UI about being on cooldown
        });
        _uiCommands.RegisterNoPermissionCallback(this, (player, method) => //[Optional]
        {
            //Show UI about not having permission
        });
        _uiCommands.RegisterValidationFailedCallback(this, (player, method) => //[Optional]
        {
            //Handle command protection failing
        });
        _uiCommands.RegisterCustomParser(this, new MyCustomClassHandler());
    }

    public void CreateUi(BasePlayer player, BuildingPrivlidge priv)
    {
        UiBuilder builder = UiBuilder.Create(UiPosition.Full, default, UiColor.White, "UiName");
        builder.CommandButton(builder.Root, UiPosition.Full, default, UiColor.Clear, _doTheThingBuilder.Build(1, false, priv, DateTime.Now));
        builder.AddUi(player);
    }
    
    [UiCommand]
    [UiProtection(ProtectionType.Simple)]
    [UiCooldown(1f)]
    [UiPermission(permissions: ["test.admin"])]
    public void HandleDoTheThing(BasePlayer player, int a, bool b, BuildingPrivlidge id, DateTime c)
    {
        
    }

    private class MyCustomClass
    {
        public ulong Id { get; set; }
    }

    private class MyCustomClassHandler : IArgHandler<MyCustomClass>
    {
        public MyCustomClass Read(ReadOnlySpan<char> arg)
        {
            ulong id = ulong.Parse(arg);
            //Lookup by ID and return value
            return default;
        }

        public void Write(UiArgWriter writer, MyCustomClass arg)
        {
            writer.AppendArg(arg.Id);
        }
    }
}