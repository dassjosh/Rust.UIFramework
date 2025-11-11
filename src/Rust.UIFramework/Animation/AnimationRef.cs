using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Animation;

public readonly struct AnimationRef<T>(T animation) where T : class, IAnimation
{
    public readonly AnimationId Id = animation.Id.IsValid ? animation.Id : throw new AnimationException("Animation is no longer valid.");

    public T Animation { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => IsValid ? animation : throw new AnimationException($"Animation ID: {Id} is no longer valid."); } 
    public bool IsValid { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => animation is not null && animation.Id == Id; }
    public IUiFrameworkPlugin Plugin { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Animation.Plugin; }
    internal Easing Easing => IsValid ? Animation.Easing : EasingFunctions.Linear;

    public void CancelAnimation()
    {
        if (IsValid)
        {
            animation.CancelAnimation();
        }
    }
    
    public void CompleteAnimation()
    {
        if (IsValid)
        {
            animation.CompleteAnimation();
        }
    }
    
    public void TimeoutAnimation()
    {
        if (IsValid)
        {
            animation.TimeoutAnimation();
        }
    }
}