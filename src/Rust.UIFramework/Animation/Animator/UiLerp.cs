using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public delegate T UiLerp<T>(T start, T end, float progress);

public static class UiLerp
{
    private static readonly Dictionary<Type, object> Cache = new()
    {
        [typeof(UiPosition)] = (UiLerp<UiPosition>)UiPosition.LerpUnclamped,
        [typeof(UiOffset)] = (UiLerp<UiOffset>)UiOffset.LerpUnclamped,
        [typeof(UiColor)] = (UiLerp<UiColor>)UiColor.Lerp,
        [typeof(UiRotation)] = (UiLerp<UiRotation>)UiRotation.LerpUnclamped,
        [typeof(UiScale)] = (UiLerp<UiScale>)UiScale.Lerp,
        [typeof(UiPadding)] = (UiLerp<UiPadding>)UiPadding.Lerp,
        [typeof(string)] = (UiLerp<string>)LevenshteinDistanceExt.Lerp,
        [typeof(sbyte)] = (UiLerp<sbyte>)((start, end, t) => (sbyte)(start + (end - start) * t)),
        [typeof(byte)] = (UiLerp<byte>)((start, end, t) => (byte)(start + (end - start) * t)),
        [typeof(short)] = (UiLerp<short>)((start, end, t) => (short)(start + (end - start) * t)),
        [typeof(ushort)] = (UiLerp<ushort>)((start, end, t) => (ushort)(start + (end - start) * t)),
        [typeof(int)] = (UiLerp<int>)((start, end, t) => (int)(start + (end - start) * t)),
        [typeof(uint)] = (UiLerp<uint>)((start, end, t) => (uint)(start + (end - start) * t)),
        [typeof(long)] = (UiLerp<long>)((start, end, t) => (long)(start + (end - start) * t)),
        [typeof(ulong)] = (UiLerp<ulong>)((start, end, t) => (ulong)(start + (end - start) * t)),
        [typeof(float)] = (UiLerp<float>)((start, end, t) => start + (end - start) * t),
        [typeof(double)] = (UiLerp<double>)((start, end, t) => start + (end - start) * t),
        [typeof(decimal)] = (UiLerp<decimal>)((start, end, t) => start + (end - start) * (decimal)t)
    };
        
    public static UiLerp<T> GetDefault<T>() => Cache.TryGetValue(typeof(T), out object cached) ? (UiLerp<T>)cached : null;
    public static UiLerp<T> GetDefaultOrError<T>() => Cache.TryGetValue(typeof(T), out object cached) ? (UiLerp<T>)cached : throw new ArgumentNullException(typeof(T).Name, $"No default lerp function found for type {typeof(T)}. Please pass a lerp function manually.");
}