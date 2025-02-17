namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class RegisteredCommand(ICommandParser parser)
{
    public readonly ICommandParser Parser = parser;
}