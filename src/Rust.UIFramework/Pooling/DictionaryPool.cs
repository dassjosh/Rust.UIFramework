using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

internal class DictionaryPool<TKey, TValue>() : BaseObjectPool<Dictionary<TKey, TValue>>(DictionaryPoolPolicy.Instance)
{
    private sealed class DictionaryPoolPolicy : IPooledObjectPolicy<Dictionary<TKey, TValue>>
    {
        public static readonly DictionaryPoolPolicy Instance = new();

        public int GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
        public Dictionary<TKey, TValue> Create() => [];
        public void Get(Dictionary<TKey, TValue> item) { }
        public bool Return(Dictionary<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}