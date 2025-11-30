using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationDuration : BasePoolable, IAnimationDuration
{
    public float ElapsedPercentage => Mathf.Clamp01((AnimationTime.CurrentTime - _startTime) / Duration);
    public float Duration { get; set; }

    private float _startTime;
    
    public static AnimationDuration Create(IUiFrameworkPlugin plugin, float duration) => plugin.PluginPool.Get<AnimationDuration>().Init(duration);

    protected AnimationDuration Init(float duration)
    {
        Duration = duration;
        return this;
    }
    
    public void OnStarted()
    {
        _startTime = AnimationTime.CurrentTime;
    }
    
    public void OnTick() { }

    public void Restart(float delay = 0f)
    {
        _startTime = AnimationTime.CurrentTime + delay;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Duration = 0;
        _startTime = 0;
    }
}