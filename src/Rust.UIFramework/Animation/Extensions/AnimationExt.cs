using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public AnimationRef<T> Repeat(int repeats, float repeatDelay = 0f) => animation.IsValid ? animation.WithRepeat(AnimationRepeat.Create(animation.Plugin, repeats, repeatDelay)) : animation;

        public AnimationRef<T> Delay(float delay) => animation.IsValid ? animation.WithDelay(TimeDelayAnimation.Create(animation.Plugin, delay)) : animation;

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

            trigger = TriggerDelayAnimation.Create(animation.Plugin);
            animation.WithDelay(trigger);
            return UiTuple.Create(animation, trigger);
        }

        public AnimationRef<T> TimeoutDelay(float timeout) => animation.IsValid ? animation.Timeout(timeout).InfiniteDelay() : animation;

        public AnimationRef<T> InfiniteDelay() => animation.WithDelay(InfiniteAnimationDelay.Instance);

        public AnimationRef<T> Duration(float seconds) => animation.IsValid ? animation.WithDuration(AnimationDuration.Create(animation.Plugin, seconds)) : animation;
        public AnimationRef<T> FramesDuration(int frames) => animation.IsValid ? animation.WithDuration(FrameDuration.Create(animation.Plugin, frames)) : animation;
        public AnimationRef<T> NoDuration() => animation.WithDuration(InfiniteAnimationDuration.Instance);
        public AnimationRef<T> Infinite() => animation.NoDuration();

        public AnimationRef<T> Timeout(float seconds, AnimationTimeoutAction action = AnimationTimeoutAction.CancelAnimation) 
            => animation.IsValid ? animation.WithTimeout(AnimationTimeout.Create(animation.Plugin, seconds, action)) : animation;

        #region Timing Functions
        public AnimationRef<T> Bezier(CubicBezier bezier) => animation.Timing(bezier);
        public AnimationRef<T> Bezier(ConfigurableBezier bezier) => animation.Timing(bezier);
        public AnimationRef<T> Step(StepTiming step) => animation.Timing(step);
        public AnimationRef<T> Linear(LinearTiming linear) => animation.Timing(linear);
        public AnimationRef<T> Linear(FrozenLinearTiming linear) => animation.Timing(linear);
        public AnimationRef<T> Linear() => animation.Timing(TimingFunctions.Linear);
        public AnimationRef<T> Ease() => animation.Timing(TimingFunctions.Ease);
        public AnimationRef<T> EaseIn() => animation.Timing(TimingFunctions.EaseIn);
        public AnimationRef<T> SharpCurve() => animation.Timing(TimingFunctions.SharpCurve);
        public AnimationRef<T> Pulse() => animation.Timing(TimingFunctions.Pulse);
        public AnimationRef<T> Quad() => animation.Timing(TimingFunctions.Quad);
        public AnimationRef<T> Cubic() => animation.Timing(TimingFunctions.Cubic);
        public AnimationRef<T> Quart() => animation.Timing(TimingFunctions.Quart);
        public AnimationRef<T> Poly(int times) => animation.Timing(TimingFunctions.Poly(times));
        public AnimationRef<T> Circle() => animation.Timing(TimingFunctions.Circle);
        public AnimationRef<T> Sin() => animation.Timing(TimingFunctions.Sin);
        public AnimationRef<T> Bounce() => animation.Timing(TimingFunctions.Bounce);
        public AnimationRef<T> Exponential() => animation.Timing(TimingFunctions.Exponential);
        public AnimationRef<T> Back() => animation.Timing(TimingFunctions.Back);
        public AnimationRef<T> Elastic1() => animation.Timing(TimingFunctions.Elastic1);
        public AnimationRef<T> Elastic(float bounciness) => animation.Timing(TimingFunctions.Elastic(bounciness));
        public AnimationRef<T> Steps(int steps) => animation.Timing(TimingFunctions.Steps(steps));
        #endregion

        #region Timing Modifiers
        public AnimationRef<T> Out() => animation.Timing(animation.TimingFunction.Out());
        public AnimationRef<T> InOut() => animation.Timing(animation.TimingFunction.InOut());
        public AnimationRef<T> PingPong() => animation.Timing(animation.TimingFunction.PingPong());
        public AnimationRef<T> PingPong(float frequency) => animation.Timing(animation.TimingFunction.PingPong(frequency));
        public AnimationRef<T> Reverse() => animation.Timing(animation.TimingFunction.Reverse());
        public AnimationRef<T> RepeatTiming(float repeats) => animation.Timing(animation.TimingFunction.Repeat(repeats));
        public AnimationRef<T> Offset(float offset) => animation.Timing(animation.TimingFunction.Offset(offset));
        public AnimationRef<T> Scaled(float min, float max) => animation.Timing(animation.TimingFunction.Scaled(min, max));
        public AnimationRef<T> FreezeBefore(float freezePoint) => animation.Timing(animation.TimingFunction.FreezeBefore(freezePoint));
        public AnimationRef<T> FreezeAfter(float freezePoint) => animation.Timing(animation.TimingFunction.FreezeAfter(freezePoint));
        public AnimationRef<T> Blend(TimingFunction end, float blendFactor) => animation.Timing(animation.TimingFunction.Blend(end, blendFactor));
        public AnimationRef<T> Blend(TimingFunction start, TimingFunction end, float blendFactor) => animation.Timing(start.Blend(end, blendFactor));
        public AnimationRef<T> Clamp01() => animation.Timing(animation.TimingFunction.Clamp01());
        public AnimationRef<T> Clamp(float min, float max) => animation.Timing(animation.TimingFunction.Clamp(min, max));
        public AnimationRef<T> StepStart() => animation.Timing(animation.TimingFunction.StepStart());
        public AnimationRef<T> StepEnd() => animation.Timing(animation.TimingFunction.StepEnd());
        public AnimationRef<T> Combine(TimingFunction next) => animation.Timing(animation.TimingFunction.Combine(next));
        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AnimationRef<T> Timing(TimingFunction timing) => animation.WithTiming(timing);
        
        public AnimationRef<T> DestroyAfter(in UiReference destroyTarget) => animation.DestroyAfter(destroyTarget.Name);
        public AnimationRef<T> DestroyAfter(string name) => animation.OnFinalized(a => { BaseBuilder.DestroyUi(a.GetSendable().Send, name); });
        
        public AnimationRef<T> OnQueued(Action<T> callback) => animation.On(AnimationEventType.Queued, callback);
        public AnimationRef<T> OnDelayed(Action<T> callback) => animation.On(AnimationEventType.Delayed, callback);
        public AnimationRef<T> OnStarted(Action<T> callback) => animation.On(AnimationEventType.Started, callback);
        public AnimationRef<T> OnRepeat(Action<T> callback) => animation.On(AnimationEventType.Repeat, callback);
        public AnimationRef<T> OnCompleted(Action<T> callback) => animation.On(AnimationEventType.Completed, callback);
        public AnimationRef<T> OnCanceled(Action<T> callback) => animation.On(AnimationEventType.Canceled, callback);
        public AnimationRef<T> OnTimeout(Action<T> callback) => animation.On(AnimationEventType.Timeout, callback);
        public AnimationRef<T> OnFinalized(Action<T> callback) => animation.On(AnimationEventType.Finalized, callback);
        
        public AnimationRef<T> On(AnimationEventType type, Action<T> callback)
        {
            if (animation.IsValid)
            {
                animation.Animation.Events.AddEvent(CallbackAnimationEvent<T>.Create(animation.Plugin, type, callback));
            }

            return animation;
        }

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

        public AnimationRef<T> WithTiming(TimingFunction timing)
        {
            if (animation.IsValid)
            {
                animation.Animation.Timing = timing;
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

        internal bool TryGetSinglePlayer(out ulong id)
        {
            if (animation.IsSinglePlayer())
            {
                id = animation.Send.connection.userid;
                return true;
            }

            id = default;
            return false;
        }
    }
    
    extension<T>(AnimationRef<IElementAnimation<T>> animation) where T : BaseUiComponent
    {
        public AnimationRef<IFieldAnimation<TField>> AnimateField<TField>(FieldSelector<TField, T> selector) => animation.IsValid ? animation.Animation.AnimateField(selector) : default;
        
        public AnimationRef<IElementAnimation<T>> DestroyAfter() => animation.IsValid ? animation.DestroyAfter(animation.Animation.Element.Reference) : animation;
    }
}