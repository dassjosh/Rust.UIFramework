using System.Collections.Generic;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public sealed class PooledDictionaryEnumerator<TKey, TValue> : BasePooledListEnumerator<KeyValuePair<TKey, TValue>>
{
    public PooledDictionaryKeyEnumerator<TKey, TValue> Keys => PooledDictionaryKeyEnumerator<TKey, TValue>.Create(PluginPool, this);
    public PooledDictionaryValueEnumerator<TKey, TValue> Values => PooledDictionaryValueEnumerator<TKey, TValue>.Create(PluginPool, this);
    
    public static PooledDictionaryEnumerator<TKey, TValue> Create(IUiFrameworkPlugin plugin, IList<KeyValuePair<TKey, TValue>> list) => plugin.PluginPool.Get<PooledDictionaryEnumerator<TKey, TValue>>().Init(list);

    private PooledDictionaryEnumerator<TKey, TValue> Init(IList<KeyValuePair<TKey, TValue>> list)
    {
        List = list;
        return this;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        if (List is BasePoolable poolable)
        {
            poolable.TryDispose();
        }
    }
}