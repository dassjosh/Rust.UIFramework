using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiTranslateDirectionConverter))]
public readonly record struct UiTranslateDirection(float Value, UiTranslateType Type)
{
    public static readonly UiTranslateDirection DistanceDefault = UiTranslateDirection.Distance(0);
    public static readonly UiTranslateDirection PercentageDefault = UiTranslateDirection.Percentage(0);

    [JsonIgnore]
    public bool HasValue => !Mathf.Approximately(Value, 0);
    
    public static UiTranslateDirection Parse(string input) => Parse(input.AsSpan());
    public static UiTranslateDirection Parse(ReadOnlySpan<char> input) => TryParse(input, out UiTranslateDirection result) ? result : throw new FormatException($"Unable to parse '{input.ToString()}' as {nameof(UiTranslateDirection)}");
    
    public static bool TryParse(string input, out UiTranslateDirection direction) => TryParse(input.AsSpan(), out direction);
    
    public static bool TryParse(ReadOnlySpan<char> input, out UiTranslateDirection direction)
    {
        direction = default;
        if (input.IsEmptyOrWhitespace)
        {
            return false;
        }
            
        if (input.EndsWith("px", StringComparison.OrdinalIgnoreCase))
        {
            if (float.TryParse(input[..^2], out float value))
            {
                direction = value.Px();
                return true;
            }
        }
        else if (input.EndsWith("%"))
        {
            if (float.TryParse(input[..^1], out float value))
            {
                direction = value.Percent();
                return true;
            }
        }
        else if (float.TryParse(input, out float value))
        {
            direction = value.Px();
            return true;
        }
        
        return false;
    }
    
    public static UiTranslateDirection Lerp(in UiTranslateDirection a, in UiTranslateDirection b, float t) => a with
    {
        Value = Mathf.LerpUnclamped(a.Value, b.Value, t)
    };
    
#pragma warning disable EPS05
    public static UiTranslateDirection Lerp(UiTranslateDirection a, UiTranslateDirection b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public static UiTranslateDirection operator -(UiTranslateDirection direction) => direction with { Value = -direction.Value };
    
    public static UiTranslateDirection operator +(UiTranslateDirection direction, float value) => direction with { Value = direction.Value + value };
    public static UiTranslateDirection operator -(UiTranslateDirection direction, float value) => direction with { Value = direction.Value - value };
    public static UiTranslateDirection operator *(UiTranslateDirection direction, float value) => direction with { Value = direction.Value * value };
    
    public static UiTranslateDirection operator +(UiTranslateDirection direction, double value) => direction + (float)value;
    public static UiTranslateDirection operator +(UiTranslateDirection direction, int value) => direction + (float)value;
    public static UiTranslateDirection operator -(UiTranslateDirection direction, double value) => direction - (float)value;
    public static UiTranslateDirection operator -(UiTranslateDirection direction, int value) => direction - (float)value;
    public static UiTranslateDirection operator *(UiTranslateDirection direction, double value) => direction * (float)value;
    public static UiTranslateDirection operator *(UiTranslateDirection direction, int value) => direction * (float)value;

    public override string ToString() => $"{Value}{(Type == UiTranslateType.Distance ? "PX": "%")}";
}