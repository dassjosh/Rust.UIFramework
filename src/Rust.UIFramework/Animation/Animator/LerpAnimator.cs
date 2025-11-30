using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class LerpAnimator<T> : BasePoolable, IAnimator<T>
{
    public T Start;
    public T End;
    public UiLerp<T> Lerp;

    public LerpAnimator() { }
    public LerpAnimator(T start, T end) : this(start, end, UiLerp.GetDefaultOrError<T>()) { }
    public LerpAnimator(T start, T end, UiLerp<T> lerp)
    {
        Start = start;
        End = end;   
        Lerp = lerp ?? throw new ArgumentNullException(nameof(lerp), "lerp cannot be null. Please pass a valid lerp function.");
    }

    public static LerpAnimator<T> Create(IUiFrameworkPlugin plugin, T start, T end) => Create(plugin, start, end, UiLerp.GetDefaultOrError<T>());
    
    public static LerpAnimator<T> Create(IUiFrameworkPlugin plugin, T start, T end, UiLerp<T> lerp)
        => plugin.PluginPool.Get<LerpAnimator<T>>().Init(start, end, lerp);
    
    protected LerpAnimator<T> Init(T start, T end, UiLerp<T> lerp)
    {
        Start = start;
        End = end;
        Lerp = lerp;
        return this;
    }

    public T Get(float progress) => Lerp(Start, End, progress);

    protected override void EnterPool()
    {
        Start = default;
        End = default;
        Lerp = null;
    }
}

public static class LerpAnimator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LerpAnimator<T> Create<T>(IUiFrameworkPlugin plugin, T start, T end) => LerpAnimator<T>.Create(plugin, start, end);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LerpAnimator<T> Create<T>(IUiFrameworkPlugin plugin, T start, T end, UiLerp<T> lerp) => LerpAnimator<T>.Create(plugin, start, end, lerp);
}