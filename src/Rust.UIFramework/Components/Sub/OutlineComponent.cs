using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(OutlineComponentSerializer))]
public class OutlineComponent : SubComponent
{
    public UiColor Color;
    public Vector2 Distance;
    public bool UseGraphicAlpha;

    public override Utf8String Type => JsonDefaults.Outline.Type;
    public override ComponentType ComponentType => ComponentType.Outline;
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

    public override void Reset()
    {
        base.Reset();
        Distance = JsonDefaults.Outline.Distance;
        UseGraphicAlpha = false;
        Color = default;
    }
    
    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is OutlineComponent component)
        {
            Color = component.Color;
            Distance = component.Distance;
            UseGraphicAlpha = component.UseGraphicAlpha;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        OutlineComponent typedOther = (OutlineComponent)other!;
        return Color == typedOther.Color 
               && Distance == typedOther.Distance 
               && UseGraphicAlpha == typedOther.UseGraphicAlpha;
    }
}