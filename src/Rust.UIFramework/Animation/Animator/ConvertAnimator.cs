using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class ConvertAnimator<TTo, TFrom> : BasePoolable, IAnimator<TTo>
{
    private IAnimator<TFrom> _animator;
    private IUiConvertable<TTo, TFrom> _converter;

    public ConvertAnimator() { }

    public ConvertAnimator(IAnimator<TFrom> animator, IUiConvertable<TTo, TFrom> converter)
    {
        _animator = animator;
        _converter = converter;
    }
    
    public static ConvertAnimator<TTo, TFrom> Create(IUiFrameworkPlugin plugin, IAnimator<TFrom> animator, IUiConvertable<TTo, TFrom> converter) => plugin.PluginPool.Get<ConvertAnimator<TTo, TFrom>>().Init(animator, converter);

    protected ConvertAnimator<TTo, TFrom> Init(IAnimator<TFrom> animator, IUiConvertable<TTo, TFrom> converter) 
    {
        _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        return this;
    }

    public TTo Get(float progress) => _converter.Convert(_animator.Get(progress));

    protected override void EnterPool()
    {
        _animator.TryReturnToPool();
        _animator = null;
        _converter.TryReturnToPool();
        _converter = null;
    }
}

public static class ConvertAnimator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConvertAnimator<TTo, TFrom> Create<TTo, TFrom>(IUiFrameworkPlugin plugin, IAnimator<TFrom> animator, IUiConvertable<TTo, TFrom> converter) => ConvertAnimator<TTo, TFrom>.Create(plugin, animator, converter);
}