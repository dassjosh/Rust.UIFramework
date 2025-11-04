using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IBaseUiComponent))]
public abstract partial class BaseUiComponent : BasePoolable, IBaseUiComponent
{
    public string Name { get => Reference.Name; set => Reference = Reference.WithName(value); }
    public string Parent { get => Reference.Parent; set => Reference = Reference.WithParent(value); } 

    private RectTransformComponent _rectTransform;
    public RectTransformComponent RectTransform => _rectTransform ??= Component.GetOrAddSubComponent<RectTransformComponent>();
    
    internal readonly CoreComponent Component;
    
    protected BaseUiComponent(CoreComponent component)
    {
        Component = component;
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
        Component.WriteComponent(writer, mode);
        Component.WriteSubComponents(writer, mode);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
    
    internal T GetOrAddSubComponent<T>() where T : SubComponent, new() => Component.GetOrAddSubComponent<T>();
    internal T GetOrAddLayoutComponent<T>() where T : BaseLayoutComponent, new()
    {
        T layout = GetOrAddSubComponent<T>();
        layout.Owner = this;
        return layout;
    }

    public OutlineComponent AddOutline() => Component.AddSubComponent<OutlineComponent>();
    
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
            Component.GetOrAddSubComponent<NeedsMouseComponent>();
        }
        else
        {
            Component.RemoveSubComponent<NeedsMouseComponent>();
        }
    }

    public void NeedsKeyboard(bool enabled = true)
    {
        if (enabled)
        {
            Component.GetOrAddSubComponent<NeedsKeyboardComponent>();
        }
        else
        {
            Component.RemoveSubComponent<NeedsKeyboardComponent>();
        }
    }

    internal override void OnInit() => Component.OverridePluginPool(PluginPool);
    internal override void OverridePluginPool(UiPluginPool pluginPool)
    {
        base.OverridePluginPool(pluginPool);
        Component.OverridePluginPool(pluginPool);
    }

    protected override void EnterPool() => Reset();

    public bool HasChanged() => Component.HasChanged() || _fadeOut.HasChanged || _active.HasChanged;
    
    public void ResetHasChanged()
    {
        Component.ResetHasChanged();
        _fadeOut.ResetHasChanged();
        _active.ResetHasChanged();
    }
    
    public void Reset()
    {
        Reference = default;
        Update = default;
        _fadeOut.Reset();
        _active.Reset();
        Component.Reset();
        _rectTransform = null;
    }

    public static implicit operator UiReference(BaseUiComponent component) => component.Reference;
}