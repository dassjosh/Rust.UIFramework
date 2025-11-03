using System;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Animation;

public static class FieldAnimationExt
{
    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T end) => field.Lerp(end, UiLerp.GetDefault<T>());
    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T end, UiLerp<T> lerp) => field.Lerp(field.Value, end, lerp);
    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T start, T end) => field.Lerp(start, end, UiLerp.GetDefault<T>());
    public static IFieldAnimation<T> Lerp<T>(this IFieldAnimation<T> field, T start, T end, UiLerp<T> lerp) => field.WithAnimator(LerpAnimator<T>.Create(field.Plugin, start, end, lerp));

    public static IFieldAnimation<string> Lerp<T>(this IFieldAnimation<string> field, T start, T end, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
        => field.Lerp(start, end, UiLerp.GetDefault<T>(), format, formatProvider);
    public static IFieldAnimation<string> Lerp<T>(this IFieldAnimation<string> field, T start, T end, UiLerp<T> lerp, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
        => field.WithAnimator(FormattableLerpAnimator<T>.Create(field.Plugin, start, end, lerp, format, formatProvider));
    
    public static IFieldAnimation<T> WithAnimator<T>(this IFieldAnimation<T> field, IAnimator<T> animator)
    {
        field.Interpolator.Animator.TryReturnToPool();
        field.Interpolator.Animator = animator;
        return field;
    }
}