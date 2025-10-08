using System;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Types;

internal interface ICacheSize
{
    uint Size { get; }
}

/// <summary>
/// A fixed-size cache that evicts the least recently used (LRU) items
/// once the total size of cached data exceeds the capacity.
/// </summary>
/// <typeparam name="TKey">The type of the keys in the cache.</typeparam>
/// <typeparam name="TValue">The type of the values in the cache.</typeparam>
internal sealed class LruDictionary<TKey, TValue> where TValue : ICacheSize
{
    public ulong CurrentSizeBytes { get; private set; }
    
    private readonly ulong _maxSizeBytes;
    
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _dictionary = new();
    private readonly LinkedList<KeyValuePair<TKey, TValue>> _list = [];

    /// <summary>
    /// Initializes a new instance of the LruCacheBySize class.
    /// </summary>
    /// <param name="maxSizeBytes">The maximum total size of values to store in bytes.</param>
    public LruDictionary(ulong maxSizeBytes)
    {
        if (maxSizeBytes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxSizeBytes), "Max size must be a positive number.");
        }

        _maxSizeBytes = maxSizeBytes;
    }

    /// <summary>
    /// Adds or updates a key/value pair. If adding the item exceeds the max size,
    /// least recently used items are evicted until the cache is back within its size limit.
    /// </summary>
    public void Add(TKey key, TValue value)
    {
        ulong itemSize = value.Size;
        KeyValuePair<TKey, TValue> kvp = new(key, value);

        // If the key already exists, update its value and size.
        if (_dictionary.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>> node))
        {
            // Adjust total size
            CurrentSizeBytes -= node.Value.Value.Size;
            CurrentSizeBytes += itemSize;

            // Update entry
            node.Value = kvp;

            // Move to front (most recently used)
            _list.Remove(node);
            _list.AddFirst(node);
        }
        else // Add a new item.
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> newNode = new(kvp);

            _list.AddFirst(newNode);
            _dictionary.Add(key, newNode);
            CurrentSizeBytes += itemSize;
        }
    }

    public void ClampToSize()
    {
        // Evict LRU items until the total size is within the capacity.
        while (CurrentSizeBytes > _maxSizeBytes && _list.Count > 0)
        {
            LinkedListNode<KeyValuePair<TKey, TValue>> lruNode = _list.Last;
            _list.RemoveLast();
            _dictionary.Remove(lruNode.Value.Key);
            CurrentSizeBytes -= lruNode.Value.Value.Size;
        }
    }

    /// <summary>
    /// Gets the value associated with the specified key and marks it as recently used.
    /// </summary>
    /// <returns>true if the key was found; otherwise, false.</returns>
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_dictionary.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>> node))
        {
            // Move the accessed item to the front of the list.
            _list.Remove(node);
            _list.AddFirst(node);
            value = node.Value.Value;
            return true;
        }


        value = default;
        return false;
    }

    /// <summary>
    /// Removes the value with the specified key from the cache.
    /// </summary>
    public bool Remove(TKey key)
    {
        if (_dictionary.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>> node))
        {
            CurrentSizeBytes -= node.Value.Value.Size;
            _dictionary.Remove(key);
            _list.Remove(node);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Clears all keys and values from the cache.
    /// </summary>
    public void Clear()
    {
        _dictionary.Clear();
        _list.Clear();
        CurrentSizeBytes = 0;
    }
}