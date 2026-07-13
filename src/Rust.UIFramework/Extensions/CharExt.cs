using System;

namespace Oxide.Ext.UiFramework.Extensions;

public static class CharExt
{
    extension(char)
    {
        public static bool TryParse(ReadOnlySpan<char> span, out char value)
        {
            if(span.Length == 1)
            {
                value = span[0];
                return true;
            }
            value = default;
            return false;
        }
    }
}