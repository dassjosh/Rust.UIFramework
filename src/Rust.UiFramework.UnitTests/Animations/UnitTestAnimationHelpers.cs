using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Enums;

namespace Rust.UiFramework.UnitTests.Animations;

public static class UnitTestAnimationHelpers
{
    public static void QueueAnimation<T>(AnimationRef<T> animation) where T : class, IAnimation
    {
        animation.Animation.ChangeState(AnimationState.Queued);
    }

    public static void StartAnimation<T>(AnimationRef<T> animation) where T : class, IAnimation
    {
        animation.Animation.OnStarted();
    }
}