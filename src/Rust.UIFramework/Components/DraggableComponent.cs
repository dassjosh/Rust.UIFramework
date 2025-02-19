using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class DraggableComponent : BasePoolable, IComponent
{
    private const string Type = "Draggable";

    public bool LimitToParent = JsonDefaults.Draggable.LimitToParent;
    public float MaxDistance = JsonDefaults.Draggable.MaxDistance;
    public bool AllowSwapping = JsonDefaults.Draggable.AllowSwapping;
    public bool DropAnywhere = JsonDefaults.Draggable.DropAnywhere;
    public float DragAlpha = JsonDefaults.Draggable.DragAlpha;
    public int ParentLimitIndex = JsonDefaults.Draggable.ParentLimitIndex;
    public string Filter = JsonDefaults.Common.NullValue;
    public Vector2 ParentPadding = JsonDefaults.Draggable.ParentPadding;
    public Vector2 AnchorOffset = JsonDefaults.Draggable.AnchorOffset;
    public bool KeepOnTop = JsonDefaults.Draggable.KeepOnTop;
    public DraggablePositionSendType? PositionRpc;
    public bool MoveToAnchor;
    public bool RebuildAnchor;

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
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
        writer.WriteEndObject();
    }

    public void Reset()
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
        PositionRpc = null;
        MoveToAnchor = false;
        RebuildAnchor = false;
    }

    protected override void EnterPool()
    {
        Reset();
    }
}