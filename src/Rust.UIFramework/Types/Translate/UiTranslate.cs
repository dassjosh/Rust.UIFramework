using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiTranslate(UiUnit X, UiUnit Y) : ICssString
{
    [JsonIgnore]
    public bool HasTranslate => X.HasValue || Y.HasValue;
    
    public UiTranslate(UiUnit value) : this(value, value) { }
    
    public static readonly UiTranslate ZeroPx = new(UiUnit.ZeroPx);
    public static readonly UiTranslate ZeroPercent = new(UiUnit.ZeroPercent);
    
    [Pure]
    public UiTranslate Scale(float scale) => new(X * scale, Y * scale);
    public UiTranslate Scale(UiScale scale) => new(X * scale.Horizontal, Y * scale.Vertical);
    [Pure]
    public UiTranslate FlipX() => new(-X, Y);
    [Pure]
    public UiTranslate FlipY() => new(X, -Y);
    [Pure]
    public UiTranslate FlipXY() => new(-X, -Y);
    
    [Pure]
    public static UiTranslate operator -(UiTranslate translate) => translate.FlipXY();
    
    public static UiTranslate Parse(string input) => Parse(input.AsSpan());
    public static UiTranslate Parse(ReadOnlySpan<char> input) => TryParse(input, out UiTranslate result) ? result : throw FormatException.FailedParse<UiTranslate>(input);
    public static bool TryParse(string input, out UiTranslate padding) => TryParse(input.AsSpan(), out padding);
    
    public static bool TryParse(ReadOnlySpan<char> input, out UiTranslate result)
    {
        if(input.TryParse(" ", UiUnit.TryParse, out (UiUnit X, UiUnit Y) parsed))
        {
            result = new UiTranslate(parsed.X, parsed.Y);
            return true;
        }

        result = default;
        return false;
    }
    
    public (Vector2 Min, Vector2 Max) Apply(Vector2 min, Vector2 max)
    {
        (float minX, float maxX) = Apply(min.x, max.x, X);
        (float minY, float maxY) = Apply(min.y, max.y, Y);
        return (new Vector2(minX, minY), new Vector2(maxX, maxY));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (float Min, float Max) Apply(float min, float max, UiUnit direction)
    {
        if (!direction.HasValue)
        {
            return (min, max);
        }
        
        if(direction.Type == UiUnitType.Px)
        {
            return (min + direction.Value, max + direction.Value);
        }

        float size = max - min;
        return (min + direction.Value * size, max + direction.Value * size);
    }
    
    public static UiTranslate Lerp(in UiTranslate a, in UiTranslate b, float t) => new(UiUnit.Lerp(a.X, b.X, t), UiUnit.Lerp(a.Y, b.Y, t));
#pragma warning disable EPS05
    public static UiTranslate Lerp(UiTranslate a, UiTranslate b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05
    
    public string ToCssString()
    {
        if (!HasTranslate)
        {
            return "translate: translate(0);";
        }

        if (!X.HasValue)
        {
            return $"translate: translateY({Y.ToCssString()});";
        }
      
        if (!Y.HasValue)
        {
            return $"translate: translateX({X.ToCssString()});";
        }
        
        return $"translate: translate({X.ToCssString()}, {Y.ToCssString()});";
    }
}