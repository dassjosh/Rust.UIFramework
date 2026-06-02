using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ConcurrentDictionaryPool<TKey, TValue>() : BaseObjectPool<ConcurrentDictionary<TKey, TValue>>(ConcurrentDictionaryPoolPolicy.Instance)
{
    private sealed class ConcurrentDictionaryPoolPolicy : IPooledObjectPolicy<ConcurrentDictionary<TKey, TValue>>
    {
        public static readonly ConcurrentDictionaryPoolPolicy Instance = new();

        public int GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
        public ConcurrentDictionary<TKey, TValue> Create() => new();
        public void Get(ConcurrentDictionary<TKey, TValue> item) { }
        public bool Return(ConcurrentDictionary<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}