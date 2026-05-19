using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(UiPivotConverter))]
public readonly record struct UiPivot(float Horizontal, float Vertical)
{
    public static readonly UiPivot TopLeft = new(0, 1);
    public static readonly UiPivot TopMiddle = new(0.5f, 1);
    public static readonly UiPivot TopRight = new(1, 1);
    public static readonly UiPivot BottomLeft = new(0, 0);
    public static readonly UiPivot BottomMiddle = new(0.5f, 0);
    public static readonly UiPivot BottomRight = new(1, 0);
    public static readonly UiPivot MiddleLeft = new(0, 0.5f);
    public static readonly UiPivot MiddleMiddle = new(0.5f, 0.5f);
    public static readonly UiPivot MiddleRight = new(1f, 0.5f);
    public static readonly UiPivot Center = new(0.5f, 0.5f);

    public static implicit operator Vector2(UiPivot pivot) => new(pivot.Horizontal, pivot.Vertical);
    public static implicit operator UiPivot(Vector2 vector) => new(vector.x, vector.y);

    public static UiPivot Parse(string input, string token = " ") => Parse(input.AsSpan(), token);
    public static UiPivot Parse(ReadOnlySpan<char> input, ReadOnlySpan<char> token = " ") => TryParse(input, out UiPivot result, token) ? result : throw FormatException.FailedParse<UiPivot>(input);
    public static bool TryParse(string input, out UiPivot pivot, string token = " ") => TryParse(input.AsSpan(), out pivot, token);

    public static bool TryParse(ReadOnlySpan<char> input, out UiPivot pivot, ReadOnlySpan<char> token = " ")
    {
        bool success = input.TryParseTwoFloats(token, out (float Horizontal, float Vertical) parsed);
        pivot = success ? new UiPivot(parsed.Horizontal, parsed.Vertical) : default;
        return success;
    }

    public static UiPivot Lerp(in UiPivot start, in UiPivot end, float progress)
    {
        return new UiPivot(
            Mathf.LerpUnclamped(start.Horizontal, end.Horizontal, progress),
            Mathf.LerpUnclamped(start.Vertical, end.Vertical, progress));
    }

#pragma warning disable EPS05
    public static UiPivot Lerp(UiPivot start, UiPivot end, float progress) => Lerp(in start, in end, progress);
#pragma warning restore EPS05

    public override string ToString() => $"({Horizontal}, {Vertical})";
}