using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

/// <summary>
/// Hash extensions
/// </summary>
public static class DictionaryExt
{
    /// <summary>
    /// Remove all records from the hash with the given predicate filter
    /// </summary>
    /// <param name="dic">Hash to have data removed from</param>
    /// <param name="predicate">Filter of which values to remove</param>
    /// <typeparam name="TKey">Key type of the hash</typeparam>
    /// <typeparam name="TValue">Value type of the hash</typeparam>
    public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dic, Func<KeyValuePair<TKey, TValue>, bool> predicate)
    {
        if (dic == null) throw new ArgumentNullException(nameof(dic));

        List<TKey> removeKeys = ListPool<TKey>.Instance.Get();
        foreach (KeyValuePair<TKey, TValue> key in dic)
        {
            if (predicate(key))
            {
                removeKeys.Add(key.Key);
            }
        }

        foreach (TKey key in removeKeys)
        {
            dic.Remove(key);
        }
            
        ListPool<TKey>.Instance.Free(removeKeys);
    }
        
    /// <summary>
    /// Remove all records from the hash with the given predicate filter
    /// </summary>
    /// <param name="hash">Hash to have data removed from</param>
    /// <param name="predicate">Filter of which values to remove</param>
    /// <param name="onRemove">Action to call when an element is removed</param>
    /// <typeparam name="TKey">Key type of the hash</typeparam>
    /// <typeparam name="TValue">Value type of the hash</typeparam>
    public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> hash, Func<TValue, bool> predicate, Action<TValue> onRemove = null)
    {
        if (hash == null) throw new ArgumentNullException(nameof(hash));

        List<TKey> removeKeys = ListPool<TKey>.Instance.Get();
        foreach (KeyValuePair<TKey, TValue> key in hash)
        {
            if (predicate(key.Value))
            {
                removeKeys.Add(key.Key);
                onRemove?.Invoke(key.Value);
            }
        }

        foreach (TKey key in removeKeys)
        {
            hash.Remove(key);
        }
            
        ListPool<TKey>.Instance.Free(removeKeys);
    }
}