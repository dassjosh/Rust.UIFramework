using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class FrameDuration : BasePoolable, IAnimationDuration
{
    public float ElapsedPercentage => Mathf.Clamp01((AnimationTime.CurrentFrame - _startFrame) / Duration);
    public float Duration { get; set; }

    private int _startFrame;
    
    public static FrameDuration Create(IUiFrameworkPlugin plugin, int duration) => plugin.PluginPool.Get<FrameDuration>().Init(duration);
    
    protected FrameDuration Init(int duration)
    {
        Duration = duration;
        return this;
    }
    
    public void OnStarted()
    {
        _startFrame = AnimationTime.CurrentFrame;
    }
    
    public void OnTick() { }

    public void Restart(float delay = 0f)
    {
        _startFrame = Mathf.CeilToInt(AnimationTime.CurrentFrame + AnimationTime.FramesPerSecond * delay);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Duration = 0;
        _startFrame = 0;
    }
}