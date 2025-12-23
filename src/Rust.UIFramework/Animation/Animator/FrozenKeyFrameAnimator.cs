using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class FrozenKeyFrameAnimator<T> : IAnimator<T>, IKeyFrame<T>
{
    private readonly KeyFrame[] _keyFrames;
    private readonly UiLerp<T> _lerp;
    private readonly TimingFunction _timing;

    public FrozenKeyFrameAnimator(IEnumerable<KeyValuePair<float, (T Value, TimingFunction Timing)>> keyFrames, UiLerp<T> lerp, TimingFunction timing)
    {
        _keyFrames = keyFrames.Select(k => new KeyFrame(k.Key, k.Value.Value, k.Value.Timing)).ToArray();
        _lerp = lerp ?? throw new ArgumentNullException(nameof(lerp), "lerp cannot be null. Please pass a valid lerp function.");
        _timing = timing;
    }
    
    public T Get(float progress)
    {
        if (_keyFrames.Length == 0) return default;
        if (progress <= _keyFrames[0].Percentage) return _keyFrames[0].Value;
        if (progress >= _keyFrames[^1].Percentage) return _keyFrames[^1].Value;
        
        progress = Mathf.Clamp(progress * 100, 0, 100);
        for (int i = 0; i < _keyFrames.Length - 1; i++)
        {
            KeyFrame start = _keyFrames[i];
            KeyFrame next = _keyFrames[i + 1];
            
            float keyFramePercentage = _keyFrames[i].Percentage;
            float nextKeyFramePercentage = _keyFrames[i + 1].Percentage;
            if (progress >= start.Percentage && progress < next.Percentage)
            {
                float elapsed = (progress - keyFramePercentage) / (nextKeyFramePercentage - keyFramePercentage);
                return _lerp(start.Value, next.Value, GetTiming(elapsed, start.Timing));
            }
        }
        
        return _keyFrames[^1].Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float GetTiming(float progress, TimingFunction timing) => timing?.Invoke(progress) ?? _timing?.Invoke(progress) ?? progress;
    
    private sealed class KeyFrame(float percentage, T value, TimingFunction timing)
    {
        public readonly float Percentage = percentage;
        public readonly T Value = value;
        public readonly TimingFunction Timing = timing;
    }
    
    public KeyFrameAnimator<T> Copy() => new(_keyFrames.Select(k => new KeyValuePair<float, (T Value, TimingFunction Timing)>(k.Percentage, (k.Value, k.Timing))), _lerp);
    public IEnumerator<KeyValuePair<float, T>> GetEnumerator() => _keyFrames.Select(frame => new KeyValuePair<float, T>(frame.Percentage, frame.Value)).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public string ToCssString() => this.BuildCssKeyFrames();
}