using System;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiScale(float Horizontal, float Vertical)
{
    public bool HasScale => !Mathf.Approximately(Horizontal, 1f) || !Mathf.Approximately(Vertical, 1f);
    
    public static UiScale Parse(string str) => Parse(str.AsSpan());

    public static UiScale Parse(ReadOnlySpan<char> span)
    {
        float horizontal = span.ParseNextFloat(" ", out span);
        float vertical = span.ParseNextFloat(" ", out span);
        return new UiScale(horizontal, vertical);
    }

    public static bool TryParse(string str, out UiScale scale) => TryParse(str.AsSpan(), out scale);

    public static bool TryParse(ReadOnlySpan<char> span, out UiScale scale)
    {
        bool horizontalParsed = span.TryParseNextFloat(" ", out span, out float horizontal);
        bool verticalParsed = span.TryParseNextFloat(" ", out span, out float vertical);
        bool success = horizontalParsed && verticalParsed;
        scale = success ? new UiScale(horizontal, vertical) : default;
        return success;
    }
    
    public static UiScale Lerp(UiScale start, UiScale end, float progress)
    {
        return new UiScale(Mathf.Lerp(start.Horizontal, end.Horizontal, progress), Mathf.Lerp(start.Vertical, end.Vertical, progress));
    }
}