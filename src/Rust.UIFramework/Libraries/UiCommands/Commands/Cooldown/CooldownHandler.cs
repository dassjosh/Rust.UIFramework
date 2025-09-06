using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CooldownHandler(PluginId pluginId, string method, float cooldown, string errorMessage, OnPlayerCooldown onCooldown) : ICooldownHandler
{
    private readonly UiMemoryCache<ulong> _cooldownExpires = new(TimeSpan.FromSeconds(cooldown));
    private static readonly IUiLogger<CooldownHandler> Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<CooldownHandler>();

    public bool IsOnCooldown(BasePlayer player)
    {
        if(_cooldownExpires.TryGetExpiresIn(player.userID, out float remaining))
        {
            HandleCallback(player, remaining);
            return true;
        }
        
        _cooldownExpires.TryAdd(player.userID.Get()); 
        return false;
    }

    private void HandleCallback(BasePlayer player, float remaining)
    {
        try
        {
            if (onCooldown != null)
            {
                onCooldown(player, cooldown, remaining, errorMessage);
                return;
            }

            Singleton<UiCommands>.Instance.OnPlayerCooldown(pluginId, player, method, cooldown, remaining, errorMessage);
        }
        catch (Exception ex)
        {
            Logger.Exception($"{nameof(HandleCallback)} An error occured during callback", ex);
        }
    }
}