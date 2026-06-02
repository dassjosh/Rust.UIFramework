using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPluginPool : IDebugLoggable
{
    internal PoolSettings Settings;
    internal readonly PluginId PluginId;
    internal readonly string PluginName;

    private readonly object _poolLock = new();
    private BasePool[] _pools = new BasePool[32];
    private BasePool[] _customPools = [];

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plugin">Plugin the pool is for</param>
    internal UiPluginPool(PluginId plugin)
    {
        PluginId = plugin;
        PluginName = plugin.FullName();
        Settings = PoolSettings.Default;
    }

    internal void SetSettings(PoolSettings settings)
    {
        Settings = settings;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TPool GetPool<TPool>() where TPool : BasePool, new() => GetPool<TPool>(ref _pools, PoolId<TPool>.Id);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TPool GetPool<TPool>(ref BasePool[] pools, int poolId) where TPool : BasePool, new()
    {
        BasePool pool;
        if (poolId < pools.Length)
        {
            pool = pools[poolId];
            if (pool is not null)
            {
                return (TPool)pool;
            }
        }

        lock (_poolLock)
        {
            if (poolId < pools.Length)
            {
                pool = pools[poolId];
                if (pool is not null)
                {
                    return (TPool)pool;
                }
            }

            if(poolId >= pools.Length)
            {
                ResizePool(ref pools, poolId);
            }

            return CreatePool<TPool>(pools, poolId);
        }
    }

    private static void ResizePool(ref BasePool[] pools, int poolId)
    {
        int newSize = (int)BitOperations.RoundUpToPowerOf2((uint)poolId + 1);
        Array.Resize(ref pools, newSize);
    }

    private TPool CreatePool<TPool>(BasePool[] pools, int poolId) where TPool : BasePool, new()
    {
        TPool newPool = new();
        pools[poolId] = newPool;
        newPool.InitPool(this);
        return newPool;
    }

    public TPool CustomPool<T, TPool>()
        where T : class
        where TPool : CustomPool<T, TPool>, new()
        => GetPool<TPool>(ref _customPools, CustomPoolId<TPool>.Id);

    public T CustomGet<T, TPool>()
        where T : class
        where TPool : CustomPool<T, TPool>, new() =>
        CustomPool<T, TPool>().Get();

    public void CustomFree<T, TPool>(T value)
        where T : class
        where TPool : CustomPool<T, TPool>, new() =>
        CustomPool<T, TPool>().Free(value);
        
    /// <summary>
    /// Returns a pooled object of {T} type
    /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
    /// </summary>
    /// <typeparam name="T">Type to be returned</typeparam>
    /// <returns>Pooled object of type T</returns>
    public T Get<T>() where T : BasePoolable, new() => (T)GetPool<ObjectPool<T>>().Get();

    /// <summary>
    /// Returns a <see cref="BasePoolable"/> back into the pool
    /// </summary>
    /// <param name="value">Object to free</param>
    /// <typeparam name="T">Type of object being freed</typeparam>
    internal void Free<T>(T value) where T : BasePoolable, new() => GetPool<ObjectPool<T>>().Free(value);

    /// <summary>
    /// Returns a pooled <see cref="UiPooledArray{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the Array</typeparam>
    /// <returns>Pooled Array</returns>
    public UiPooledArray<T> GetArray<T>(int minSize) => GetPool<ArrayPool<T>>().Get(minSize);

    /// <summary>
    /// Free's a pooled <see cref="UiPooledArray{T}"/>
    /// </summary>
    /// <param name="array">Array to be freed</param>
    /// <typeparam name="T">Type of the Array</typeparam>
    public void FreeArray<T>(UiPooledArray<T> array) => GetPool<ArrayPool<T>>().Free(array);
    
    /// <summary>
    /// Returns a pooled <see cref="List{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    public List<T> GetList<T>() => GetPool<ListPool<T>>().Get();

    /// <summary>
    /// Free's a pooled <see cref="List{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    public void FreeList<T>(List<T> list) => GetPool<ListPool<T>>().Free(list);
    
    /// <summary>
    /// Returns a pooled <see cref="ConcurrentList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    public ConcurrentList<T> GetConcurrentList<T>() => GetPool<ConcurrentListPool<T>>().Get();

    /// <summary>
    /// Free's a pooled <see cref="ConcurrentList{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    public void FreeConcurrentList<T>(ConcurrentList<T> list) => GetPool<ConcurrentListPool<T>>().Free(list);
    
    /// <summary>
    /// Returns a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the HashSet</typeparam>
    /// <returns>Pooled HashSet</returns>
    public HashSet<T> GetHashSet<T>() => GetPool<HashSetPool<T>>().Get();

    /// <summary>
    /// Free's a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <param name="set">HashSet to be freed</param>
    /// <typeparam name="T">Type of the HashSet</typeparam>
    public void FreeHashSet<T>(HashSet<T> set) => GetPool<HashSetPool<T>>().Free(set);
    
    /// <summary>
    /// Returns a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the HashSet</typeparam>
    /// <returns>Pooled HashSet</returns>
    public ConcurrentHashSet<T> GetConcurrentHashSet<T>() => GetPool<ConcurrentHashSetPool<T>>().Get();

    /// <summary>
    /// Free's a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <param name="set">HashSet to be freed</param>
    /// <typeparam name="T">Type of the HashSet</typeparam>
    public void FreeConcurrentHashSet<T>(ConcurrentHashSet<T> set) => GetPool<ConcurrentHashSetPool<T>>().Free(set);

    /// <summary>
    /// Returns a pooled <see cref="Dictionary{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Dictionary</returns>
    public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>() => GetPool<DictionaryPool<TKey, TValue>>().Get();

    /// <summary>
    /// Frees a pooled <see cref="Dictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="dic">Dictionary to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeDictionary<TKey, TValue>(Dictionary<TKey, TValue> dic) => GetPool<DictionaryPool<TKey, TValue>>().Free(dic);
    
    /// <summary>
    /// Returns a pooled <see cref="ConcurrentDictionary{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled ConcurrentDictionary</returns>
    public ConcurrentDictionary<TKey, TValue> GetConcurrentDictionary<TKey, TValue>() => GetPool<ConcurrentDictionaryPool<TKey, TValue>>().Get();

    /// <summary>
    /// Frees a pooled <see cref="ConcurrentDictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="dic">ConcurrentDictionary to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeConcurrentDictionary<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dic) => GetPool<ConcurrentDictionaryPool<TKey, TValue>>().Free(dic);
    
    /// <summary>
    /// Returns a pooled <see cref="Hash{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Hash</returns>
    public Hash<TKey, TValue> GetHash<TKey, TValue>() => GetPool<HashPool<TKey, TValue>>().Get();

    /// <summary>
    /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <param name="hash">Hash to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash) => GetPool<HashPool<TKey, TValue>>().Free(hash);

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    public StringBuilder GetStringBuilder() => GetPool<StringBuilderPool>().Get();

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <param name="initial">Initial text for the builder</param>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    public StringBuilder GetStringBuilder(string initial)
    {
        StringBuilder builder = GetPool<StringBuilderPool>().Get();
        builder.Append(initial);
        return builder;
    }

    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool
    /// </summary>
    /// <param name="sb">StringBuilder being freed</param>
    public void FreeStringBuilder(StringBuilder sb) => GetPool<StringBuilderPool>().Free(sb);

    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool returning the built <see cref="string"/>
    /// </summary>
    /// <param name="sb"><see cref="StringBuilder"/> being freed</param>
    public string ToStringAndFree(StringBuilder sb)
    {
        string result = sb?.ToString();
        FreeStringBuilder(sb);
        return result;
    }
    
    /// <summary>
    /// Returns a pooled <see cref="MemoryStream"/>
    /// </summary>
    /// <returns>Pooled <see cref="MemoryStream"/></returns>
    public MemoryStream GetMemoryStream() => GetPool<MemoryStreamPool>().Get();

    /// <summary>
    /// Frees a <see cref="MemoryStream"/> back to the pool
    /// </summary>
    /// <param name="stream"><see cref="MemoryStream"/> being freed</param>
    public void FreeMemoryStream(MemoryStream stream) => GetPool<MemoryStreamPool>().Free(stream);

    internal void OnPluginUnloaded()
    {
        Wipe();
    }
        
    internal void Clear()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            IPool pool = _pools[index];
            pool?.ClearPool();
        }
    }

    internal void Wipe()
    {
        _pools.Clear();
    }

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.StartArray(PluginId.PluginName());
        foreach (IPool pool in _pools)
        {
            pool?.LogDebug(logger);
        }
        logger.EndArray();
    }
    
    internal bool HasLeaks()
    {
        bool hasLeaked = false;
        for (int index = 0; index < _pools.Length; index++)
        {
            IPool pool = _pools[index];
            hasLeaked |= pool?.HasPoolLeaked() ?? false;
        }

        return hasLeaked;
    }

    internal void PrintLeaks()
    {
        for (int index = 0; index < _pools.Length; index++)
        {
            IPool pool = _pools[index];
            pool?.PrintLeaks();
        }
    }
}