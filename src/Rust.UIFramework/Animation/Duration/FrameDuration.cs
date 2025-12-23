using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class FrameDuration : BasePoolable, IAnimationDuration, IAnimationComponent
{
    public float ElapsedPercentage => Mathf.Clamp01((Owner.Time.CurrentFrame - _startFrame) / Duration);
    public float Duration { get; set; }
    public IAnimation Owner { get; private set; }

    private int _startFrame;
    
    public static FrameDuration Create(IAnimation owner, int duration) => owner.Plugin.PluginPool.Get<FrameDuration>().Init(owner, duration);
    
    protected FrameDuration Init(IAnimation owner, int duration)
    {
        Owner = owner;
        Duration = duration;
        return this;
    }
    
    public void OnStarted()
    {
        _startFrame = Owner.Time.CurrentFrame;
    }
    
    public void OnTick() { }

    public void Restart(float delay = 0f)
    {
        _startFrame = Mathf.CeilToInt(Owner.Time.CurrentFrame + Owner.Time.FramesPerSecond * delay);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Duration = 0;
        _startFrame = 0;
        Owner = null;
    }
}