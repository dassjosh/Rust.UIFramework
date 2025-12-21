using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiTranslate(UiTranslateDirection X, UiTranslateDirection Y)
{
    [JsonIgnore]
    public bool HasTranslate => X.HasValue || Y.HasValue;
    
    public UiTranslate(UiTranslateDirection value) : this(value, value) { }
    
    public static readonly UiTranslate DistanceDefault = new(UiTranslateDirection.DistanceDefault);
    public static readonly UiTranslate PercentageDefault = new(UiTranslateDirection.PercentageDefault);
    
    public static UiTranslate Lerp(in UiTranslate a, in UiTranslate b, float t) => new(UiTranslateDirection.Lerp(a.X, b.X, t), UiTranslateDirection.Lerp(a.Y, b.Y, t));
#pragma warning disable EPS05
    public static UiTranslate Lerp(UiTranslate a, UiTranslate b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05
    
    public UiTranslate Scale(float scale) => new(X * scale, Y * scale);
    public UiTranslate FlipX() => new(-X, Y);
    public UiTranslate FlipY() => new(X, -Y);
    public UiTranslate FlipXY() => new(-X, -Y);
    
    public static bool TryParse(ReadOnlySpan<char> input, out UiTranslate result)
    {
        result = default;
        if (input.IsEmptyOrWhitespace)
        {
            return false;
        }

        int splitIndex = input.IndexOf(' ');
        if (splitIndex == -1)
        {
            if (!UiTranslateDirection.TryParse(input, out UiTranslateDirection dir))
            {
                return false;
            }
            
            result = new UiTranslate(dir);
            return true;
        }

        ReadOnlySpan<char> spanX = input[..splitIndex];
        ReadOnlySpan<char> spanY = input[(splitIndex + 1)..];

        if (spanX.IsEmptyOrWhitespace || spanY.IsEmptyOrWhitespace)
        {
            return false;
        }

        if (!UiTranslateDirection.TryParse(spanX, out UiTranslateDirection x) || !UiTranslateDirection.TryParse(spanY, out UiTranslateDirection y))
        {
            return false;
        }
            
        result = new UiTranslate(x, y);
        return true;
    }
    
    public static UiTranslate Parse(string input) => TryParse(input, out UiTranslate result) ? result : throw new FormatException($"Unable to parse '{input}' as UiTranslate");
    
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