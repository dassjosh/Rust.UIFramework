using System;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiDimensions2D(float Width, float Height)
{
    public int WidthInt => Mathf.RoundToInt(Width);
    public int HeightInt => Mathf.RoundToInt(Height);

    public UiDimensions2D(int size) : this(size, size) { }

    public static UiDimensions2D Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiDimensions2D Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiDimensions2D result, token) ? result : throw FormatException.FailedParse<UiDimensions2D>(input);
    public static bool TryParse(string input, out UiDimensions2D scale, string token = " ") => TryParse(input.AsSpan(), out scale, token);

    public static bool TryParse(ReadOnlySpan<char> input, out UiDimensions2D scale, ReadOnlySpan<char> token = " ")
    {
        bool success = input.TryParseTwoFloats(token, out (float horizontal, float vertical) parsed);
        scale = success ? new UiDimensions2D(parsed.horizontal, parsed.vertical) : default;
        return success;
    }

    public static UiDimensions2D Lerp(in UiDimensions2D a, in UiDimensions2D b, float t) => new UiDimensions2D(Mathf.Lerp(a.Width, b.Width, t), Mathf.Lerp(a.Height, b.Height, t));
    
#pragma warning disable EPS05
    public static UiDimensions2D Lerp(UiDimensions2D a, UiDimensions2D b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public static UiDimensions2D operator -(UiDimensions2D size) => new(-size.Width, -size.Height);

    public static UiDimensions2D operator +(UiDimensions2D size, float value) => new(size.Width + value, size.Height + value);
    public static UiDimensions2D operator -(UiDimensions2D size, float value) => new(size.Width - value, size.Height - value);
    public static UiDimensions2D operator *(UiDimensions2D size, float value) => new(size.Width * value, size.Height * value);
    
    public static UiDimensions2D operator +(UiDimensions2D size, double value) => size + (float)value;
    public static UiDimensions2D operator +(UiDimensions2D size, int value) => size + (float)value;
    public static UiDimensions2D operator -(UiDimensions2D size, double value) => size - (float)value;
    public static UiDimensions2D operator -(UiDimensions2D size, int value) => size - (float)value;
    public static UiDimensions2D operator *(UiDimensions2D size, double value) => size * (float)value;
    public static UiDimensions2D operator *(UiDimensions2D size, int value) => size * (float)value;

    public override string ToString() => $"{Width}x{Height}";
}