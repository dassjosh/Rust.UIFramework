using System;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Animation;

public class LoopedTriggeredDuration : TriggeredDuration
{
    protected float LoopDuration;
    
    public static LoopedTriggeredDuration Create(IAnimationBuilder builder, TimeSpan timeout, float loopDuration) => builder.PluginPool.Get<LoopedTriggeredDuration>().Init(timeout, loopDuration);
    
    public LoopedTriggeredDuration Init(TimeSpan timeout, float loopDuration)
    {
        base.Init(timeout);
        LoopDuration = loopDuration;
        IsRunning = true;
        IsDelayed = false;
        return this;
    }
    
    public override void OnTick(float currentTime)
    {
        base.OnTick(currentTime);
        float previous = ElapsedPercentage;
        ElapsedPercentage = (currentTime - TimeoutDelay.StartTime) / LoopDuration;
        HasChanged = ElapsedPercentage != previous;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        LoopDuration = default;
    }
}