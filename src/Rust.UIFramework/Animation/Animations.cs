using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public static class Animations
{
    public static AnimationRef<IAnimation> GetAnimation(AnimationId id) => new(Singleton<AnimationData>.Instance.GetAnimation(id));
    public static AnimationRef<ISendableAnimation> GetSendableAnimation(AnimationId id) => new(Singleton<AnimationData>.Instance.GetSendableAnimation(id));
   
    public static bool TryGetAnimation(AnimationId id, out AnimationRef<IAnimation> animation)
    {
        if (Singleton<AnimationData>.Instance.TryGetAnimation(id, out IAnimation ani))
        {
            animation = new AnimationRef<IAnimation>(ani);
            return true;
        }

        animation = default;
        return false;
    }
   
    public static bool TryGetSendableAnimation(AnimationId id, out AnimationRef<ISendableAnimation> sendable)
    {
        if (Singleton<AnimationData>.Instance.TryGetSendableAnimation(id, out ISendableAnimation ani))
        {
            sendable = new AnimationRef<ISendableAnimation>(ani);
            return true;
        }

        sendable = default;
        return false;
    }

    public static void CancelAnimation(AnimationId id)
    {
        if (TryGetAnimation(id, out AnimationRef<IAnimation> animation))
        {
            animation.CancelAnimation();
        }
    }

    public static void CancelPluginAnimations(IUiFrameworkPlugin plugin)
    {
        Singleton<AnimationData>.Instance.CancelPluginAnimations(plugin);
    }
    
    public static void CancelPlayerAnimations(IUiFrameworkPlugin plugin, ulong playerId)
    {
        Singleton<AnimationData>.Instance.CancelPlayerAnimations(plugin, playerId);
    }
    
    public static void CancelPlayerAnimations(IUiFrameworkPlugin plugin, BasePlayer player) 
    {
        if (player)
        {
            CancelPlayerAnimations(plugin, player.userID);
        }
    }

    internal static void CancelAnimations(SendInfo send, ICancelAnimationRequest request)
    {
        if (send.connection != null)
        {
            CancelAnimations(send.connection.userid, request);
        }
        else
        {
            CancelAnimations(send.connections, request);
        }
    }
    
    public static void CancelAnimations(IList<Connection> connections, ICancelAnimationRequest request)
    {
        for (int index = 0; index < connections.Count; index++)
        {
            Connection connection = connections[index];
            CancelAnimations(connection.userid, request);
        }
    }
    
    public static void CancelAnimations(ulong playerId, ICancelAnimationRequest request)
    {
        List<AnimationRef<ISendableAnimation>> animations = UiPool.Internal.GetList<AnimationRef<ISendableAnimation>>();
        Singleton<AnimationTracker>.Instance.GetAnimations(playerId, request.Name, animations);
        for (int index = 0; index < animations.Count; index++)
        {
            AnimationRef<ISendableAnimation> animation = animations[index];
            if (animation.IsValid)
            {
                animation.Animation.RemovePlayer(playerId);
                request.OnAnimationCancelled(playerId, animation.Animation);
            }
        }
    }
}