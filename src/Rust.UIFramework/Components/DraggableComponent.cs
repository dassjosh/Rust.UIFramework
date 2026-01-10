using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class DraggableComponent : IComponent
{
    private const string Type = "Draggable";

    public bool LimitToParent;
    public float MaxDistance = JsonDefaults.Draggable.MaxDistanceDefault;
    public int ParentLimitIndex = JsonDefaults.Draggable.ParentLimitIndexDefault;
    public bool AllowSwapping;
    public bool DropAnywhere = true;
    public float DragAlpha = JsonDefaults.Draggable.DragAlphaDefault;
    public string Filter;
    public Vector2 ParentPadding;
    public Vector2 AnchorOffset;
    public bool KeepOnTop;
    public DraggablePositionType PositionRpc;
    public bool MoveToAnchor;
    public bool RebuildAnchor;
    public bool Enabled = true;

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Draggable.LimitToParentName, LimitToParent, false);
        writer.AddField(JsonDefaults.Draggable.MaxDistanceName, MaxDistance, JsonDefaults.Draggable.MaxDistanceDefault);
        writer.AddField(JsonDefaults.Draggable.ParentLimitIndexName, ParentLimitIndex, JsonDefaults.Draggable.ParentLimitIndexDefault);
        writer.AddField(JsonDefaults.Draggable.AllowSwappingName, AllowSwapping, false);
        writer.AddField(JsonDefaults.Draggable.DropAnywhereName, DropAnywhere, true);
        writer.AddField(JsonDefaults.Draggable.DragAlphaName, DragAlpha, JsonDefaults.Draggable.DragAlphaDefault);
        writer.AddField(JsonDefaults.Draggable.FilterName, Filter, null);
        writer.AddField(JsonDefaults.Draggable.ParentPaddingName, ParentPadding, Vector2.zero);
        writer.AddField(JsonDefaults.Draggable.AnchorOffsetName, AnchorOffset, Vector2.zero);
        writer.AddField(JsonDefaults.Draggable.KeepOnTopName, KeepOnTop, false);
        writer.AddField(JsonDefaults.Draggable.PositionRpcName, PositionRpc);
        writer.AddField(JsonDefaults.Draggable.MoveToAnchorName, MoveToAnchor, false);
        writer.AddField(JsonDefaults.Draggable.RebuildAnchorName, RebuildAnchor, false);
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
        writer.WriteEndObject();
    }

    public void Reset()
    {
        LimitToParent = false;
        MaxDistance = JsonDefaults.Draggable.MaxDistanceDefault;
        ParentLimitIndex = JsonDefaults.Draggable.ParentLimitIndexDefault;
        AllowSwapping = false;
        DropAnywhere = true;
        DragAlpha = JsonDefaults.Draggable.DragAlphaDefault;
        Filter = null;
        ParentPadding = Vector2.zero;
        AnchorOffset = Vector2.zero;
        KeepOnTop = false;
        PositionRpc = DraggablePositionType.NormalizedScreen;
        MoveToAnchor = false;
        RebuildAnchor = false;
        Enabled = true;
    }
}
