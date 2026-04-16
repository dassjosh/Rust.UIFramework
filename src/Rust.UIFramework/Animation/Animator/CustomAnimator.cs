using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class CustomAnimator<T> : BasePoolable, IAnimator<T>
{
    private Func<float, T> _func;

    public CustomAnimator() { }

    public CustomAnimator(Func<float, T> func)
    {
        _func = func;
    }
    
    public static CustomAnimator<T> Create(IUiFrameworkPlugin plugin, Func<float, T> func) => plugin.PluginPool.Get<CustomAnimator<T>>().Init(func);

    protected CustomAnimator<T> Init(Func<float, T> func)
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

public static class CustomAnimator
{
    public static CustomAnimator<T> Create<T>(IUiFrameworkPlugin plugin, Func<float, T> func) => CustomAnimator<T>.Create(plugin, func);
}