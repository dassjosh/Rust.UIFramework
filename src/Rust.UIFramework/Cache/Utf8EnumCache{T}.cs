using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Cache;

internal static class Utf8EnumCache<T> where T : Enum
{
    private static readonly Dictionary<T, Utf8String> Utf8Strings = new();

    static Utf8EnumCache()
    {
        foreach (T value in Enum.GetValues(typeof(T)).Cast<T>())
        {
            Utf8Strings[value] = value.ToString("D");
        }
    }
    
    public static Utf8String ToUtf8Number(T value) => Utf8Strings[value];
}