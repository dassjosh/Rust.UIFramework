using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public delegate float Easing(float t);

public static class EasingExt
{
    public static Easing In(this Easing easing) => easing;
    public static Easing Out(this Easing easing) => t => 1f - easing(1f - t);
    public static Easing InOut(this Easing easing) => t => t < 0.5f ? easing(t * 2f) / 2f : 1f - easing((1f - t) * 2f) / 2f;
    public static Easing PingPong(this Easing easing) => t => t < 0.5f ? easing(t * 2f) : 1f - easing((1f - t) * 2f);
    public static Easing PingPong(this Easing easing, float frequency) => t => easing((t * frequency) % 1f);
    public static Easing Reverse(this Easing easing) => t => easing(1f - t);
    public static Easing Repeat(this Easing easing, float repeats) => t => repeats < 0 ? easing(t) : easing((t * repeats) % 1f);
    public static Easing Offset(this Easing easing, float offset) => t => easing((t + offset) % 1f);
    public static Easing Scaled(this Easing easing, float min, float max) => t => min + (max - min) * easing(t);
    public static Easing FreezeAfter(this Easing easing, float freezePercentage) => t => t < freezePercentage ? easing(t) : easing(freezePercentage);
    public static Easing FreezeBefore(this Easing easing, float freezePercentage) => t => t >= freezePercentage ? easing(t) : easing(0);
    public static Easing Blend(this Easing a, Easing b, float blendFactor) => t => a(t) * (1f - blendFactor) + b(t) * blendFactor;
    
    public static Easing Clamp01(this Easing easing) => t => Mathf.Clamp01(easing(t));
    public static Easing Clamp(this Easing easing, float min, float max) => t => Mathf.Clamp(easing(t), min, max);
    
    public static float[] Sample(this Easing easing, int steps = 100)
    {
        float[] samples = new float[steps];
        for (int i = 0; i < steps; i++)
        {
            float t = i / (float)(steps - 1);
            samples[i] = easing(t);
        }
        return samples;
    }
}