using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class KeyFrameAnimator<T> : IAnimator<T>
{
    private readonly SortedList<float, KeyFrameValue> _keyFrames;
    private readonly UiLerp<T> _lerp;
    private TimingFunction _timing;

    public KeyFrameAnimator() : this(UiLerp.GetDefaultOrError<T>()) { }

    public KeyFrameAnimator(UiLerp<T> lerp)
    {
        _keyFrames = [];
        _lerp = lerp ?? throw new ArgumentNullException(nameof(lerp), "lerp cannot be null. Please pass a valid lerp function.");
    }

    public KeyFrameAnimator(IEnumerable<KeyValuePair<float, (T Value, TimingFunction Timing)>> keyFrames, UiLerp<T> lerp)
    {
        _keyFrames = new SortedList<float, KeyFrameValue>(keyFrames.ToDictionary(k => k.Key, k => new KeyFrameValue(k.Value.Value, k.Value.Timing)));
        _lerp = lerp ?? throw new ArgumentNullException(nameof(lerp), "lerp cannot be null. Please pass a valid lerp function.");
    }
    
    public KeyFrameAnimator<T> WithTiming(TimingFunction timing)
    {
        _timing = timing;
        return this;
    }
    
    public KeyFrameAnimator<T> From(T value, TimingFunction timing = null) => AddFrame(0, value, timing);
    public KeyFrameAnimator<T> To(T value) => AddFrame(100, value);
    
    public KeyFrameAnimator<T> AddFrame(float percentage, T value, TimingFunction timing = null)
    {
        if(percentage is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage must be between 0 and 100.");
        _keyFrames.Add(percentage, new KeyFrameValue(value, timing));
        return this;
    }
    
    public KeyFrameAnimator<T> AddFrames(IEnumerable<float> percentages, T value, TimingFunction timing = null)
    {
        foreach (float percentage in percentages)
        {
            AddFrame(percentage, value, timing);
        }

        return this;
    }
    
    public KeyFrameAnimator<T> AddFrames(ReadOnlySpan<float> percentages, T value, TimingFunction timing = null)
    {
        foreach (float percentage in percentages)
        {
            AddFrame(percentage, value, timing);
        }

        return this;
    }

    public KeyFrameAnimator<T> RemoveKeyFrame(float percentage)
    {
        _keyFrames.Remove(percentage);
        return this;
    }

    public T Get(float progress)
    {
        if (_keyFrames.Count == 0) return default;
        if (progress <= _keyFrames.Keys[0]) return _keyFrames.Values[0].Value;
        if (progress >= _keyFrames.Keys[^1]) return _keyFrames.Values[^1].Value;
        
        progress = Mathf.Clamp(progress * 100, 0, 100);
        for (int i = 0; i < _keyFrames.Count - 1; i++)
        {
            float keyFramePercentage = _keyFrames.Keys[i];
            float nextKeyFramePercentage = _keyFrames.Keys[i + 1];
            if (progress >= keyFramePercentage && progress < nextKeyFramePercentage)
            {
                KeyFrameValue start = _keyFrames.Values[i];
                KeyFrameValue next = _keyFrames.Values[i + 1];

                float elapsed = (progress - keyFramePercentage) / (nextKeyFramePercentage - keyFramePercentage);
                return _lerp(start.Value, next.Value, GetTiming(elapsed, start.Timing));
            }
        }
        
        return _keyFrames.Values[^1].Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float GetTiming(float progress, TimingFunction timing) => timing?.Invoke(progress) ?? _timing?.Invoke(progress) ?? progress;

    public FrozenKeyFrameAnimator<T> Freeze() => new(_keyFrames.Select(k => new KeyValuePair<float, (T Value, TimingFunction Timing)>(k.Key, (k.Value.Value, k.Value.Timing))), _lerp, _timing);
    
    private readonly struct KeyFrameValue(T value, TimingFunction timing)
    {
        public readonly T Value = value;
        public readonly TimingFunction Timing = timing;
    }
}