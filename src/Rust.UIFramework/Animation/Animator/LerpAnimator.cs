using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Rotation;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class UiPositionLerpAnimator : BaseLerpAnimator<UiPosition>
{
    public UiPositionLerpAnimator() { }
    public UiPositionLerpAnimator(UiPosition start, UiPosition end, Func<UiPosition, UiPosition, float, UiPosition> lerp) : base(start, end, lerp) { }
    public static UiPositionLerpAnimator Create(UiPluginPool pool, in UiPosition start, in UiPosition end) 
        => Create<UiPositionLerpAnimator>(pool, start, end, (start, end, progress) => UiPosition.LerpUnclamped(start, end, progress)); 
}

public class UiOffsetLerpAnimator : BaseLerpAnimator<UiOffset>
{
    public UiOffsetLerpAnimator() { }
    public UiOffsetLerpAnimator(UiOffset start, UiOffset end, Func<UiOffset, UiOffset, float, UiOffset> lerp) : base(start, end, lerp) { }
    public static UiOffsetLerpAnimator Create(UiPluginPool pool, in UiOffset start, in UiOffset end) 
        => Create<UiOffsetLerpAnimator>(pool, start, end, (start, end, progress) => UiOffset.LerpUnclamped(start, end, progress)); 
}

public class UiColorLerpAnimator : BaseLerpAnimator<UiColor>
{
    public UiColorLerpAnimator() { }
    public UiColorLerpAnimator(UiColor start, UiColor end, Func<UiColor, UiColor, float, UiColor> lerp) : base(start, end, lerp) { }
    public static UiColorLerpAnimator Create(UiPluginPool pool, UiColor start, UiColor end) => Create<UiColorLerpAnimator>(pool, start, end, UiColor.Lerp);
}

public class UiRotationLerpAnimator : BaseLerpAnimator<UiRotation>
{
    public UiRotationLerpAnimator() { }
    public UiRotationLerpAnimator(UiRotation start, UiRotation end, Func<UiRotation, UiRotation, float, UiRotation> lerp) : base(start, end, lerp) { }
    public static UiRotationLerpAnimator Create(UiPluginPool pool, UiRotation start, UiRotation end) => Create<UiRotationLerpAnimator>(pool, start, end, UiRotation.LerpUnclamped);
}

public class StringLerpAnimator : BaseLerpAnimator<string>
{
    public StringLerpAnimator() { }
    public StringLerpAnimator(string start, string end, Func<string, string, float, string> lerp) : base(start, end, lerp) { }
    public static StringLerpAnimator Create(UiPluginPool pool, string start, string end) => pool.Get<StringLerpAnimator>().Init<StringLerpAnimator>(start, end, LevenshteinDistanceExt.Lerp);
}

public class LerpAnimator<T> : BaseLerpAnimator<T>
{
    public LerpAnimator() { }
    public LerpAnimator(T start, T end, Func<T, T, float, T> lerp) : base(start, end, lerp) { }
    
    public static LerpAnimator<T> Create(UiPluginPool pool, T start, T end, Func<T, T, float, T> lerp) => Create<LerpAnimator<T>>(pool, start, end, lerp);
}

public abstract class BaseLerpAnimator<T> : BasePoolable, ISimpleAnimator<T>
{
    public T Start;
    public T End;
    public Func<T, T, float, T> Lerp;

    protected BaseLerpAnimator() { }

    protected BaseLerpAnimator(T start, T end, Func<T, T, float, T> lerp)
    {
        Start = start;
        End = end;
        Lerp = lerp;
    }
    
    protected static TAnimator Create<TAnimator>(UiPluginPool pool, T start, T end, Func<T, T, float, T> lerp) where TAnimator : BaseLerpAnimator<T>, new()
        => pool.Get<TAnimator>().Init<TAnimator>(start, end, lerp);
    
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
        Lerp = null;
    }
} 