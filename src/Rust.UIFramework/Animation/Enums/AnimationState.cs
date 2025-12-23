namespace Oxide.Ext.UiFramework.Animation;

public enum AnimationState : byte
{
    Pooled,
    Init,
    Queued,
    Delayed,
    Running,
    Completed,
    Cancelled,
    Timeout
}