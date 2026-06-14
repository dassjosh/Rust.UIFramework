using System.Collections.Concurrent;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

internal sealed class PlayerAnimationData : BasePoolable
{
    public ulong PlayerId;
    public SendInfo Send;
    internal readonly ConcurrentDictionary<AnimationId, ISendableAnimation> Animations = new();
    public bool IsEmpty => Animations.Count == 0;
    private static readonly IUiLogger Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<PlayerAnimationData>();

    public static PlayerAnimationData Create(ISendableAnimation animation, ulong playerId) => UiPool.Internal.Get<PlayerAnimationData>().Init(animation.Send, playerId);
    
    private PlayerAnimationData Init(SendInfo send, ulong playerId)
    {
        PlayerId = playerId;   
        Send = send;
        return this;
    }

    public void AddAnimation(ISendableAnimation animation)
    {
        Animations[animation.Id] = animation;
        Logger.Debug("Added animation {0} for player {1}", animation.Id, PlayerId);
    }

    public void RemoveAnimation(ISendableAnimation animation)
    {
        if (Animations.TryRemove(animation.Id, out ISendableAnimation _))
        {
            Logger.Debug("Removed animation {0} for player {1}", animation.Id, PlayerId);
        }
    }

    protected override void EnterPool()
    {
        PlayerId = 0;
        Send = default;
        Animations.Clear();
        base.EnterPool();
    }
}