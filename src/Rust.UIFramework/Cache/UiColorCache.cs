using System.Collections.Concurrent;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Cache;

internal static class UiColorCache
{
    private const string Format = "0.####";
    private const char Space = ' ';

    private static readonly ConcurrentDictionary<UiColor, Utf8String> ColorCache = new();
        
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
        StringBuilder builder = UiPool.Internal.GetStringBuilder();
        builder.Append(FormatCache<float>.ToString(color.r, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(color.g, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(color.b, Format));
        if (color.a < 1f)
        {
            builder.Append(Space);
            builder.Append(FormatCache<float>.ToString(color.a, Format));
        }

        return UiPool.Internal.ToStringAndFree(builder);
    }
}