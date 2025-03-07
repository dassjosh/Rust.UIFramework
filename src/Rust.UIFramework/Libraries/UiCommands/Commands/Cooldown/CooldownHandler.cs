using System;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CooldownHandler(PluginId pluginId, string method, float cooldown, string errorMessage) : ICooldownHandler
{
    private readonly UiMemoryCache<ulong> _cooldownExpires = new(TimeSpan.FromSeconds(cooldown));

    public bool IsOnCooldown(BasePlayer player)
    {
        if(_cooldownExpires.TryGetExpiresIn(player.userID, out float remaining))
        {
            Singleton<UiCommands>.Instance.OnPlayerCooldown(pluginId, player, method, cooldown, remaining, errorMessage);
            return true;
        }
        
        _cooldownExpires.TryAdd(player.userID.Get()); 
        return false;
    }
}