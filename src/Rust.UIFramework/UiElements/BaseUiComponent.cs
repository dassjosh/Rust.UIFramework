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
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiComponent(CoreComponent component) : BasePoolable
{
    public UiReference Reference;
    public float FadeOut;
    public UpdateMode Update;
    public bool Active = true;
    private readonly CoreComponent _component = component;
    
    public string Name { get => Reference.Name; set => Reference = Reference.WithName(value); }
    public string Parent { get => Reference.Parent; set => Reference = Reference.WithParent(value); }
    public bool Enabled { get => _component.Enabled; set => _component.Enabled = value; }
    public UiPosition Position { get => RectTransform.Position; set => RectTransform.Position = value; }
    public UiOffset Offset { get => RectTransform.Offset; set => RectTransform.Offset = value; }
    public UiRotation Rotation { get => RectTransform.Rotation; set => RectTransform.Rotation = value; }
    public UiPadding Padding { get => RectTransform.Padding; set => RectTransform.Padding = value; }

    private RectTransformComponent _rectTransform;
    public RectTransformComponent RectTransform => _rectTransform ??= _component.GetOrAddSubComponent<RectTransformComponent>();

    [Obsolete("This method is obsolete. Please use UiBuilder.Component<T>() instead.")]
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
    }
    
    internal T GetOrAddSubComponent<T>() where T : SubComponent, new() => _component.GetOrAddSubComponent<T>();
    internal T GetOrAddLayoutComponent<T>() where T : BaseLayoutComponent, new()
    {
        T layout = GetOrAddSubComponent<T>();
        layout.Owner = this;
        return layout;
    }

    public OutlineComponent AddOutline() => _component.AddSubComponent<OutlineComponent>();
    
    public OutlineComponent AddOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false)
    {
        OutlineComponent outline = AddOutline();
        outline.Color = color;
        outline.Distance = distance ?? JsonDefaults.Outline.Distance;
        outline.UseGraphicAlpha = useGraphicAlpha;
        return outline;
    }

    [Obsolete("Use AddOutline instead")]
    public OutlineComponent AddElementOutline(UiColor color, Vector2? distance = null, bool useGraphicAlpha = false) => AddOutline(color, distance, useGraphicAlpha);
    
    public void NeedsMouse(bool enabled = true)
    {
        if (enabled)
        {
            _component.AddSubComponent<NeedsMouseComponent>(true);
        }
        else
        {
            _component.RemoveSubComponent<NeedsMouseComponent>();
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
            _component.RemoveSubComponent<NeedsKeyboardComponent>();
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
        _rectTransform = null;
    }

    public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
    public static implicit operator AnimationReference(BaseUiComponent component) => new(component.Reference, component._component.Type);
}