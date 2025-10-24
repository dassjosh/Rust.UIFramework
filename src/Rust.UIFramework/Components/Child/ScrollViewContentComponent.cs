using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IScrollViewContentComponent))]
[GenerateBuilderMethods]
public partial class ScrollViewContentComponent : ChildComponent, IScrollViewContentComponent
{
    public override ComponentType ComponentType => ComponentType.ScrollViewContent;
    
    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddField(_position, mode);
        writer.AddField(_offset, mode);
        writer.AddField(JsonDefaults.ScrollView.PivotName, _pivot, mode);
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