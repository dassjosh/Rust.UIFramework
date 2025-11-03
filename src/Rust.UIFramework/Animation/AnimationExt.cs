using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public static class AnimationExt
{
    public static T ComesAfter<T>(this T animation, IAnimation target, bool includeRepeats = false, float? timeout = null, AnimationTimeoutAction action = AnimationTimeoutAction.StartAnimation) where T : IAnimation
    {
        TriggerDelayAnimation trigger = TriggerDelay(animation);
        if (!includeRepeats && target.Repeat is { Repeats: > 0 })
        {
            target.OnRepeat(_ => trigger.Trigger());
        }
        else
        {
            target.OnCompleted(_ => trigger.Trigger());
        }

        target.Timeout(timeout ?? 15f, action);
        return animation;
    }

    public static T Repeat<T>(this T animation, int repeats, float repeatDelay = 0f) where T : IAnimation
    {
        animation.Repeat ??= AnimationRepeat.Create(animation.Plugin);
        animation.Repeat.Repeats = repeats;
        animation.Repeat.RepeatDelay = repeatDelay;
        return animation;
    }
    
    public static T Delay<T>(this T animation, float delay) where T : IAnimation
    {
        animation.Delay ??= TimeDelayAnimation.Create(animation.Plugin);
        if (animation.Delay is TimeDelayAnimation time)
        {
            time.Delay = delay;
        }
        return animation;
    }
    
    public static UiTuple<T, TriggerDelayAnimation> TriggerDelay<T>(this T animation) where T : IAnimation
    {
        if (animation.Delay is TriggerDelayAnimation trigger)
        {
            return UiTuple.Create(animation, trigger);
        }
        
        animation.Delay = trigger = TriggerDelayAnimation.Create(animation.Plugin);
        return UiTuple.Create(animation, trigger);
    }

    public static T Duration<T>(this T animation, float seconds) where T : IAnimation
    {
        animation.Duration ??= AnimationDuration.Create(animation.Plugin);
        animation.Duration.Duration = seconds;
        return animation;
    }
    
    public static T Timeout<T>(this T animation, float seconds, AnimationTimeoutAction action = AnimationTimeoutAction.CancelAnimation) where T : IAnimation
    {
        animation.Timeout ??= AnimationTimeout.Create(animation.Plugin);
        animation.Timeout.Timeout = seconds;
        animation.Timeout.Action = action;
        return animation;
    }
    
    public static T Bezier<T>(this T animation, in BezierEasing points) where T : IAnimation => animation.WithEasing(points);
    public static T Linear<T>(this T animation) where T : IAnimation => animation.WithEasing(EasingFunctions.Linear);
    public static T Ease<T>(this T animation) where T : IAnimation => animation.WithEasing(BezierEasing.Ease);
    
    public static T Out<T>(this T animation) where T : IAnimation => animation.WithEasing(animation.Easing.Out());
    public static T InOut<T>(this T animation) where T : IAnimation => animation.WithEasing(animation.Easing.InOut());
    public static T PingPong<T>(this T animation) where T : IAnimation => animation.WithEasing(animation.Easing.PingPong());
    public static T PingPong<T>(this T animation, float frequency) where T : IAnimation => animation.WithEasing(animation.Easing.PingPong(frequency));
    public static T Reverse<T>(this T animation) where T : IAnimation => animation.WithEasing(animation.Easing.Reverse());
    public static T Repeat<T>(this T animation, float repeats) where T : IAnimation => animation.WithEasing(animation.Easing.Repeat(repeats));
    public static T Offset<T>(this T animation, float offset) where T : IAnimation => animation.WithEasing(animation.Easing.Offset(offset));
    public static T Scaled<T>(this T animation, float min, float max) where T : IAnimation => animation.WithEasing(animation.Easing.Scaled(min, max));
    public static T FreezeBefore<T>(this T animation, float freezePoint) where T : IAnimation => animation.WithEasing(animation.Easing.FreezeBefore(freezePoint));
    public static T FreezeAfter<T>(this T animation, float freezePoint) where T : IAnimation => animation.WithEasing(animation.Easing.FreezeAfter(freezePoint));
    public static T Blend<T>(this T animation, Easing end, float blendFactor) where T : IAnimation => animation.WithEasing(animation.Easing.Blend(end, blendFactor));
    public static T Blend<T>(this T animation, Easing start, Easing end, float blendFactor) where T : IAnimation => animation.WithEasing(start.Blend(end, blendFactor));
    
    public static T DestroyAfter<T, TElement>(this T animation) 
        where T : IElementAnimation<TElement> 
        where TElement : BaseUiComponent, new() 
        => animation.DestroyAfter(animation.Element.Reference);
    public static T DestroyAfter<T>(this T animation, in UiReference destroyTarget) where T : IAnimation => animation.DestroyAfter(destroyTarget.Name);
    public static T DestroyAfter<T>(this T animation, string name) where T : IAnimation
    {
        animation.OnFinalized(a =>
        {
            BaseBuilder.DestroyUi(a.GetSendable().Send, name);
        });
        return animation;
    }
    
    public static T On<T>(this T animation, AnimationEventType type, Action<T> callback) where T : IAnimation
    {
        animation.Events.AddEvent(CallbackAnimationEvent.Create(animation.Plugin, type, animate => callback((T)animate)));
        return animation;
    }
    
    public static T OnQueued<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Queued, callback);
    public static T OnDelayed<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Delayed, callback);
    public static T OnStarted<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Started, callback);
    public static T OnRepeat<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Repeat, callback);
    public static T OnCompleted<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Completed, callback);
    public static T OnCanceled<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Canceled, callback);
    public static T OnTimeout<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Timeout, callback);
    public static T OnFinalized<T>(this T animation, Action<T> callback) where T : IAnimation => animation.On(AnimationEventType.Finalized, callback);


    public static T WithDuration<T>(this T animation, IAnimationDuration duration) where T : IAnimation
    {
        animation.Duration.TryReturnToPool();
        animation.Duration = duration;
        return animation;
    }
    
    public static T WithRepeat<T>(this T animation, IAnimationRepeat repeat) where T : IAnimation
    {
        animation.Repeat.TryReturnToPool();
        animation.Repeat = repeat;
        return animation;
    }
    
    public static T WithEasing<T>(this T animation, Easing easing) where T : IAnimation
    {
        animation.Easing = easing;
        return animation;
    }
    
    public static T WithInterpolator<T>(this T animation, IAnimationInterpolator interpolator) where T : IAnimation
    {
        animation.Interpolator.TryReturnToPool();
        animation.Interpolator = interpolator;
        return animation;
    }
    
    public static T WithDelay<T>(this T animation, IAnimationDelay delay) where T : IAnimation
    {
        animation.Delay.TryReturnToPool();
        animation.Delay = delay;
        return animation;
    }
    
    public static T WithTimeout<T>(this T animation, IAnimationTimeout timeout) where T : IAnimation
    {
        animation.Timeout.TryReturnToPool();
        animation.Timeout = timeout;
        return animation;
    }

    public static IEnumerable<T> AllOfType<T>(this IAnimation animation) where T : IAnimation
    {
        if (animation is T type)
        {
            yield return type;
        }

        foreach (T child in animation.Children.SelectMany(c => c.AllOfType<T>()))
        {
            yield return child;
        }
    }

    internal static bool IsSinglePlayer(this ISendableAnimation animation) => animation.Send.connection != null;
    internal static ulong SinglePlayerId(this ISendableAnimation animation) => animation.Send.connection.userid;
}