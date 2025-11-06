using System;
using System.Threading;
using Network;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationHandler : ISingleton
{
    private readonly AnimationData _data = new();
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

    public ISendableAnimation GetAnimation(AnimationId id) => _data.GetAnimation(id);
    
    public void EnqueueAnimation(ISendableAnimation animation, SendInfo send)
    {
        if (animation == null) throw new ArgumentNullException(nameof(animation));
        animation.Send = send;
        animation.ChangeState(AnimationState.Queued);
        _data.EnqueueAnimation(animation);
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
                AnimationTime.UpdateTime(startTime, isPaused);
                isPaused = false;
                _logger.Debug("Processing {0} animations", _data.Count);
                ProcessAnimations();
                _logger.Debug("Processed animations. {0} remaining", _data.Count);
                float endTime = Time.realtimeSinceStartup;
                
                if (_data.Count == 0)
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
        foreach (PlayerAnimationData playerAnimations in _data.PlayerAnimations)
        {
            JsonFrameworkWriter writer = Create();
            foreach ((AnimationId id, ISendableAnimation animation) in playerAnimations.Animations)
            {
                ProcessAnimation(id, animation, writer);
            }
            
            SendAnimations(writer, playerAnimations.Send);
        }
        
        foreach ((AnimationId id, ISendableAnimation animation) in _data.GroupAnimations)
        {
            JsonFrameworkWriter writer = Create();
            ProcessAnimation(id, animation, writer);
            SendAnimations(writer, animation.Send);
        }
        
        _data.CleanupCompletedAnimations();
    }

    private void ProcessAnimation(AnimationId id, ISendableAnimation animation, JsonFrameworkWriter writer)
    {
        _logger.Debug("Processing Animation {0}", id.Id);

        try
        {
            if (animation.Parent is null)
            {
                if (animation.State == AnimationState.Queued)
                {
                    animation.OnStarted();
                }
                
                animation.OnTick();
                UiFrameworkExtension.GlobalLogger.Debug($"{nameof(AnimationHandler)}.{nameof(ProcessAnimation)} ID: {{0}} HasChanged: {{1}}", id.Id, animation.HasChanged);
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

    public void OnPlayerDisconnected(ulong playerId) => _data.OnPlayerDisconnected(playerId);
    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin) => _data.OnPluginUnloaded(plugin);
    internal void OnServerShutdown() => _cancellationTokenSource.Cancel();
}