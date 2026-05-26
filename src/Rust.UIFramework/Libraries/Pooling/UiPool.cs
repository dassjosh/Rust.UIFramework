using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPool : BaseUiFrameworkLibrary, ISingleton
{
    internal static readonly UiPluginPool Internal = Singleton<UiPool>.Instance._internal;
    
    private readonly ConcurrentDictionary<PluginId, UiPluginPool> _pluginPools = new();
    private readonly UiPluginPool _internal;
    private readonly IUiLogger<UiPool> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPool>();

    private UiPool()
    {
        _internal = CreatePoolInternal(new PluginId(UiFrameworkExtension.Instance.Name), PoolSettings.CreateInternal());
    }
    
    internal UiPluginPool CreateObsoletePool()
    {
        UiPluginPool obsolete = CreatePoolInternal(PluginId.CreateInternal($"{UiFrameworkExtension.Instance.Name}.Obsolete"), PoolSettings.CreateInternal());
        return obsolete; 
    }

    /// <summary>
    /// Returns an existing <see cref="UiPluginPool"/> for the given plugin or returns a new one
    /// </summary>
    /// <param name="plugin">The pool the plugin is for</param>
    /// <param name="settings">Settings for the pools</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Thrown if the plugin is null</exception>
    public UiPluginPool GetOrCreate(IUiFrameworkPlugin plugin, PoolSettings settings = null)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return GetOrCreate(plugin.Id(), settings);
    }
    
    internal UiPluginPool GetOrCreate(PluginId plugin, PoolSettings settings = null)
    {
        return CreatePoolInternal(plugin, settings);
    }
    
    private UiPluginPool CreatePoolInternal(PluginId id, PoolSettings settings)
    {
        UiPluginPool pool = _pluginPools.GetOrAdd(id, static poolId => new UiPluginPool(poolId));
        pool.SetSettings(settings ?? PoolSettings.Default);
        return pool;
    }

    ///<inheritdoc/>
    protected override void OnPluginLoaded(IUiFrameworkPlugin plugin)
    {
        plugin.PluginPool = GetOrCreate(plugin);
    }

    ///<inheritdoc/>
    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        if (_pluginPools.TryRemove(plugin.Id(), out UiPluginPool pool))
        {
            pool.OnPluginUnloaded();
        }
    }

    internal void Clear()
    {
        foreach (KeyValuePair<PluginId, UiPluginPool> pools in _pluginPools)
        {
            pools.Value.Clear();
        }
    }
        
    internal void Wipe()
    {
        foreach (KeyValuePair<PluginId, UiPluginPool> pools in _pluginPools)
        {
            pools.Value.Wipe();
        }
    }
    
    internal bool CheckForLeaks()
    {
        bool hasLeaked = false;
        foreach (KeyValuePair<PluginId, UiPluginPool> pools in _pluginPools)
        {
            hasLeaked |= pools.Value.CheckForLeaks();
        }

        return hasLeaked;
    }

    internal void PrintLeaks()
    {
        foreach (KeyValuePair<PluginId, UiPluginPool> pools in _pluginPools)
        {
            pools.Value.PrintLeaks();
        }
    }

    public void LogDebug(DebugLogger logger)
    {
        logger.AppendObject("Internal", _internal);
        logger.AppendObject("Obsolete", UiFrameworkPool.Pool);
        foreach (KeyValuePair<PluginId, UiPluginPool> pools in _pluginPools)
        {
            if (pools.Value != null && pools.Value != _internal)
            {
                logger.AppendObject(pools.Value.PluginName, pools.Value);
            }
        }
    }
}