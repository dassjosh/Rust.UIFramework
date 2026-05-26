using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents a pool for Hash&lt;TKey, TValue&gt;
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
internal class HashPool<TKey, TValue>() : BaseObjectPool<Hash<TKey, TValue>>(HashPoolPolicy.Instance)
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.DictionaryPoolSize;
    
    private sealed class HashPoolPolicy : IPooledObjectPolicy<Hash<TKey, TValue>>
    {
        public static readonly HashPoolPolicy Instance = new();
        
        public Hash<TKey, TValue> Create() => [];
        public void Get(Hash<TKey, TValue> item) { }
        public bool Return(Hash<TKey, TValue> item)
        {
            item.Clear();
            return true;
        }
    }
}