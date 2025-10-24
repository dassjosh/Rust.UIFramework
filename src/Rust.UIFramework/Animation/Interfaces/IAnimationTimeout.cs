using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationTimeout : IAnimationTick, IAnimationStarted
{
    float Timeout { get; set; }
    AnimationTimeoutAction Action { get; set; }
    bool HasTimedOut { get; }
}