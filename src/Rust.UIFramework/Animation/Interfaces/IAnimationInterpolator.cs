namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationInterpolator : IAnimationStarted
{
    bool HasChanged { get; }
    void OnTick(float progress);
}