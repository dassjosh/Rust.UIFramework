using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Builder;
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
    public int Repeats;
    public float RepeatDelay;
    public bool Loop;
    internal SendInfo Send;
    public BezierPoints? Points;
    public bool DestroyAfter;
    public UiReference? DestroyTarget;
    public float StartTime { get; private set; }
    internal bool WasQueued { get; private set; }
    public bool Cancelled { get; private set; }
    public float TotalDuration => Delay + Duration;
    
    public float ElapsedPercentage
    {
        get
        {
            if (Elapsed < Delay)
            {
                return 0;
            }

            float elapsedPercentage = Math.Min((Elapsed - Delay) / Duration, 1f);
            
            if (Loop)
            {
                if (elapsedPercentage <= 0.5f)
                {
                    return elapsedPercentage * 2;
                }

                return 1f - (elapsedPercentage - 0.5f) * 2;
            }

            return elapsedPercentage;
        }
    }

    protected void Init(BaseUiComponent component, float delay, float duration)
    {
        Id = AnimationId.GetNextId();
        Reference = component;
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

    public BaseAnimation DestroyUiAfter(in UiReference? destroyTarget = null)
    {
        DestroyAfter = true;
        DestroyTarget = destroyTarget;
        return this;
    }
    
    public BaseAnimation WithRepeats(int repeats, float repeatDelay)
    {
        Repeats = repeats;
        RepeatDelay = repeatDelay;
        return this;
    }

    public BaseAnimation WithLoop()
    {
        Loop = true;
        return this;
    }
    
    internal void OnTick(float currentTime)
    {
        Elapsed = currentTime - StartTime;
    }
    
    public void SendAnimation(float elapsedPercentage)
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create();
        WriteAnimationComponent(writer, elapsedPercentage);
        BaseBuilder.AddUi(Send, writer);
        writer.Dispose();
    }

    private void WriteAnimationComponent(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        writer.WriteStartArray();
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        WriteAnimation(writer, elapsedPercentage);    
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.WriteEndArray();
    }

    internal void OnQueued(SendInfo send)
    {
        Send = send;
        WasQueued = true;
        StartTime = Time.realtimeSinceStartup;
    }
    
    internal bool OnAnimationEnded(float currentTime)
    {
        if (Repeats > 0)
        {
            OnRepeat(currentTime);
            return false;
        }

        OnCompleted();
        return true;
    }

    private void OnRepeat(float currentTime)
    {
        Repeats--;
        StartTime = currentTime + RepeatDelay;
    }

    private void OnCompleted()
    {
        if (DestroyAfter)
        {
            UiBuilder.DestroyUi(Send, DestroyTarget.HasValue ? DestroyTarget.Value.Name : Reference.Name);
        }
        else
        {
            SendAnimation(1f);
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
        Repeats = default;
        RepeatDelay = default;
        Loop = default;
        if (Send.connections != null)
        {
            UiFrameworkPool.FreeList(Send.connections);
        }
        Send = default;
        Points = null;
        DestroyAfter = default;
        DestroyTarget = default;
        StartTime = default;
        WasQueued = false;
        Cancelled = false;
    }
}