using Network;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal interface ICommandParser
{
    void RunCommand(Connection connection, UiCommandTokenizer command);
}