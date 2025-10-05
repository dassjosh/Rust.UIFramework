using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(DraggableComponentSerializer))]
public class DraggableComponent : SubComponent
{
    private readonly TrackedValue<bool> _limitToParent = new(JsonDefaults.Draggable.LimitToParent);
    private readonly TrackedValue<float> _maxDistance = new(JsonDefaults.Draggable.MaxDistance);
    private readonly TrackedValue<bool> _allowSwapping = new(JsonDefaults.Draggable.AllowSwapping);
    private readonly TrackedValue<bool> _dropAnywhere = new(JsonDefaults.Draggable.DropAnywhere);
    private readonly TrackedValue<float> _dragAlpha = new(JsonDefaults.Draggable.DragAlpha);
    private readonly TrackedValue<int> _parentLimitIndex = new(JsonDefaults.Draggable.ParentLimitIndex);
    private readonly TrackedValue<string> _filter = new();
    private readonly TrackedValue<Vector2> _parentPadding = new(JsonDefaults.Draggable.ParentPadding);
    private readonly TrackedValue<Vector2> _anchorOffset = new(JsonDefaults.Draggable.AnchorOffset);
    private readonly TrackedValue<bool> _keepOnTop = new(JsonDefaults.Draggable.KeepOnTop);
    private readonly TrackedValue<DraggablePositionSendType?> _positionRpc = new(JsonDefaults.Draggable.PositionRpc);
    private readonly TrackedValue<bool> _moveToAnchor = new(JsonDefaults.Draggable.MoveToAnchor);
    private readonly TrackedValue<bool> _rebuildAnchor = new(JsonDefaults.Draggable.RebuildAnchor);
    
    public bool LimitToParent { get => _limitToParent.Value; set => _limitToParent.Value = value; }
    public float MaxDistance { get => _maxDistance.Value; set => _maxDistance.Value = value; }
    public bool AllowSwapping { get => _allowSwapping.Value; set => _allowSwapping.Value = value; }
    public bool DropAnywhere { get => _dropAnywhere.Value; set => _dropAnywhere.Value = value; }
    public float DragAlpha { get => _dragAlpha.Value; set => _dragAlpha.Value = value; }
    public int ParentLimitIndex { get => _parentLimitIndex.Value; set => _parentLimitIndex.Value = value; }
    public string Filter { get => _filter.Value; set => _filter.Value = value; }
    public Vector2 ParentPadding { get => _parentPadding.Value; set => _parentPadding.Value = value; }
    public Vector2 AnchorOffset { get => _anchorOffset.Value; set => _anchorOffset.Value = value; }
    public bool KeepOnTop { get => _keepOnTop.Value; set => _keepOnTop.Value = value; }
    public DraggablePositionSendType? PositionRpc { get => _positionRpc.Value; set => _positionRpc.Value = value; }
    public bool MoveToAnchor { get => _moveToAnchor.Value; set => _moveToAnchor.Value = value; }
    public bool RebuildAnchor { get => _rebuildAnchor.Value; set => _rebuildAnchor.Value = value; }

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

    public DraggableComponent SetLimitToParent(bool limitToParent)
    {
        LimitToParent = limitToParent;
        return this;
    }
    
    public DraggableComponent SetMaxDistance(float maxDistance)
    {
        MaxDistance = maxDistance;
        return this;
    }
    
    public DraggableComponent SetAllowSwapping(bool allowSwapping)
    {
        AllowSwapping = allowSwapping;
        return this;
    }

    public DraggableComponent SetDropAnywhere(bool dropAnywhere)
    {
        DropAnywhere = dropAnywhere;
        return this;
    }

    public DraggableComponent SetDragAlpha(float dragAlpha)
    {
        DragAlpha = dragAlpha;
        return this;
    }

    public DraggableComponent SetParentLimitIndex(int parentLimitIndex)
    {
        ParentLimitIndex = parentLimitIndex;
        return this;
    }

    public DraggableComponent SetFilter(string filter)
    {
        Filter = filter;
        return this;
    }

    public DraggableComponent SetParentPadding(Vector2 parentPadding)
    {
        ParentPadding = parentPadding;
        return this;
    }

    public DraggableComponent SetAnchorOffset(Vector2 anchorOffset)
    {
        AnchorOffset = anchorOffset;
        return this;
    }

    public DraggableComponent SetKeepOnTop(bool keepOnTop)
    {
        KeepOnTop = keepOnTop;
        return this;
    }
    
    public DraggableComponent SetPositionRpc(DraggablePositionSendType positionRpc)
    {
        PositionRpc = positionRpc;
        return this;
    }
    
    public DraggableComponent SetMoveToAnchor(bool moveToAnchor)
    {
        MoveToAnchor = moveToAnchor;
        return this;
    }
    
    public DraggableComponent SetRebuildAnchor(bool rebuildAnchor)
    {
        RebuildAnchor = rebuildAnchor;
        return this;
    }
    
    public override bool HasChanged()
    {
        return _limitToParent.HasChanged ||
               _maxDistance.HasChanged ||
               _allowSwapping.HasChanged ||
               _dropAnywhere.HasChanged ||
               _dragAlpha.HasChanged ||
               _parentLimitIndex.HasChanged ||
               _filter.HasChanged ||
               _parentPadding.HasChanged ||
               _anchorOffset.HasChanged ||
               _keepOnTop.HasChanged ||
               _positionRpc.HasChanged ||
               _moveToAnchor.HasChanged ||
               _rebuildAnchor.HasChanged;
    }

    public override void Reset()
    {
        base.Reset();
        _limitToParent.Reset();
        _maxDistance.Reset();
        _allowSwapping.Reset();
        _dropAnywhere.Reset();
        _dragAlpha.Reset();
        _parentLimitIndex.Reset();
        _filter.Reset();
        _parentPadding.Reset();
        _anchorOffset.Reset();
        _keepOnTop.Reset();
        _positionRpc.Reset();
        _moveToAnchor.Reset();
        _rebuildAnchor.Reset();
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is DraggableComponent component)
        {
            LimitToParent = component.LimitToParent;
            MaxDistance = component.MaxDistance;
            AllowSwapping = component.AllowSwapping;
            DropAnywhere = component.DropAnywhere;
            DragAlpha = component.DragAlpha;
            ParentLimitIndex = component.ParentLimitIndex;
            Filter = component.Filter;
            ParentPadding = component.ParentPadding;
            AnchorOffset = component.AnchorOffset;
            KeepOnTop = component.KeepOnTop;
            PositionRpc = component.PositionRpc;
            MoveToAnchor = component.MoveToAnchor;
            RebuildAnchor = component.RebuildAnchor;
        }
    }

    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        DraggableComponent typedOther = (DraggableComponent)other!;
        return LimitToParent == typedOther.LimitToParent 
            && MaxDistance == typedOther.MaxDistance 
            && AllowSwapping == typedOther.AllowSwapping 
            && DropAnywhere == typedOther.DropAnywhere 
            && DragAlpha == typedOther.DragAlpha 
            && ParentLimitIndex == typedOther.ParentLimitIndex 
            && Filter == typedOther.Filter 
            && ParentPadding == typedOther.ParentPadding 
            && AnchorOffset == typedOther.AnchorOffset 
            && KeepOnTop == typedOther.KeepOnTop 
            && PositionRpc == typedOther.PositionRpc 
            && MoveToAnchor == typedOther.MoveToAnchor 
            && RebuildAnchor == typedOther.RebuildAnchor;
    }
}