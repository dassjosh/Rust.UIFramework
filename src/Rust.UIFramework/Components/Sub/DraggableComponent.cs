using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(DraggableComponentSerializer))]
public class DraggableComponent : SubComponent
{
    public bool LimitToParent;
    public float MaxDistance;
    public bool AllowSwapping;
    public bool DropAnywhere;
    public float DragAlpha;
    public int ParentLimitIndex;
    public string Filter;
    public Vector2 ParentPadding;
    public Vector2 AnchorOffset;
    public bool KeepOnTop;
    public DraggablePositionSendType? PositionRpc;
    public bool MoveToAnchor;
    public bool RebuildAnchor;

    public override Utf8String Type => JsonDefaults.Draggable.Type;
    public override ComponentType ComponentType => ComponentType.Draggable;
    public override bool AllowMultiple => false;

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

    public override void Reset()
    {
        base.Reset();
        LimitToParent = JsonDefaults.Draggable.LimitToParent;
        MaxDistance = JsonDefaults.Draggable.MaxDistance;
        AllowSwapping = JsonDefaults.Draggable.AllowSwapping;
        DropAnywhere = JsonDefaults.Draggable.DropAnywhere;
        DragAlpha = JsonDefaults.Draggable.DragAlpha;
        ParentLimitIndex = JsonDefaults.Draggable.ParentLimitIndex;
        Filter = JsonDefaults.Common.NullValue;
        ParentPadding = JsonDefaults.Draggable.ParentPadding;
        AnchorOffset = JsonDefaults.Draggable.AnchorOffset;
        KeepOnTop = JsonDefaults.Draggable.KeepOnTop;
        PositionRpc = JsonDefaults.Draggable.PositionRpc;
        MoveToAnchor = JsonDefaults.Draggable.MoveToAnchor;
        RebuildAnchor = JsonDefaults.Draggable.RebuildAnchor;
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

    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
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