namespace Oxide.Ext.UiFramework.Libraries;

internal interface ICommandParser
{
    void RunCommand(BasePlayer player, UiCommandTokenizer command);
}