using System;
using System.Collections;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;


internal class BehaviorAnimationHandler : FacepunchBehaviour, IAnimationHandler, IBehaviorSingleton
{
    private AnimationHandler _handler;
    private readonly WaitForSecondsRealtime _nextAnimationTick = new(UiFrameworkConfig.Instance.Animations.UpdateRate * 1000f);
    private readonly IUiLogger _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<BehaviorAnimationHandler>();
    private Coroutine _animationCoroutine;

    public void OnInit(AnimationHandler handler)
    {
        _handler = handler;
    }

    public void OnAnimationQueued() => TryStartCoroutine();

    private void TryStartCoroutine()
    {
        if (_animationCoroutine != null)
        {
            _logger.Error("Not Starting Coroutine. Animation coroutine already running");
            return;
        }
        
        StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        try
        {
            bool isFirst = true;
            float updateRateSeconds = UiFrameworkConfig.Instance.Animations.UpdateRate / 1000f;

            while (Singleton<AnimationData>.Instance.Count != 0)
            {
                float timeTaken = 0;
                try
                {
                    timeTaken = _handler.TickAnimation(isFirst);
                }
                catch (Exception ex)
                {
                    _logger.Exception("An error occurred while processing animations", ex);
                }
                
                float sleepDuration = Mathf.Clamp(updateRateSeconds - timeTaken, 0f, updateRateSeconds);
                _nextAnimationTick.waitTime = sleepDuration;
                isFirst = false;
                yield return _nextAnimationTick;
            }
        }
        finally
        {
            _animationCoroutine = null;
        }
    }

    public void OnServerShutdown()
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }
    }
}
