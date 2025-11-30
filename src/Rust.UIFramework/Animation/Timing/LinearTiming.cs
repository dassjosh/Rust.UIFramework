using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class LinearTiming
{
    private readonly SortedList<float, float> _stops;

    public LinearTiming()
    {
        _stops = [];
    }
    
    public LinearTiming(IEnumerable<KeyValuePair<float, float>> stops)
    {
        _stops = new SortedList<float, float>(stops.ToDictionary(k => k.Key, k => k.Value));
    }
    
    public LinearTiming AddFrame(float percentage, float value)
    {
        if(percentage is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage must be between 0 and 100.");
        _stops.Add(percentage, value);
        return this;
    }
    
    public LinearTiming AddFrames(IEnumerable<float> percentages, float value)
    {
        foreach (float percentage in percentages)
        {
            AddFrame(percentage, value);
        }

        return this;
    }
    
    public LinearTiming AddFrames(ReadOnlySpan<float> percentages, float value)
    {
        foreach (float percentage in percentages)
        {
            AddFrame(percentage, value);
        }

        return this;
    }

    public LinearTiming RemoveFrame(float percentage)
    {
        _stops.Remove(percentage);
        return this;
    }

    public float Evaluate(float t)
    {
        if (_stops.Count == 0) return t;
        if (t <= _stops.Keys[0]) return _stops.Values[0];
        if (t >= _stops.Keys[^1]) return _stops.Values[^1];

        float progress = Mathf.Clamp(t * 100, 0, 100);
        for (int i = 0; i < _stops.Count - 1; i++)
        {
            float keyFramePercentage = _stops.Keys[i];
            float nextKeyFramePercentage = _stops.Keys[i + 1];
            if (progress >= keyFramePercentage && progress < nextKeyFramePercentage)
            {
                float start = _stops.Values[i];
                float next = _stops.Values[i + 1];

                float elapsed = (progress - keyFramePercentage) / (nextKeyFramePercentage - keyFramePercentage);
                return Mathf.Lerp(start, next, elapsed);
            }
        }
        
        return _stops.Values[^1];
    }
    
    public FrozenLinearTiming Freeze() => new(_stops);

    public static implicit operator TimingFunction(LinearTiming linear) => linear.Evaluate;
    public TimingFunction ToTiming() => Evaluate;
}