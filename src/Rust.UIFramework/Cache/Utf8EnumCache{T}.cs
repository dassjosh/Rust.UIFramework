using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8EnumCache<T>
{
    private static readonly Dictionary<T, Utf8String> Utf8Strings = new();

    static Utf8EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            Utf8Strings[value] = value.ToString();
        }
    }
    
    public static Utf8String ToUtf8String(T value) => Utf8Strings[value];
}