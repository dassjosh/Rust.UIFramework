using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public delegate float Easing(float t);

public static class EasingExt
{
    extension(Easing easing)
    {
        public Easing Out() => t => 1f - easing(1f - t);
        public Easing InOut() => t => t < 0.5f ? easing(t * 2f) / 2f : 1f - easing((1f - t) * 2f) / 2f;
        public Easing PingPong() => t => t < 0.5f ? easing(t * 2f) : 1f - easing((1f - t) * 2f);
        public Easing PingPong(float frequency) => t => easing((t * frequency) % 1f);
        public Easing Reverse() => t => easing(1f - t);
        public Easing Repeat(float repeats) => t => repeats < 0 ? easing(t) : easing((t * repeats) % 1f);
        public Easing Offset(float offset) => t => easing((t + offset) % 1f);
        public Easing Scaled(float min, float max) => t => min + (max - min) * easing(t);
        public Easing FreezeAfter(float freezePercentage) => t => t < freezePercentage ? easing(t) : easing(freezePercentage);
        public Easing FreezeBefore(float freezePercentage) => t => t >= freezePercentage ? easing(t) : easing(0);
        public Easing Blend(Easing b, float blendFactor) => t => easing(t) * (1f - blendFactor) + b(t) * blendFactor;
        public Easing Clamp01() => t => Mathf.Clamp01(easing(t));
        public Easing Clamp(float min, float max) => t => Mathf.Clamp(easing(t), min, max);

        public float[] Sample(int steps = 100)
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
}