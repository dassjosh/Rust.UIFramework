using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class SimpleAnimator<T> : BasePoolable, ISimpleAnimator<T>
{
    private Func<float, T> _func;

    public SimpleAnimator() { }

    public SimpleAnimator(Func<float, T> func)
    {
        _func = func;
    }
    
    public static SimpleAnimator<T> Create(IUiFrameworkPlugin plugin, Func<float, T> func) => plugin.PluginPool.Get<SimpleAnimator<T>>().Init(func);

    protected SimpleAnimator<T> Init(Func<float, T> func)
    {
        _func = func;
        return this;
    }
    
    public T Get(float progress) => _func(progress);

    protected override void EnterPool()
    {
        _func = null;
    }
}