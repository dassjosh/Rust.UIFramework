using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation : BasePoolable
{
    public AnimationId Id { get; private set; }
    public UiReference Reference { get; private set; }
    internal SendInfo Send { get; private set; }
    public IAnimationDuration Duration { get; private set; }
    public IAnimationProgressor Progressor { get; private set; }
    public AnimationState State { get; private set; }
    internal bool IsSinglePlayer => PlayerId != 0;
    internal ulong PlayerId { get; private set; }
    private readonly List<IAnimationEvent> _events = [];
    
    protected void Init(in UiReference reference, IAnimationDuration duration)
    {
        Id = AnimationId.GetNextId();
        Reference = reference;
        Duration = duration;
        UpdateState(AnimationState.Init);
    }

    public BaseAnimation WithDuration(IAnimationDuration duration)
    {
        Duration = duration;
        return this;
    }

    public IConfigurableAnimationDuration GetConfigurableDuration()
    {
        return Duration as IConfigurableAnimationDuration;
    }
    
    public BaseAnimation ComesAfter(BaseAnimation animation, bool includeRepeats = false) => WithDelay(animation.Duration.GetRemainingDuration(includeRepeats));
    
    public BaseAnimation WithDelay(float delay)
    {
        if (Duration is IConfigurableAnimationDuration duration)
        {
            duration.Delay = delay;
        }
        return this;
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

    public BaseAnimation DestroyAfter(in UiReference? destroyTarget = null)
    {
        AddEvent(DestroyAfterEvent.Create(destroyTarget));
        return this;
    }

    public BaseAnimation WithLoop() => WithProgressor(LoopProgressor.Default);

    public BaseAnimation WithProgressor(IAnimationProgressor progressor)
    {
        Progressor = progressor;
        return this;
    }
    
    public BaseAnimation WithBezierProgressor(in BezierProgressor points) => WithProgressor(points);
    
    public void WriteCompletedComponent(JsonFrameworkWriter writer) => WriteAnimationComponent(writer, 1f);
    
    public void WriteAnimationComponent(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        float progress = Progressor?.GetProgress(Mathf.Clamp01(elapsedPercentage)) ?? elapsedPercentage;
        WriteAnimation(writer, progress);    
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    internal void UpdateState(AnimationState newState)
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
        Duration.OnStarted(Time.realtimeSinceStartup);
        if (send.connection != null)
        {
            PlayerId = send.connection.userid;
        }
    }

    internal void OnTick(float currentTime)
    {
        if (State != AnimationState.Running)
        {
            UpdateState(AnimationState.Running);
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
    
    protected abstract void WriteAnimation(JsonFrameworkWriter writer, float value);

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
            UiFrameworkPool.FreeList(Send.connections);
        }

        if (Duration is BasePoolable poolable)
        {
            poolable.Dispose();
        }

        Id = default;
        Reference = default;
        Duration = null;
        Send = default;
        _events.ClearAndTryPoolValues();
        UpdateState(AnimationState.Pooled);
    }
}