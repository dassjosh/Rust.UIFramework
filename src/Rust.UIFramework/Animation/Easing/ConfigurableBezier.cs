using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class ConfigurableBezier
{
    private readonly float[] _controlPointsX;
    private readonly float[] _controlPointsY;
    private readonly int _count;

    public ConfigurableBezier(ICollection<Vector2> controlPoints)
    {
        if (controlPoints == null) throw new ArgumentNullException(nameof(controlPoints));
        _controlPointsX = controlPoints.Select(c => c.x).ToArray();
        _controlPointsY = controlPoints.Select(c => c.y).ToArray();
        _count = controlPoints.Count;
    }
    
    public ConfigurableBezier(IEnumerable<Vector2> controlPoints) : this(controlPoints as ICollection<Vector2> ?? controlPoints.ToArray()) { }

    public ConfigurableBezier(params Vector2[] controlPoints) : this((ICollection<Vector2>)controlPoints) { }

    // Evaluate the full point at parameter t
    public float Get(float[] controlPoints, float t)
    {
        int count = _count;
        Span<float> points = stackalloc float[count];
        controlPoints.CopyTo(points);
        
        while (count > 1)
        {
            for (int i = 0; i < count - 1; i++)
            {
                points[i] = Lerp(points[i], points[i + 1], t);
            }
            count--;
        }

        return points[0];
    }
    
    // Evaluate only the Y value at parameter t
    public float GetY(float t)
    {
        return Get(_controlPointsY, t);
    }

    // Evaluate only the X value at parameter t
    public float BezierX(float t)
    {
        return Get(_controlPointsX, t);
    }

    // Evaluate Y value given an X input using Newton-Raphson iteration
    public float SolveY(float t)
    {
        float x = t; // Initial guess
        for (int i = 0; i < 4; i++)
        {
            float f = BezierX(x) - t;
            if (Mathf.Abs(f) < 1e-5f) break; // Early exit if close enough
            float dx = BezierXDerivative(x);
            if (Mathf.Abs(dx) < 1e-6f) break; // Avoid division by near-zero

            x -= f / dx;
            x = Mathf.Clamp01(x);
        }

        return GetY(x);
    }

    // Approximate derivative of X(t) using central difference
    private float BezierXDerivative(float t)
    {
        const float delta = 1e-4f;
        float t1 = Mathf.Clamp01(t - delta);
        float t2 = Mathf.Clamp01(t + delta);
        return (BezierX(t2) - BezierX(t1)) / (t2 - t1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float Lerp(float start, float end, float t) => start + (end - start) * t;


    public static implicit operator Easing(ConfigurableBezier bezier) => bezier.SolveY;
    public Easing ToEasing() => SolveY;
}
