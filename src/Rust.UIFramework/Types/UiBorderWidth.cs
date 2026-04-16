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

    public static UiBorderWidth Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiBorderWidth Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiBorderWidth result, token) ? result : throw FormatException.FailedParse<UiBorderWidth>(input);
    public static bool TryParse(string input, out UiBorderWidth padding, string token = " ") => TryParse(input.AsSpan(), out padding, token);

    public static bool TryParse(ReadOnlySpan<char> input, out UiBorderWidth padding, ReadOnlySpan<char> token = " ")
    {
        bool success = input.TryParseFourFloats(token, out (float left, float top, float right, float bottom) parsed);
        padding = success ? new UiBorderWidth(parsed.left, parsed.top, parsed.right, parsed.bottom) : default;
        return success;
    }
}