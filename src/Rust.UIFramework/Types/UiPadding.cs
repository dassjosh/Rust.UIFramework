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

    public static UiPadding Parse(string str) => Parse(str.AsSpan());

    public static UiPadding Parse(ReadOnlySpan<char> span)
    {
        float left = span.ParseNextFloat(" ", out span);
        float top = span.ParseNextFloat(" ", out span);
        float right = span.ParseNextFloat(" ", out span);
        float bottom = span.ParseNextFloat(" ", out span);
        return new UiPadding(left, bottom, right, top);
    }

    public static bool TryParse(string str, out UiPadding padding) => TryParse(str.AsSpan(), out padding);

    public static bool TryParse(ReadOnlySpan<char> span, out UiPadding padding)
    {
        bool leftParsed = span.TryParseNextFloat(" ", out span, out float left);
        bool topParsed = span.TryParseNextFloat(" ", out span, out float top);
        bool rightParsed = span.TryParseNextFloat(" ", out span, out float right);
        bool bottomParsed = span.TryParseNextFloat(" ", out span, out float bottom);
        bool success = leftParsed && topParsed && rightParsed && bottomParsed;
        padding = success ? new UiPadding(left, bottom, right, top) : default;
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