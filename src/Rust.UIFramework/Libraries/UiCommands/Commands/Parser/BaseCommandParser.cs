using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Libraries;

internal abstract class BaseCommandParser : ICommandParser
{
    protected readonly ICommandParserData Command;
    protected readonly Action<Exception> OnException;

    protected BaseCommandParser(ICommandParserData command)
    {
        Command = command;
        OnException = LogException;
    }
    
    public void RunCommand(BasePlayer player, UiCommandTokenizer command)
    {
        ExecutionData data = ExecutionData.Create(Command.Plugin, player);
        if (!ValidateProtection(player, ref command) || !RunCooldown(player, data) || !RunPermission(player, data))
        {
            data.TryDispose();
            return;
        }
        
        RunCommandInternal(data, command);
    }
    
    protected abstract void RunCommandInternal(ExecutionData data, UiCommandTokenizer args);

    private bool RunCooldown(BasePlayer player, ExecutionData data)
    {
        if (Command.Cooldown is null || !Command.Cooldown.IsOnCooldown(player))
        {
            return true;
        }

        float remainingSeconds = Command.Cooldown.GetRemainingSeconds(player);
        if(Command.RunOnCooldown)
        {
            data.IsOnCooldown = true;
            data.RemainingCooldown = remainingSeconds;
            return true;
        }

        Command.Cooldown.OnCooldown(player, remainingSeconds);
        return false;
    }

    private bool RunPermission(BasePlayer player, ExecutionData data)
    {
        if (Command.Permission is null || !Command.Permission.HasPermission(player))
        {
            return true;
        }

        if(Command.RunOnNoPermission)
        {
            data.HasPermission = false;
            return true;
        }

        Command.Permission.OnNoPermission(player);
        return false;
    }

    private bool ValidateProtection(BasePlayer player, ref UiCommandTokenizer command)
    {
        if (Command.Protection is null || Command.Protection.TryValidateProtection(player, ref command))
        {
            return true;
        }
        
        UiFrameworkExtension.GlobalLogger.Error("Failed to validate command protection for {0}({1}) on plugin {2} calling {3}", player.displayName, player.UserIDString, Command.Plugin.FullName(), Command.Delegate.Method.GetMethodWithParams());
        return false;
    }
    
    protected ArgReaderIterator GetReader() => new(Command.Readers);

    protected void LogException(Exception ex)
    {
        UiFrameworkExtension.GlobalLogger.Exception($"[{Command.Plugin.Title}] Threw an exception invoking UiCommand callback {Command.Delegate.Method.GetMethodWithParams()}", ex);
    }
}