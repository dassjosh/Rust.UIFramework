using System;
using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Helpers;

public static class GenericMath
{
    public static T Add<T>(T left, T right) where T : struct
    {
        Type type = typeof(T);
        if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) + GenericsUtil.Cast<T, int>(right));
        if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) + GenericsUtil.Cast<T, float>(right));
        if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) + GenericsUtil.Cast<T, double>(right));
        if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) + GenericsUtil.Cast<T, long>(right));
        if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) + GenericsUtil.Cast<T, uint>(right));
        if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) + GenericsUtil.Cast<T, ulong>(right));

        throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
    }
        
    public static T Subtract<T>(T left, T right) where T : struct
    {
        Type type = typeof(T);
        if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) - GenericsUtil.Cast<T, int>(right));
        if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) - GenericsUtil.Cast<T, float>(right));
        if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) - GenericsUtil.Cast<T, double>(right));
        if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) - GenericsUtil.Cast<T, long>(right));
        if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) - GenericsUtil.Cast<T, uint>(right));
        if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) - GenericsUtil.Cast<T, ulong>(right));

        throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
    }
        
    public static T Multiply<T>(T left, T right) where T : struct
    {
        Type type = typeof(T);
        if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) * GenericsUtil.Cast<T, int>(right));
        if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) * GenericsUtil.Cast<T, float>(right));
        if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) * GenericsUtil.Cast<T, double>(right));
        if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) * GenericsUtil.Cast<T, long>(right));
        if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) * GenericsUtil.Cast<T, uint>(right));
        if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) * GenericsUtil.Cast<T, ulong>(right));

        throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
    }
        
    public static T Divide<T>(T left, T right) where T : struct
    {
        Type type = typeof(T);
        if (type == typeof(int)) return GenericsUtil.Cast<int, T>( GenericsUtil.Cast<T, int>(left) / GenericsUtil.Cast<T, int>(right));
        if (type == typeof(float)) return GenericsUtil.Cast<float, T>( GenericsUtil.Cast<T, float>(left) / GenericsUtil.Cast<T, float>(right));
        if (type == typeof(double)) return GenericsUtil.Cast<double, T>( GenericsUtil.Cast<T, double>(left) / GenericsUtil.Cast<T, double>(right));
        if (type == typeof(long)) return GenericsUtil.Cast<long, T>( GenericsUtil.Cast<T, long>(left) / GenericsUtil.Cast<T, long>(right));
        if (type == typeof(uint)) return GenericsUtil.Cast<uint, T>( GenericsUtil.Cast<T, uint>(left) / GenericsUtil.Cast<T, uint>(right));
        if (type == typeof(ulong)) return GenericsUtil.Cast<ulong, T>( GenericsUtil.Cast<T, ulong>(left) / GenericsUtil.Cast<T, ulong>(right));

        throw new UiFrameworkException($"{typeof(T).Name} is not a supported numeric type");
    }
}