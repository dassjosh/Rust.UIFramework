using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Animation;

public class UiPositionLerpAnimator : BaseLerpAnimator<UiPosition>
{
    public static UiPositionLerpAnimator Create(in UiPosition start, in UiPosition end) => Create<UiPositionLerpAnimator>(start, end, (start, end, progress) => UiPosition.LerpUnclamped(start, end, progress));
}

public class UiOffsetLerpAnimator : BaseLerpAnimator<UiOffset>
{
    public static UiOffsetLerpAnimator Create(in UiOffset start, in UiOffset end) => Create<UiOffsetLerpAnimator>(start, end, (start, end, progress) => UiOffset.LerpUnclamped(start, end, progress));
}

public class UiColorLerpAnimator : BaseLerpAnimator<UiColor>
{
    public static UiColorLerpAnimator Create(UiColor start, UiColor end) => Create<UiColorLerpAnimator>(start, end, UiColor.Lerp);
}

public abstract class BaseLerpAnimator<T> : BasePoolable, IAnimator<T>
{
    public T Start;
    public T End;
    public Func<T, T, float, T> Lerp;
    
    protected static TAnimator Create<TAnimator>(T start, T end, Func<T, T, float, T> lerp) where TAnimator : BaseLerpAnimator<T>, new()
    {
        TAnimator animator = UiFrameworkPool.Get<TAnimator>();
        animator.Init(start, end, lerp);
        return animator;
    }

    private void Init(T start, T end, Func<T, T, float, T> lerp)
    {
        Start = start;
        End = end;
        Lerp = lerp;
    }

    public T Get(float progress) => Lerp(Start, End, progress);

    protected override void EnterPool()
    {
        Start = default;
        End = default;
        Lerp = default;
    }
} 