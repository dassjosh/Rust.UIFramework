using System.Collections.Concurrent;
using Network;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

internal sealed class PlayerAnimations : BasePoolable
{
    public SendInfo Send;
    public readonly ConcurrentDictionary<AnimationId, BaseAnimation> Animations = new();
    public bool IsEmpty => Animations.Count == 0;

    public static PlayerAnimations Create(SendInfo send)
    {
        PlayerAnimations animations = UiFrameworkPool.Get<PlayerAnimations>();
        animations.Send = send;
        return animations;
    }

    public void AddAnimation(BaseAnimation animation) => Animations[animation.Id] = animation;
    public void RemoveAnimation(BaseAnimation animation) => Animations.TryRemove(animation.Id, out BaseAnimation _);

    protected override void EnterPool()
    {
        Send = default;
        UiFrameworkPool.FreeConcurrentDictionary(Animations);
        Animations.Clear();
        base.EnterPool();
    }
}