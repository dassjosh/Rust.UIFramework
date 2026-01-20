using System;
using Oxide.Ext.UiFramework.Animation;

namespace Oxide.Ext.UiFramework.Exceptions;

public class InvalidAnimationStateException : BaseUiFrameworkException
{
    private InvalidAnimationStateException(string message) : base(message) { }

    internal static void ThrowIfInvalidState(AnimationState currentState, AnimationState newState)
    {
        switch (newState)
        {
            case AnimationState.Init:
                if (currentState != AnimationState.Pooled) ThrowExpectedException(currentState, newState, AnimationState.Pooled);
                    break;
            case AnimationState.Queued:
                if (currentState != AnimationState.Init) ThrowExpectedException(currentState, newState, AnimationState.Init);
                break;
            case AnimationState.Delayed:
                if (currentState != AnimationState.Queued) ThrowExpectedException(currentState, newState, AnimationState.Queued);
                break;
            case AnimationState.Running:
                if (currentState != AnimationState.Queued && currentState != AnimationState.Delayed) 
                    throw new InvalidAnimationStateException($"Tried change animation state to {newState} but animation is in the {currentState} state. Expected animation to be in the {AnimationState.Queued} or {AnimationState.Delayed} state.");
                break;
            case AnimationState.Completed:
                if (currentState != AnimationState.Running && currentState != AnimationState.Queued && currentState != AnimationState.Delayed) 
                    throw new InvalidAnimationStateException($"Tried change animation state to {newState} but animation is in the {currentState} state. Expected animation to be in the {AnimationState.Running}, {AnimationState.Queued}, or {AnimationState.Delayed} state.");
                break;
            case AnimationState.Cancelled:
                if (currentState == AnimationState.Pooled) ThrowNotExpectedException(currentState, newState);
                break;
            case AnimationState.Timeout:
                if (currentState == AnimationState.Pooled) ThrowNotExpectedException(currentState, newState);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private static void ThrowExpectedException(AnimationState currentState, AnimationState newState, AnimationState expectedState)
    {
        throw new InvalidAnimationStateException($"Tried change animation state to {newState} but animation is in the {currentState} state. Expected animation in the {expectedState} state.");
    }
    
    private static void ThrowNotExpectedException(AnimationState currentState, AnimationState newState)
    {
        throw new InvalidAnimationStateException($"Tried change animation state to {newState} but animation is in the {currentState} state. Expected animation to not be in the {currentState} state.");
    }
}