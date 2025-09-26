using System.Collections.Concurrent;
using System.Text;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class UiPaddingCache
{
    private const string Format = "0.####";
    private const char Space = ' ';

    private static readonly ConcurrentDictionary<UiPadding, Utf8String> PaddingCache = new();
        
    public static void WritePadding(JsonUtf8Writer writer, in UiPadding uiPadding)
    {
        if (!PaddingCache.TryGetValue(uiPadding, out Utf8String padding))
        {
            PaddingCache[uiPadding] = padding = GetPadding(uiPadding);
        }

        writer.Write(padding);
    }

    private static string GetPadding(in UiPadding padding)
    {
        if (padding.IsSingleValue)
        {
            return FormatCache<float>.ToString(padding.Left, Format);
        }
        
        StringBuilder builder = UiPool.Internal.GetStringBuilder();
        builder.Append(FormatCache<float>.ToString(padding.Left, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(padding.Top, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(padding.Right, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(padding.Bottom, Format));
        return UiPool.Internal.ToStringAndFree(builder);
    }
}