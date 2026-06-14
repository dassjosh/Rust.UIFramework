using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

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
        [typeof(UiTranslate)] = (UiLerp<UiTranslate>)UiTranslate.Lerp,
        [typeof(UiUnit)] = (UiLerp<UiUnit>)UiUnit.Lerp,
        [typeof(UiOpacity)] = (UiLerp<UiOpacity>)UiOpacity.Lerp,
        [typeof(Vector2)] = (UiLerp<Vector2>)Vector2.LerpUnclamped,
        [typeof(Vector3)] = (UiLerp<Vector3>)Vector3.LerpUnclamped,
        [typeof(Vector4)] = (UiLerp<Vector4>)Vector4.LerpUnclamped,
        [typeof(Quaternion)] = (UiLerp<Quaternion>)Quaternion.LerpUnclamped,
        [typeof(string)] = (UiLerp<string>)LevenshteinDistance.Lerp,
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
        [typeof(decimal)] = (UiLerp<decimal>)((start, end, t) => start + (end - start) * (decimal)t),
        [typeof(TimeSpan)] = (UiLerp<TimeSpan>)((start, end, t) => start + (end - start) * t),
        [typeof(DateTime)] = (UiLerp<DateTime>)((start, end, t) => start + (end - start) * t),
        [typeof(DateTimeOffset)] = (UiLerp<DateTimeOffset>)((start, end, t) => start + (end - start) * t),
    };
        
    public static UiLerp<T> GetDefault<T>() => Cache.TryGetValue(typeof(T), out object cached) ? (UiLerp<T>)cached : null;
    public static UiLerp<T> GetDefaultOrError<T>() => Cache.TryGetValue(typeof(T), out object cached) ? (UiLerp<T>)cached : throw new ArgumentNullException(typeof(T).Name, $"No default lerp function found for type {typeof(T)}. Please pass a lerp function manually.");
    public static bool TryGetLerp<T>(out UiLerp<T> lerp)
    {
        if(Cache.TryGetValue(typeof(T), out object cachedLerp) && cachedLerp is UiLerp<T> lerpFunc)
        {
            lerp = lerpFunc;
            return true;
        }
        
        lerp = null;
        return false;
    }
}