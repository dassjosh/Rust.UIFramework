using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiComponent(CoreComponent component) : BasePoolable
{
    public UiReference Reference;
    public float FadeOut;
    public UiPosition Position = UiPosition.Full;
    public UiOffset Offset;
    public UpdateMode Update;
    public bool Active = true;
    private readonly CoreComponent _component = component;
    
    public string Name { get => Reference.Name; set => Reference = Reference.WithName(value); }
    public string Parent { get => Reference.Parent; set => Reference = Reference.WithParent(value); }
    public bool Enabled { get => _component.Enabled; set => _component.Enabled = value; }

    [Obsolete]
    public static T CreateBase<T>() where T : BaseUiComponent, new() => UiFrameworkPool.Get<T>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        writer.AddField(JsonDefaults.Common.FadeOutName, FadeOut, JsonDefaults.Common.FadeOut);
        writer.AddField(JsonDefaults.Common.ActiveName, Active, JsonDefaults.Common.Active);
        switch (Update)
        {
            case UpdateMode.AutoDestroy:
                writer.AddFieldRaw(JsonDefaults.Common.AutoDestroy, Reference.Name);
                break;
            case UpdateMode.Update:
                writer.AddFieldRaw(JsonDefaults.Common.Update, true);
                break;
            case UpdateMode.None:
                break;
        }

        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        WriteComponents(writer);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteComponents(JsonFrameworkWriter writer)
    {
        _component.WriteComponent(writer);
        _component.WriteSubComponents(writer);        
        WriteTransform(writer);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteTransform(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddField(JsonDefaults.Position.AnchorMinName, Position.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Position.AnchorMaxName, Position.Max, JsonDefaults.Common.Max);
        writer.AddField(JsonDefaults.Offset.OffsetMinName, Offset.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Offset.OffsetMaxName, Offset.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }
    
    public OutlineComponent AddOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false)
    {
        OutlineComponent outline = _component.AddSubComponent<OutlineComponent>();
        outline.Color = color;
        outline.Distance = distance ?? JsonDefaults.Outline.Distance;
        outline.UseGraphicAlpha = useGraphicAlpha;
        return outline;
    }

    [Obsolete("Use AddOutline instead")]
    public OutlineComponent AddElementOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false) => AddOutline(color, distance, useGraphicAlpha);
    
    [Obsolete("This component is not currently supported in the Client. This will not work until the changes are merged in.")]
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
        DraggableComponent draggable = _component.AddSubComponent<DraggableComponent>();
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
    
    [Obsolete("This component is not currently supported in the Client. This will not work until the changes are merged in.")]
    public SlotComponent AddSlot(string filter = null)
    {
        SlotComponent slot = _component.AddSubComponent<SlotComponent>();
        slot.Filter = filter;
        return slot;
    }
    
    public void NeedsMouse(bool enabled = true)
    {
        if (enabled)
        {
            _component.AddSubComponent<NeedsMouseComponent>(true);
        }
        else
        {
            _component.RemoveComponent<NeedsMouseComponent>();
        }
    }

    public void NeedsKeyboard(bool enabled = true)
    {
        if (enabled)
        {
            _component.AddSubComponent<NeedsKeyboardComponent>(true);
        }
        else
        {
            _component.RemoveComponent<NeedsKeyboardComponent>();
        }
    }

    internal override void OnInit()
    {
        base.OnInit();
        _component.OverridePluginPool(PluginPool);
    }

    protected override void EnterPool()
    {
        Reference = default;
        FadeOut = 0;
        Position = UiPosition.Full;
        Offset = default;
        Update = default;
        Active = true;
        _component.Reset();
    }

    public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
    public static implicit operator AnimationReference(BaseUiComponent component) => new(component.Reference, component._component.Type);
}