using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface ICommandParserData
{
    IUiFrameworkPlugin Plugin { get; }
    Delegate Delegate { get; }
    ExecutorMode Mode { get; }
    ICommandProtection Protection { get; }
    ICooldownHandler Cooldown { get; }
    IPermissionHandler Permission { get; }
    IArgReader[] Readers { get; }
    bool RunOnCooldown { get; }
    bool RunOnNoPermission { get; }
}