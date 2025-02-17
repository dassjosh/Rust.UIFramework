namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICooldownHandler
{
    bool IsOnCooldown(BasePlayer player);
}