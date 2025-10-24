namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationEvent
{
    public bool IsForEvent(AnimationEventType type);
    void OnAnimationEvent(IAnimation animation, AnimationEventType type);
}