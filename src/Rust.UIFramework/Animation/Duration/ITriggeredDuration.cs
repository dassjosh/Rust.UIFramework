namespace Oxide.Ext.UiFramework.Animation;

public interface ITriggeredDuration : IAnimationDuration
{
    bool HasTimedOut { get; }
    void Trigger();
}