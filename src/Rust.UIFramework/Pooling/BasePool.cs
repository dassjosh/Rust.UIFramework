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
public abstract class BasePool<TPooled, TPool> : IPool<TPooled> 
    where TPooled : class 
    where TPool : BasePool<TPooled, TPool>, new()
{
    /// <summary>
    /// Owner Plugin Pool
    /// </summary>
    private UiPluginPool _pluginPool;
    
    UiPluginPool IPool.PluginPool => _pluginPool;
    
    private TPooled[] _pool;
    private readonly object _lock = new();
    private int _index;
    private PoolSize _size;
    private bool _isInitialized;
    private LeakHandler _leakHandler;
    
    private static readonly ConcurrentDictionary<PluginId, TPool> Pools = new();
    
    private void InitPool(UiPluginPool pluginPool)
    {
        lock (_lock)
        {
            if (_isInitialized)
            {
                return;
            }
            _size = GetPoolSize(pluginPool.Settings);
            InvalidPoolException.ThrowIfInvalidPoolSize(_size);
            _pluginPool = pluginPool;
            pluginPool.AddPool(this);
            _pool = new TPooled[_size.StartingSize];
            _isInitialized = true;
            UiFrameworkExtension.GlobalLogger.Debug("Creating Pool. Plugin ID: {0} Type: {1}", pluginPool.PluginName, GetType().GetRealTypeName());
        }
    }
    
    /// <summary>
    /// Returns the pool size from the pool settings for the pool
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    protected abstract PoolSize GetPoolSize(PoolSettings settings);
    
    /// <summary>
    /// Returns a pool for the given plugin pool
    /// </summary>
    /// <param name="pluginPool"><see cref="UiPluginPool"/> to get the pool from</param>
    /// <returns></returns>
    public static TPool ForPlugin(UiPluginPool pluginPool)
    {
        TPool pool = Pools.GetOrAdd(pluginPool.PluginId, CreatePool);
        if (!pool._isInitialized)
        {
            pool.InitPool(pluginPool);
        }
        return pool;
    }
    
    private static TPool CreatePool(PluginId id) => new();

    /// <summary>
    /// Returns an element from the pool if it exists else it creates a new one
    /// </summary>
    /// <returns></returns>
    public TPooled Get()
    {
        TPooled item = null;
        lock (_lock)
        {
            int index = _index;
            if (index == _pool.Length && _size.CanResize(_pool.Length))
            {
                int nextSize = PoolSize.GetNextSize(_pool.Length);
                UiFrameworkExtension.GlobalLogger.Debug("{0} Resizing Pool {1} Current Size: {2} Next Size: {3}", _pluginPool.PluginName, GetType(), _pool.Length, nextSize);
                Array.Resize(ref _pool, nextSize);
            }
                
            if (index < _pool.Length)
            {
                item = _pool[index];
                _pool[index] = null;
                _index++;
            }
            else 
            {
                LeakHandler leak = _leakHandler ??= new LeakHandler(_pluginPool.PluginId, GetType().ToString());
                leak.OnLeak(index, _pool.Length);
            }
        }
                
        item ??= CreateNew();
                
        OnGetItem(item);
        return item;
    }

    /// <summary>
    /// Creates new type of T
    /// </summary>
    /// <returns>Newly created type of T</returns>
    protected abstract TPooled CreateNew();

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    public void Free(TPooled item) => Free(ref item);

    /// <summary>
    /// Frees an item back to the pool
    /// </summary>
    /// <param name="item">Item being freed</param>
    private void Free(ref TPooled item)
    {
        if (item == null)
        {
            return;
        }

        if (!OnFreeItem(ref item))
        {
            return;
        }

        lock (_lock)
        {
            if (_index != 0)
            {
                _pool[--_index] = item;
            }
        }

        item = null;
    }

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
    protected virtual bool OnFreeItem(ref TPooled item) => true;

    public void OnPluginUnloaded(UiPluginPool pluginPool)
    {
        Pools.TryRemove(pluginPool.PluginId, out TPool _);
    }

    /// <summary>
    /// Clears the pool of all pooled objects and resets state to when the pool was first created
    /// </summary>
    public void ClearPoolEntities()
    {
        lock (_lock)
        {
            for (int i = _pool.Length - 1; i >= 0; i--)
            {
                _pool[i] = null;
            }
            _index = 0;
        }
    }

    /// <summary>
    /// Wipes all the pools for this type
    /// </summary>
    public void RemoveAllPools()
    {
        Pools.Clear();
    }

    public void CheckForLeaks()
    {
        if (_index != 0)
        {
            UiFrameworkExtension.GlobalLogger.Error("Plugin: {0} Pool: {1} Has Leaked {2}/{3} Entities", _pluginPool.PluginName, GetType().GetRealTypeName(), _index, _pool.Length);
        }
    }
}