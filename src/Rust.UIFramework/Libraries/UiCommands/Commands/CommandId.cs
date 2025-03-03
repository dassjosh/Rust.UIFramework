namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal readonly record struct CommandId(uint Id)
{
    public string GetCommand() => $"{UiCommands.UiCommandName} {Id}";
}