using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

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

    public override void ResetHasChanged()
    {
        _position.ResetHasChanged();
        _offset.ResetHasChanged();
        _pivot.ResetHasChanged();
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
}