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
    
    protected readonly object PoolLock = new();
    
    private bool _isInitialized;
    
    private static TPool[] Pools = new TPool[32];
    private static readonly object CreatePoolLock = new();
    
    protected void InitPool(UiPluginPool pluginPool)
    {
        lock (PoolLock)
        {
            if (_isInitialized)
            {
                return;
            }
            PluginPool = pluginPool;
            pluginPool.AddPool(this);
            _isInitialized = true;
            OnInit(pluginPool);
            UiFrameworkExtension.GlobalLogger.Debug("Creating Pool. Plugin ID: {0} Type: {1}", pluginPool.PluginName, GetType().GetRealTypeName());
        }
    }

    protected abstract void OnInit(UiPluginPool pluginPool);
    
    /// <summary>
    /// Returns a pool for the given plugin pool
    /// </summary>
    /// <param name="pluginPool"><see cref="UiPluginPool"/> to get the pool from</param>
    /// <returns></returns>
    public static TPool ForPlugin(UiPluginPool pluginPool)
    {
        TPool pool = Pools[pluginPool.Id.Id];
        if (pool is { _isInitialized: true })
        {
            return pool;
        }

        lock (CreatePoolLock)
        {
            pool = Pools[pluginPool.Id.Id];
            if (pool is { _isInitialized: true })
            {
                return pool;
            }
            
            if(Pools.Length <= pluginPool.Id.Id)
            {
                Array.Resize(ref Pools, Pools.Length * 2);
            }
            
            pool = Pools[pluginPool.Id.Id] = new TPool();
            pool.InitPool(pluginPool);
            return pool;
        }
    }

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public abstract void Free(TPooled item);

    /// <summary>
    /// Called when an item is retrieved from the pool
    /// </summary>
    /// <param name="item">Item being retrieved</param>
    protected virtual void OnGetItem(TPooled item) { }

    /// <summary>
    /// Returns if an item can be freed to the pool
    /// </summary>
    /// <param name="item">Item to be freed</param>
    /// <returns>True if can be freed; false otherwise</returns>
    protected virtual bool OnFreeItem(TPooled item) => true;

    public void OnPluginUnloaded(UiPluginPool pluginPool)
    {
        if (pluginPool.Id.Id < Pools.Length)
        {
            Pools[pluginPool.Id.Id] = null;
        }
    }

    /// <summary>
    /// Clears the pool of all pooled objects and resets state to when the pool was first created
    /// </summary>
    public abstract void ClearPoolEntities();

    /// <summary>
    /// Wipes all the pools for this type
    /// </summary>
    public void RemoveAllPools()
    {
        Pools.Clear();
    }

    public abstract bool HasPoolLeaked();

    public abstract void PrintLeaks();

    ///<inheritdoc/>
    public abstract void LogDebug(DebugLogger logger);
}