using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation : BasePoolable
{
    public AnimationId Id { get; private set; }
    public IUiFrameworkPlugin Plugin { get; private set; }
    public UiReference Reference { get; private set; }
    internal SendInfo Send { get; private set; }
    public IAnimationDuration Duration { get; private set; }
    public IAnimationProgressor Progressor { get; private set; }
    public AnimationState State { get; private set; }
    internal bool IsSinglePlayer => PlayerId != 0;
    internal ulong PlayerId { get; private set; }
    private readonly List<IAnimationEvent> _events = [];
    public bool IsCompleted => State is AnimationState.Completed or AnimationState.Cancelled;
    public bool IsDelayed => TriggeredTracker is { IsTriggeredOrTimedOut: false } || Duration.IsDelayed;
    
    protected TriggeredTimeoutTracker TriggeredTracker;
    protected TimeoutAnimationAction TimeoutAction;
    protected bool HasStarted;
    
    protected void Init(IUiFrameworkPlugin plugin, in UiReference reference, IAnimationDuration duration)
    {
        Id = AnimationId.GetNextId();
        Plugin = plugin;
        Reference = reference;
        Duration = duration;
        UpdateState(AnimationState.Init);
    }

    public BaseAnimation WithDuration(IAnimationDuration duration)
    {
        Duration.TryReturnToPool();
        Duration = duration;
        return this;
    }

    public IConfigurableAnimationDuration GetConfigurableDuration()
    {
        return Duration as IConfigurableAnimationDuration;
    }
    
    public BaseAnimation ComesAfter(BaseAnimation animation, bool includeRepeats)
    {
        if (animation.Duration is IRemainingDuration remaining)
        {
            return WithDelay(remaining.GetRemainingDuration(includeRepeats));
        }

        return ComesAfter(animation);
    }

    public BaseAnimation ComesAfter(BaseAnimation animation, in TimeSpan? timeout = null, TimeoutAnimationAction action = TimeoutAnimationAction.StartAnimation)
    {
        animation.AddEvent(StartAnimationAfterEvent.Create(this, action));
        TriggeredTracker = TriggeredTimeoutTracker.Create(PluginPool, timeout ?? TimeSpan.FromMinutes(1));
        TimeoutAction = action;
        return this;
    }

    public BaseAnimation WithDelay(float delay)
    {
        if (Duration is IConfigurableAnimationDuration duration)
        {
            duration.Delay = delay;
        }
        return this;
    }
    
    public void TriggerDelayComplete()
    {
        if (State is AnimationState.Queued or AnimationState.Running)
        {
            TriggeredTracker?.Trigger();
            OnStarted();
        }
    }
    
    public BaseAnimation WithRepeats(int repeats, float repeatDelay = 0f)
    {
        if (Duration is IConfigurableAnimationDuration duration)
        {
            duration.Repeats = repeats;
            duration.RepeatDelay = repeatDelay;
        }
        return this;
    }

    public BaseAnimation DestroyAfter() => DestroyAfter(Reference);
    public BaseAnimation DestroyAfter(in UiReference destroyTarget) => DestroyAfter(destroyTarget.Name);
    public BaseAnimation DestroyAfter(string name)
    {
        AddEvent(DestroyUiAfterEvent.Create(PluginPool, name));
        return this;
    }
    
    public BaseAnimation WithPingPongProgressor() => WithProgressor(PingPongProgressor.Default);
    public BaseAnimation WithBezierProgressor(in BezierProgressor points) => WithProgressor(points);

    public BaseAnimation WithProgressor(IAnimationProgressor progressor)
    {
        Progressor?.TryReturnToPool();
        Progressor = progressor;
        return this;
    }
    
    public void WriteCompletedAnimation(JsonFrameworkWriter writer) => WriteAnimation(writer, 1f);

    public abstract void WriteAnimation(JsonFrameworkWriter writer, float elapsedPercentage);

    private void UpdateState(AnimationState newState)
    {
        InvalidAnimationStateException.ThrowIfInvalidState(State, newState);
        State = newState;
    }
    
    public void Cancel()
    {
        UpdateState(AnimationState.Cancelled);
        OnEvent(AnimationEventType.Canceled);
    }

    internal void OnQueued(SendInfo send)
    {
        Send = send;
        UpdateState(AnimationState.Queued);
        if (send.connection != null)
        {
            PlayerId = send.connection.userid;
        }
    }

    private void OnStarted()
    {
        Duration.OnStarted(Time.realtimeSinceStartup);
        HasStarted = true;
    }

    internal void OnTick(float currentTime)
    {
        if (State != AnimationState.Running)
        {
            UpdateState(AnimationState.Running);
        }

        if (TriggeredTracker != null)
        {
            TriggeredTracker.OnTick(currentTime);
            if (TriggeredTracker.HasTimedOut && TimeoutAction == TimeoutAnimationAction.CancelAnimation)
            {
                Cancel();
                return;
            }
            
            if (!TriggeredTracker.IsTriggeredOrTimedOut)
            {
                return;
            }
        }

        if (!HasStarted)
        {
            OnStarted();
        }
        
        Duration.OnTick(currentTime);
    }

    internal void OnRepeat()
    {
        OnEvent(AnimationEventType.Repeat);
    }

    internal void OnCompleted()
    {
        UpdateState(AnimationState.Completed);
        OnEvent(AnimationEventType.Completed);
    }

    internal void OnRemoved()
    {
        Singleton<AnimationTracker>.Instance.OnAnimationCompleted(Id);
        OnEvent(AnimationEventType.Removed);
        Dispose();
    }
    
    public void AddEvent(IAnimationEvent @event) => _events.Add(@event);
    
    public void RemoveEvent(IAnimationEvent @event) => _events.Remove(@event);
    public void RemoveEvent(Predicate<IAnimationEvent> predicate) => _events.RemoveAll(predicate);

    internal void OnEvent(AnimationEventType type)
    {
        for (int index = 0; index < _events.Count; index++)
        {
            IAnimationEvent @event = _events[index];
            switch (type)
            {
                case AnimationEventType.Repeat when @event is IAnimationRepeat repeatEvent:
                    repeatEvent.OnAnimationRepeat(this);
                    break;
                case AnimationEventType.Completed when @event is IAnimationCompleted completedEvent:
                    completedEvent.OnAnimationCompleted(this);
                    break;
                case AnimationEventType.Removed when @event is IAnimationRemoved removedEvent:
                    removedEvent.OnAnimationRemoved(this);
                    break;
                case AnimationEventType.Canceled when @event is IAnimationCancelled cancelledEvent:
                    cancelledEvent.OnAnimationCancelled(this);
                    break;
            }
        }
    }

    public void RemoveForPlayer(ulong playerId)
    {
        if (Send.connections != null)
        {
            List<Connection> connections = Send.connections;
            for (int i = connections.Count - 1; i >= 0; i--)
            {
                if (connections[i].userid == playerId)
                {
                    connections.RemoveAt(i);
                }
            }

            if (connections.Count == 0)
            {
                Cancel();
            }

            return;
        }
        
        if (Send.connection != null && Send.connection.userid == playerId)
        {
            Cancel();
        }
    }
    
    protected override void EnterPool()
    {
        if (Send.connections != null)
        {
            PluginPool.FreeList(Send.connections);
        }

        Duration.TryReturnToPool();

        Id = default;
        Reference = default;
        Duration = null;
        Send = default;
        _events.TryFreeValues();
        TriggeredTracker?.TryDispose();
        UpdateState(AnimationState.Pooled);
    }
}