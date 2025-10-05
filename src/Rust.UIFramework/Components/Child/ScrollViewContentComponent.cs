using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ScrollViewContentComponentSerializer))]
public class ScrollViewContentComponent : ChildComponent
{
    private readonly TrackedValue<UiPosition> _position = new(UiPosition.Full);
    private readonly TrackedValue<UiOffset> _offset = new();
    private readonly TrackedValue<Vector2> _pivot = new(JsonDefaults.ScrollView.Pivot);
    
    public UiPosition Position { get => _position.Value; set => _position.Value = value; }
    public UiOffset Offset { get => _offset.Value; set => _offset.Value = value; }
    public Vector2 Pivot { get => _pivot.Value; set => _pivot.Value = value; }
    
    public override ComponentType ComponentType => ComponentType.ScrollView;
    
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

    public override void Reset()
    {
        _position.Reset();
        _offset.Reset();
        _pivot.Reset();
    }

    public override void CopyFrom(object value)
    {
        if (value is ScrollViewContentComponent component)
        {
            Position = component.Position;
            Offset = component.Offset;
            Pivot = component.Pivot;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ScrollViewContentComponent typedOther = (ScrollViewContentComponent)other!;
        return Position == typedOther.Position 
               && Offset == typedOther.Offset 
               && Pivot == typedOther.Pivot;
    }
}