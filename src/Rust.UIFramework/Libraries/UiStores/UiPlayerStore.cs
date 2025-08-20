using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPlayerStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginPlayerStore, IPlayerStore> _stores = new();

    private UiPlayerStore() { }

    public void CreateStore<T>(Plugin plugin, T store) where T : class, IPlayerStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (store == null) throw new ArgumentNullException(nameof(store));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(store.PlayerId);
        _stores[new PluginPlayerStore(plugin.Id(), store.PlayerId)] = store;
    }

    public T GetStore<T>(Plugin plugin, BasePlayer player) where T : class, IPlayerStore
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        return GetStore<T>(plugin, player.userID.Get());
    }

    public T GetStore<T>(Plugin plugin, ulong playerId) where T : class, IPlayerStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        if (_stores.TryGetValue(new PluginPlayerStore(plugin.Id(), playerId), out IPlayerStore store))
        {
            return (T)store;
        }

        return default;
    }

    public void RemoveStore(Plugin plugin, BasePlayer player)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        RemoveStore(plugin, player.userID.Get());
    }
    
    public void RemoveStore(Plugin plugin, ulong playerId)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        _stores.Remove(new PluginPlayerStore(plugin.Id(), playerId));
    }

    public T GetOrCreateStore<T>(Plugin plugin, BasePlayer player) where T : class, IPlayerStore, new() => GetOrCreateStore<T>(plugin, player.userID.Get());

    public T GetOrCreateStore<T>(Plugin plugin, ulong playerId) where T : class, IPlayerStore, new() => (T)GetOrCreateStore<T>(plugin.Id(), playerId);
    
    internal IPlayerStore GetOrCreateStore<T>(PluginId pluginId, ulong playerId)
    {
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        PluginPlayerStore key = new(pluginId, playerId);
        if (_stores.TryGetValue(key, out IPlayerStore value))
        {
            return value;
        }

        value = (IPlayerStore)Activator.CreateInstance(typeof(T));
        value!.PlayerId = playerId;
        _stores[key] = value;
        return value;
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
    }

    protected override void OnPlayerDisconnected(BasePlayer player)
    {
        ulong playerId = player.userID.Get();
        _stores.RemoveAll(s => s.Key.PlayerId == playerId);
    }

    private readonly record struct PluginPlayerStore(PluginId PluginId, ulong PlayerId);
}