using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for Hash&lt;TKey, TValue&gt;
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
internal class HashPool<TKey, TValue> : BasePool<Hash<TKey, TValue>, HashPool<TKey, TValue>>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
    protected override Hash<TKey, TValue> CreateNew() => new();

    ///<inheritdoc/>
    protected override bool OnFreeItem(Hash<TKey, TValue> item)
    {
        item.Clear();
        return true;
    }
}