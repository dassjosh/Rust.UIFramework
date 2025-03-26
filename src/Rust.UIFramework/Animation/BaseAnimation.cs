using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation : BasePoolable
{
    public AnimationId Id;
    public UiReference Reference { get; private set; }
    public float Delay;
    public float Duration;
    public float Elapsed;
    private int _repeats;
    private float _repeatDelay;
    internal SendInfo Send { get; private set; }
    private ICustomProgressor _customProgressor;
    protected ICustomAnimator CustomAnimator;
    private bool _destroyAfter;
    private UiReference? _destroyTarget;
    public float StartTime { get; private set; }
    internal bool WasQueued { get; private set; }
    public bool Cancelled { get; private set; }
    public float TotalDuration => Delay + Duration;
    internal bool IsSinglePlayer { get; private set; }
    internal ulong PlayerId { get; private set; }
    public float ElapsedPercentage => Elapsed < Delay ? 0 : Math.Min((Elapsed - Delay) / Duration, 1f);

    protected void Init(in AnimationReference reference, float delay, float duration)
    {
        Id = AnimationId.GetNextId();
        Reference = reference.Reference;
        Delay = delay;
        Duration = duration;
    }

    public BaseAnimation ComesAfter(BaseAnimation animation) => WithDelay(animation.TotalDuration);
    
    public BaseAnimation WithDelay(float delay)
    {
        Delay = delay;
        return this;
    }
    
    public BaseAnimation WithDuration(float duration)
    {
        Duration = duration;
        return this;
    }
    
    public BaseAnimation WithElapsed(BaseAnimation animation)
    {
        Elapsed = animation.Elapsed;
        return this;
    }

    public BaseAnimation DestroyAfter(in UiReference? destroyTarget = null)
    {
        _destroyAfter = true;
        _destroyTarget = destroyTarget;
        return this;
    }
    
    public BaseAnimation WithRepeats(int repeats, float repeatDelay)
    {
        _repeats = repeats;
        _repeatDelay = repeatDelay;
        return this;
    }

    public BaseAnimation WithLoop() => WithCustomProgressor(LoopProgressor.Default);
    
    public BaseAnimation WithCustomAnimation(ICustomAnimator customAnimator)
    {
        CustomAnimator = customAnimator;
        return this;
    }

    public BaseAnimation WithCustomProgressor(ICustomProgressor progressor)
    {
        _customProgressor = progressor;
        return this;
    }
    
    public BaseAnimation WithBezierProgressor(in BezierProgressor points) => WithCustomProgressor(points);
    
    internal void OnTick(float currentTime)
    {
        Elapsed = currentTime - StartTime;
    }

    public void WriteAnimationComponent(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        float progress = _customProgressor?.GetProgress(Mathf.Clamp01(elapsedPercentage)) ?? elapsedPercentage;
        WriteAnimation(writer, progress);    
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    internal void OnQueued(SendInfo send)
    {
        Send = send;
        WasQueued = true;
        StartTime = Time.realtimeSinceStartup;
        if (send.connection != null)
        {
            IsSinglePlayer = true;
            PlayerId = send.connection.userid;
        }
    }
    
    internal bool OnAnimationEnded(float currentTime)
    {
        if (_repeats > 0)
        {
            OnRepeat(currentTime);
            return false;
        }

        OnCompleted();
        return true;
    }

    private void OnRepeat(float currentTime)
    {
        _repeats--;
        StartTime = currentTime + _repeatDelay;
    }

    private void OnCompleted()
    {
        if (_destroyAfter)
        {
            UiBuilder.DestroyUi(Send, _destroyTarget.HasValue ? _destroyTarget.Value.Name : Reference.Name);
        }
    }

    internal void OnRemoved()
    {
        Singleton<AnimationTracker>.Instance.OnAnimationCompleted(Id);
        Dispose();
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
                Cancelled = true;
            }

            return;
        }
        
        if (Send.connection != null && Send.connection.userid == playerId)
        {
            Cancelled = true;
        }
    }
    
    protected override void EnterPool()
    {
        Id = default;
        Reference = default;
        Delay = default;
        Duration = default;
        Elapsed = default;
        _repeats = default;
        _repeatDelay = default;
        if (Send.connections != null)
        {
            UiFrameworkPool.FreeList(Send.connections);
        }
        Send = default;
        CustomAnimator = default;
        _destroyAfter = default;
        _destroyTarget = default;
        StartTime = default;
        WasQueued = false;
        Cancelled = false;
    }
}