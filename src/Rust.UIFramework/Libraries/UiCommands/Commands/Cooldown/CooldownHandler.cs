using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CooldownHandler(RegisteredCommand command, float cooldown, string errorMessage) : ICooldownHandler
{
    private readonly UiMemoryCache<ulong> _cooldownExpires = new(TimeSpan.FromSeconds(cooldown));
    private static readonly IUiLogger<CooldownHandler> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<CooldownHandler>();

    public bool IsOnCooldown(BasePlayer player)
    {
        if(_cooldownExpires.TryGetExpiresIn(player.userID, out float _))
        {
            return true;
        }
        
        _cooldownExpires.TryAdd(player.userID.Get()); 
        return false;
    }

    public float GetRemainingSeconds(BasePlayer player) => _cooldownExpires.TryGetExpiresIn(player.userID, out float remaining) ? remaining : 0;

    public void OnCooldown(BasePlayer player, float remaining)
    {
        try
        {
            if (command.OnPlayerCooldown != null)
            {
                command.OnPlayerCooldown(player, cooldown, remaining, errorMessage);
                return;
            }

            Singleton<UiCommands>.Instance.OnPlayerCooldown(command.PluginId, player, command.Delegate.Method, cooldown, remaining, errorMessage);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(OnCooldown)} An error occured during callback", ex);
        }
    }
}