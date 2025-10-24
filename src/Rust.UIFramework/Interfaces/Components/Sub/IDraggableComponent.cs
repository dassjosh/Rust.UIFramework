using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IDraggableComponent : ISubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.LimitToParent))]
    bool LimitToParent { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.MaxDistance))]
    float MaxDistance { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.AllowSwapping))]
    bool AllowSwapping { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.DropAnywhere))]
    bool DropAnywhere { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.DragAlpha))]
    float DragAlpha { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.ParentLimitIndex))]
    int ParentLimitIndex { get; set; }
    
    string Filter { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.ParentPadding))]
    Vector2 ParentPadding { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.AnchorOffset))]
    Vector2 AnchorOffset { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.KeepOnTop))]
    bool KeepOnTop { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.PositionRpc))]
    CommunityEntity.DraggablePositionSendType? PositionRpc { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.MoveToAnchor))]
    bool MoveToAnchor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Draggable), nameof(JsonDefaults.Draggable.RebuildAnchor))]
    bool RebuildAnchor { get; set; }
}