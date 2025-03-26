using System.Collections.Concurrent;

namespace Oxide.Ext.UiFramework.Pooling;

public class ConcurrentDictionaryPool<TKey, TValue> : BasePool<ConcurrentDictionary<TKey, TValue>>
{
    public static readonly IPool<ConcurrentDictionary<TKey, TValue>> Instance = new ConcurrentDictionaryPool<TKey, TValue>();

    private ConcurrentDictionaryPool() : base(128) { }

    protected override ConcurrentDictionary<TKey, TValue> CreateNew() => [];

    ///<inheritdoc/>
    protected override bool OnFreeItem(ref ConcurrentDictionary<TKey, TValue> item)
    {
        item.Clear();
        return true;
    }
}