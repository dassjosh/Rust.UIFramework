using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CooldownHandler(PluginId pluginId, string method, float cooldown, string errorMessage) : ICooldownHandler
{
    private readonly Dictionary<ulong, DateTime> _cooldownExpires = new();

    public bool IsOnCooldown(BasePlayer player)
    {
        if(_cooldownExpires.TryGetValue(player.userID, out DateTime expires) && expires > DateTime.UtcNow)
        {
            Singleton<UiCommands>.Instance.OnPlayerCooldown(pluginId, player, method, cooldown, (float)expires.Subtract(DateTime.UtcNow).TotalSeconds, errorMessage);
            return true;
        }
        
        _cooldownExpires[player.userID.Get()] = DateTime.UtcNow.AddSeconds(cooldown);
        return false;
    }
}