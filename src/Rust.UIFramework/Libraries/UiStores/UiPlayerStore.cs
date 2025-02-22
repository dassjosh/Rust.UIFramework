using System;
using System.Collections.Generic;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPlayerStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly Dictionary<PluginPlayerStore, IPlayerStore> _stores = new();

    private UiPlayerStore() { }

    public void CreateStore<T>(Plugin plugin, T store) where T : IPlayerStore => _stores[new PluginPlayerStore(plugin.Id(), store.PlayerId)] = store;
    public T GetStore<T>(Plugin plugin, BasePlayer player) where T : IPlayerStore => GetStore<T>(plugin, player.userID.Get());
    public T GetStore<T>(Plugin plugin, ulong playerId) where T : IPlayerStore => (T)_stores[new PluginPlayerStore(plugin.Id(), playerId)];
    
    public T GetOrCreateStore<T>(Plugin plugin, BasePlayer player) where T : IPlayerStore, new() => GetOrCreateStore<T>(plugin, player.userID.Get());

    public T GetOrCreateStore<T>(Plugin plugin, ulong playerId) where T : IPlayerStore, new() => (T)GetOrCreateStore<T>(plugin.Id(), playerId);
    
    internal IPlayerStore GetOrCreateStore<T>(PluginId pluginId, ulong playerId)
    {
        PluginPlayerStore key = new(pluginId, playerId);
        if (_stores.TryGetValue(key, out IPlayerStore value))
        {
            return value;
        }

        value = (IPlayerStore)Activator.CreateInstance(typeof(T));
        value.PlayerId = playerId;
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
        ulong playerId = player.userID;
        _stores.RemoveAll(s => s.Key.PlayerId == playerId);
    }

    private readonly record struct PluginPlayerStore(PluginId PluginId, ulong PlayerId);
}