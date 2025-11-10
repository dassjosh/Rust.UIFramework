using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface ICommandBuilderData
{
    IUiFrameworkPlugin Plugin { get; }
    string Command { get; }
    ICommandProtection Protection { get; }
    IArgWriter[] Writers { get; }
}