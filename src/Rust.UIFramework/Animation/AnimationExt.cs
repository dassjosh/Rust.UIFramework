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
    extension<T>(in AnimationRef<T> animation) where T : class, IAnimation
    {
        public AnimationRef<T> ComesAfter(in AnimationRef<IAnimation> target, bool includeRepeats = false, float? timeout = null, AnimationTimeoutAction action = AnimationTimeoutAction.StartAnimation)
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

        public AnimationRef<T> Repeat(int repeats, float repeatDelay = 0f)
        {
            if (animation.IsValid)
            {
                animation.Animation.Repeat ??= AnimationRepeat.Create(animation.Plugin);
                animation.Animation.Repeat.Repeats = repeats;
                animation.Animation.Repeat.RepeatDelay = repeatDelay;
            }

            return animation;
        }

        public AnimationRef<T> Delay(float delay)
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

        public UiTuple<AnimationRef<T>, TriggerDelayAnimation> TriggerDelay()
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

        public AnimationRef<T> TimeoutDelay(float timeout)
        {
            if (animation.IsValid)
            {
                animation.Timeout(timeout).InfiniteDelay();
            }

            return animation;
        }

        public AnimationRef<T> InfiniteDelay()
        {
            if (animation.IsValid)
            {
                animation.Animation.Delay ??= InfiniteAnimationDelay.Create(animation.Plugin);
            }

            return animation;
        }

        public AnimationRef<T> Duration(float seconds)
        {
            if (animation.IsValid)
            {
                animation.Animation.Duration ??= AnimationDuration.Create(animation.Plugin);
                animation.Animation.Duration.Duration = seconds;
            }

            return animation;
        }

        public AnimationRef<T> NoDuration()
        {
            if (animation.IsValid)
            {
                animation.Animation.Duration ??= InfiniteAnimationDuration.Default;
            }

            return animation;
        }

        public AnimationRef<T> Infinite()
        {
            if (animation.IsValid)
            {
                animation.Animation.Repeat ??= InfiniteAnimationRepeat.Default;
            }

            return animation;
        }

        public AnimationRef<T> Timeout(float seconds, AnimationTimeoutAction action = AnimationTimeoutAction.CancelAnimation)
        {
            if (animation.IsValid)
            {
                animation.Animation.Timeout ??= AnimationTimeout.Create(animation.Plugin);
                animation.Animation.Timeout.Timeout = seconds;
                animation.Animation.Timeout.Action = action;
            }

            return animation;
        }

        public AnimationRef<T> Bezier(CubicBezier points) => animation.WithEasing(points);
        public AnimationRef<T> Easing(Easing easing) => animation.WithEasing(easing);
        public AnimationRef<T> Linear() => animation.WithEasing(EasingFunctions.Linear);
        public AnimationRef<T> Ease() => animation.WithEasing(EasingFunctions.Ease);
        public AnimationRef<T> EaseIn() => animation.WithEasing(EasingFunctions.EaseIn);
        public AnimationRef<T> Out() => animation.WithEasing(animation.Easing.Out());
        public AnimationRef<T> InOut() => animation.WithEasing(animation.Easing.InOut());
        public AnimationRef<T> PingPong() => animation.WithEasing(animation.Easing.PingPong());
        public AnimationRef<T> PingPong(float frequency) => animation.WithEasing(animation.Easing.PingPong(frequency));
        public AnimationRef<T> Reverse() => animation.WithEasing(animation.Easing.Reverse());
        public AnimationRef<T> RepeatEasing(float repeats) => animation.WithEasing(animation.Easing.Repeat(repeats));
        public AnimationRef<T> Offset(float offset) => animation.WithEasing(animation.Easing.Offset(offset));
        public AnimationRef<T> Scaled(float min, float max) => animation.WithEasing(animation.Easing.Scaled(min, max));
        public AnimationRef<T> FreezeBefore(float freezePoint) => animation.WithEasing(animation.Easing.FreezeBefore(freezePoint));
        public AnimationRef<T> FreezeAfter(float freezePoint) => animation.WithEasing(animation.Easing.FreezeAfter(freezePoint));
        public AnimationRef<T> Blend(Easing end, float blendFactor) => animation.WithEasing(animation.Easing.Blend(end, blendFactor));
        public AnimationRef<T> Blend(Easing start, Easing end, float blendFactor) => animation.WithEasing(start.Blend(end, blendFactor));
        public AnimationRef<T> Clamp01() => animation.WithEasing(animation.Easing.Clamp01());
        public AnimationRef<T> Clamp(float min, float max) => animation.WithEasing(animation.Easing.Clamp(min, max));
    }

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

    extension<T>(in AnimationRef<T> animation) where T : class, IAnimation
    {
        public AnimationRef<T> DestroyAfter(in UiReference destroyTarget) => animation.DestroyAfter(destroyTarget.Name);

        public AnimationRef<T> DestroyAfter(string name)
        {
            animation.OnFinalized(a =>
            {
                BaseBuilder.DestroyUi(a.GetSendable().Send, name);
            });
            return animation;
        }

        public AnimationRef<T> On(AnimationEventType type, Action<T> callback)
        {
            if (animation.IsValid)
            {
                animation.Animation.Events.AddEvent(CallbackAnimationEvent<T>.Create(animation.Plugin, type, callback));
            }

            return animation;
        }

        public AnimationRef<T> OnQueued(Action<T> callback) => animation.On(AnimationEventType.Queued, callback);
        public AnimationRef<T> OnDelayed(Action<T> callback) => animation.On(AnimationEventType.Delayed, callback);
        public AnimationRef<T> OnStarted(Action<T> callback) => animation.On(AnimationEventType.Started, callback);
        public AnimationRef<T> OnRepeat(Action<T> callback) => animation.On(AnimationEventType.Repeat, callback);
        public AnimationRef<T> OnCompleted(Action<T> callback) => animation.On(AnimationEventType.Completed, callback);
        public AnimationRef<T> OnCanceled(Action<T> callback) => animation.On(AnimationEventType.Canceled, callback);
        public AnimationRef<T> OnTimeout(Action<T> callback) => animation.On(AnimationEventType.Timeout, callback);
        public AnimationRef<T> OnFinalized(Action<T> callback) => animation.On(AnimationEventType.Finalized, callback);

        public AnimationRef<T> WithDuration(IAnimationDuration duration)
        {
            if (animation.IsValid)
            {
                animation.Animation.Duration.TryReturnToPool();
                animation.Animation.Duration = duration;
            }

            return animation;
        }

        public AnimationRef<T> WithRepeat(IAnimationRepeat repeat)
        {
            if (animation.IsValid)
            {
                animation.Animation.Repeat.TryReturnToPool();
                animation.Animation.Repeat = repeat;
            }

            return animation;
        }

        public AnimationRef<T> WithEasing(Easing easing)
        {
            if (animation.IsValid)
            {
                animation.Animation.Easing = easing;
            }

            return animation;
        }

        public AnimationRef<T> WithInterpolator(IAnimationInterpolator interpolator)
        {
            if (animation.IsValid)
            {
                animation.Animation.Interpolator.TryReturnToPool();
                animation.Animation.Interpolator = interpolator;
            }

            return animation;
        }

        public AnimationRef<T> WithDelay(IAnimationDelay delay)
        {
            if (animation.IsValid)
            {
                animation.Animation.Delay.TryReturnToPool();
                animation.Animation.Delay = delay;
            }

            return animation;
        }

        public AnimationRef<T> WithTimeout(IAnimationTimeout timeout)
        {
            if (animation.IsValid)
            {
                animation.Animation.Timeout.TryReturnToPool();
                animation.Animation.Timeout = timeout;
            }

            return animation;
        }
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

    extension(ISendableAnimation animation)
    {
        internal bool IsSinglePlayer() => animation.Send.connection != null;
        internal ulong SinglePlayerId() => animation.Send.connection.userid;
    }
    
    extension<T>(AnimationRef<IElementAnimation<T>> animation) where T : BaseUiComponent
    {
        public AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(FieldSelector<TField, T> selector) => animation.IsValid ? animation.Animation.AnimateField(selector) : default;
    }
}