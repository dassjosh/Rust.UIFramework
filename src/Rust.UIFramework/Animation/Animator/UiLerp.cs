using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public delegate T UiLerp<T>(T start, T end, float progress);

public static class UiLerp
{
    public static UiLerp<T> GetDefault<T>()
    {
        Type type = typeof(T);
        if (type == typeof(UiPosition)) return (UiLerp<T>)(object)(UiLerp<UiPosition>)UiPosition.LerpUnclamped;
        if (type == typeof(UiOffset)) return (UiLerp<T>)(object)(UiLerp<UiOffset>)UiOffset.LerpUnclamped;
        if (type == typeof(UiColor)) return (UiLerp<T>)(object)(UiLerp<UiColor>)UiColor.Lerp;
        if (type == typeof(UiRotation)) return (UiLerp<T>)(object)(UiLerp<UiRotation>)UiRotation.LerpUnclamped;
        if (type == typeof(UiScale)) return (UiLerp<T>)(object)(UiLerp<UiScale>)UiScale.Lerp;
        if (type == typeof(UiPadding)) return (UiLerp<T>)(object)(UiLerp<UiPadding>)UiPadding.Lerp;
        if (type == typeof(string)) return (UiLerp<T>)(object)(UiLerp<string>)LevenshteinDistanceExt.Lerp;
        switch (type.GetTypeCode())
        {
            case TypeCode.SByte:
                return (UiLerp<T>)(object)(UiLerp<sbyte>) (static (start, end, t) => (sbyte)(start + (end - start) * t));
            case TypeCode.Byte:
                return (UiLerp<T>)(object)(UiLerp<byte>) (static (start, end, t) => (byte)(start + (end - start) * t));
            case TypeCode.Int16:
                return (UiLerp<T>)(object)(UiLerp<short>) (static (start, end, t) => (short)(start + (end - start) * t));
            case TypeCode.UInt16:
                return (UiLerp<T>)(object)(UiLerp<ushort>) (static (start, end, t) => (ushort)(start + (end - start) * t));
            case TypeCode.Int32:
                return (UiLerp<T>)(object)(UiLerp<int>) (static (start, end, t) => (int)(start + (end - start) * t));
            case TypeCode.UInt32:
                return (UiLerp<T>)(object)(UiLerp<uint>) (static (start, end, t) => (uint)(start + (end - start) * t));
            case TypeCode.Int64:
                return (UiLerp<T>)(object)(UiLerp<long>) (static (start, end, t) => (long)(start + (end - start) * t));
            case TypeCode.UInt64:
                return (UiLerp<T>)(object)(UiLerp<ulong>) (static (start, end, t) => (ulong)(start + (end - start) * t));
            case TypeCode.Single:
                return (UiLerp<T>)(object)(UiLerp<float>) (static (start, end, t) => start + (end - start) * t);
            case TypeCode.Double:
                return (UiLerp<T>)(object)(UiLerp<double>) (static (start, end, t) => start + (end - start) * t);
            case TypeCode.Decimal:
                return (UiLerp<T>)(object)(UiLerp<decimal>) (static (start, end, t) => start + (end - start) * (decimal)t);
        }
        return null;
    }
}