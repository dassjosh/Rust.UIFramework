using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiPaddingConverter))]
public readonly record struct UiPadding(float Left, float Bottom, float Right, float Top)
{
    public static readonly UiPadding None = new(0);
    
    public bool IsSingleValue => Left == Bottom && Left == Right && Left == Top;

    public UiPadding(float horizontal, float vertical) : this(horizontal, vertical, horizontal, vertical) {}

    public UiPadding(float padding) : this(padding, padding, padding, padding) {}
    
    public static UiOffset operator +(UiOffset offset, UiPadding padding) => new(offset.Min.x + padding.Left, offset.Min.y + padding.Bottom, offset.Max.x - padding.Right,  offset.Max.y - padding.Top);
    public static UiPosition operator +(UiPosition position, UiPadding padding) => new(position.Min.x + padding.Left, position.Min.y + padding.Bottom, position.Max.x - padding.Right,  position.Max.y - padding.Top);
    
    public UiOffset ToOffset() => new(Left, Bottom, -Right, -Top);
    public UiPosition ToPosition() => new(Left, Bottom, -Right, -Top);

    public static UiPadding Parse(string str, string token = " ") => Parse(str.AsSpan(), token);

    public static UiPadding Parse(ReadOnlySpan<char> span, ReadOnlySpan<char> token = " ")
    {
        (float left, float top, float right, float bottom) = span.ParseFourFloats(token);
        return new UiPadding(left, top, right, bottom);
    }

    public static bool TryParse(string str, out UiPadding padding, string token = " ") => TryParse(str.AsSpan(), out padding, token);

    public static bool TryParse(ReadOnlySpan<char> span, out UiPadding padding, ReadOnlySpan<char> token = " ")
    {
        bool success = span.TryParseFourFloats(token, out (float left, float top, float right, float bottom) parsed);
        padding = success ? new UiPadding(parsed.left, parsed.top, parsed.right, parsed.bottom) : default;
        return success;
    }
    
    public static UiPadding Lerp(in UiPadding start, in UiPadding end, float progress)
    {
        return new UiPadding(
            Mathf.LerpUnclamped(start.Left, end.Left, progress), 
            Mathf.LerpUnclamped(start.Top, end.Top, progress), 
            Mathf.LerpUnclamped(start.Right, end.Right, progress), 
            Mathf.LerpUnclamped(start.Bottom, end.Bottom, progress));
    }
    
#pragma warning disable EPS05
    public static UiPadding Lerp(UiPadding start, UiPadding end, float progress)
    {
        return new UiPadding(
            Mathf.LerpUnclamped(start.Left, end.Left, progress), 
            Mathf.LerpUnclamped(start.Top, end.Top, progress), 
            Mathf.LerpUnclamped(start.Right, end.Right, progress), 
            Mathf.LerpUnclamped(start.Bottom, end.Bottom, progress));
    }
#pragma warning restore EPS05
    
    public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
}