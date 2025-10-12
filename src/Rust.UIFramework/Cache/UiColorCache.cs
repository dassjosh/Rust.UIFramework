using System.Collections.Concurrent;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

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

    private static string GetColor(UiColor color)
    {
        StringBuilder builder = UiPool.Internal.GetStringBuilder();
        builder.Append(FormatCache<float>.ToString(color.RedFloat, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(color.GreenFloat, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(color.BlueFloat, Format));
        if (color.AlphaFloat < 1f)
        {
            builder.Append(Space);
            builder.Append(FormatCache<float>.ToString(color.AlphaFloat, Format));
        }

        return UiPool.Internal.ToStringAndFree(builder);
    }
}