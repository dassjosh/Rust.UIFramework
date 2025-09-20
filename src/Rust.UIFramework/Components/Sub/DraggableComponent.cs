using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

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

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Draggable.LimitToParentName, LimitToParent, JsonDefaults.Draggable.LimitToParent);
        writer.AddField(JsonDefaults.Draggable.MaxDistanceName, MaxDistance, JsonDefaults.Draggable.MaxDistance);
        writer.AddField(JsonDefaults.Draggable.AllowSwappingName, AllowSwapping, JsonDefaults.Draggable.AllowSwapping);
        writer.AddField(JsonDefaults.Draggable.DropAnywhereName, DropAnywhere, JsonDefaults.Draggable.DropAnywhere);
        writer.AddField(JsonDefaults.Draggable.DragAlphaName, DragAlpha, JsonDefaults.Draggable.DragAlpha);
        writer.AddField(JsonDefaults.Draggable.ParentLimitIndexName, ParentLimitIndex, JsonDefaults.Draggable.ParentLimitIndex);
        writer.AddField(JsonDefaults.Draggable.FilterName, Filter, JsonDefaults.Common.NullValue);
        writer.AddField(JsonDefaults.Draggable.ParentPaddingName, ParentPadding, JsonDefaults.Draggable.ParentPadding);
        writer.AddField(JsonDefaults.Draggable.AnchorOffsetName, AnchorOffset, JsonDefaults.Draggable.AnchorOffset);
        writer.AddField(JsonDefaults.Draggable.KeepOnTopName, KeepOnTop, JsonDefaults.Draggable.KeepOnTop);
        writer.AddField(JsonDefaults.Draggable.PositionRpcName, PositionRpc);
        writer.AddKeyField(JsonDefaults.Draggable.MoveToAnchorName, MoveToAnchor);
        writer.AddKeyField(JsonDefaults.Draggable.RebuildAnchorName, RebuildAnchor);
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
}