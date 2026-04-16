using System;
using System.Collections.Generic;

namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationEvents
{
    IReadOnlyList<IAnimationEvent> Events { get; }
    void OnEvent(AnimationEventType type);
    void AddEvent(IAnimationEvent @event);
    void RemoveEvent(IAnimationEvent @event);
    void RemoveEvents(Predicate<IAnimationEvent> predicate);
}