using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Oxide.Ext.UiFramework.Cache;

public static class EnumCache<T> where T : Enum
{
    private static readonly Dictionary<T, string> CachedStrings = new();
    private static readonly Dictionary<T, string> LowerStrings = new();
    private static readonly Dictionary<T, string> NumberStrings = new();
    private static readonly ReadOnlyCollection<T> EnumValues = new(Enum.GetValues(typeof(T)).Cast<T>().ToArray());

    static EnumCache()
    {
        foreach (T value in EnumValues)
        {
            string enumString = value.ToString();
            CachedStrings[value] = enumString;
            LowerStrings[value] = enumString.ToLower();
            NumberStrings[value] = value.ToString("D");
        }
    }
        
    public static string ToString(T value) => CachedStrings[value];

    public static string ToLower(T value) => LowerStrings[value];
    public static string ToNumber(T value) => NumberStrings[value];
    public static IReadOnlyCollection<T> GetValues() => EnumValues;
}