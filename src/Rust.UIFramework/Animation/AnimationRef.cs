using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct AnimationRef<T>(T Animation) where T : class, IAnimation
{
    public readonly AnimationId Id = Animation.Id.IsValid ? Animation.Id : throw new AnimationException("Animation is no longer valid.");

    public T Animation { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => IsValid ? Animation : throw new AnimationException($"Animation ID: {Id} is no longer valid."); } 
    public bool IsValid { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Animation is not null && Animation.Id == Id; }
    public IUiFrameworkPlugin Plugin { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Animation.Plugin; }
    internal TimingFunction TimingFunction => IsValid ? Animation.Timing : TimingFunctions.Linear;

    public bool TryGetAnimation(out T animation)
    {
        if (IsValid)
        {
            animation = Animation;
            return true;
        }

        animation = default;
        return false;
    }
    
    public void CancelAnimation()
    {
        if (IsValid)
        {
            Animation.CancelAnimation();
        }
    }
    
    public void CompleteAnimation()
    {
        if (IsValid)
        {
            Animation.CompleteAnimation();
        }
    }
    
    public void TimeoutAnimation()
    {
        if (IsValid)
        {
            Animation.TimeoutAnimation();
        }
    }
}