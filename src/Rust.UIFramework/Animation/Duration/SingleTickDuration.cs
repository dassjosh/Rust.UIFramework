using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class SingleTickDuration : BasePoolable, IAnimationDuration
{
    public float ElapsedPercentage => IsCompleted ? 1 : 0;
    public bool IsDelayed => false;
    public bool IsRunning => !IsCompleted;
    public bool IsCompleted { get; set; }
    public bool HasChanged => IsCompleted;
    
    public static SingleTickDuration Create(UiPluginPool pool) => pool.Get<SingleTickDuration>();
    
    public void OnStarted(float startTime) { }

    public void OnTick(float currentTime)
    {
        IsCompleted = true;
    }

    public void OnAnimationCompleted(float currentTime) { }
    
    protected override void EnterPool()
    {
        IsCompleted = false;
    }
}