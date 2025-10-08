using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class OutlineComponent : SubComponent
{
    private readonly TrackedValue<UiColor> _color = new();
    private readonly TrackedValue<Vector2> _distance = new(JsonDefaults.Outline.Distance, JsonDefaults.Outline.FpDistance);
    private readonly TrackedValue<bool> _useGraphicAlpha = new();
    
    public UiColor Color { get => _color.Value; set => _color.Value = value; }
    public Vector2 Distance { get => _distance.Value; set => _distance.Value = value; }
    public bool UseGraphicAlpha { get => _useGraphicAlpha.Value; set => _useGraphicAlpha.Value = value; }

    public override Utf8String Type => JsonDefaults.Outline.Type;
    public override ComponentType ComponentType => ComponentType.Outline;
    public override bool AllowMultiple => true;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Outline.DistanceName, _distance, mode);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, _useGraphicAlpha.ShouldSerialize(mode) && _useGraphicAlpha.Value);
    }
    
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

    public override bool HasChanged()
    {
        return _color.HasChanged || _distance.HasChanged || _useGraphicAlpha.HasChanged;
    }
    
    public override void ResetHasChanged()
    {
        _color.ResetHasChanged();
        _distance.ResetHasChanged();
        _useGraphicAlpha.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _color.Reset();
        _distance.Reset();
        _useGraphicAlpha.Reset();
    }
}