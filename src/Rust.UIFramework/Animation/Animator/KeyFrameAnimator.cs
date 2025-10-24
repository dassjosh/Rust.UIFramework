using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class KeyFrameAnimator<T> : IAnimator<T>
{
    private readonly SortedList<float, T> _keyFrames = [];
    private readonly UiLerp<T> _lerp;

    public KeyFrameAnimator(T start, T end) : this(start, end, UiLerp.GetDefault<T>()) { }
    
    public KeyFrameAnimator(T start, T end, UiLerp<T> lerp)
    {
        AddKeyFrame(0, start);
        AddKeyFrame(100, end);
        _lerp = lerp ?? throw new ArgumentNullException(nameof(lerp), "lerp cannot be null. Please pass a valid lerp function.");;
    }

    public void AddKeyFrame(float percentage, T value)
    {
        if(percentage is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage must be between 0 and 100.");
        _keyFrames.Add(percentage, value);
    }

    public T Get(float progress)
    {
        progress = Mathf.Clamp(progress * 100, 0, 100);
        for (int i = 0; i < _keyFrames.Count - 1; i++)
        {
            float keyFramePercentage = _keyFrames.Keys[i];
            float nextKeyFramePercentage = _keyFrames.Keys[i + 1];
            if (progress >= keyFramePercentage && progress < nextKeyFramePercentage)
            {
                T start = _keyFrames.Values[i];
                T next = _keyFrames.Values[i + 1];

                float elapsed = (progress - keyFramePercentage) / (nextKeyFramePercentage - keyFramePercentage);
                return _lerp(start, next, elapsed);
            }
        }
        
        return _keyFrames.Values[^1];
    }
}