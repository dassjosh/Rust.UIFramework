using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class OutlineComponent : SubComponent
{
    public UiColor Color;
    public Vector2 Distance;
    public bool UseGraphicAlpha;

    public override Utf8String Type => JsonDefaults.Outline.Type;
    public override bool AllowMultiple => true;

    public OutlineComponent SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
    
    public OutlineComponent SetDistance(Vector2 distance)
    {
        Distance = distance;
        return this;
    }
    
    public OutlineComponent SetUseGraphicAlpha(bool useGraphicAlpha)
    {
        UseGraphicAlpha = useGraphicAlpha;
        return this;
    }

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.FpDistance);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, UseGraphicAlpha);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
    }

    public override void Reset()
    {
        base.Reset();
        Distance = JsonDefaults.Outline.Distance;
        UseGraphicAlpha = false;
        Color = default;
    }
}