using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public class UiPluginPool : IDebugLoggable
{
    private readonly List<IPool> _pools = [];
    private PoolSettings _settings;

    internal PoolSettings Settings => _settings ?? DefaultSettings;
    internal readonly PluginId PluginId;
    internal readonly string PluginName;

    private static readonly PoolSettings DefaultSettings = new();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plugin">Plugin the pool is for</param>
    internal UiPluginPool(PluginId plugin)
    {
        PluginId = plugin;
        PluginName = plugin.FullName();
    }

    /// <summary>
    /// Sets the settings for the pools
    /// </summary>
    /// <param name="settings"></param>
    public void SetSettings(PoolSettings settings)
    {
        _settings = settings;
    }

    internal void AddPool(IPool pool) => _pools.Add(pool);
        
    /// <summary>
    /// Returns a pooled object of {T} type
    /// Must inherit from <see cref="BasePoolable"/> and have an empty default constructor
    /// </summary>
    /// <typeparam name="T">Type to be returned</typeparam>
    /// <returns>Pooled object of type T</returns>
    public T Get<T>() where T : BasePoolable, new() => (T)ObjectPool<T>.ForPlugin(this).Get();

    /// <summary>
    /// Returns a <see cref="BasePoolable"/> back into the pool
    /// </summary>
    /// <param name="value">Object to free</param>
    /// <typeparam name="T">Type of object being freed</typeparam>
    internal void Free<T>(T value) where T : BasePoolable, new() => ObjectPool<T>.ForPlugin(this).Free(value);

    /// <summary>
    /// Returns a pooled <see cref="UiPooledArray{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the Array</typeparam>
    /// <returns>Pooled Array</returns>
    public UiPooledArray<T> GetArray<T>(int minSize) => ArrayPool<T>.ForPlugin(this).Get(minSize);

    /// <summary>
    /// Free's a pooled <see cref="UiPooledArray{T}"/>
    /// </summary>
    /// <param name="array">Array to be freed</param>
    /// <typeparam name="T">Type of the Array</typeparam>
    public void FreeArray<T>(UiPooledArray<T> array) => ArrayPool<T>.ForPlugin(this).Free(array);
    
    /// <summary>
    /// Returns a pooled <see cref="List{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    public List<T> GetList<T>() => ListPool<T>.ForPlugin(this).Get();

    /// <summary>
    /// Free's a pooled <see cref="List{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    public void FreeList<T>(List<T> list) => ListPool<T>.ForPlugin(this).Free(list);
    
    /// <summary>
    /// Returns a pooled <see cref="ConcurrentList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the list</typeparam>
    /// <returns>Pooled List</returns>
    public ConcurrentList<T> GetConcurrentList<T>() => ConcurrentListPool<T>.ForPlugin(this).Get();

    /// <summary>
    /// Free's a pooled <see cref="ConcurrentList{T}"/>
    /// </summary>
    /// <param name="list">List to be freed</param>
    /// <typeparam name="T">Type of the list</typeparam>
    public void FreeConcurrentList<T>(ConcurrentList<T> list) => ConcurrentListPool<T>.ForPlugin(this).Free(list);
    
    /// <summary>
    /// Returns a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the HashSet</typeparam>
    /// <returns>Pooled HashSet</returns>
    public HashSet<T> GetHashSet<T>() => HashSetPool<T>.ForPlugin(this).Get();

    /// <summary>
    /// Free's a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <param name="set">HashSet to be freed</param>
    /// <typeparam name="T">Type of the HashSet</typeparam>
    public void FreeHashSet<T>(HashSet<T> set) => HashSetPool<T>.ForPlugin(this).Free(set);    
    
    /// <summary>
    /// Returns a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <typeparam name="T">Type for the HashSet</typeparam>
    /// <returns>Pooled HashSet</returns>
    public ConcurrentHashSet<T> GetConcurrentHashSet<T>() => ConcurrentHashSetPool<T>.ForPlugin(this).Get();

    /// <summary>
    /// Free's a pooled <see cref="HashSet{T}"/>
    /// </summary>
    /// <param name="set">HashSet to be freed</param>
    /// <typeparam name="T">Type of the HashSet</typeparam>
    public void FreeConcurrentHashSet<T>(ConcurrentHashSet<T> set) => ConcurrentHashSetPool<T>.ForPlugin(this).Free(set);

    /// <summary>
    /// Returns a pooled <see cref="Dictionary{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Dictionary</returns>
    public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>() => DictionaryPool<TKey, TValue>.ForPlugin(this).Get();

    /// <summary>
    /// Frees a pooled <see cref="Dictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="dic">Dictionary to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeDictionary<TKey, TValue>(Dictionary<TKey, TValue> dic) => DictionaryPool<TKey, TValue>.ForPlugin(this).Free(dic);
    
    /// <summary>
    /// Returns a pooled <see cref="ConcurrentDictionary{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled ConcurrentDictionary</returns>
    public ConcurrentDictionary<TKey, TValue> GetConcurrentDictionary<TKey, TValue>() => ConcurrentDictionaryPool<TKey, TValue>.ForPlugin(this).Get();

    /// <summary>
    /// Frees a pooled <see cref="ConcurrentDictionary{TKey, TValue}"/>
    /// </summary>
    /// <param name="dic">ConcurrentDictionary to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeConcurrentDictionary<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dic) => ConcurrentDictionaryPool<TKey, TValue>.ForPlugin(this).Free(dic);
    
    /// <summary>
    /// Returns a pooled <see cref="Hash{TKey,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Type for the key</typeparam>
    /// <typeparam name="TValue">Type for the value</typeparam>
    /// <returns>Pooled Hash</returns>
    public Hash<TKey, TValue> GetHash<TKey, TValue>() => HashPool<TKey, TValue>.ForPlugin(this).Get();

    /// <summary>
    /// Frees a pooled <see cref="Hash{TKey, TValue}"/>
    /// </summary>
    /// <param name="hash">Hash to be freed</param>
    /// <typeparam name="TKey">Type for key</typeparam>
    /// <typeparam name="TValue">Type for value</typeparam>
    public void FreeHash<TKey, TValue>(Hash<TKey, TValue> hash) => HashPool<TKey, TValue>.ForPlugin(this).Free(hash);

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    public StringBuilder GetStringBuilder() => StringBuilderPool.ForPlugin(this).Get();

    /// <summary>
    /// Returns a pooled <see cref="StringBuilder"/>
    /// </summary>
    /// <param name="initial">Initial text for the builder</param>
    /// <returns>Pooled <see cref="StringBuilder"/></returns>
    public StringBuilder GetStringBuilder(string initial)
    {
        StringBuilder builder = StringBuilderPool.ForPlugin(this).Get();
        builder.Append(initial);
        return builder;
    }

    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool
    /// </summary>
    /// <param name="sb">StringBuilder being freed</param>
    public void FreeStringBuilder(StringBuilder sb) => StringBuilderPool.ForPlugin(this).Free(sb);

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
    public MemoryStream GetMemoryStream() => MemoryStreamPool.ForPlugin(this).Get();

    /// <summary>
    /// Frees a <see cref="MemoryStream"/> back to the pool
    /// </summary>
    /// <param name="stream"><see cref="MemoryStream"/> being freed</param>
    public void FreeMemoryStream(MemoryStream stream) => MemoryStreamPool.ForPlugin(this).Free(stream);

    internal void OnPluginUnloaded()
    {
        for (int index = 0; index < _pools.Count; index++)
        {
            IPool pool = _pools[index];
            pool.OnPluginUnloaded(this);
        }
    }
        
    internal void Clear()
    {
        for (int index = 0; index < _pools.Count; index++)
        {
            IPool pool = _pools[index];
            pool.ClearPoolEntities();
        }
    }

    internal void Wipe()
    {
        for (int index = 0; index < _pools.Count; index++)
        {
            IPool pool = _pools[index];
            pool.RemoveAllPools();
        }
    }

    ///<inheritdoc/>
    public void LogDebug(DebugLogger logger)
    {
        logger.StartArray(PluginId.PluginName());
        foreach (IPool pool in _pools)
        {
            pool.LogDebug(logger);
        }
        logger.EndArray();
    }
    
    internal bool CheckForLeaks()
    {
        bool hasLeaked = false;
        for (int index = 0; index < _pools.Count; index++)
        {
            IPool pool = _pools[index];
            hasLeaked |= pool.HasPoolLeaked();
        }

        return hasLeaked;
    }
}