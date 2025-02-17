using Network;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal abstract class BaseCommandParser(ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission, IArgReader[] reader) : ICommandParser
{
    public void RunCommand(Connection connection, UiCommandTokenizer command)
    {
        BasePlayer player = connection.player as BasePlayer;
        if ((cooldown?.IsOnCooldown(player) ?? false) 
            || !(permission?.HasPermission(player) ?? true)
            || !ValidateProtection(player, ref command))
        {
            return;
        }
        
        RunCommandInternal(player, command);
    }
    
    protected abstract void RunCommandInternal(BasePlayer player, UiCommandTokenizer args);
    
    private bool ValidateProtection(BasePlayer player, ref UiCommandTokenizer command)
    {
        if (protection is null)
        {
            return true;
        }

        if (protection.TryValidateProtection(command, out command))
        {
            return true;
        }
        
        
        Interface.Oxide.LogError($"[UiFramework] failed to validate command protection for {player.displayName}({player.UserIDString})"); //TODO: Logger
        return false;
    }
    
    protected ArgReaderIterator GetReader() => new(reader);
}