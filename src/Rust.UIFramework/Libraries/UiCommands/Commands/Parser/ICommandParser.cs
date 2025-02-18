namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandParser
{
    void RunCommand(BasePlayer player, UiCommandTokenizer command);
}