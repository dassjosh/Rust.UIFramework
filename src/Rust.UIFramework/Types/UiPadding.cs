using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiPaddingConverter))]
public readonly record struct UiPadding(float Left, float Bottom, float Right, float Top)
{
    public static readonly UiPadding None = new(0);
    
    public bool IsSingleValue => Left == Bottom && Left == Right && Left == Top;

    public UiPadding(float horizontal, float vertical) : this(horizontal, vertical, horizontal, vertical) {}

    public UiPadding(float padding) : this(padding, padding, padding, padding) {}
    
    public static implicit operator UiOffset(UiPadding padding) => padding.ToOffset();
    public static implicit operator UiPosition(UiPadding padding) => padding.ToPosition();
    
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
    
    public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
    
}