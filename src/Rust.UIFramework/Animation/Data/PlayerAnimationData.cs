using System.Collections.Concurrent;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

internal sealed class PlayerAnimationData : BasePoolable
{
    public SendInfo Send;
    public readonly ConcurrentDictionary<AnimationId, ISendableAnimation> Animations = new();
    public bool IsEmpty => Animations.Count == 0;

    public static PlayerAnimationData Create(ISendableAnimation animation) => UiPool.Internal.Get<PlayerAnimationData>().Init(animation.Send);
    
    private PlayerAnimationData Init(SendInfo send)
    {
        Send = send;
        return this;
    }

    public void AddAnimation(ISendableAnimation animation) => Animations[animation.Id] = animation;
    public void RemoveAnimation(ISendableAnimation animation) => Animations.TryRemove(animation.Id, out ISendableAnimation _);

    protected override void EnterPool()
    {
        Send = default;
        Animations.Clear();
        base.EnterPool();
    }
}