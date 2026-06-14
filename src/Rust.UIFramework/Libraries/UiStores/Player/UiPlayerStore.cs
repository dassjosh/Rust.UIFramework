using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate IPlayerStore CreatePlayerStore(ulong playerId);

public class UiPlayerStore : BaseUiFrameworkLibrary, ISingleton
{
    private readonly ConcurrentDictionary<PluginPlayerStore, IPlayerStore> _stores = [];
    private readonly ConcurrentDictionary<PluginId, PlayerStoreData> _pluginData = [];
    private readonly IUiLogger<UiPlayerStore> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPlayerStore>();

    private UiPlayerStore() { }

    public void RegisterStore(IUiFrameworkPlugin plugin, CreatePlayerStore creator, PlayerStoreOptions options = null)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        _pluginData[plugin.Id()] = new PlayerStoreData
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator)),
            Options = options ?? PlayerStoreOptions.Default
        };
    }

    public void CreateStore<T>(IUiFrameworkPlugin plugin, T store) where T : class, IPlayerStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (store == null) throw new ArgumentNullException(nameof(store));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(store.PlayerId);
        _stores[new PluginPlayerStore(plugin.Id(), store.PlayerId)] = store;
    }

    public T GetStore<T>(IUiFrameworkPlugin plugin, BasePlayer player) where T : class, IPlayerStore
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        return GetStore<T>(plugin, player.userID.Get());
    }

    public T GetStore<T>(IUiFrameworkPlugin plugin, ulong playerId) where T : class, IPlayerStore
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        if (_stores.TryGetValue(new PluginPlayerStore(plugin.Id(), playerId), out IPlayerStore store))
        {
            return (T)store;
        }

        return default;
    }

    public void RemoveStore(IUiFrameworkPlugin plugin, BasePlayer player)
    {
        if (!player) throw new ArgumentNullException(nameof(player));
        RemoveStore(plugin, player.userID.Get());
    }
    
    public void RemoveStore(IUiFrameworkPlugin plugin, ulong playerId)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        _stores.TryRemove(new PluginPlayerStore(plugin.Id(), playerId), out _);
    }

    public T GetOrCreateStore<T>(IUiFrameworkPlugin plugin, BasePlayer player) where T : class, IPlayerStore => GetOrCreateStore<T>(plugin, player.userID.Get());

    public T GetOrCreateStore<T>(IUiFrameworkPlugin plugin, ulong playerId) where T : class, IPlayerStore => (T)GetOrCreateStore(plugin.Id(), playerId);
    
    internal IPlayerStore GetOrCreateStore(PluginId pluginId, ulong playerId)
    {
        Guard.IsValid(pluginId);
        Guard.IsValidPlayerId(playerId);
        InvalidStorePlayerIdException.ThrowIfInvalidPlayerId(playerId);
        PluginPlayerStore key = new(pluginId, playerId);
        if (_stores.TryGetValue(key, out IPlayerStore value))
        {
            return value;
        }

        if (_pluginData.TryGetValue(pluginId, out PlayerStoreData data))
        {
            _stores[key] = value = data.Creator(playerId);
            return value;
        }

        _logger.Error("No player store found for player '{0}' and no creator found for plugin '{1}'", playerId, pluginId);
        _logger.Error($"IDS:{string.Join(", ", _pluginData.Keys.Select(s => s.Id))}");
        return default;
    }

    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _stores.RemoveAll(s => s.Key.PluginId == pluginId);
        _pluginData.TryRemove(pluginId, out _);
    }

    protected override void OnPlayerDisconnected(BasePlayer player)
    {
        ulong playerId = player.userID.Get();
        foreach (PluginPlayerStore key in _stores.Keys)
        {
            if (key.PlayerId != playerId)
            {
                continue;
            }
            
            if (_pluginData.TryGetValue(key.PluginId, out PlayerStoreData data) && data.Options.RemoveOnDisconnect)
            {
                _stores.TryRemove(key, out _);
            }
        }
    }

    private readonly record struct PluginPlayerStore(PluginId PluginId, ulong PlayerId);

    private sealed class PlayerStoreData
    {
        public CreatePlayerStore Creator { get; init; }
        public PlayerStoreOptions Options { get; init; }
    }
}