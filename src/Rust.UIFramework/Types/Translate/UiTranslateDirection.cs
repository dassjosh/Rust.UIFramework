using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public readonly record struct UiTranslateDirection(float Value, UiTranslateType Type)
{
    public static readonly UiTranslateDirection DistanceDefault = UiTranslateDirection.Distance(0);
    public static readonly UiTranslateDirection PercentageDefault = UiTranslateDirection.Percentage(0);

    public bool HasValue => !Mathf.Approximately(Value, 0);
    
    public static UiTranslateDirection Lerp(in UiTranslateDirection a, in UiTranslateDirection b, float t) => a with
    {
        Value = Mathf.LerpUnclamped(a.Value, b.Value, t)
    };
    
#pragma warning disable EPS05
    public static UiTranslateDirection Lerp(UiTranslateDirection a, UiTranslateDirection b, float t) => Lerp(in a, in b, t);
#pragma warning restore EPS05

    public static UiTranslateDirection operator -(UiTranslateDirection direction) => direction with { Value = -direction.Value };
    public static UiTranslateDirection operator -(UiTranslateDirection direction, float a) => direction with { Value = direction.Value - a };
    public static UiTranslateDirection operator -(UiTranslateDirection direction, int a) => direction with { Value = direction.Value - a };
    public static UiTranslateDirection operator +(UiTranslateDirection direction, float a) => direction with { Value = direction.Value + a };
    public static UiTranslateDirection operator +(UiTranslateDirection direction, int a) => direction with { Value = direction.Value + a };

    public override string ToString() => $"{Value}{(Type == UiTranslateType.Distance ? "PX": "%")}";
}