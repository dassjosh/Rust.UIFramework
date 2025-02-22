using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class OutlineComponent : SubComponent
{
    public UiColor Color;
    public Vector2 Distance = JsonDefaults.Outline.Distance;
    public bool UseGraphicAlpha;

    public override bool AllowMultiple => true;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Outline.Type);
        writer.AddField(JsonDefaults.Outline.DistanceName, Distance, JsonDefaults.Outline.FpDistance);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, UseGraphicAlpha);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Distance = JsonDefaults.Outline.Distance;
        UseGraphicAlpha = false;
        Color = default;
    }
}