namespace Oxide.Ext.UiFramework.Animation;

/// <summary>
/// Cubic Bezier Points for Animations. You can find more or player around at the following URLs:
/// <a href="https://easings.net">easings.net</a>
/// <a href="https://cubic-bezier.com">cubic-bezier.com</a>
/// </summary>
/// <param name="X1"></param>
/// <param name="Y1"></param>
/// <param name="X2"></param>
/// <param name="Y2"></param>
public readonly record struct BezierEasing(float X1, float Y1, float X2, float Y2)
{
    public static readonly BezierEasing Ease = new(.25f, .1f, .25f, 1f);
    // public static readonly BezierProgressor Linear = new(0f, 0f, 1f, 1f);
    // public static readonly BezierProgressor EaseIn = new(0.42f, 0f, 1f, 1f);
    // public static readonly BezierProgressor EaseOut = new(0f, 0f, 0.58f, 1f);
    // public static readonly BezierProgressor EaseInOut = new(0.42f, 0f, 0.58f, 1f);
    
    public float GetBezierResult(float t)
    {
        // The cubic Bezier formula for each dimension:
        // B(t) = (1-t)³*P0 + 3*(1-t)²*t*P1 + 3*(1-t)*t²*P2 + t³*P3
        float oneMinusT = 1 - t;
        float oneMinusTSquared = oneMinusT * oneMinusT;
        float oneMinusTCubed = oneMinusTSquared * oneMinusT;
        float timeSquared = t * t;
        float timeCubed = timeSquared * t;
        
        return oneMinusTCubed * X1 + 3 * oneMinusTSquared * t * Y1 + 3 * oneMinusT * timeSquared * Y2 + timeCubed * X2;
    }

    public static implicit operator Easing(BezierEasing bezier) => bezier.GetBezierResult;
}