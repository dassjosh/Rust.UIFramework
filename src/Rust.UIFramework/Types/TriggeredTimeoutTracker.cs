using System;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Types;

public class TriggeredTimeoutTracker : BasePoolable
{
    public float StartTime { get; private set; }
    public float CurrentTIme { get; private set; }
    public TimeSpan Timeout { get; private set; }
    public bool IsTriggered { get; private set; }
    
    public bool IsTriggeredOrTimedOut => IsTriggered || HasTimedOut;
    
    public bool HasTimedOut => TimeSpan.FromSeconds(CurrentTIme - StartTime) >= Timeout;

    public static TriggeredTimeoutTracker Create(UiPluginPool pool, TimeSpan timeout) => pool.Get<TriggeredTimeoutTracker>().Init(timeout);

    public TriggeredTimeoutTracker Init(TimeSpan timeout)
    {
        Timeout = timeout;
        return this;
    }

    public void Start(float startTime) => StartTime = startTime;
    public void OnTick(float currentTime) => CurrentTIme = currentTime;
    public void Trigger() => IsTriggered = true;

    public void Reset()
    {
        StartTime = 0;
        CurrentTIme = 0;
        Timeout = default;
        IsTriggered = false;
    }

    protected override void EnterPool() => Reset();
}