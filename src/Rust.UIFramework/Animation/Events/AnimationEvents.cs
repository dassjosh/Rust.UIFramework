using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationEvents(IAnimation animation) : IAnimationEvents
{
    private readonly List<IAnimationEvent> _events = [];

    public IReadOnlyList<IAnimationEvent> Events => _events;
    public void AddEvent(IAnimationEvent @event) => _events.Add(@event);
    public void RemoveEvent(IAnimationEvent @event)
    {
        if (_events.Remove(@event))
        {
            @event.TryReturnToPool();
        }
    }

    public void RemoveEvents(Predicate<IAnimationEvent> predicate)
    {
        for (int index = _events.Count - 1; index >= 0; index--)
        {
            IAnimationEvent @event = _events[index];
            if (predicate(@event))
            {
                _events.RemoveAt(index);
                @event.TryReturnToPool();
            }
        }
    }

    public void OnEvent(AnimationEventType type)
    {
        UiFrameworkExtension.GlobalLogger.Debug($"{nameof(AnimationEvents)}.{nameof(OnEvent)} ID: {{0}} Event: {{1}}", animation.Id, type);
        for (int index = 0; index < _events.Count; index++)
        {
            IAnimationEvent @event = _events[index];
            if (@event.IsForEvent(type))
            {
                @event.OnAnimationEvent(animation, type);
            }
        }

        if (type == AnimationEventType.Finalized)
        {
            _events.TryFreeValues();
        }
    }
}