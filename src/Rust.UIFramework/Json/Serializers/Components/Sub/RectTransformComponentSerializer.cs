using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;

namespace Oxide.Ext.UiFramework.Json;

public class RectTransformComponentSerializer : SubComponentSerializer<RectTransformComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, RectTransformComponent component, RectTransformComponent defaults, SerializeMode mode)
    {
        UiOffset computedOffset = component.Offset + component.Padding;
        UiOffset computedDefault = defaults.Offset + defaults.Padding;
        
        writer.AddField(JsonDefaults.RectTransform.AnchorMinName, component.Position.Min, defaults.Position.Min);
        writer.AddField(JsonDefaults.RectTransform.AnchorMaxName, component.Position.Max, defaults.Position.Max);
        writer.AddField(JsonDefaults.RectTransform.OffsetMinName, computedOffset.Min, computedDefault.Min);
        if (mode == SerializeMode.Create)
        {
            writer.AddField(JsonDefaults.RectTransform.OffsetMaxName, computedOffset.Max, JsonDefaults.RectTransform.OffsetMax);
        }
        else
        {
            writer.AddField(JsonDefaults.RectTransform.OffsetMaxName, computedOffset.Max, computedDefault.Max);
        }
        
        writer.AddField(JsonDefaults.RectTransform.RotationName, component.Rotation.Rotation, defaults.Rotation.Rotation);

        if (mode == SerializeMode.Update)
        {
            if (!string.IsNullOrEmpty(component.ChangeParent))
            {
                writer.AddField(JsonDefaults.RectTransform.SetParentName, component.ChangeParent, defaults.ChangeParent);
            }
            writer.AddField(JsonDefaults.RectTransform.SetTransformIndexName, component.TransformIndex, defaults.TransformIndex);
        }
    }
}