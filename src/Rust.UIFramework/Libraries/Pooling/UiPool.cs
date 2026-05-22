using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPool : BaseUiFrameworkLibrary, ISingleton
{
    public static readonly UiPluginPool Internal = Singleton<UiPool>.Instance._internal;
    
    private readonly Dictionary<PluginId, PluginPoolId> _pluginPoolIds = new();
    private UiPluginPool[] _pluginPools = new UiPluginPool[32];
    private readonly UiPluginPool _internal;
    private readonly IUiLogger<UiPool> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiPool>();
    private readonly object _lock = new();

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
    
    private UiPluginPool CreatePoolInternal(IUiFrameworkPlugin plugin, PoolSettings settings) => CreatePoolInternal(plugin.Id(), settings);
    
    private UiPluginPool CreatePoolInternal(PluginId id, PoolSettings settings)
    {
        if (_pluginPoolIds.TryGetValue(id, out PluginPoolId poolId) && TryGetPoolByPoolId(poolId, out UiPluginPool pool))
        {
            return pool;
        }

        lock (_lock)
        {
            if (_pluginPoolIds.TryGetValue(id, out poolId) && TryGetPoolByPoolId(poolId, out pool))
            {
                return pool;
            }

            if (!poolId.IsValid)
            {
                _pluginPoolIds[id] = poolId = PluginPoolId.GetNextId();
            }
            
            if(poolId.Id >= _pluginPools.Length)
            {
                Array.Resize(ref _pluginPools, _pluginPools.Length * 2);
            }
            
            pool = _pluginPools[poolId.Id] = new UiPluginPool(poolId, id);
            pool.SetSettings(settings);
            return pool;
        }
    }

    private bool TryGetPoolByPoolId(PluginPoolId poolId, out UiPluginPool pool)
    {
        pool = _pluginPools[poolId.Id];
        return pool != null;
    }

    ///<inheritdoc/>
    protected override void OnPluginLoaded(IUiFrameworkPlugin plugin)
    {
        plugin.PluginPool = GetOrCreate(plugin);
    }

    ///<inheritdoc/>
    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        if (_pluginPoolIds.TryGetValue(plugin.Id(), out PluginPoolId id))
        {
            UiPluginPool pool = _pluginPools[id.Id];
            if (pool != null)
            {
                pool.OnPluginUnloaded();
                _pluginPools[id.Id] = null;
            }
        }
    }

    internal void Clear()
    {
        for (int index = 0; index < _pluginPools.Length; index++)
        {
            UiPluginPool pool = _pluginPools[index];
            pool?.Clear();
        }
    }
        
    internal void Wipe()
    {
        for (int index = 0; index < _pluginPools.Length; index++)
        {
            UiPluginPool pool = _pluginPools[index];
            pool?.Wipe();
        }
    }
    
    internal bool CheckForLeaks()
    {
        bool hasLeaked = false;
        for (int index = 0; index < _pluginPools.Length; index++)
        {
            UiPluginPool pool = _pluginPools[index];
            if (pool != null)
            {
                hasLeaked |= pool.CheckForLeaks();
            }
        }

        return hasLeaked;
    }

    internal void PrintLeaks()
    {
        for (int index = 0; index < _pluginPools.Length; index++)
        {
            UiPluginPool pool = _pluginPools[index];
            pool?.PrintLeaks();
        }
    }

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.AppendObject("Internal", _internal);
        logger.AppendObject("Obsolete", UiFrameworkPool.Pool);
        for (int index = 0; index < _pluginPools.Length; index++)
        {
            UiPluginPool pool = _pluginPools[index];
            if (pool != null && pool != _internal)
            {
                logger.AppendObject(pool.PluginName, pool);
            }
        }
    }
}