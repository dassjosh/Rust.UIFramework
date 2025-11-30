using System.Runtime.CompilerServices;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiTranslate(UiTranslateDirection X, UiTranslateDirection Y)
{
    public bool HasTranslate => X.HasValue || Y.HasValue;
    
    public UiTranslate(UiTranslateDirection value) : this(value, value) { }
    
    public static readonly UiTranslate DistanceDefault = new(UiTranslateDirection.DistanceDefault, UiTranslateDirection.DistanceDefault);
    public static readonly UiTranslate PercentageDefault = new(UiTranslateDirection.PercentageDefault, UiTranslateDirection.PercentageDefault);
    
    public static UiTranslate Lerp(in UiTranslate a, in UiTranslate b, float t) => new(UiTranslateDirection.Lerp(a.X, b.X, t), UiTranslateDirection.Lerp(a.Y, b.Y, t));
#pragma warning disable EPS05
    public static UiTranslate Lerp(UiTranslate a, UiTranslate b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public (Vector2 Min, Vector2 Max) Apply(Vector2 min, Vector2 max)
    {
        (float minX, float maxX) = Apply(min.x, max.x, X);
        (float minY, float maxY) = Apply(min.y, max.y, Y);
        return (new Vector2(minX, minY), new Vector2(maxX, maxY));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (float Min, float Max) Apply(float min, float max, UiTranslateDirection direction)
    {
        if (!direction.HasValue)
        {
            return (min, max);
        }
        
        if(direction.Type == UiTranslateType.Distance)
        {
            return (min + direction.Value, max + direction.Value);
        }

        float size = max - min;
        return (min + direction.Value * size, max + direction.Value * size);
    }
}