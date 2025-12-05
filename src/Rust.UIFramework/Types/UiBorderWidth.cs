using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiBorderWidthConverter))]
public readonly record struct UiBorderWidth(float Left, float Top, float Right, float Bottom)
{
    public static readonly UiBorderWidth Empty = new(0);
    public static readonly UiBorderWidth One = new(1);
    public static readonly UiBorderWidth Two = new(2);
    public static readonly UiBorderWidth Three = new(3);
    public static readonly UiBorderWidth Four = new(4);

    public UiBorderWidth(float width, float height) : this(width, height, width, height) { }
        
    public UiBorderWidth(float width) : this(width, width) { }

    public bool IsDefault() => this == Empty;

    public static UiBorderWidth Parse(string str, string token = " ") => Parse(str.AsSpan(), token);

    public static UiBorderWidth Parse(ReadOnlySpan<char> span, ReadOnlySpan<char> token = " ")
    {
        (float left, float top, float right, float bottom) = span.ParseFourFloats(token);
        return new UiBorderWidth(left, top, right, bottom);
    }

    public static bool TryParse(string str, out UiBorderWidth padding, string token = " ") => TryParse(str.AsSpan(), out padding, token);

    public static bool TryParse(ReadOnlySpan<char> span, out UiBorderWidth padding, ReadOnlySpan<char> token = " ")
    {
        bool success = span.TryParseFourFloats(token, out (float left, float top, float right, float bottom) parsed);
        padding = success ? new UiBorderWidth(parsed.left, parsed.top, parsed.right, parsed.bottom) : default;
        return success;
    }
}