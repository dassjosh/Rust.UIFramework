using System;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class TriggeredDuration : BasePoolable, ITriggeredDuration
{
    public float ElapsedPercentage { get; set; }
    public bool IsDelayed { get; set; }
    public bool IsRunning { get; set; }
    public bool HasChanged { get; set; }
    public bool IsCompleted { get; protected set; }
    public bool HasTimedOut { get; protected set; }
    
    protected readonly TriggeredTimeoutTracker TimeoutDelay = new();
    
    public static TriggeredDuration Create(IAnimationBuilder builder, TimeSpan timeout) => builder.PluginPool.Get<TriggeredDuration>().Init(timeout);
    
    public TriggeredDuration Init(TimeSpan timeout)
    {
        TimeoutDelay.Init(timeout);
        IsDelayed = true;
        HasChanged = false;
        return this;
    }
    
    public virtual void Trigger()
    {
        TimeoutDelay.Trigger();
        SetAsCompleted();
    }

    private void SetAsCompleted()
    {
        IsCompleted = true;
        IsDelayed = false;
        HasChanged = true;
    }

    public virtual void OnStarted(float startTime)
    {
        TimeoutDelay.Start(startTime);
    }

    public virtual void OnTick(float currentTime)
    {
        TimeoutDelay.OnTick(currentTime);
        if (TimeoutDelay.HasTimedOut)
        {
            Trigger();
            HasTimedOut = true;
        }
    }

    public virtual void OnAnimationCompleted(float currentTime) { }

    protected override void EnterPool()
    {
        base.EnterPool();
        ElapsedPercentage = 0;
        IsDelayed = false;
        IsRunning = false;
        HasChanged = false;
        IsCompleted = false;
        HasTimedOut = false;
        TimeoutDelay.Reset();
    }
}