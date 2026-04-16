using System;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class StringExt
{
    extension(string input)
    {
        internal bool TryParseBool(out bool value)
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

        internal bool TryParseInt(out int value) => int.TryParse(input, out value);

        internal bool IsValidUrl()
        {
            return !string.IsNullOrEmpty(input) && input.StartsWith("http", StringComparison.OrdinalIgnoreCase);
        }
    }
}