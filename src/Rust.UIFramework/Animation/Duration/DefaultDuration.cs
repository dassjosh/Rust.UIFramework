using System;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class DefaultDuration : BasePoolable, IConfigurableAnimationDuration
{
    public float Delay { get; set; }
    public float Duration { get; set; }
    public float Elapsed;
    public int Repeats { get; set; }
    public float RepeatDelay { get; set; }
    public float StartTime { get; private set; }
    
    public float TotalDuration => Delay + Duration;
    public float ElapsedPercentage => Math.Min((Elapsed - Delay) / Duration, 1f);
    public bool IsDelayed => Elapsed < Delay;
    public bool IsRunning => Elapsed >= Delay && Elapsed < TotalDuration;
    public bool IsCompleted => Repeats <= 0;
    
    public static DefaultDuration Create(UiPluginPool pool, float duration, float delay = 0, int repeats = 1, float repeatDelay = 0) => pool.Get<DefaultDuration>().Init(duration, delay, repeats, repeatDelay);
    
    public DefaultDuration Init(float duration, float delay = 0, int repeats = 1, float repeatDelay = 0)
    {
        if(delay <= 0) throw new ArgumentOutOfRangeException(nameof(delay), $"{nameof(delay)} cannot be less than 0");
        if(duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration), $"{nameof(duration)} cannot be less than 0");
        if(repeats <= 0) throw new ArgumentOutOfRangeException(nameof(repeats), $"{nameof(repeats)} cannot be less than 0");
        if(repeatDelay <= 0) throw new ArgumentOutOfRangeException(nameof(repeatDelay), $"{nameof(repeatDelay)} cannot be less than 0");
        Delay = delay;
        Duration = duration;
        Repeats = repeats;
        RepeatDelay = repeatDelay;
        return this;
    }

    public void OnStarted(float startTime)
    {
        StartTime = startTime;
    }

    public float GetRemainingDuration(bool includeRepeats)
    {
        float remaining = Mathf.Max(Delay + Duration - Elapsed, 0);
        if (includeRepeats)
        {
            remaining += (RepeatDelay + Duration) * (Repeats - 1);
        }
        return remaining;
    }

    public void OnTick(float currentTime)
    {
        Elapsed = currentTime - StartTime;
    }

    public void OnAnimationCompleted(float currentTime)
    {
        Repeats--;
        Delay = RepeatDelay;
        StartTime = currentTime;
    }

    protected override void EnterPool()
    {
        Delay = default;
        Duration = default;
        Elapsed = default;
        Repeats = default;
        RepeatDelay = default;
    }
}