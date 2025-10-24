namespace Oxide.Ext.UiFramework.Enums;

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