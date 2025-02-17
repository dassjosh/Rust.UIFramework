using System;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Positions;


namespace Oxide.Plugins;

[Info("Test", "MJSU", "1.0.0")]
[Description("Test")]
public class Test : RustPlugin
{
    private readonly UiCommands _uiCommands = GetLibrary<UiCommands>();

    private ICommandBuilder<int, bool, BuildingPrivlidge, DateTime> _doTheThingBuilder;
    
    private void Init()
    {
        _doTheThingBuilder = _uiCommands.RegisterCommand<int, bool, BuildingPrivlidge, DateTime>(this, HandleDoTheThing);
    }

    public void CreateUi(BasePlayer player, BuildingPrivlidge priv)
    {
        UiBuilder builder = UiBuilder.Create(UiPosition.Full, default, UiColor.White, "UiName");
        builder.CommandButton(builder.Root, UiPosition.Full, default, UiColor.Clear, _doTheThingBuilder.Build(1, false, priv, DateTime.Now));
        builder.AddUi(player);
    }
    
    [UiCommand(protectionType: ProtectionType.Simple, permission: "test.admin", cooldown: 1f)]
    public void HandleDoTheThing(BasePlayer player, int a, bool b, BuildingPrivlidge id, DateTime c)
    {
        
    }
}