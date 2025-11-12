using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiBorderWidthConverter))]
public readonly struct UiBorderWidth(float left, float top, float right, float bottom) : IEquatable<UiBorderWidth>
{
    public static readonly UiBorderWidth Empty = new(0);
    public static readonly UiBorderWidth One = new(1);
    public static readonly UiBorderWidth Two = new(2);
    public static readonly UiBorderWidth Three = new(3);
    public static readonly UiBorderWidth Four = new(4);
        
    public readonly float Left = left;
    public readonly float Top = top;
    public readonly float Right = right;
    public readonly float Bottom = bottom;

    public UiBorderWidth(float width, float height) : this(width, height, width, height) { }
        
    public UiBorderWidth(float width) : this(width, width) { }

    public bool IsEmpty() => Left == 0 || Top == 0 || Right == 0 || Bottom == 0;

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
    
    public bool Equals(UiBorderWidth other) => Left.Equals(other.Left) && Top.Equals(other.Top) && Right.Equals(other.Right) && Bottom.Equals(other.Bottom);

    public override bool Equals(object obj) => obj is UiBorderWidth other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
    
    public static bool operator ==(UiBorderWidth left, UiBorderWidth right) => left.Equals(right);
    public static bool operator !=(UiBorderWidth left, UiBorderWidth right) => !(left == right);
    public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
}