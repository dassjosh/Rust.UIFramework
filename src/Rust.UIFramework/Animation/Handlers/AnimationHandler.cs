using System;
using System.Threading;
using Network;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationHandler : ISingleton
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly AutoResetEvent _reset = new(false);
    private int _sendCount;
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<AnimationHandler>();

    private AnimationHandler()
    {
        Thread thread = new(AnimationLoop)
        {
            IsBackground = true
        };
        thread.Start(_cancellationTokenSource.Token);
    }
    
    public void EnqueueAnimation(ISendableAnimation animation, SendInfo send)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        animation.Send = send;
        animation.ChangeState(AnimationState.Queued);
        _reset.Set();
        _logger.Debug("Adding animation {0}", animation.Id);
    }
    
    private void AnimationLoop(object tokenObj)
    {
        CancellationToken token = (CancellationToken)tokenObj;

        bool isPaused = false;
        while (!token.IsCancellationRequested)
        {
            try
            {
                float startTime = Time.realtimeSinceStartup;
                Singleton<AnimationTime>.Instance.UpdateTime(startTime, isPaused);
                isPaused = false;
                _logger.Debug("Processing {0} animations", Singleton<AnimationData>.Instance.Count);
                ProcessAnimations();
                _logger.Debug("Processed animations. {0} remaining", Singleton<AnimationData>.Instance.Count);
                float endTime = Time.realtimeSinceStartup;
                
                if (Singleton<AnimationData>.Instance.Count == 0)
                {
                    _sendCount++;
                    int timeout = GetSleepDuration();
                    if (timeout == -1)
                    {
                        isPaused = true;
                    }
                    _reset.WaitOne(timeout);
                }
                else
                {
                    _sendCount = 0;
                    int processDuration = Mathf.RoundToInt((endTime - startTime) * 1000);
                    int sleepDuration = Mathf.Max(UiFrameworkConfig.Instance.Animations.UpdateRate - processDuration, 1);
                    Thread.Sleep(sleepDuration);
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(UiFrameworkConfig.Instance.Animations.UpdateRate);
                _logger.Exception("An error occurred while processing animations", ex);
            }
        }
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

    private int GetSleepDuration()
    {
        if (_sendCount > 4)
        {
            return -1;
        }
        
        return 25 + (1 << _sendCount);
    }

    public void OnPlayerDisconnected(ulong playerId) => Singleton<AnimationData>.Instance.OnPlayerDisconnected(playerId);
    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin) => Singleton<AnimationData>.Instance.OnPluginUnloaded(plugin);
    internal void OnServerShutdown() => _cancellationTokenSource.Cancel();
}