using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct BezierPoints(float X1, float Y1, float X2, float Y2)
{
    public static readonly BezierPoints Ease = new(.25f, .1f, .25f, 1f);
    public static readonly BezierPoints Linear = new(0f, 0f, 1f, 1f);
    public static readonly BezierPoints EaseIn = new(0.42f, 0f, 1f, 1f);
    public static readonly BezierPoints EaseOut = new(0f, 0f, 0.58f, 1f);
    public static readonly BezierPoints EaseInOut = new(0.42f, 0f, 0.58f, 1f);
    
    public float GetBezierResult(float t)
    {
        // The cubic Bezier formula for each dimension:
        // B(t) = (1-t)³*P0 + 3*(1-t)²*t*P1 + 3*(1-t)*t²*P2 + t³*P3
        t = Mathf.Clamp01(t);
        float oneMinusT = 1 - t;
        float oneMinusTSquared = oneMinusT * oneMinusT;
        float oneMinusTCubed = oneMinusTSquared * oneMinusT;
        float timeSquared = t * t;
        float timeCubed = timeSquared * t;
        
        return oneMinusTCubed * X1 + 3 * oneMinusTSquared * t * Y1 + 3 * oneMinusT * timeSquared * Y2 + timeCubed * X2;
    }
    
    public UiPosition GetPosition(in UiPosition startPos, in UiPosition endPos, float t)
    {
        float result = GetBezierResult(t);
        return UiPosition.LerpUnclamped(startPos, endPos, result);
    }
    
    public UiOffset GetOffset(in UiOffset startPos, in UiOffset endPos, float t)
    {
        float result = GetBezierResult(t);
        return UiOffset.LerpUnclamped(startPos, endPos, result);
    }
    
    public UiColor GetColor(in UiColor startPos, in UiColor endPos, float t)
    {
        float result = GetBezierResult(t);
        return UiColor.Lerp(startPos, endPos, result);
    }
}