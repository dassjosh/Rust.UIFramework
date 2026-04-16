using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct AnimationRef<T> where T : class, IAnimation
{
    private readonly T _animation;
    public readonly AnimationId Id;

    public AnimationRef(T animation)
    {
        _animation = animation;
        Id = animation.Id.IsValid ? animation.Id : throw new AnimationException("Animation is no longer valid.");
    }

    public T Animation { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => IsValid ? _animation : throw new AnimationException($"Animation ID: {Id} is no longer valid."); } 
    public bool IsValid { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => _animation is not null && _animation.Id == Id; }
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

    public void Deconstruct(out T animation)
    {
        animation = Animation;
    }
}