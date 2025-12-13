using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Extensions;

// ReSharper disable StaticMemberInGenericType
#pragma warning disable S2743 // Static is not shared across generics
#pragma warning disable S3963 // Static constructor

namespace Oxide.Ext.UiFramework.Cache;

public static class EnumCache<T> where T : Enum
{
    private static readonly IEnumCache Cache;
    private static readonly ReadOnlyCollection<T> EnumValues = new(Enum.GetValues(typeof(T)).Cast<T>().ToArray());
    private static readonly TypeCode TypeCode = typeof(T).GetTypeCode();

    static EnumCache()
    {
        if (!typeof(T).HasFlags && IsValidFastEnumRange(out FastCacheParams @params))
        {
            Cache = new EnumCacheFastImpl(@params);
        }
        else
        {
            Cache = new EnumCacheImpl();
        }
    }

    public static string ToString(T value) => Cache.ToString(value);

    public static string ToLower(T value) => Cache.ToLower(value);
    public static string ToNumber(T value) => Cache.ToNumber(value);
    public static IReadOnlyCollection<T> GetValues() => EnumValues;

    private static bool IsValidFastEnumRange(out FastCacheParams @params)
    {
        @params = default;
        switch (typeof(T).GetTypeCode())
        {
            case TypeCode.SByte:
            case TypeCode.Byte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
                break;
            default:
                return false;
        }
 
        int[] values = Enum.GetValues(typeof(T)).Cast<T>().Select(GetValue).ToArray();
        int min = values.Min();
        int max = values.Max();
        
        @params = new FastCacheParams(min, max - min + 1);
        return max - min <= 128;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetValue(T value)
    {
        return TypeCode switch
        {
            TypeCode.SByte => Unsafe.As<T, sbyte>(ref value),
            TypeCode.Byte => Unsafe.As<T, byte>(ref value),
            TypeCode.Int16 => Unsafe.As<T, short>(ref value),
            TypeCode.UInt16 => Unsafe.As<T, ushort>(ref value),
            TypeCode.Int32 => Unsafe.As<T, int>(ref value),
            TypeCode.UInt32 => (int)Unsafe.As<T, uint>(ref value),
            _ => 0
        };
    }
    
    private interface IEnumCache
    {
        string ToString(T value);
        string ToLower(T value);
        string ToNumber(T value);
        IReadOnlyCollection<T> GetValues();
    }
    
    private sealed class EnumCacheImpl : IEnumCache
    {
        private readonly Dictionary<T, string> CachedStrings = new();
        private readonly Dictionary<T, string> LowerStrings = new();
        private readonly Dictionary<T, string> NumberStrings = new();
        
        public EnumCacheImpl()
        {
            foreach (T value in EnumValues)
            {
                string enumString = value.ToString();
                CachedStrings[value] = enumString;
                LowerStrings[value] = enumString.ToLower();
                NumberStrings[value] = value.ToString("D");
            }
        }

        public string ToString(T value) => CachedStrings[value];

        public string ToLower(T value) => LowerStrings[value];
        public string ToNumber(T value) => NumberStrings[value];
        public IReadOnlyCollection<T> GetValues() => EnumValues;
    }
    
    private sealed class EnumCacheFastImpl : IEnumCache
    {
        private readonly int _offset;
        private readonly string[] CachedStrings;
        private readonly string[] LowerStrings;
        private readonly string[] NumberStrings;
        
        public EnumCacheFastImpl(FastCacheParams @params)
        {
            _offset = @params.Offset;
            CachedStrings = new string[@params.Size];
            LowerStrings = new string[@params.Size];
            NumberStrings = new string[@params.Size];
            
            foreach (T value in EnumValues)
            {
                string enumString = value.ToString();
                int index = GetIndex(value);
                CachedStrings[index] = enumString;
                LowerStrings[index] = enumString.ToLower();
                NumberStrings[index] = value.ToString("D");
            }
        }

        public string ToString(T value) => CachedStrings[GetIndex(value)];

        public string ToLower(T value) => LowerStrings[GetIndex(value)];
        public string ToNumber(T value) => NumberStrings[GetIndex(value)];
        public IReadOnlyCollection<T> GetValues() => EnumValues;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetIndex(T value) => GetValue(value) - _offset;
    }

    private readonly record struct FastCacheParams(int Offset, int Size);
}