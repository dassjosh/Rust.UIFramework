using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationDuration : BasePoolable, IAnimationDuration, IAnimationComponent
{
    public float ElapsedPercentage => Mathf.Clamp01((Owner.Time.CurrentTime - _startTime) / Duration);
    public float Duration { get; set; }
    public IAnimation Owner { get; private set; }

    private float _startTime;
    
    public static AnimationDuration Create(IAnimation owner, float duration) => owner.Plugin.PluginPool.Get<AnimationDuration>().Init(owner, duration);

    protected AnimationDuration Init(IAnimation owner, float duration)
    {
        Owner = owner;
        Duration = duration;
        return this;
    }
    
    public void OnStarted()
    {
        _startTime = Owner.Time.CurrentTime;
    }
    
    public void OnTick() { }

    public void Restart(float delay = 0f)
    {
        _startTime = Owner.Time.CurrentTime + delay;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Duration = 0;
        _startTime = 0;
        Owner = null;
    }
}