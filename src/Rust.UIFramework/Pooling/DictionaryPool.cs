using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Pooling;

internal class DictionaryPool<TKey, TValue> : BasePool<Dictionary<TKey, TValue>, DictionaryPool<TKey, TValue>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
    protected override Dictionary<TKey, TValue> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(ref Dictionary<TKey, TValue> item)
    {
        item.Clear();
        return true;
    }
}