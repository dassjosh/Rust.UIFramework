using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8EnumCache<T>
{
    private static readonly Dictionary<T, byte[]> Utf8Strings = new();

    static Utf8EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            Utf8Strings[value] = Encoding.UTF8.GetBytes(value.ToString());
        }
    }
    
    public static byte[] ToUtf8String(T value) => Utf8Strings[value];
}