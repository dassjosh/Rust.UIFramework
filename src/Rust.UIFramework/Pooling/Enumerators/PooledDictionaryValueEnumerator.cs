using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling;

public sealed class PooledDictionaryValueEnumerator<TKey, TValue> : BasePooledEnumerator<TValue>
{
    private PooledDictionaryEnumerator<TKey, TValue> _enumerator;
    
    public override TValue Current { get; protected set; }
    
    public static PooledDictionaryValueEnumerator<TKey, TValue> Create(IUiFrameworkPlugin plugin, PooledDictionaryEnumerator<TKey, TValue> enumerator) => Create(plugin.PluginPool, enumerator);
    public static PooledDictionaryValueEnumerator<TKey, TValue> Create(UiPluginPool pool, PooledDictionaryEnumerator<TKey, TValue> enumerator) => pool.Get<PooledDictionaryValueEnumerator<TKey, TValue>>().Init(enumerator);

    private PooledDictionaryValueEnumerator<TKey, TValue> Init(PooledDictionaryEnumerator<TKey, TValue> enumerator)
    {
        _enumerator = enumerator;
        return this;
    }
    
    public override bool MoveNext()
    {
        if (_enumerator.MoveNext())
        {
            Current = _enumerator.Current.Value;
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