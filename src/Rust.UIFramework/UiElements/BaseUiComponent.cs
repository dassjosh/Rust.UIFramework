using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiComponent : BasePoolable
{
    public UiReference Reference;
    public float FadeOut;
    public UiPosition Position;
    public UiOffset Offset;
    internal abstract CoreComponent Component { get; }

    protected static T CreateBase<T>(in UiPosition pos, in UiOffset offset) where T : BaseUiComponent, new()
    {
        T component = UiFrameworkPool.Get<T>();
        component.Position = pos;
        component.Offset = offset;
        return component;
    }

    public void WriteRootComponent(JsonFrameworkWriter writer, bool needsMouse, bool needsKeyboard, bool autoDestroy)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
            
        if (autoDestroy)
        {
            writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Reference.Name);
        }

        writer.WritePropertyName("components");
        writer.WriteStartArray();
        WriteComponents(writer);

        if (needsMouse)
        {
            writer.AddMouse();
        }

        if (needsKeyboard)
        {
            writer.AddKeyboard();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    public void WriteUpdateComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
        writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Reference.Name);

        writer.WritePropertyName("components");
        writer.WriteStartArray();
        WriteComponents(writer);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);

        writer.WritePropertyName("components");
        writer.WriteStartArray();
        WriteComponents(writer);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    protected void WriteComponents(JsonFrameworkWriter writer)
    {
        Component.WriteComponent(writer);
        WriteTransform(writer);
        Component.WriteSubComponents(writer);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteTransform(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddPosition(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Common.Min);
        writer.AddPosition(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Common.Max);
        writer.AddOffset(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.Common.Min);
        writer.AddOffset(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }

    public void SetFadeOut(float duration)
    {
        FadeOut = duration;
    }
    
    public OutlineComponent AddOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false)
    {
        OutlineComponent outline = Component.AddSubComponent<OutlineComponent>();
        outline.Color = color;
        outline.Distance = distance ?? JsonDefaults.Outline.Distance;
        outline.UseGraphicAlpha = useGraphicAlpha;
        return outline;
    }

    [Obsolete("Use AddOutline instead")]
    public OutlineComponent AddElementOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false) => AddOutline(color, distance, useGraphicAlpha);
    
    public DraggableComponent AddDraggable(bool limitToParent = JsonDefaults.Draggable.LimitToParent,
        float maxDistance = JsonDefaults.Draggable.MaxDistance,
        bool allowSwapping = JsonDefaults.Draggable.AllowSwapping,
        bool dropAnywhere = JsonDefaults.Draggable.DropAnywhere,
        float dragAlpha = JsonDefaults.Draggable.DragAlpha,
        int parentLimitIndex = JsonDefaults.Draggable.ParentLimitIndex,
        string filter = JsonDefaults.Common.NullValue,
        Vector2? parentPadding = null,
        Vector2? anchorOffset = null,
        bool keepOnTop = JsonDefaults.Draggable.KeepOnTop,
        DraggablePositionSendType? positionRpc = null,
        bool moveToAnchor = JsonDefaults.Draggable.MoveToAnchor,
        bool rebuildAnchor = JsonDefaults.Draggable.RebuildAnchor)
    {
        DraggableComponent draggable = Component.AddSubComponent<DraggableComponent>();
        draggable.LimitToParent = limitToParent;
        draggable.MaxDistance = maxDistance;
        draggable.AllowSwapping = allowSwapping;
        draggable.DropAnywhere = dropAnywhere;
        draggable.DragAlpha = dragAlpha;
        draggable.ParentLimitIndex = parentLimitIndex;
        draggable.Filter = filter;
        draggable.ParentPadding = parentPadding ?? Vector2.zero;
        draggable.AnchorOffset = anchorOffset ?? Vector2.zero;
        draggable.KeepOnTop = keepOnTop;
        draggable.PositionRpc = positionRpc;
        draggable.MoveToAnchor = moveToAnchor;
        draggable.RebuildAnchor = rebuildAnchor;
        return draggable;
    }
    
    public SlotComponent AddSlot(string filter = null)
    {
        SlotComponent slot = Component.AddSubComponent<SlotComponent>();
        slot.Filter = filter;
        return slot;
    }

    protected override void EnterPool()
    {
        Reference = default;
        FadeOut = 0;
        Position = default;
        Offset = default;
        Component.Reset();
    }

    public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
}