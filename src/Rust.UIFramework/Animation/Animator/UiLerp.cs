using System;
using Oxide.Ext.UiFramework.Colors;
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
        if (type == typeof(UiPosition))
            return (UiLerp<T>)(object)(UiLerp<UiPosition>)UiPosition.LerpUnclamped;
        if (type == typeof(UiOffset))
            return (UiLerp<T>)(object)(UiLerp<UiOffset>)UiOffset.LerpUnclamped;
        if (type == typeof(UiColor))
            return (UiLerp<T>)(object)(UiLerp<UiColor>)UiColor.Lerp;
        if (type == typeof(UiRotation))
            return (UiLerp<T>)(object)(UiLerp<UiRotation>)UiRotation.LerpUnclamped;
        if (type == typeof(string))
            return (UiLerp<T>)(object)(UiLerp<string>)LevenshteinDistanceExt.Lerp;

        return null;
    }
}