using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiComponent : BasePoolable
{
    private readonly TrackedValue<float> _fadeOut = new();
    private readonly TrackedValue<bool> _active = new(true);
    
    public UiReference Reference;
    public float FadeOut { get => _fadeOut.Value; set => _fadeOut.Value = value; }
    public UpdateMode Update;
    public bool Active { get => _active.Value; set => _active.Value = value; }
    internal readonly CoreComponent _component;
    
    public string Name { get => Reference.Name; set => Reference = Reference.WithName(value); }
    public string Parent { get => Reference.Parent; set => Reference = Reference.WithParent(value); }
    public bool Enabled { get => _component.Enabled; set => _component.Enabled = value; }
    public UiPosition Position { get => RectTransform.Position; set => RectTransform.Position = value; }
    public UiOffset Offset { get => RectTransform.Offset; set => RectTransform.Offset = value; }
    public UiRotation Rotation { get => RectTransform.Rotation; set => RectTransform.Rotation = value; }
    public UiPadding Padding { get => RectTransform.Padding; set => RectTransform.Padding = value; }

    private RectTransformComponent _rectTransform;
    public RectTransformComponent RectTransform => _rectTransform ??= _component.GetOrAddSubComponent<RectTransformComponent>();
    
    protected BaseUiComponent(CoreComponent component)
    {
        _component = component;
        Reset();
    }

    [Obsolete("This method is obsolete. Please use UiBuilder.Component<T>() instead.")]
    public static T CreateBase<T>() where T : BaseUiComponent, new() => UiFrameworkPool.Get<T>();

    public void WriteElement(JsonFrameworkWriter writer)
    {
        SerializeMode mode = Update == UpdateMode.Update ? SerializeMode.Update : SerializeMode.Create;
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        if (mode == SerializeMode.Create)
        {
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, Reference.Parent);
        }
        writer.AddField(JsonDefaults.Common.FadeOutName, _fadeOut, mode);
        writer.AddField(JsonDefaults.Common.ActiveName, _active, mode);
        switch (Update)
        {
            case UpdateMode.Replace:
                writer.AddFieldRaw(JsonDefaults.Common.Replace, Reference.Name);
                break;
            case UpdateMode.Update:
                writer.AddFieldRaw(JsonDefaults.Common.Update, true);
                break;
            case UpdateMode.None:
                break;
        }
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        _component.WriteComponent(writer, mode);
        _component.WriteSubComponents(writer, mode);
        writer.WriteEndArray();
        writer.WriteEndObject();
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
            _component.GetOrAddSubComponent<NeedsMouseComponent>();
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
            _component.GetOrAddSubComponent<NeedsKeyboardComponent>();
        }
        else
        {
            _component.RemoveSubComponent<NeedsKeyboardComponent>();
        }
    }

    internal override void OnInit()
    {
        _component.OverridePluginPool(PluginPool);
    }

    protected override void EnterPool() => Reset();

    public void Reset()
    {
        Reference = default;
        _fadeOut.Reset();
        Update = default;
        _active.Reset();
        _component.Reset();
        _rectTransform = null;
    }

    public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
    public static implicit operator AnimationReference(BaseUiComponent component) => new(component.Reference, component._component.Type);
}