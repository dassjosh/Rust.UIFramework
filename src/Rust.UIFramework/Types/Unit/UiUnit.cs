using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces.Types;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiUnitConverter))]
public readonly record struct UiUnit(float Value, UiUnitType Type) : ICssString
{
    public static readonly UiUnit ZeroPx = 0.Px();
    public static readonly UiUnit ZeroPercent = 0.Percent();

    [JsonIgnore]
    public bool HasValue => !Mathf.Approximately(Value, 0);
    
    public static UiUnit Parse(string input) => Parse(input.AsSpan());
    public static UiUnit Parse(ReadOnlySpan<char> input) => TryParse(input, out UiUnit result) ? result : throw FormatException.FailedParse<UiUnit>(input);
    public static bool TryParse(string input, out UiUnit direction) => TryParse(input.AsSpan(), out direction);

    public static bool TryParse(ReadOnlySpan<char> input, out UiUnit direction)
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

    public static bool TryParse(ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out UiUnit unit)
    {
        if(input.TryParseNextString(token, out remaining, out ReadOnlySpan<char> parsed) && TryParse(parsed, out unit))
        {
            return true;
        }

        unit = default;
        return false;
    }
    
    public static UiUnit Lerp(in UiUnit a, in UiUnit b, float t) => a with
    {
        Value = Mathf.LerpUnclamped(a.Value, b.Value, t)
    };
    
#pragma warning disable EPS05
    public static UiUnit Lerp(UiUnit a, UiUnit b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public static UiUnit operator -(UiUnit direction) => direction with { Value = -direction.Value };
    
    public static UiUnit operator +(UiUnit direction, float value) => direction with { Value = direction.Value + value };
    public static UiUnit operator -(UiUnit direction, float value) => direction with { Value = direction.Value - value };
    public static UiUnit operator *(UiUnit direction, float value) => direction with { Value = direction.Value * value };
    
    public static UiUnit operator +(UiUnit direction, double value) => direction + (float)value;
    public static UiUnit operator +(UiUnit direction, int value) => direction + (float)value;
    public static UiUnit operator -(UiUnit direction, double value) => direction - (float)value;
    public static UiUnit operator -(UiUnit direction, int value) => direction - (float)value;
    public static UiUnit operator *(UiUnit direction, double value) => direction * (float)value;
    public static UiUnit operator *(UiUnit direction, int value) => direction * (float)value;

    public override string ToString() => $"{Value}{(Type == UiUnitType.Px ? "px": "%")}";
    public string ToCssString() => ToString();
}