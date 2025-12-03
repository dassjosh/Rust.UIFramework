using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class DraggableComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.LimitToParent))]
    public partial bool LimitToParent { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.MaxDistance))]
    public partial float MaxDistance { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.AllowSwapping))]
    public partial bool AllowSwapping { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.DropAnywhere))]
    public partial bool DropAnywhere { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.DragAlpha))]
    public partial float DragAlpha { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.ParentLimitIndex))]
    public partial int ParentLimitIndex { get; set; }
    
    public partial string Filter { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.ParentPadding))]
    public partial Vector2 ParentPadding { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.AnchorOffset))]
    public partial Vector2 AnchorOffset { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.KeepOnTop))]
    public partial bool KeepOnTop { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.PositionRpc))]
    public partial CommunityEntity.DraggablePositionSendType? PositionRpc { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.MoveToAnchor))]
    public partial bool MoveToAnchor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.RebuildAnchor))]
    public partial bool RebuildAnchor { get; set; }
    
    public override Utf8String Type => JsonDefaults.Draggable.Type;
    public override ComponentType ComponentType => ComponentType.Draggable;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Draggable.LimitToParentName, _limitToParent, mode);
        writer.AddField(JsonDefaults.Draggable.MaxDistanceName, _maxDistance, mode);
        writer.AddField(JsonDefaults.Draggable.AllowSwappingName, _allowSwapping, mode);
        writer.AddField(JsonDefaults.Draggable.DropAnywhereName, _dropAnywhere, mode);
        writer.AddField(JsonDefaults.Draggable.DragAlphaName, _dragAlpha, mode);
        writer.AddField(JsonDefaults.Draggable.ParentLimitIndexName, _parentLimitIndex, mode);
        writer.AddField(JsonDefaults.Draggable.FilterName, _filter, mode);
        writer.AddField(JsonDefaults.Draggable.ParentPaddingName, _parentPadding, mode);
        writer.AddField(JsonDefaults.Draggable.AnchorOffsetName, _anchorOffset, mode);
        writer.AddField(JsonDefaults.Draggable.KeepOnTopName, _keepOnTop, mode);
        writer.AddField(JsonDefaults.Draggable.PositionRpcName, _positionRpc, mode);
        writer.AddKeyField(JsonDefaults.Draggable.MoveToAnchorName, _moveToAnchor.ShouldSerialize(mode) && _moveToAnchor.Value);
        writer.AddKeyField(JsonDefaults.Draggable.RebuildAnchorName, _rebuildAnchor.ShouldSerialize(mode) && _rebuildAnchor.Value);
    }
}