using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class CooldownHandler(PluginId pluginId, string method, float cooldown, string errorMessage, OnPlayerCooldown onCooldown) : ICooldownHandler
{
    private readonly UiMemoryCache<ulong> _cooldownExpires = new(TimeSpan.FromSeconds(cooldown));

    public bool IsOnCooldown(BasePlayer player)
    {
        if(_cooldownExpires.TryGetExpiresIn(player.userID, out float remaining))
        {
            if (onCooldown != null)
            {
                onCooldown(player, cooldown, remaining, errorMessage);
                return true;
            }
            
            Singleton<UiCommands>.Instance.OnPlayerCooldown(pluginId, player, method, cooldown, remaining, errorMessage);
            return true;
        }
        
        _cooldownExpires.TryAdd(player.userID.Get()); 
        return false;
    }
}