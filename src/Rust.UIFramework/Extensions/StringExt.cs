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
}