using System;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

/// <summary>
/// Sourced from <a href="https://easings.net/">https://easings.net/</a>
/// </summary>
public static class TimingFunctions
{
    private const float PI = (float)Math.PI;
    private const float BackOvershoot = 1.70158f;

    public static readonly TimingFunction Ease = new CubicBezier(0.25, .1, 0.25, 1);
    public static readonly TimingFunction EaseIn = new CubicBezier(0.42,0,1,1);
    public static readonly TimingFunction SharpCurve = new CubicBezier(0.4, 0.0, 0.2, 1.0);
    public static readonly TimingFunction Pulse = new CubicBezier(0, 0, 0.2, 1);
    public static readonly TimingFunction Linear = t => t;
    public static readonly TimingFunction Quad = t => t * t;
    public static readonly TimingFunction Cubic = t => t * t * t;
    public static readonly TimingFunction Quart = t => t * t * t * t;
    public static TimingFunction Poly(int times) => t => Mathf.Pow(t, times);
    public static readonly TimingFunction Circle = t => 1f - Mathf.Sqrt(1f - t * t);
    public static readonly TimingFunction Sin = t => 1f - Mathf.Cos(t * PI / 2f);
    public static readonly TimingFunction Bounce = t => 1f - BounceOut(1f - t);
    public static readonly TimingFunction Exponential = t => Mathf.Pow(2f, 10f * (t - 1));
    public static readonly TimingFunction Back = t => (BackOvershoot + 1f) * t * t * t - BackOvershoot * t * t;
    public static readonly TimingFunction Elastic1 = Elastic(1);
    public static TimingFunction Elastic(float bounciness) => t => 1 - Mathf.Pow(Mathf.Cos(t * PI / 2), 3) * Mathf.Cos(t * PI * bounciness);
    public static TimingFunction Steps(int steps) => t => (float)Math.Floor(t * steps) / steps;
    
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

    //https://m3.material.io/styles/motion/easing-and-duration/tokens-specs#601d5552-a6e6-4d74-9886-ff8f24b9ec35
    public static class Material
    {
        public static readonly TimingFunction Emphasized = new ConfigurableBezier(new Vector2(0,0), new Vector2(0.05f, 0), new Vector2(0.133333f, 0.06f), new Vector2(0.166666f, 0.4f), new Vector2(0.208333f, 0.82f), new Vector2(0.25f, 1), new Vector2(1, 1));
        public static readonly TimingFunction EmphasizedDecelerate = new CubicBezier(0.05, 0.7, 0.1, 1);
        public static readonly TimingFunction EmphasizedAccelerate = new CubicBezier(0.3, 0.0, 0.8, 0.15);
        public static readonly TimingFunction Standard = new CubicBezier(0.2, 0.0, 0, 1.0);
        public static readonly TimingFunction StandardDecelerate = new CubicBezier(0, 0, 0, 1);
        public static readonly TimingFunction StandardAccelerate = new CubicBezier(0.3, 0, 1, 1);
        public static readonly TimingFunction FastOutSlowIn = new CubicBezier(0.4, 0, 0.2, 1);
    }
}