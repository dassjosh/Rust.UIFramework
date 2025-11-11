using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public static class AnimationExt
{
    public static AnimationRef<T> ComesAfter<T>(this in AnimationRef<T> animation, in AnimationRef<IAnimation> target, bool includeRepeats = false, float? timeout = null, AnimationTimeoutAction action = AnimationTimeoutAction.StartAnimation) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            TriggerDelayAnimation trigger = TriggerDelay(animation);
            if (!target.IsValid)
            {
                trigger.Trigger();
            }
            else if (!includeRepeats && target.Animation.Repeat is { Repeats: > 0 })
            {
                target.OnRepeat(_ => trigger.Trigger());
            }
            else
            {
                target.OnFinalized(_ => trigger.Trigger());
            }

            target.Timeout(timeout ?? 15f, action);
        }

        return animation;
    }

    public static AnimationRef<T> Repeat<T>(this in AnimationRef<T> animation, int repeats, float repeatDelay = 0f) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Repeat ??= AnimationRepeat.Create(animation.Plugin);
            animation.Animation.Repeat.Repeats = repeats;
            animation.Animation.Repeat.RepeatDelay = repeatDelay;
        }

        return animation;
    }
    
    public static AnimationRef<T> Delay<T>(this in AnimationRef<T> animation, float delay) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Delay ??= TimeDelayAnimation.Create(animation.Plugin);
            if (animation.Animation.Delay is TimeDelayAnimation time)
            {
                time.Delay = delay;
            }
        }

        return animation;
    }
    
    public static UiTuple<AnimationRef<T>, TriggerDelayAnimation> TriggerDelay<T>(this in AnimationRef<T> animation) where T : class, IAnimation
    {
        if (!animation.IsValid)
        {
            return UiTuple.Create(animation, (TriggerDelayAnimation)null);
        }
        
        if (animation.Animation.Delay is TriggerDelayAnimation trigger)
        {
            return UiTuple.Create(animation, trigger);
        }
        
        animation.Animation.Delay = trigger = TriggerDelayAnimation.Create(animation.Plugin);
        return UiTuple.Create(animation, trigger);
    }
    
    public static AnimationRef<T> TimeoutDelay<T>(this in AnimationRef<T> animation, float timeout) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Timeout(timeout).InfiniteDelay();
        }

        return animation;
    }
    
    public static AnimationRef<T> InfiniteDelay<T>(this in AnimationRef<T> animation) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Delay ??= InfiniteAnimationDelay.Create(animation.Plugin);
        }

        return animation;
    }

    public static AnimationRef<T> Duration<T>(this in AnimationRef<T> animation, float seconds) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Duration ??= AnimationDuration.Create(animation.Plugin);
            animation.Animation.Duration.Duration = seconds;
        }

        return animation;
    }
    
    public static AnimationRef<T> NoDuration<T>(this in AnimationRef<T> animation) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Duration ??= InfiniteAnimationDuration.Default;
        }

        return animation;
    }
    
    public static AnimationRef<T> Infinite<T>(this in AnimationRef<T> animation) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Repeat ??= InfiniteAnimationRepeat.Default;
        }

        return animation;
    }
    
    public static AnimationRef<T> Timeout<T>(this in AnimationRef<T> animation, float seconds, AnimationTimeoutAction action = AnimationTimeoutAction.CancelAnimation) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Timeout ??= AnimationTimeout.Create(animation.Plugin);
            animation.Animation.Timeout.Timeout = seconds;
            animation.Animation.Timeout.Action = action;
        }

        return animation;
    }
    
    public static AnimationRef<T> Bezier<T>(this in AnimationRef<T> animation, CubicBezier points) where T : class, IAnimation => animation.WithEasing(points);
    public static AnimationRef<T> Easing<T>(this in AnimationRef<T> animation, Easing easing) where T : class, IAnimation => animation.WithEasing(easing);
    public static AnimationRef<T> Linear<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(EasingFunctions.Linear);
    public static AnimationRef<T> Ease<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(EasingFunctions.Ease);
    public static AnimationRef<T> EaseIn<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(EasingFunctions.EaseIn);
    
    public static AnimationRef<T> Out<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(animation.Easing.Out());
    public static AnimationRef<T> InOut<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(animation.Easing.InOut());
    public static AnimationRef<T> PingPong<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(animation.Easing.PingPong());
    public static AnimationRef<T> PingPong<T>(this in AnimationRef<T> animation, float frequency) where T : class, IAnimation => animation.WithEasing(animation.Easing.PingPong(frequency));
    public static AnimationRef<T> Reverse<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(animation.Easing.Reverse());
    public static AnimationRef<T> RepeatEasing<T>(this in AnimationRef<T> animation, float repeats) where T : class, IAnimation => animation.WithEasing(animation.Easing.Repeat(repeats));
    public static AnimationRef<T> Offset<T>(this in AnimationRef<T> animation, float offset) where T : class, IAnimation => animation.WithEasing(animation.Easing.Offset(offset));
    public static AnimationRef<T> Scaled<T>(this in AnimationRef<T> animation, float min, float max) where T : class, IAnimation => animation.WithEasing(animation.Easing.Scaled(min, max));
    public static AnimationRef<T> FreezeBefore<T>(this in AnimationRef<T> animation, float freezePoint) where T : class, IAnimation => animation.WithEasing(animation.Easing.FreezeBefore(freezePoint));
    public static AnimationRef<T> FreezeAfter<T>(this in AnimationRef<T> animation, float freezePoint) where T : class, IAnimation => animation.WithEasing(animation.Easing.FreezeAfter(freezePoint));
    public static AnimationRef<T> Blend<T>(this in AnimationRef<T> animation, Easing end, float blendFactor) where T : class, IAnimation => animation.WithEasing(animation.Easing.Blend(end, blendFactor));
    public static AnimationRef<T> Blend<T>(this in AnimationRef<T> animation, Easing start, Easing end, float blendFactor) where T : class, IAnimation => animation.WithEasing(start.Blend(end, blendFactor));
    public static AnimationRef<T> Clamp01<T>(this in AnimationRef<T> animation) where T : class, IAnimation => animation.WithEasing(animation.Easing.Clamp01());
    public static AnimationRef<T> Clamp<T>(this in AnimationRef<T> animation, float min, float max) where T : class, IAnimation => animation.WithEasing(animation.Easing.Clamp(min, max));
    
    public static AnimationRef<T> DestroyAfter<T, TElement>(this in AnimationRef<T> animation) 
        where T : class,IElementAnimation<TElement> 
        where TElement : BaseUiComponent, new()
    {
        if (animation.IsValid)
        {
            animation.DestroyAfter(animation.Animation.Element.Reference);
        }

        return animation;
    }

    public static AnimationRef<T> DestroyAfter<T>(this in AnimationRef<T> animation, in UiReference destroyTarget) where T : class, IAnimation => animation.DestroyAfter(destroyTarget.Name);
    public static AnimationRef<T> DestroyAfter<T>(this in AnimationRef<T> animation, string name) where T : class, IAnimation
    {
        animation.OnFinalized(a =>
        {
            BaseBuilder.DestroyUi(a.GetSendable().Send, name);
        });
        return animation;
    }
    
    public static AnimationRef<T> On<T>(this in AnimationRef<T> animation, AnimationEventType type, Action<T> callback) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Events.AddEvent(CallbackAnimationEvent<T>.Create(animation.Plugin, type, callback));
        }

        return animation;
    }
    
    public static AnimationRef<T> OnQueued<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Queued, callback);
    public static AnimationRef<T> OnDelayed<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Delayed, callback);
    public static AnimationRef<T> OnStarted<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Started, callback);
    public static AnimationRef<T> OnRepeat<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Repeat, callback);
    public static AnimationRef<T> OnCompleted<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Completed, callback);
    public static AnimationRef<T> OnCanceled<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Canceled, callback);
    public static AnimationRef<T> OnTimeout<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Timeout, callback);
    public static AnimationRef<T> OnFinalized<T>(this in AnimationRef<T> animation, Action<T> callback) where T : class, IAnimation => animation.On(AnimationEventType.Finalized, callback);


    public static AnimationRef<T> WithDuration<T>(this in AnimationRef<T> animation, IAnimationDuration duration) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Duration.TryReturnToPool();
            animation.Animation.Duration = duration;
        }

        return animation;
    }
    
    public static AnimationRef<T> WithRepeat<T>(this in AnimationRef<T> animation, IAnimationRepeat repeat) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Repeat.TryReturnToPool();
            animation.Animation.Repeat = repeat;
        }

        return animation;
    }
    
    public static AnimationRef<T> WithEasing<T>(this in AnimationRef<T> animation, Easing easing) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Easing = easing;
        }

        return animation;
    }
    
    public static AnimationRef<T> WithInterpolator<T>(this in AnimationRef<T> animation, IAnimationInterpolator interpolator) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Interpolator.TryReturnToPool();
            animation.Animation.Interpolator = interpolator;
        }

        return animation;
    }
    
    public static AnimationRef<T> WithDelay<T>(this in AnimationRef<T> animation, IAnimationDelay delay) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Delay.TryReturnToPool();
            animation.Animation.Delay = delay;
        }

        return animation;
    }
    
    public static AnimationRef<T> WithTimeout<T>(this in AnimationRef<T> animation, IAnimationTimeout timeout) where T : class, IAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Timeout.TryReturnToPool();
            animation.Animation.Timeout = timeout;
        }

        return animation;
    }
    
    public static AnimationRef<T> SetTracked<T>(this in AnimationRef<T> animation, bool tracked) where T : class, IElementAnimation
    {
        if (animation.IsValid)
        {
            animation.Animation.Tracked = tracked;
        }

        return animation;
    }

    public static void AllOfType<T>(this IAnimation animation, List<T> types) where T : class, IAnimation
    {
        if (animation is T type)
        {
            types.Add(type);
        }

        for (int index = 0; index < animation.Children.Count; index++)
        {
            IAnimation child = animation.Children[index];
            child.AllOfType(types);
        }
    }

    internal static bool IsSinglePlayer(this ISendableAnimation animation) => animation.Send.connection != null;
    internal static ulong SinglePlayerId(this ISendableAnimation animation) => animation.Send.connection.userid;
}