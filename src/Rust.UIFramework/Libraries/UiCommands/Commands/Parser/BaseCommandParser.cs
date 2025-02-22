using System;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal abstract class BaseCommandParser(Plugin plugin, MethodInfo method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission, IArgReader[] reader) : ICommandParser
{
    public void RunCommand(BasePlayer player, UiCommandTokenizer command)
    {
        if (!IsOnCooldown(player) && HasPermission(player) && ValidateProtection(player, ref command))
        {
            RunCommandInternal(player, command);
        }
    }
    
    protected abstract void RunCommandInternal(BasePlayer player, UiCommandTokenizer args);

    private bool IsOnCooldown(BasePlayer player) => cooldown is not null && cooldown.IsOnCooldown(player);
    private bool HasPermission(BasePlayer player) => permission is null || permission.HasPermission(player);

    private bool ValidateProtection(BasePlayer player, ref UiCommandTokenizer command)
    {
        if (protection is null)
        {
            return true;
        }

        if (protection.TryValidateProtection(player, command, out command))
        {
            return true;
        }
        
        Interface.Oxide.LogError($"[UiFramework] failed to validate command protection for {player.displayName}({player.UserIDString})"); //TODO: Logger
        return false;
    }
    
    protected ArgReaderIterator GetReader() => new(reader);

    protected void LogException(Exception ex)
    {
        Interface.Oxide.LogException($"[{plugin.Title}] Threw an exception invoking UiCommand callback {method.GetMethodWithParams()}", ex);
    }
}