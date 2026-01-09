using System;
using System.Threading;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class ThreadedAnimationHandler : IAnimationHandler
{
    private AnimationHandler _handler;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly AutoResetEvent _reset = new(false);
    private int _sendCount;
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<ThreadedAnimationHandler>();

    public void OnAnimationQueued()
    {
        _reset.Set();
    }

    public void OnInit(AnimationHandler handler)
    {
        _handler = handler;
        Thread thread = new(AnimationLoop)
        {
            IsBackground = true
        };
        thread.Start();
    }
    
    private void AnimationLoop()
    {
        CancellationToken token = _cancellationTokenSource.Token;

        bool isPaused = false;
        while (!token.IsCancellationRequested)
        {
            try
            {
                float timeTaken = _handler.TickAnimation(ref isPaused);
                
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
                    int processDuration = Mathf.RoundToInt(timeTaken * 1000);
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
    
    private int GetSleepDuration()
    {
        if (_sendCount > 4)
        {
            return -1;
        }
        
        return 25 + (1 << _sendCount);
    }
    
    public void OnServerShutdown()
    {
        _cancellationTokenSource.Cancel();
    }
}