using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public sealed class PooledDictionaryKeyEnumerator<TKey, TValue> : BasePooledEnumerator<TKey>
{
    private PooledDictionaryEnumerator<TKey, TValue> _enumerator;
    
    public override TKey Current { get; protected set; }

    public static PooledDictionaryKeyEnumerator<TKey, TValue> Create(IUiFrameworkPlugin plugin, PooledDictionaryEnumerator<TKey, TValue> enumerator) => Create(plugin.PluginPool, enumerator);
    public static PooledDictionaryKeyEnumerator<TKey, TValue> Create(UiPluginPool pool, PooledDictionaryEnumerator<TKey, TValue> enumerator) => pool.Get<PooledDictionaryKeyEnumerator<TKey, TValue>>().Init(enumerator);

    private PooledDictionaryKeyEnumerator<TKey, TValue> Init(PooledDictionaryEnumerator<TKey, TValue> enumerator)
    {
        _enumerator = enumerator;
        return this;
    }
    
    public override bool MoveNext()
    {
        if (_enumerator.MoveNext())
        {
            Current = _enumerator.Current.Key;
            return true;
        }

        return false;
    }

    public override void Reset()
    {
        _enumerator.Reset();
    }

    protected override void EnterPool()
    {
        _enumerator.TryDispose();
    }
}