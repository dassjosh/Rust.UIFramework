using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;

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

    ///<inheritdoc/>
    public abstract void LogDebug(DebugLogger logger);
}

public abstract class BaseObjectPool<TPooled, TPool> : BasePool<TPooled, TPool>, IObjectPool<TPooled> 
    where TPooled : class
    where TPool : BasePool<TPooled, TPool>, new()
{
    private PoolSize _size;
    private TPooled[] _pool;
    private int _index;
    private LeakHandler _leakHandler;

    protected override void OnInit(UiPluginPool pluginPool)
    {
        _size = GetPoolSize(pluginPool.Settings);
        InvalidPoolException.ThrowIfInvalidPoolSize(_size);
        _pool = new TPooled[_size.StartingSize];
    }
    
    /// <summary>
    /// Returns the pool size from the pool settings for the pool
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    protected abstract PoolSize GetPoolSize(PoolSettings settings);
    
    /// <summary>
    /// Returns an element from the pool if it exists else it creates a new one
    /// </summary>
    /// <returns></returns>
    public TPooled Get()
    {
        TPooled item = null;
        lock (PoolLock)
        {
            int index = _index;
            if (index == _pool.Length && _size.CanResize(_pool.Length))
            {
                int nextSize = PoolSize.GetNextSize(_pool.Length);
                UiFrameworkExtension.GlobalLogger.Debug("{0} Resizing Pool {1} Current Size: {2} Next Size: {3}", PluginPool.PluginName, GetType(), _pool.Length, nextSize);
                Array.Resize(ref _pool, nextSize);
            }
                
            if (index < _pool.Length)
            {
                item = _pool[index];
                _pool[index] = null;
                _index = index + 1;
            }
            else
            {
                HandleLeak(index);
            }
        }
                
        item ??= CreateNew();
                
        OnGetItem(item);
        return item;
    }
    
    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public override void Free(TPooled item)
    {
        if (item == null)
        {
            return;
        }

        if (!OnFreeItem(item))
        {
            return;
        }

        lock (PoolLock)
        {
#if DEBUG
            for (int index = 0; index < _pool.Length; index++)
            {
                TPooled pooled = _pool[index];
                if (pooled == item)
                {
                    throw new Exception();
                }
            }
#endif

            if (_index != 0)
            {
                _pool[--_index] = item;
            }
        }
    }
    
    /// <summary>
    /// Creates new type of T
    /// </summary>
    /// <returns>Newly created type of T</returns>
    protected abstract TPooled CreateNew();
    
    public override void ClearPoolEntities()
    {
        lock (PoolLock)
        {
            _pool.Clear();
        }
    }

    private void HandleLeak(int index)
    {
        LeakHandler leak = _leakHandler ??= new LeakHandler(PluginPool.PluginId, GetType().ToString());
        leak.OnLeak(index, _pool.Length);
    }
    
    public override bool HasPoolLeaked()
    {
        if (_index != 0)
        {
            UiFrameworkExtension.GlobalLogger.Error("Plugin: {0} Pool: {1} Has Leaked {2}/{3} Entities", PluginPool.PluginName, GetType().GetRealTypeName(), _index, _pool.Length);
            return true;
        }

        return false;
    }

    ///<inheritdoc/>
    public override void LogDebug(DebugLogger logger)
    {
        logger.AppendLine($"{GetType().GetRealTypeName()}: Pool: {_pool.Length - _index}/{_pool.Length}");
    }
}