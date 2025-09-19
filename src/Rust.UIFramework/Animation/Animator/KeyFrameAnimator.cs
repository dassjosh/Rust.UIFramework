using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class KeyFrameColorAnimator(UiColor startColor, UiColor endColor) : KeyFrameAnimator<UiColor>(startColor, endColor, UiColor.Lerp);
public class KeyFrameOffsetAnimator(UiOffset startOffset, UiOffset endOffset) : KeyFrameAnimator<UiOffset>(startOffset, endOffset, (start, end, t) => UiOffset.LerpUnclamped(start, end, t));
public class KeyFramePositionAnimator(UiPosition startPosition, UiPosition endPosition) : KeyFrameAnimator<UiPosition>(startPosition, endPosition, (start, end, t) => UiPosition.LerpUnclamped(start, end, t));
public class KeyFrameStringAnimator(string start, string end) : KeyFrameAnimator<string>(start, end, LevenshteinDistanceExt.Lerp);

public abstract class KeyFrameAnimator<T> : ISimpleAnimator<T>
{
    private readonly SortedList<float, T> _keyFrames = [];
    private readonly Func<T, T, float, T> _lerp;

    protected KeyFrameAnimator(T start, T end, Func<T, T, float, T> lerp)
    {
        _lerp = lerp;
        AddKeyFrame(0, start);
        AddKeyFrame(100, end);
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