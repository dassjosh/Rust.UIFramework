using System;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public abstract class BaseCommandProtection<T>(RegisteredCommand command) : ICommandProtection where T : BaseCommandProtection<T>
{
    protected readonly RegisteredCommand Command = command;
    protected static readonly IUiLogger<T> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<T>();
    
    protected void HandleCallback(BasePlayer player)
    {
        try
        {
            if (Command.OnPlayerProtectionFailed != null)
            {
                Command.OnPlayerProtectionFailed(player);
                return;
            }

            Singleton<UiCommands>.Instance.OnProtectionValidationFailed(Command.PluginId, player, Command.Delegate.Method);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(HandleCallback)} An error occured during callback", ex);
        }
    }

    public abstract void ProtectCommand(ref UiArgWriter writer);
    public abstract bool TryValidateProtection(BasePlayer player, ref UiCommandTokenizer tokenizer);
}