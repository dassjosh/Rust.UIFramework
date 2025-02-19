using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate IPlayerStore CreatePlayerStore(ulong playerId);

public class UiPlayerStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginPlayerStore, IPlayerStore> _stores = new();
    private readonly Dictionary<PluginId, CreatePlayerStore> _storeCreators = new();

    private UiPlayerStore() { }
    
    public void RegisterStore(Plugin plugin, CreatePlayerStore creator)
    {
        _storeCreators[plugin.Id()] = creator;
    }

    public T GetOrCreateStore<T>(Plugin plugin, BasePlayer player) where T : IPlayerStore => GetOrCreateStore<T>(plugin, player.userID.Get());

    public T GetOrCreateStore<T>(Plugin plugin, ulong playerId) where T : IPlayerStore => (T)GetOrCreateStore<T>(plugin.Id(), playerId);
    
    internal IPlayerStore GetOrCreateStore<T>(PluginId pluginId, ulong playerId)
    {
        PluginPlayerStore key = new(pluginId, playerId);
        if (_stores.TryGetValue(key, out IPlayerStore value))
        {
            return value;
        }

        if (_storeCreators.TryGetValue(key.PluginId, out CreatePlayerStore creator))
        {
            _stores[key] = value = creator(playerId);
            return value;
        }

        value = (IPlayerStore)Activator.CreateInstance(typeof(T), playerId);
        _stores[key] = value;
        return value;
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
        _storeCreators.Remove(pluginId);
    }

    protected override void OnPlayerDisconnected(BasePlayer player)
    {
        ulong playerId = player.userID;
        _stores.RemoveAll(s => s.Key.PlayerId == playerId);
    }

    private readonly record struct PluginPlayerStore(PluginId PluginId, ulong PlayerId);
}