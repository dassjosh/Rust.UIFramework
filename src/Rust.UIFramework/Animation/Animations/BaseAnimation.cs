using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
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
    public Easing Easing { get; set; }
    public IAnimationInterpolator Interpolator { get; set; }
    public IAnimationDelay Delay { get; set; }
    public IAnimationTimeout Timeout { get; set; }
    public IAnimationEvents Events { get; } = new AnimationEvents();
    public IAnimation Parent { get; private set;  }
    public virtual bool HasChanged => Interpolator?.HasChanged ?? false;

    public IReadOnlyList<IAnimation> Children => _children;
    private readonly List<BaseAnimation> _children = [];
    
    protected void Init(IUiFrameworkPlugin plugin)
    {
        Id = AnimationId.GetNextId();
        Plugin = plugin;
        ChangeState(AnimationState.Init);
    }
    
    public virtual void OnStarted()
    {
        Timeout?.OnStarted();
        Delay?.OnStarted();
        Duration?.OnStarted();
        Interpolator?.OnStarted();

        ChangeState(Delay == null ? AnimationState.Running : AnimationState.Delayed);
        
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
            return;
        }

        if (State == AnimationState.Running)
        {
            TickAnimation();
        }
        
        TickCleanup();
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

        if (Duration is { IsCompleted : true } || Interpolator == null && _children.Count == 0)
        {
            CompleteAnimation();
        }
    }
    
    protected virtual bool TickDelay()
    {
        if (State != AnimationState.Delayed || Delay == null)
        {
            return false;
        }
        
        Delay.OnTick();
        if (!Delay.IsDelayed)
        {
            ChangeState(AnimationState.Running);
            return false;
        }

        TickTimeout();

        return true;
    }
    
    protected virtual void TickTimeout()
    {
        if (Timeout == null)
        {
            return;
        }
        
        Timeout.OnTick();
        if (!Timeout.HasTimedOut)
        {
            return;
        }
        
        TimeoutAnimation();
    }

    protected virtual void TickDuration()
    {
        if (Duration == null)
        {
            return;
        }
        
        Duration.OnTick();
        if (!Duration.IsCompleted)
        {
            return;
        }
        
        if (Repeat is not null && Repeat.OnRepeat())
        {
            Duration.Restart(Repeat.RepeatDelay);
            Events.OnEvent(AnimationEventType.OnRepeat, this);
        }
    }
    
    protected virtual void TickAnimation()
    {
        TickDuration();
        Interpolator?.OnTick(GetProgress());

        for (int index = 0; index < _children.Count; index++)
        {
            IAnimation child = _children[index];
            child.OnTick();
        }
    }

    public void ChangeState(AnimationState newState)
    {
        InvalidAnimationStateException.ThrowIfInvalidState(State, newState);
        State = newState;

        switch (State)
        {
            case AnimationState.Queued:
                Events.OnEvent(AnimationEventType.OnQueued, this);
                break;
            case AnimationState.Delayed:
                Events.OnEvent(AnimationEventType.OnDelayed, this);
                break;
            case AnimationState.Running:
                Events.OnEvent(AnimationEventType.OnStarted, this);
                break;
            case AnimationState.Completed:
                Events.OnEvent(AnimationEventType.OnCompleted, this);
                break;
            case AnimationState.Cancelled:
                Events.OnEvent(AnimationEventType.OnCanceled, this);
                break;
            case AnimationState.Timeout:
                Events.OnEvent(AnimationEventType.OnTimeout, this);
                break;
        }

        for (int index = 0; index < _children.Count; index++)
        {
            IAnimation child = _children[index];
            if (child.State < State)
            {
                child.ChangeState(State);
            }
        }
    }

    public virtual void CompleteAnimation()
    {
        ChangeState(AnimationState.Completed);
    }

    public virtual void CancelAnimation()
    {
        ChangeState(AnimationState.Cancelled);
    }

    public virtual void TimeoutAnimation()
    {
        ChangeState(AnimationState.Timeout);
    }

    public virtual float GetProgress()
    {
        if (!AnimationTime.AnimationsEnabled)
        {
            return 1;
        }
        
        if (Duration != null)
        {
            float progress = Duration.ElapsedPercentage;
            if (Easing != null)
            {
                progress = Easing(progress);
            }
            return progress;
        }
        
        return Parent?.GetProgress() ?? 0;
    }

    public virtual ISendableAnimation GetSendable()
    {
        return Parent?.GetSendable();
    }

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
    
    public override void Dispose()
    {
        Singleton<AnimationTracker>.Instance.OnAnimationCompleted(Id);
        Events.OnEvent(AnimationEventType.OnRemoved, this);
        base.Dispose();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Id = default;
        Plugin = default;
        State = default;
        Duration.TryReturnToPool();
        Duration = default;
        Repeat.TryReturnToPool();
        Repeat = default;
        Easing = default;
        Interpolator.TryReturnToPool();
        Interpolator = default;
        Delay.TryReturnToPool();
        Delay = default;
        Timeout.TryReturnToPool();
        Timeout = default;
        Parent = default;
        _children.TryFreeValues();
    }
}