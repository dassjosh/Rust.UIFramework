using System;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;

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
        
        UiFrameworkExtension.GlobalLogger.Error("Failed to validate command protection for {0}({1}) on plugin {2}", player.displayName, player.UserIDString, plugin.FullName());
        return false;
    }
    
    protected ArgReaderIterator GetReader() => new(reader);

    protected void LogException(Exception ex)
    {
        //TODO: Have error to come from plugin instead of extension
        UiFrameworkExtension.GlobalLogger.Exception("[{0}] Threw an exception invoking UiCommand callback {1}", plugin.FullName(), method.GetMethodWithParams(), ex);
    }
}