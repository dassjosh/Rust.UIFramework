using System;
using Oxide.Ext.UiFramework.Animation;

namespace Oxide.Ext.UiFramework.Libraries;

internal class ImageDownloadDuration : IAnimationDuration
{
    public float ElapsedPercentage => 0f;
    public bool IsDelayed => !IsCompleted;
    public bool IsRunning => false;
    public bool HasChanged => true;
    public bool IsCompleted { get; private set; }
    public bool HasTimedOut { get; private set; }
    
    private float _startTime;
    private TimeSpan _timeout;

    public ImageDownloadDuration()
    {
        Reset();
    }

    public void Init(TimeSpan timeout)
    {
        _timeout = timeout;
    }
    
    public void OnDownloadFinished()
    {
        IsCompleted = true;
    }

    public void OnStarted(float startTime)
    {
        _startTime = startTime;
    }

    public float GetRemainingDuration(bool includeRepeats)
    {
        return 0f;
    }

    public void OnTick(float currentTime)
    {
        if (TimeSpan.FromSeconds(currentTime - _startTime) >= _timeout)
        {
            OnDownloadFinished();
            HasTimedOut = true;
        }
    }

    public void OnAnimationCompleted(float currentTime) { }

    public void Reset()
    {
        IsCompleted = false;
        HasTimedOut = false;
    }
}