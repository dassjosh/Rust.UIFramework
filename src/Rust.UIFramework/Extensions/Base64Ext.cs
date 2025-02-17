using System;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class Base64Ext
{
    private static readonly byte[] KeyBytes = new byte[8];
    private static readonly char[] Chars = new char[20];
    
    internal static ReadOnlySpan<char> ToBase64Span(this int value)
    {
        BitConverter.TryWriteBytes(KeyBytes, value);
        Convert.TryToBase64Chars(KeyBytes, Chars, out int written);
        return Chars[..written];
    }
    
    internal static ReadOnlySpan<char> ToBase64Span(this long value)
    {
        BitConverter.TryWriteBytes(KeyBytes, value);
        Convert.TryToBase64Chars(KeyBytes, Chars, out int written);
        return Chars[..written];
    }

    internal static int ToIntFromBase64Span(this ReadOnlySpan<char> value)
    {
        Convert.TryFromBase64Chars(value, KeyBytes, out int written);
        return BitConverter.ToInt32(KeyBytes[..written], 0);
    }
    
    internal static long ToLongFromBase64Span(this ReadOnlySpan<char> value)
    {
        Convert.TryFromBase64Chars(value, KeyBytes, out int written);
        return BitConverter.ToInt64(KeyBytes[..written], 0);
    }
}