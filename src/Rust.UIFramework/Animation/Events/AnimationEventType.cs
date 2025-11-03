namespace Oxide.Ext.UiFramework.Animation;

public enum AnimationEventType : byte
{
    /// <summary>
    /// Animation has been added to the queue to be ran
    /// </summary>
    Queued,
    
    /// <summary>
    /// Animation is delayed by an <see cref="IAnimationDelay"/>
    /// </summary>
    Delayed,
    
    /// <summary>
    /// Animation has started running
    /// </summary>
    Started,
    
    /// <summary>
    /// Animation has completed a cycle and is going to repeat
    /// </summary>
    Repeat,
    
    /// <summary>
    /// Animation ran to completion without being canceled or timed out
    /// </summary>
    Completed,
    
    /// <summary>
    /// Animation was canceled
    /// </summary>
    Canceled,
    
    /// <summary>
    /// Animation timed out
    /// </summary>
    Timeout,
    
    /// <summary>
    /// Animation has been finalized. Either the animation was completed, was canceled, or timed out
    /// </summary>
    Finalized
}