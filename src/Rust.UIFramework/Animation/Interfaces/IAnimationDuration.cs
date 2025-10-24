namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationDuration : IAnimationTick, IAnimationStarted
{
    float ElapsedPercentage { get; }
    float Duration { get; set; }
    bool IsCompleted => ElapsedPercentage >= 1f;
    
    void Restart(float delay = 0f);
}