using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Animation;

public class UiPositionLerpAnimator : BaseLerpAnimator<UiPosition>
{
    public static UiPositionLerpAnimator Create(UiPluginPool pool, in UiPosition start, in UiPosition end) => pool.Get<UiPositionLerpAnimator>().Init(start, end);
    public UiPositionLerpAnimator Init(in UiPosition start, in UiPosition end) => Init<UiPositionLerpAnimator>(start, end, (start, end, progress) => UiPosition.LerpUnclamped(start, end, progress));
}

public class UiOffsetLerpAnimator : BaseLerpAnimator<UiOffset>
{
    public static UiOffsetLerpAnimator Create(UiPluginPool pool, in UiOffset start, in UiOffset end) => pool.Get<UiOffsetLerpAnimator>().Init(start, end);
    public UiOffsetLerpAnimator Init(in UiOffset start, in UiOffset end) => Init<UiOffsetLerpAnimator>(start, end, (start, end, progress) => UiOffset.LerpUnclamped(start, end, progress));
}

public class UiColorLerpAnimator : BaseLerpAnimator<UiColor>
{
    public static UiColorLerpAnimator Create(UiPluginPool pool, UiColor start, UiColor end) => pool.Get<UiColorLerpAnimator>().Init(start, end);
    public UiColorLerpAnimator Init(UiColor start, UiColor end) => Init<UiColorLerpAnimator>(start, end, UiColor.Lerp);
}

public abstract class BaseLerpAnimator<T> : BasePoolable, IAnimator<T>
{
    public T Start;
    public T End;
    public Func<T, T, float, T> Lerp;
    
    protected TAnimator Init<TAnimator>(T start, T end, Func<T, T, float, T> lerp) where TAnimator : BaseLerpAnimator<T>, new()
    {
        Start = start;
        End = end;
        Lerp = lerp;
        return (TAnimator)this;
    }

    public T Get(float progress) => Lerp(Start, End, progress);

    protected override void EnterPool()
    {
        Start = default;
        End = default;
        Lerp = default;
    }
} 