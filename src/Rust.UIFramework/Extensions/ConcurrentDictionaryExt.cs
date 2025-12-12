using System.Collections.Concurrent;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ConcurrentDictionaryExt
{
    extension<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary)
    {
        public UiPooledArray<KeyValuePair<TKey, TValue>> ToArrayPooled(IUiFrameworkPlugin plugin)
        {
            int count = dictionary.Count * 2;
            UiPooledArray<KeyValuePair<TKey, TValue>> array = plugin.PluginPool.GetArray<KeyValuePair<TKey, TValue>>(count);
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, 0);
            return array;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetEnumeratorPooled(IUiFrameworkPlugin plugin)
        {
            return PooledListEnumerator<KeyValuePair<TKey, TValue>>.Create(plugin, dictionary.ToArrayPooled(plugin));
        }
    }
}