using System;
using Network;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Helpers;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationHandler : ISingleton
{
    private readonly IAnimationHandler _handler;
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<AnimationHandler>();

    private AnimationHandler()
    {
        if (UiFrameworkConfig.Instance.Threading.EnableAnimationThread)
        {
            _handler = new ThreadedAnimationHandler();
        }
        else
        {
            _handler = SingletonBehavior<BehaviorAnimationHandler>.Instance;
        }
        
        _handler.OnInit(this);
    }
    
    public void EnqueueAnimation(ISendableAnimation animation, SendInfo send)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        animation.Send = send;
        animation.ChangeState(AnimationState.Queued);
        AnimationException.ThrowIfMissingSend(animation);
        Singleton<AnimationData>.Instance.OnAnimationQueued(animation);
        _handler.OnAnimationQueued();
        _logger.Debug("Adding animation {0}", animation.Id);
    }
    
    internal float TickAnimation(bool wasPaused)
    {
        float startTime = Time.realtimeSinceStartup;
        Singleton<AnimationTime>.Instance.UpdateTime(startTime, wasPaused);
        _logger.Debug("Processing {0} animations. Delta: {1:0.0000} seconds", Singleton<AnimationData>.Instance.Count, Singleton<AnimationTime>.Instance.DeltaTime);
        ProcessAnimations();
        _logger.Debug("Processed animations. {0} remaining", Singleton<AnimationData>.Instance.Count);
        float endTime = Time.realtimeSinceStartup;
        return endTime - startTime;
    }
    
    private void ProcessAnimations()
    {
        foreach (PlayerAnimationData playerAnimations in Singleton<AnimationData>.Instance.PlayerAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance).Values)
        {
            JsonFrameworkWriter writer = Create();
            foreach (ISendableAnimation animation in playerAnimations.Animations.GetEnumeratorPooled(UiFrameworkPlugin.Instance).Values)
            {
                ProcessAnimation(animation, writer);
            }

            SendAnimations(writer, playerAnimations.Send);
        }
        
        foreach (ISendableAnimation animation in Singleton<AnimationData>.Instance.GroupAnimations.GetEnumeratorPooled(UiFrameworkPlugin.Instance).Values)
        {
            JsonFrameworkWriter writer = Create();
            ProcessAnimation(animation, writer);
            SendAnimations(writer, animation.Send);
        }

        Singleton<AnimationData>.Instance.CleanupCompletedAnimations();
    }

    private void ProcessAnimation(ISendableAnimation animation, JsonFrameworkWriter writer)
    {
        _logger.Debug("Processing Animation {0}", animation.Id);

        try
        {
            if (animation.Parent is null)
            {
                if (animation.State == AnimationState.Pooled)
                {
                    return;
                }
                
                if (animation.State == AnimationState.Queued)
                {
                    animation.OnStarted();
                }
                
                animation.OnTick();
                UiFrameworkExtension.GlobalLogger.Debug($"{nameof(AnimationHandler)}.{nameof(ProcessAnimation)} ID: {{0}} HasChanged: {{1}}", animation.Id, animation.HasChanged);
                if (animation.HasChanged)
                {
                    animation.Serialize(writer);
                }
            }
        }
        catch (Exception ex)
        {
            if (animation is not IPoolable { IsPooled: true })
            {
                _logger.Exception("An error occured processing animation ID: {0} Plugin: {1}. Cancelling Animation.", animation.Id, animation.Plugin, ex);
                animation.CancelAnimation();
            }
            else
            {
                _logger.Exception("An error occured processing animation. Animation is DISPOSED.", ex);
            }
        }
    }

    private static JsonFrameworkWriter Create()
    {
        JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UiFrameworkPlugin.Instance);
        writer.WriteStartArray();
        return writer;
    }

    private static void SendAnimations(JsonFrameworkWriter writer, SendInfo send)
    {
        writer.WriteEndArray();
        RpcFunctions.SendAddUi(send, writer);
        writer.Dispose();
    }

    public void OnPlayerDisconnected(ulong playerId) => Singleton<AnimationData>.Instance.OnPlayerDisconnected(playerId);
    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin) => Singleton<AnimationData>.Instance.OnPluginUnloaded(plugin);
    internal void OnServerShutdown() => _handler.OnServerShutdown();
}