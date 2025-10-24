namespace Oxide.Ext.UiFramework.Animation;

public enum AnimationEventType : byte
{
    OnQueued,
    OnDelayed,
    OnStarted,
    OnRepeat,
    OnCompleted,
    OnCanceled,
    OnTimeout,
    OnRemoved
}