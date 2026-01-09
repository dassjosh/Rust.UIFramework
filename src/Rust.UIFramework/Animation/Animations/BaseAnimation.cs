using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation : BasePoolable, IAnimation
{
    public AnimationId Id { get; private set; }
    public IUiFrameworkPlugin Plugin { get; private set;  }
    public AnimationState State { get; private set; }
    public IAnimationDuration Duration { get; set; }
    public IAnimationRepeat Repeat { get; set; }
    public TimingFunction Timing { get; set; }
    public IAnimationInterpolator Interpolator { get; set; }
    public IAnimationDelay Delay { get; set; }
    public IAnimationTimeout Timeout { get; set; }
    public IAnimationEvents Events { get; }
    public IAnimation Parent { get; private set;  }
    public virtual bool HasChanged => Interpolator?.HasChanged ?? false;
    public AnimationCancelOption CancelOption { get; set; }

    public IReadOnlyList<IAnimation> Children => _children;
    private readonly List<BaseAnimation> _children = [];
    
    public IAnimationTime Time => _time ?? Parent?.Time ?? Singleton<AnimationTime>.Instance;
    private IAnimationTime _time;
    
    protected BaseAnimation()
    {
        Events = new AnimationEvents(this);
    }
    
    protected void Init(IUiFrameworkPlugin plugin)
    {
        Id = AnimationId.GetNextId();
        Plugin = plugin;
        ChangeState(AnimationState.Init);
        Singleton<AnimationData>.Instance.AddAnimation(this);
    }
    
    public virtual void OnStarted()
    {
        ChangeState(Delay != null || Timeout != null ? AnimationState.Delayed : AnimationState.Running);
        
        for (int index = 0; index < _children.Count; index++)
        {
            IAnimation child = _children[index];
            child.OnStarted();
        }
    }

    public virtual void OnTick()
    {
        if (TickDelay())
        {
            TickTimeout();
            return;
        }

        if (State == AnimationState.Delayed)
        {
            ChangeState(AnimationState.Running);
        }

        if (State == AnimationState.Running)
        {
            TickDuration();
            TickAnimation();
        }
        else if (State == AnimationState.Cancelled && CancelOption != AnimationCancelOption.NoTick)
        {
            TickAnimation();
        }
        
        TickCleanup();
    }
    
    protected virtual bool TickDelay()
    {
        if (State != AnimationState.Delayed || Delay == null)
        {
            return false;
        }

        Delay.OnTick();
        return Delay.IsDelayed;
    }
    
    protected virtual void TickTimeout()
    {
        if (Timeout == null)
        {
            return;
        }

        Timeout.OnTick();
        if (Timeout.HasTimedOut)
        {
            TimeoutAnimation();
        }
    }

    protected virtual void TickDuration()
    {
        if (Duration == null)
        {
            return;
        }
        
        float previous = Duration.ElapsedPercentage;
        Duration.OnTick();
        UiFrameworkExtension.GlobalLogger.Debug("Animation {0} TickDuration {1:0.00} -> {2:0.00}. IsCompleted: {3}", Id.Id, previous, Duration.ElapsedPercentage, Duration.IsCompleted);
        if (!Duration.IsCompleted)
        {
            return;
        }
        
        if (Repeat is not null && Repeat.OnRepeat())
        {
            Duration.Restart(Repeat.RepeatDelay);
            Events.OnEvent(AnimationEventType.Repeat);
        }
    }
    
    protected virtual void TickAnimation()
    {
        Interpolator?.OnTick(GetProgress());

        if (State == AnimationState.Running || (State == AnimationState.Cancelled && CancelOption == AnimationCancelOption.TickAll))
        {
            for (int index = 0; index < _children.Count; index++)
            {
                IAnimation child = _children[index];
                child.OnTick();
            }
        }
    }
    
    protected virtual void TickCleanup()
    {
        for (int index = _children.Count - 1; index >= 0; index--)
        {
            BaseAnimation child = _children[index];
            if (child.State > AnimationState.Running)
            {
                child.Dispose();
                _children.RemoveAt(index);
            }
        }
        
        UiFrameworkExtension.GlobalLogger.Debug("Animation {0} TickCleanup", Id.Id);

        if (State is AnimationState.Running or AnimationState.Delayed && (Duration is { IsCompleted : true } || Interpolator == null) && _children.Count == 0)
        {
            CompleteAnimation();
        }
    }

    public void ChangeState(AnimationState newState)
    {
        InvalidAnimationStateException.ThrowIfInvalidState(State, newState);

        //Don't allow changing states once we hit Completed, Canceled, or Timeout
        switch (State)
        {
            case AnimationState.Completed:
            case AnimationState.Cancelled:
            case AnimationState.Timeout:
                return;
        }
        
        UiFrameworkExtension.GlobalLogger.Debug("Animation {0} changed state {1} -> {2}", Id, State, newState);
        State = newState;
        
        switch (State)
        {
            case AnimationState.Queued:
                Events.OnEvent(AnimationEventType.Queued);
                ChangeChildState();
                break;
            case AnimationState.Delayed:
                Timeout?.OnStarted();
                Delay?.OnStarted();
                Events.OnEvent(AnimationEventType.Delayed);
                break;
            case AnimationState.Running:
                Duration?.OnStarted();
                Interpolator?.OnStarted();
                Events.OnEvent(AnimationEventType.Started);
                break;
            case AnimationState.Completed:
                Events.OnEvent(AnimationEventType.Completed);
                Events.OnEvent(AnimationEventType.Finalized);
                break;
            case AnimationState.Cancelled:
                Events.OnEvent(AnimationEventType.Canceled);
                Events.OnEvent(AnimationEventType.Finalized);
                ChangeChildState();
                break;
            case AnimationState.Timeout:
                Events.OnEvent(AnimationEventType.Timeout);
                Events.OnEvent(AnimationEventType.Finalized);
                ChangeChildState();
                break;
            case AnimationState.Pooled:
            case AnimationState.Init:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState.ToString());
        }
    }

    private void ChangeChildState()
    {
        for (int index = 0; index < _children.Count; index++)
        {
            IAnimation child = _children[index];
            if (child.State < State)
            {
                child.ChangeState(State);
            }
        }
    }

    public virtual void CompleteAnimation() => ChangeState(AnimationState.Completed);
    public virtual void CancelAnimation() => ChangeState(AnimationState.Cancelled);
    public virtual void TimeoutAnimation() => ChangeState(AnimationState.Timeout);
    public void SetTime(IAnimationTime time)
    {
        _time.TryReturnToPool();
        _time = time;
    } 
    
    public virtual float GetProgress()
    {
        if (!Singleton<AnimationTime>.Instance.AnimationsEnabled || State == AnimationState.Cancelled && CancelOption != AnimationCancelOption.NoTick)
        {
            return AnimationConstants.CompletedProgress;
        }

        float progress;
        if (Duration != null)
        {
            progress = Duration.ElapsedPercentage;
            if (Timing == null)
            {
                UiFrameworkExtension.GlobalLogger.Debug("Animation {0} GetProgress {1:0.00}%", Id.Id, progress * 100f);
                return progress;
            }
            
            float previous = progress;
            progress = Timing(progress);
            UiFrameworkExtension.GlobalLogger.Debug("Animation {0} GetProgress Timing {1:0.00}% -> {2:0.00}%", Id.Id, previous * 100f, progress * 100f);
            return progress;
        }
        
        progress = Parent?.GetProgress() ?? AnimationConstants.NoProgress;
        UiFrameworkExtension.GlobalLogger.Debug("Animation {0} Parent GetProgress {1:0.00}%", Id.Id, progress * 100f);
        return progress;
    }

    public virtual ISendableAnimation GetSendable() => Parent?.GetSendable();

    internal void AddChildAnimation(BaseAnimation animation)
    {
        if (!_children.Contains(animation) && animation.Parent is null) 
        {
            _children.Add(animation);
            animation.Parent = this;
        }
    }

    internal void RemoveChildAnimation(BaseAnimation animation)
    {
        _children.Remove(animation);
    }

    protected override void EnterPool()
    {
        Singleton<AnimationTracker>.Instance.OnAnimationFinalized(Id);
        Id = default;
        Plugin = default;
        State = default;
        Duration.TryReturnToPool();
        Duration = default;
        Repeat.TryReturnToPool();
        Repeat = default;
        Timing = default;
        Interpolator.TryReturnToPool();
        Interpolator = default;
        Delay.TryReturnToPool();
        Delay = default;
        Timeout.TryReturnToPool();
        Timeout = default;
        Parent = default;
        _children.TryFreeValues();
        CancelOption = default;
        Time.TryReturnToPool();
        _time = default;
    }
}