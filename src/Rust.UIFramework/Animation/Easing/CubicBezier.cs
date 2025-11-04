using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

/// <summary>
/// Cubic Bezier Points for Animations. You can find more or play around at the following URLs:
/// <br/><a href="https://easings.net">easings.net</a>
/// <br/><a href="https://cubic-bezier.com">cubic-bezier.com</a>
/// </summary>
public class CubicBezier
{
    private readonly float _ax, _bx, _cx;
    private readonly float _ay, _by, _cy;

    public CubicBezier(float x1, float y1, float x2, float y2)
    {
        _cx = 3f * x1;
        _bx = 3f * (x2 - x1) - _cx;
        _ax = 1f - _cx - _bx;

        _cy = 3f * y1;
        _by = 3f * (y2 - y1) - _cy;
        _ay = 1f - _cy - _by;
    }

    public CubicBezier(Vector2 p1, Vector2 p2) : this(p1.x, p1.y, p2.x, p2.y) { }

    public CubicBezier(double x1, double y1, double x2, double y2) : this((float)x1, (float)y1, (float)x2, (float)y2) { }

    public float GetBezierResult(float t)
    {
        float x = t;
        for (int i = 0; i < 4; i++)
        {
            float f = BezierX(x) - t;
            if (Mathf.Abs(f) < 1e-5f) break; // Early exit if close enough

            float df = BezierXDerivative(x);
            if (Mathf.Abs(df) < 1e-6f) break; // Avoid division by near-zero

            x -= f / df;
            x = Mathf.Clamp01(x);
        }

        return BezierY(x);
    }

    private float BezierX(float t) => ((_ax * t + _bx) * t + _cx) * t;
    private float BezierY(float t) => ((_ay * t + _by) * t + _cy) * t;
    private float BezierXDerivative(float t) => (3f * _ax * t + 2f * _bx) * t + _cx;

    public static implicit operator Easing(CubicBezier bezier) => bezier.GetBezierResult;
    public Easing ToEasing() => GetBezierResult;
}
