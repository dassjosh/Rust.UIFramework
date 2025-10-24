using System;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Types;

public class TimeoutTracker : BasePoolable
{
    public float StartTime { get; private set; }
    public TimeSpan TimeoutDuration { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool HasTimedOut { get; private set; }
    
    private Action _onTimeout;
    
    public bool IsCompletedOrTimedOut => IsCompleted || HasTimedOut;

    public static TimeoutTracker Create(UiPluginPool pool, TimeSpan timeout, Action onTimeout = null) => pool.Get<TimeoutTracker>().Init(timeout, onTimeout);

    public TimeoutTracker Init(TimeSpan timeout, Action onTimeout = null)
    {
        TimeoutDuration = timeout;
        _onTimeout = onTimeout;
        return this;
    }

    public void Start(float startTime) => StartTime = startTime;
    public void OnTick(float currentTime)
    {
        if (TimeSpan.FromSeconds(currentTime - StartTime) >= TimeoutDuration)
        {
            Timeout();
        }
    }

    public void OnComplete()
    {
        if (!IsCompletedOrTimedOut)
        {
            IsCompleted = true;
        }
    }

    public void Timeout()
    {
        if (!IsCompletedOrTimedOut)
        {
            HasTimedOut = true;
            _onTimeout?.Invoke();
        }
    }

    public void Reset()
    {
        StartTime = 0;
        TimeoutDuration = default;
        IsCompleted = false;
        HasTimedOut = false;
        _onTimeout = null;
    }

    protected override void EnterPool() => Reset();
}