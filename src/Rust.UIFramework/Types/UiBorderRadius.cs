using System;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiBorderRadius(UiUnit TopLeft, UiUnit TopRight, UiUnit BottomRight, UiUnit BottomLeft)
{
    public static readonly UiBorderRadius None = new(0.Px());
    public static readonly UiBorderRadius PX4 = new(4.Px());
    public static readonly UiBorderRadius PX8 = new(8.Px());
    public static readonly UiBorderRadius PX12 = new(12.Px());
    public static readonly UiBorderRadius PX16 = new(16.Px());
    public static readonly UiBorderRadius PX20 = new(20.Px());
    public static readonly UiBorderRadius PX24 = new(24.Px());
    public static readonly UiBorderRadius PX28 = new(28.Px());
    public static readonly UiBorderRadius PX32 = new(32.Px());
    public static readonly UiBorderRadius PX36 = new(36.Px());

    public bool IsDefault() => this == None;

    public UiBorderRadius(UiUnit radius) : this(radius, radius, radius, radius) { }

    public static UiBorderRadius Horizontal(UiUnit left, UiUnit right) => new(left, right, right, left);
    public static UiBorderRadius Vertical(UiUnit top, UiUnit bottom) => new(top, top, bottom, bottom);

    public static UiBorderRadius Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiBorderRadius Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiBorderRadius result, token) ? result : throw FormatException.FailedParse<UiBorderRadius>(input);
    public static bool TryParse(string input, out UiBorderRadius padding, string token = " ") => TryParse(input.AsSpan(), out padding, token);
    public static bool TryParse(ReadOnlySpan<char> input, out UiBorderRadius padding, ReadOnlySpan<char> token = " ")
    {
        if(input.TryParse(token, UiUnit.TryParse, out (UiUnit topLeft, UiUnit topRight, UiUnit bottomRight, UiUnit bottomLeft) parsed))
        {
            padding = new UiBorderRadius(parsed.topLeft, parsed.topRight, parsed.bottomLeft, parsed.bottomRight);
            return true;
        }

        padding = default;
        return false;
    }

    public (Vector2 Tl, Vector2 Tr, Vector2 Br, Vector2 Bly) Apply(UiSize2D dimensions)
    {
        Vector2 tl = ApplyUnit(dimensions, TopLeft);
        Vector2 tr = ApplyUnit(dimensions, TopRight);
        Vector2 br = ApplyUnit(dimensions, BottomRight);
        Vector2 bl = ApplyUnit(dimensions, BottomLeft);

        return (tl, tr, br, bl);
    }

    private static Vector2 ApplyUnit(UiSize2D dimensions, UiUnit unit)
    {
        return unit.Type == UiUnitType.Px ? new Vector2(unit.Value, unit.Value) : new Vector2(unit.Value / 100f * dimensions.Width, unit.Value / 100f * dimensions.Height);
    }

    public static UiBorderRadius operator -(UiBorderRadius radius) => new(-radius.TopLeft, -radius.TopRight, -radius.BottomRight, -radius.BottomLeft);

    public static UiBorderRadius operator +(UiBorderRadius radius, float value) => new(radius.TopLeft + value, radius.TopRight + value, radius.BottomRight + value, radius.BottomLeft + value);
    public static UiBorderRadius operator -(UiBorderRadius radius, float value) => new(radius.TopLeft - value, radius.TopRight - value, radius.BottomRight - value, radius.BottomLeft - value);
    public static UiBorderRadius operator *(UiBorderRadius radius, float value) => new(radius.TopLeft * value, radius.TopRight * value, radius.BottomRight * value, radius.BottomLeft * value);

    public static UiBorderRadius operator +(UiBorderRadius radius, double value) => radius + (float)value;
    public static UiBorderRadius operator +(UiBorderRadius radius, int value) => radius + (float)value;
    public static UiBorderRadius operator -(UiBorderRadius radius, double value) => radius - (float)value;
    public static UiBorderRadius operator -(UiBorderRadius radius, int value) => radius - (float)value;
    public static UiBorderRadius operator *(UiBorderRadius radius, double value) => radius * (float)value;
    public static UiBorderRadius operator *(UiBorderRadius radius, int value) => radius * (float)value;

    public override string ToString() => $"{TopLeft}x{TopRight}x{BottomRight}x{BottomLeft}";
}