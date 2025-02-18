using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewContentTransformComponent : RectTransformComponent
{
    public ScrollViewContentTransformComponent()
    {
        Position = UiPosition.Full;
        Offset = default;
    }
    
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
        Position = new UiPosition(0, 0, 1, 1);
        Offset = default;
    }
}