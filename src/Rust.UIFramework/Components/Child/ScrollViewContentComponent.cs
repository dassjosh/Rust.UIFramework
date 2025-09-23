using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewContentComponent : ChildComponent
{
    public UiPosition Position;
    public UiOffset Offset;
    public Vector2 Pivot;
    
    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null, in Vector2? pivot = null)
    {
        if (position.HasValue)
        {
            Position = position.Value;
        }

        if (offset.HasValue)
        {
            Offset = offset.Value;
        }
        
        if (pivot.HasValue)
        {
            Pivot = pivot.Value;
        }
    }
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddField(JsonDefaults.RectTransform.AnchorMinName, Position.Min, JsonDefaults.RectTransform.AnchorMin);
        writer.AddField(JsonDefaults.RectTransform.AnchorMaxName, Position.Max, JsonDefaults.RectTransform.AnchorMax);
        writer.AddField(JsonDefaults.RectTransform.OffsetMinName, Offset.Min, JsonDefaults.RectTransform.OffsetMin);
        writer.AddField(JsonDefaults.RectTransform.OffsetMaxName, Offset.Max, JsonDefaults.ScrollView.OffsetMax);
        writer.AddField(JsonDefaults.ScrollView.PivotName, Pivot, JsonDefaults.ScrollView.Pivot);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Position = UiPosition.Full;
        Offset = default;
        Pivot = JsonDefaults.ScrollView.Pivot;
    }
}