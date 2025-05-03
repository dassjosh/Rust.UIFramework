namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationDuration
{
    float ElapsedPercentage { get; }
    bool IsDelayed { get; }
    bool IsRunning { get; }
    bool IsCompleted { get; }

    void OnStarted(float startTime);
    float GetRemainingDuration(bool includeRepeats);
    void OnTick(float currentTime);
    void OnAnimationCompleted(float currentTime);
}