using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

/// <summary>
/// Hash extensions
/// </summary>
public static class DictionaryExt
{
    /// <param name="dic">Hash to have data removed from</param>
    /// <typeparam name="TKey">Key type of the hash</typeparam>
    /// <typeparam name="TValue">Value type of the hash</typeparam>
    extension<TKey, TValue>(IDictionary<TKey, TValue> dic)
    {
        /// <summary>
        /// Remove all records from the hash with the given predicate filter
        /// </summary>
        /// <param name="predicate">Filter of which values to remove</param>
        public void RemoveAll(Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            if (dic == null) throw new ArgumentNullException(nameof(dic));

            List<TKey> removeKeys = UiPool.Internal.GetList<TKey>();
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
            
            UiPool.Internal.FreeList(removeKeys);
        }

        /// <summary>
        /// Remove all records from the hash with the given predicate filter
        /// </summary>
        /// <param name="predicate">Filter of which values to remove</param>
        /// <param name="onRemove">Action to call when an element is removed</param>
        public void RemoveAll(Func<TValue, bool> predicate, Action<TValue> onRemove = null)
        {
            if (dic == null) throw new ArgumentNullException(nameof(dic));

            List<TKey> removeKeys = UiPool.Internal.GetList<TKey>();
            foreach (KeyValuePair<TKey, TValue> key in dic)
            {
                if (predicate(key.Value))
                {
                    removeKeys.Add(key.Key);
                    onRemove?.Invoke(key.Value);
                }
            }

            foreach (TKey key in removeKeys)
            {
                dic.Remove(key);
            }
            
            UiPool.Internal.FreeList(removeKeys);
        }
    }
}