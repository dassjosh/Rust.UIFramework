using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiScaleConverter))]
public readonly record struct UiScale(float Horizontal, float Vertical)
{
    public bool HasScale => !Mathf.Approximately(Horizontal, 1f) || !Mathf.Approximately(Vertical, 1f);
    
    public static UiScale Parse(string str, string token = " ") => Parse(str.AsSpan(), token);

    public static UiScale Parse(ReadOnlySpan<char> span, ReadOnlySpan<char> token = " ")
    {
        (float horizontal, float vertical) = span.ParseTwoFloats(token);
        return new UiScale(horizontal, vertical);
    }

    public static bool TryParse(string str, out UiScale scale, string token = " ") => TryParse(str.AsSpan(), out scale, token);

    public static bool TryParse(ReadOnlySpan<char> span, out UiScale scale, ReadOnlySpan<char> token = " ")
    {
        bool success = span.TryParseTwoFloats(token, out (float horizontal, float vertical) parsed);
        scale = success ? new UiScale(parsed.horizontal, parsed.vertical) : default;
        return success;
    }
    
    public static UiScale Lerp(UiScale start, UiScale end, float progress)
    {
        return new UiScale(Mathf.Lerp(start.Horizontal, end.Horizontal, progress), Mathf.Lerp(start.Vertical, end.Vertical, progress));
    }

    public override string ToString() => $"{Horizontal} {Vertical}";
}