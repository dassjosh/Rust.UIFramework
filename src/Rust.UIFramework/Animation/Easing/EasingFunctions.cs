using System;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

/// <summary>
/// Sourced from <a href="https://easings.net/">https://easings.net/</a>
/// </summary>
public static class EasingFunctions
{
    private const float PI = (float)Math.PI;
    private const float BackOvershoot = 1.70158f;

    public static readonly Easing Linear = t => t;
    public static readonly Easing Quad = t => t * t;
    public static readonly Easing Cubic = t => t * t * t;
    public static readonly Easing Quart = t => t * t * t * t;
    public static Easing Poly(int times) => t => Mathf.Pow(t, times);
    public static readonly Easing Circle = t => 1f - Mathf.Sqrt(1f - t * t);
    public static readonly Easing Sin = t => 1f - Mathf.Cos(t * PI / 2f);
    public static readonly Easing Bounce = t => 1f - BounceOut(1f - t);
    public static readonly Easing Exponential = t => Mathf.Pow(2f, 10f * (t - 1));
    public static readonly Easing Back = t => (BackOvershoot + 1f) * t * t * t - BackOvershoot * t * t;
    public static readonly Easing Elastic1 = Elastic(1);
    public static Easing Elastic(float bounciness) => t => 1 - Mathf.Pow(Mathf.Cos((t * PI) / 2), 3) * Mathf.Cos(t * PI * bounciness);
    public static readonly Easing Ease = BezierEasing.Ease.GetBezierResult;
    public static Easing Steps(int steps) => t => (float)Math.Floor(t * steps) / steps;
    
    private static float BounceOut(float t)
    {
        const float intensityFactor = 7.5625f;
        const float durationFactor = 2.75f;

        if (t < 1f / durationFactor)
        {
            return intensityFactor * t * t;
        }

        if (t < 2f / durationFactor)
        {
            t -= 1.5f / durationFactor;
            return intensityFactor * t * t + 0.75f;
        }

        if (t < 2.5f / durationFactor)
        {
            t -= 2.25f / durationFactor;
            return intensityFactor * t * t + 0.9375f;
        }

        t -= 2.625f / durationFactor;
        return intensityFactor * t * t + 0.984375f;
    }
}