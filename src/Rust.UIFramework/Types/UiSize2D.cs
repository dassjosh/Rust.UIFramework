using System;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiSize2D(float Width, float Height)
{
    public int WidthInt => Mathf.RoundToInt(Width);
    public int HeightInt => Mathf.RoundToInt(Height);
    public float AspectRatio => Width / Height;

    public static readonly UiSize2D Zero = new(0);
    public static readonly UiSize2D Size16 = new(16);
    public static readonly UiSize2D Size32 = new(32);
    public static readonly UiSize2D Size64 = new(64);
    public static readonly UiSize2D Size128 = new(128);
    public static readonly UiSize2D Size256 = new(256);
    public static readonly UiSize2D Size512 = new(512);
    public static readonly UiSize2D Size1024 = new(1024);

    public UiSize2D(int size) : this(size, size) { }

    public static UiSize2D Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiSize2D Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiSize2D result, token) ? result : throw FormatException.FailedParse<UiSize2D>(input);
    public static bool TryParse(string input, out UiSize2D scale, string token = " ") => TryParse(input.AsSpan(), out scale, token);

    public static bool TryParse(ReadOnlySpan<char> input, out UiSize2D scale, ReadOnlySpan<char> token = " ")
    {
        bool success = input.TryParseTwoFloats(token, out (float horizontal, float vertical) parsed);
        scale = success ? new UiSize2D(parsed.horizontal, parsed.vertical) : default;
        return success;
    }

    public static UiSize2D Lerp(in UiSize2D a, in UiSize2D b, float t) => new(Mathf.Lerp(a.Width, b.Width, t), Mathf.Lerp(a.Height, b.Height, t));
    
#pragma warning disable EPS05
    public static UiSize2D Lerp(UiSize2D a, UiSize2D b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public static UiSize2D operator -(UiSize2D size) => new(-size.Width, -size.Height);

    public static UiSize2D operator +(UiSize2D size, float value) => new(size.Width + value, size.Height + value);
    public static UiSize2D operator -(UiSize2D size, float value) => new(size.Width - value, size.Height - value);
    public static UiSize2D operator *(UiSize2D size, float value) => new(size.Width * value, size.Height * value);
    public static UiSize2D operator /(UiSize2D size, float value) => new(size.Width / value, size.Height / value);

    public static UiSize2D operator +(UiSize2D size, double value) => size + (float)value;
    public static UiSize2D operator +(UiSize2D size, int value) => size + (float)value;
    public static UiSize2D operator -(UiSize2D size, double value) => size - (float)value;
    public static UiSize2D operator -(UiSize2D size, int value) => size - (float)value;
    public static UiSize2D operator *(UiSize2D size, double value) => size * (float)value;
    public static UiSize2D operator *(UiSize2D size, int value) => size * (float)value;
    public static UiSize2D operator /(UiSize2D size, double value) => size / (float)value;
    public static UiSize2D operator /(UiSize2D size, int value) => size / (float)value;

    public override string ToString() => $"{Width:0.##}x{Height:0.##}";
}