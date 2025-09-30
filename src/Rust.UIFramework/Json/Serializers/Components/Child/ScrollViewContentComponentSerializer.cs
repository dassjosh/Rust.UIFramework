using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ScrollViewContentComponentSerializer : BaseSerializer<ScrollViewContentComponent>
{
    public override void Serialize(JsonFrameworkWriter writer, ScrollViewContentComponent component, ScrollViewContentComponent defaults, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.RectTransform.AnchorMinName, component.Position.Min, defaults.Position.Min);
        writer.AddField(JsonDefaults.RectTransform.AnchorMaxName, component.Position.Max, defaults.Position.Max);
        writer.AddField(JsonDefaults.RectTransform.OffsetMinName, component.Offset.Min, defaults.Offset.Min);
        writer.AddField(JsonDefaults.RectTransform.OffsetMaxName, component.Offset.Max, defaults.Offset.Max);
        writer.AddField(JsonDefaults.ScrollView.PivotName, component.Pivot, defaults.Pivot);
        writer.WriteEndObject();
    }
}