using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a BasePool in UiFramework
/// </summary>
/// <typeparam name="TPooled">Type being pooled</typeparam>
/// <typeparam name="TPool">Type of the pool</typeparam>
public abstract class BasePool<TPooled, TPool> : IPool, IDebugLoggable
    where TPooled : class 
    where TPool : BasePool<TPooled, TPool>, new()
{
    /// <summary>
    /// Owner Plugin Pool
    /// </summary>
    protected UiPluginPool PluginPool;
    
    UiPluginPool IPool.PluginPool => PluginPool;
    
    private bool _isInitialized;
    
    private static TPool[] _pools;
    private static readonly object CreatePoolLock = new();
    
    internal void InitPool(UiPluginPool pluginPool)
    {
        if (_isInitialized)
        {
            return;
        }
        PluginPool = pluginPool;
        pluginPool.AddPool(this);
        OnInit(pluginPool);
        _isInitialized = true;
        UiFrameworkExtension.GlobalLogger.Debug("Creating Pool. Plugin ID: {0} Type: {1}", pluginPool.PluginName, GetType().GetRealTypeName());
    }

    protected abstract void OnInit(UiPluginPool pluginPool);

    /// <summary>
    /// Returns a pool for the given plugin pool
    /// </summary>
    /// <param name="pluginPool"><see cref="UiPluginPool"/> to get the pool from</param>
    /// <returns></returns>
    public static TPool ForPlugin(UiPluginPool pluginPool)
    {
        if(_pools == null)
        {
            lock (CreatePoolLock)
            {
                _pools ??= new TPool[8];
            }
        }

        TPool pool;
        int index = pluginPool.Id.Id;
        if (index < _pools.Length)
        {
            pool = _pools[index];
            if (pool is { _isInitialized: true })
            {
                return pool;
            }
        }

        lock (CreatePoolLock)
        {
            if (index < _pools.Length)
            {
                pool = _pools[pluginPool.Id.Id];
                if (pool is { _isInitialized: true })
                {
                    return pool;
                }
            }

            if(index >= _pools.Length)
            {
                Array.Resize(ref _pools, _pools.Length * 2);
            }

            _pools[pluginPool.Id.Id] = pool = new TPool();
            pool.InitPool(pluginPool);
            return pool;
        }
    }

    public void OnPluginUnloaded(UiPluginPool pluginPool)
    {
        if (pluginPool.Id.Id < _pools.Length)
        {
            _pools[pluginPool.Id.Id] = null;
        }
    }

    /// <summary>
    /// Clears the pool of all pooled objects and resets state to when the pool was first created
    /// </summary>
    public abstract void ClearPoolEntities();

    /// <summary>
    /// Wipes all the pools for this type
    /// </summary>
    public void RemoveAllPools() => _pools.Clear();

    public abstract bool HasPoolLeaked();

    public abstract void PrintLeaks();

    ///<inheritdoc/>
    public abstract void LogDebug(DebugLogger logger);
}