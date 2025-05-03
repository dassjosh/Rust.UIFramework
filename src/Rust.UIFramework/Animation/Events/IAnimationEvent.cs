namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationEvent;

public interface IAnimationRepeat : IAnimationEvent
{
    void OnAnimationRepeat(BaseAnimation animation);
}

public interface IAnimationCompleted : IAnimationEvent
{
    void OnAnimationCompleted(BaseAnimation animation);
}

public interface IAnimationCancelled : IAnimationEvent
{
    void OnAnimationCancelled(BaseAnimation animation);
}

public interface IAnimationRemoved : IAnimationEvent
{
    void OnAnimationRemoved(BaseAnimation animation);
}