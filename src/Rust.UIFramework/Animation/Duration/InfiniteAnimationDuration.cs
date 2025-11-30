using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteAnimationDuration : IAnimationDuration
{
    public float ElapsedPercentage => 0;
    public float Duration { get => float.MaxValue; set => throw new AnimationException($"Cannot set {nameof(Duration)} on {nameof(InfiniteAnimationDuration)}"); }
    
    public static readonly InfiniteAnimationDuration Instance = new();

    public void OnTick() { }
    public void OnStarted() { }
    public void Restart(float delay = 0f) { }
}