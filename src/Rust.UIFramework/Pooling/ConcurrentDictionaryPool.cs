using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Pooling;

internal class ConcurrentDictionaryPool<TKey, TValue> : BasePool<ConcurrentDictionary<TKey, TValue>, ConcurrentDictionaryPool<TKey, TValue>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
    protected override ConcurrentDictionary<TKey, TValue> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(ref ConcurrentDictionary<TKey, TValue> item)
    {
        item.Clear();
        return true;
    }
}