using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewContentComponent : RectTransformComponent
{
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.ScrollView.Min);
        writer.AddField(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.ScrollView.AnchorMax);
        writer.AddField(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.ScrollView.Min);
        writer.AddField(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.ScrollView.OffsetMax);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Position = UiPosition.Full;
        Offset = default;
    }
}