using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache;

internal static class UiColorCache
{
    private const string Format = "0.####";
    private const char Space = ' ';

    private static readonly Dictionary<UiColor, Utf8String> ColorCache = new();
        
    public static void WriteColor(JsonUtf8Writer writer, UiColor uiColor)
    {
        if (!ColorCache.TryGetValue(uiColor, out Utf8String color))
        {
            ColorCache[uiColor] = color = GetColor(uiColor);
        }

        writer.Write(color);
    }

    private static string GetColor(Color color)
    {
        StringBuilder builder = UiFrameworkPool.GetStringBuilder();
        builder.Append(StringCache<float>.ToString(color.r, Format));
        builder.Append(Space);
        builder.Append(StringCache<float>.ToString(color.g, Format));
        builder.Append(Space);
        builder.Append(StringCache<float>.ToString(color.b, Format));
        if (color.a < 1f)
        {
            builder.Append(Space);
            builder.Append(StringCache<float>.ToString(color.a, Format));
        }

        return builder.ToStringAndFree();
    }
}