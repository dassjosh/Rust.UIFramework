namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationEvent
{
    public bool IsForEvent(AnimationEventType type);
    void OnAnimationEvent(in AnimationRef<IAnimation> animation, AnimationEventType type);
}