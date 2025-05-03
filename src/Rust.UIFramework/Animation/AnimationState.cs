namespace Oxide.Ext.UiFramework.Animation;

public enum AnimationState : byte
{
    Pooled,
    Init,
    Queued,
    Running,
    Completed,
    Cancelled
}