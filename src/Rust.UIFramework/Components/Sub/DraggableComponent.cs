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