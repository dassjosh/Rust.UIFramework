using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimation : IAnimationTick, IAnimationStarted, IPoolable
{
    AnimationId Id { get; }
    IUiFrameworkPlugin Plugin { get; }
    AnimationState State { get; }
    IAnimationDuration Duration { get; set; }
    IAnimationRepeat Repeat { get; set; }
    TimingFunction Timing { get; set; }
    IAnimationInterpolator Interpolator { get; set; }
    IAnimationDelay Delay { get; set; }
    IAnimationTimeout Timeout { get; set; }
    IAnimationEvents Events { get; }
    IAnimation Parent { get; }
    IReadOnlyList<IAnimation> Children { get; }
    bool HasChanged { get; }
    float GetProgress();
    ISendableAnimation GetSendable();
    void ChangeState(AnimationState newState);
    void CompleteAnimation();
    void CancelAnimation();
    void TimeoutAnimation();
}