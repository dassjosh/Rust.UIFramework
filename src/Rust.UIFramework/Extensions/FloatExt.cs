using System.Runtime.CompilerServices;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

public static class FloatExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float a, float b, float t) => a + (b - a) * Mathf.Clamp01(t);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;
}