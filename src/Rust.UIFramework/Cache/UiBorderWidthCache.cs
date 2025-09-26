using System.Collections.Concurrent;
using System.Text;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class UiBorderWidthCache
{
    private const string Format = "0.####";
    private const char Space = ' ';

    private static readonly ConcurrentDictionary<UiBorderWidth, Utf8String> BorderCache = new();
        
    public static void WriteBorderWidth(JsonUtf8Writer writer, in UiBorderWidth uiBorder)
    {
        if (!BorderCache.TryGetValue(uiBorder, out Utf8String border))
        {
            BorderCache[uiBorder] = border = GetBorder(uiBorder);
        }

        writer.Write(border);
    }

    private static string GetBorder(in UiBorderWidth border)
    {
        StringBuilder builder = UiPool.Internal.GetStringBuilder();
        builder.Append(FormatCache<float>.ToString(border.Left, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(border.Top, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(border.Right, Format));
        builder.Append(Space);
        builder.Append(FormatCache<float>.ToString(border.Bottom, Format));
        return UiPool.Internal.ToStringAndFree(builder);
    }
}