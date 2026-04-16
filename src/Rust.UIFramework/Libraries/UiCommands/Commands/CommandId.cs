namespace Oxide.Ext.UiFramework.Libraries;

internal readonly record struct CommandId(uint Id)
{
    public string GetCommand() => $"{UiCommands.UiCommandName} {Id}";
}