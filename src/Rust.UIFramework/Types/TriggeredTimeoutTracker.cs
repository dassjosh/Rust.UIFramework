using System;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Types;

public class TriggeredTimeoutTracker : BasePoolable
{
    public float StartTime { get; private set; }
    public TimeSpan TimeoutDuration { get; private set; }
    public bool IsTriggered { get; private set; }
    public bool HasTimedOut { get; private set; }
    
    private Action _onTimeout;
    
    public bool IsTriggeredOrTimedOut => IsTriggered || HasTimedOut;

    public static TriggeredTimeoutTracker Create(UiPluginPool pool, TimeSpan timeout, Action onTimeout = null) => pool.Get<TriggeredTimeoutTracker>().Init(timeout, onTimeout);

    public TriggeredTimeoutTracker Init(TimeSpan timeout, Action onTimeout = null)
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

    public void Trigger()
    {
        if (!IsTriggeredOrTimedOut)
        {
            IsTriggered = true;
        }
    }

    public void Timeout()
    {
        if (!IsTriggeredOrTimedOut)
        {
            HasTimedOut = true;
            _onTimeout?.Invoke();
        }
    }

    public void Reset()
    {
        StartTime = 0;
        TimeoutDuration = default;
        IsTriggered = false;
        HasTimedOut = false;
        _onTimeout = null;
    }

    protected override void EnterPool() => Reset();
}