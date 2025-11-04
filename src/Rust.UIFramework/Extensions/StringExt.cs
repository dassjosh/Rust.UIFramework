using System;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class StringExt
{
    internal static bool TryParseBool(this string input, out bool value)
    {
        if (bool.TryParse(input, out value))
        {
            return true;
        }

        if (char.IsNumber(input[0]))
        {
            value = input[0] != '0';
            return true;
        }

        return false;
    }

    internal static bool TryParseInt(this string input, out int value) => int.TryParse(input, out value);

    internal static bool IsValidUrl(this string url)
    {
        return !string.IsNullOrEmpty(url) && url.StartsWith("http", StringComparison.OrdinalIgnoreCase);
    }
}