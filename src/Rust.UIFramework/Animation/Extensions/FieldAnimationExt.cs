using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class FieldAnimationExt
{
    extension<T>(in AnimationRef<IFieldAnimation<T>> field)
    {
        public AnimationRef<IFieldAnimation<T>> Lerp(T end) => field.IsValid ? field.Lerp(end, UiLerp.GetDefault<T>()) : field;
        public AnimationRef<IFieldAnimation<T>> Lerp(T end, UiLerp<T> lerp) => field.IsValid ? field.Lerp(field.Animation.Value, end, lerp) : field;
        public AnimationRef<IFieldAnimation<T>> Lerp(T start, T end) => field.IsValid ? field.Lerp(start, end, UiLerp.GetDefault<T>()) : field;
        public AnimationRef<IFieldAnimation<T>> Lerp(T start, T end, UiLerp<T> lerp) => field.IsValid ? field.WithAnimator(LerpAnimator.Create(field.Plugin, start, end, lerp)) : field;
        public AnimationRef<IFieldAnimation<T>> Lerp<TFrom>(TFrom start, TFrom end, IUiConvertable<T, TFrom> convertable) => field.IsValid ? field.Lerp(start, end, UiLerp.GetDefault<TFrom>(), convertable) : field;
        public AnimationRef<IFieldAnimation<T>> Lerp<TFrom>(TFrom start, TFrom end, UiLerp<TFrom> lerp, IUiConvertable<T, TFrom> convertable) 
            => field.IsValid ? field.WithAnimator(LerpAnimator.Create(field.Plugin, start, end, lerp).Convert(field.Plugin, convertable)) : field;

        public AnimationRef<IFieldAnimation<T>> WithAnimator(IAnimator<T> animator)
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

    extension(in AnimationRef<IFieldAnimation<string>> field)
    {
        public AnimationRef<IFieldAnimation<string>> Format<T>(T start, T end, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
            => field.IsValid ? field.Format(start, end, UiLerp.GetDefault<T>(), format, formatProvider) : field;

        public AnimationRef<IFieldAnimation<string>> Format<T>(T start, T end, UiLerp<T> lerp, string format = null, IFormatProvider formatProvider = null) where T : unmanaged, IFormattable
            => field.IsValid ? field.Lerp(start, end, lerp, FormattableConvertable<T>.Create(field.Plugin, format, formatProvider)) : field;
        
        /// <summary>
        /// Formats the string using an enumerable of animators or predefined objects
        /// </summary>
        /// <param name="format">string to be formatted</param>
        /// <param name="animators">enumerable of animators or object</param>
        /// <param name="formatProvider">Provider for the formatting</param>
        /// <returns></returns>
        public AnimationRef<IFieldAnimation<string>> Format(string format, IEnumerable<object> animators, IFormatProvider formatProvider = null)
            => field.IsValid ? field.WithAnimator(StringFormatAnimator.Create(field.Plugin, format, formatProvider, animators)) : field;
    }
    
    extension(in AnimationRef<IFieldAnimation<UiColor>> field)
    {
        public AnimationRef<IFieldAnimation<UiColor>> FadeIn() => field.IsValid ? field.Lerp(UiOpacity.None, UiOpacity.Full) : field;
        public AnimationRef<IFieldAnimation<UiColor>> FadeOut() => field.IsValid ? field.Lerp(UiOpacity.Full, UiOpacity.None) : field;
        public AnimationRef<IFieldAnimation<UiColor>> FadeIn(UiColor color) => field.IsValid ? field.Lerp(color, UiOpacity.None, UiOpacity.Full) : field;
        public AnimationRef<IFieldAnimation<UiColor>> FadeOut(UiColor color) => field.IsValid ? field.Lerp(color, UiOpacity.Full, UiOpacity.None) : field;
        public AnimationRef<IFieldAnimation<UiColor>> Lerp(UiColor color, UiOpacity start, UiOpacity end) => field.IsValid ? field.Lerp(start, end, color) : field;
        public AnimationRef<IFieldAnimation<UiColor>> Lerp(UiOpacity start, UiOpacity end)  => field.IsValid ? field.Lerp(field.Animation.Value, start, end) : field;
    }

    extension(in AnimationRef<IFieldAnimation<UiTranslate>> animation)
    {
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeX() => animation.IsValid ? animation.WithAnimator(KeyFrames.ShakeX) : animation;
        public AnimationRef<IFieldAnimation<UiTranslate>> ShakeY() => animation.IsValid ? animation.WithAnimator(KeyFrames.ShakeY) : animation;
        public AnimationRef<IFieldAnimation<UiTranslate>> Wobble() => animation.IsValid ? animation.WithAnimator(KeyFrames.Wobble.Translate) : animation;
        public AnimationRef<IFieldAnimation<UiTranslate>> HeadShake() => animation.IsValid ? animation.WithAnimator(KeyFrames.HeadShake.Translate) : animation;
    }

    extension(in AnimationRef<IFieldAnimation<UiRotation>> animation)
    {
        public AnimationRef<IFieldAnimation<UiRotation>> Wobble() => animation.IsValid ? animation.WithAnimator(KeyFrames.Wobble.Rotation) : animation;
        public AnimationRef<IFieldAnimation<UiRotation>> HeadShake() => animation.IsValid ? animation.WithAnimator(KeyFrames.HeadShake.Rotation) : animation;
    }
}