using System;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Animation;

public static class FieldAnimationExt
{
    public static AnimationRef<IFieldAnimation<T>> Lerp<T>(this in AnimationRef<IFieldAnimation<T>> field, T end) => field.IsValid ? field.Lerp(end, UiLerp.GetDefault<T>()) : field;
    public static AnimationRef<IFieldAnimation<T>> Lerp<T>(this in AnimationRef<IFieldAnimation<T>> field, T end, UiLerp<T> lerp) => field.IsValid ? field.Lerp(field.Animation.Value, end, lerp) : field;
    public static AnimationRef<IFieldAnimation<T>> Lerp<T>(this in AnimationRef<IFieldAnimation<T>> field, T start, T end) => field.IsValid ? field.Lerp(start, end, UiLerp.GetDefault<T>()) : field;
    public static AnimationRef<IFieldAnimation<T>> Lerp<T>(this in AnimationRef<IFieldAnimation<T>> field, T start, T end, UiLerp<T> lerp) => field.IsValid ? field.WithAnimator(LerpAnimator<T>.Create(field.Plugin, start, end, lerp)) : field;

    public static AnimationRef<IFieldAnimation<string>> Lerp<T>(this in AnimationRef<IFieldAnimation<string>> field, T start, T end, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
        => field.IsValid ? field.Lerp(start, end, UiLerp.GetDefault<T>(), format, formatProvider) : field;
    public static AnimationRef<IFieldAnimation<string>> Lerp<T>(this in AnimationRef<IFieldAnimation<string>> field, T start, T end, UiLerp<T> lerp, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
        => field.IsValid ? field.WithAnimator(FormattableLerpAnimator<T>.Create(field.Plugin, start, end, lerp, format, formatProvider)) : field;
    
    public static AnimationRef<IFieldAnimation<T>> WithAnimator<T>(this in AnimationRef<IFieldAnimation<T>> field, IAnimator<T> animator)
    {
        if (!field.IsValid)
        {
            return field;
        }
        
        field.Animation.Interpolator.Animator.TryReturnToPool();
        field.Animation.Interpolator.Animator = animator;
        return field;
    }
}