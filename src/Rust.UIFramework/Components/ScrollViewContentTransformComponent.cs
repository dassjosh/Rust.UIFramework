using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewContentTransformComponent : RectTransformComponent
{
    public ScrollViewContentTransformComponent()
    {
        Position = new UiPosition(0, 0, 1, 1);
        Offset = new UiOffset(0, 0, 0, 0);
    }
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.ScrollView.Min);
        writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.ScrollView.AnchorMax);
        writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.ScrollView.Min);
        writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.ScrollView.OffsetMax);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Position = new UiPosition(0, 0, 1, 1);
        Offset = new UiOffset(0, 0, 0, 0);
    }
}