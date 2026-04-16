using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public delegate float TimingFunction(float t);

public static class TimingFunctionExt
{
    extension(TimingFunction timing)
    {
        public TimingFunction Out() => t => 1f - timing(1f - t);
        public TimingFunction InOut() => t => t < 0.5f ? timing(t * 2f) / 2f : 1f - timing((1f - t) * 2f) / 2f;
        public TimingFunction PingPong() => t => t < 0.5f ? timing(t * 2f) : 1f - timing((1f - t) * 2f);
        public TimingFunction PingPong(float frequency) => t => timing((t * frequency) % 1f);
        public TimingFunction Reverse() => t => timing(1f - t);
        public TimingFunction Repeat(float repeats) => t => repeats < 0 ? timing(t) : timing((t * repeats) % 1f);
        public TimingFunction Offset(float offset) => t => timing((t + offset) % 1f);
        public TimingFunction Scaled(float min, float max) => t => min + (max - min) * timing(t);
        public TimingFunction FreezeAfter(float freezePercentage) => t => t < freezePercentage ? timing(t) : timing(freezePercentage);
        public TimingFunction FreezeBefore(float freezePercentage) => t => t >= freezePercentage ? timing(t) : timing(0);
        public TimingFunction Blend(TimingFunction b, float blendFactor) => t => timing(t) * (1f - blendFactor) + b(t) * blendFactor;
        public TimingFunction Clamp01() => t => Mathf.Clamp01(timing(t));
        public TimingFunction Clamp(float min, float max) => t => Mathf.Clamp(timing(t), min, max);
        public TimingFunction StepStart() => t => timing(1);
        public TimingFunction StepEnd() => t => t < 1 ? timing(0) : timing(1);
        public TimingFunction Combine(TimingFunction next) => t => next(timing(t));

        public float[] Sample(int steps = 100)
        {
            float[] samples = new float[steps];
            for (int i = 0; i < steps; i++)
            {
                float t = i / (float)(steps - 1);
                samples[i] = timing(t);
            }
            return samples;
        }
    }
}