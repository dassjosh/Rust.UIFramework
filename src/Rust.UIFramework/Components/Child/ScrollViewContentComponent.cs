using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ScrollViewContentComponent : ChildComponent
{
    [TrackedDefaults(typeof(UiPosition), nameof(UiPosition.Full))]
    public partial UiPosition Position { get; set; }
    public partial UiOffset Offset { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Pivot))]
    public partial UiPivot Pivot { get; set; }
    
    public override ComponentType ComponentType => ComponentType.ScrollViewContent;
    
    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(Position, mode);
        writer.AddField(Offset, mode);
        writer.AddField(JsonDefaults.ScrollView.PivotName, PivotTracked, mode);
        writer.WriteEndObject();
    }
    
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
}