namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationDelay : IAnimationTick, IAnimationStarted
{
    bool IsDelayed { get; }
}