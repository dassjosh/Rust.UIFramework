using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiDraggable : BaseUiImage
{
    public readonly DraggableComponent Draggable = new();

    public static UiDraggable Create(in UiPosition pos, in UiOffset offset, UiColor color)
    {
        UiDraggable draggable = CreateBase<UiDraggable>(pos, offset);
        draggable.Image.Color = color;
        return draggable;
    }

    public static UiDraggable Create(
        in UiPosition pos,
        in UiOffset offset,
        UiColor color,
        bool limitToParent,
        float maxDistance,
        bool allowSwapping,
        bool dropAnywhere,
        float dragAlpha,
        string filter,
        DraggablePositionType positionRpc)
    {
        UiDraggable draggable = CreateBase<UiDraggable>(pos, offset);
        draggable.Image.Color = color;

        DraggableComponent comp = draggable.Draggable;
        comp.LimitToParent = limitToParent;
        comp.MaxDistance = maxDistance;
        comp.AllowSwapping = allowSwapping;
        comp.DropAnywhere = dropAnywhere;
        comp.DragAlpha = dragAlpha;
        comp.Filter = filter;
        comp.PositionRpc = positionRpc;

        return draggable;
    }

    public void SetLimitToParent(bool limit, int parentIndex = 1, Vector2? padding = null)
    {
        Draggable.LimitToParent = limit;
        Draggable.ParentLimitIndex = parentIndex;
        if (padding.HasValue)
        {
            Draggable.ParentPadding = padding.Value;
        }
    }

    public void SetMaxDistance(float distance)
    {
        Draggable.MaxDistance = distance;
    }

    public void SetSwapping(bool allowSwapping)
    {
        Draggable.AllowSwapping = allowSwapping;
    }

    public void SetDragBehavior(bool dropAnywhere, bool keepOnTop = false, float dragAlpha = 1f)
    {
        Draggable.DropAnywhere = dropAnywhere;
        Draggable.KeepOnTop = keepOnTop;
        Draggable.DragAlpha = dragAlpha;
    }

    public void SetFilter(string filter)
    {
        Draggable.Filter = filter;
    }

    public void SetAnchorOffset(Vector2 offset)
    {
        Draggable.AnchorOffset = offset;
    }

    public void SetPositionRpcType(DraggablePositionType type)
    {
        Draggable.PositionRpc = type;
    }

    public void SetMoveToAnchor(bool moveToAnchor)
    {
        Draggable.MoveToAnchor = moveToAnchor;
    }

    public void SetRebuildAnchor(bool rebuildAnchor)
    {
        Draggable.RebuildAnchor = rebuildAnchor;
    }

    public void SetEnabled(bool enabled)
    {
        Draggable.Enabled = enabled;
    }

    protected override void WriteComponents(JsonFrameworkWriter writer)
    {
        Image.WriteComponent(writer);
        Draggable.WriteComponent(writer);
        base.WriteComponents(writer);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Draggable.Reset();
    }
}
