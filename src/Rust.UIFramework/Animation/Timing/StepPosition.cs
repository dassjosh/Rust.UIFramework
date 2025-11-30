namespace Oxide.Ext.UiFramework.Animation;

public enum StepPosition : byte
{
    /// <summary>
    /// Jumps occur immediately at the start of each step.
    /// Equivalent to CSS 'jump-start'.
    /// The first jump happens at t = 0.
    /// </summary>
    JumpStart,

    /// <summary>
    /// Jumps occur at the end of each step.
    /// Equivalent to CSS 'jump-end'.
    /// The last jump happens at t = 1.
    /// </summary>
    JumpEnd,

    /// <summary>
    /// Jumps occur in the middle of each step.
    /// Equivalent to CSS 'jump-none'.
    /// No jump at the start or end; steps are centered.
    /// </summary>
    JumpNone,

    /// <summary>
    /// Jumps occur at both the start and end of the timeline.
    /// Equivalent to CSS 'jump-both'.
    /// Includes an extra step at t = 0 and t = 1.
    /// </summary>
    JumpBoth
}