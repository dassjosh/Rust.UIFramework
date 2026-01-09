using System;
using System.Collections;
using System.Threading;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

internal class BehaviorAnimationHandler : FacepunchBehaviour, IAnimationHandler
{
    private AnimationHandler _handler;
    private readonly WaitForSecondsRealtime _nextAnimationTick = new(1f);
    private readonly WaitUntil _waitUntilQueued = new(() => Singleton<AnimationData>.Instance.Count != 0);
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<BehaviorAnimationHandler>();
    
    public void OnInit(AnimationHandler handler)
    {
        _handler = handler;
        StartCoroutine(AnimationLoop());
    }

    public void OnAnimationQueued() { }

    private IEnumerator AnimationLoop()
    {
        bool isPaused = false;
        while (true)
        {
            float timeTaken = UiFrameworkConfig.Instance.Animations.UpdateRate;
            try
            {
                timeTaken = _handler.TickAnimation(ref isPaused);
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occurred while processing animations", ex);
            }
            
            if (Singleton<AnimationData>.Instance.Count == 0)
            {
                yield return _waitUntilQueued;
            }
            else
            {
                int processDuration = Mathf.RoundToInt(timeTaken * 1000);
                int sleepDuration = Mathf.Max(UiFrameworkConfig.Instance.Animations.UpdateRate - processDuration, 1);
                _nextAnimationTick.waitTime = sleepDuration / 1000f;
                yield return _nextAnimationTick;
            }
        }
    }
    
    public void OnServerShutdown() { }
}