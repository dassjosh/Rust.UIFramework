using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public abstract partial class BaseUiComponent : BasePoolable
{
    [GenerateBuilderMethod]
    public UiReference Reference { get; set; }
    [GenerateBuilderMethod]
    public string Name { get => Reference.Name; set => Reference = Reference.WithName(value); }
    [GenerateBuilderMethod]
    public string Parent { get => Reference.Parent; set => Reference = Reference.WithParent(value); } 
    [GenerateBuilderMethod]
    public UpdateMode Update { get; set; }
    
    [Tracked]
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeOut))]
    public partial float FadeOut { get; set; }
    
    [Tracked]
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.Active))]
    public partial bool Active { get; set; }
    
    [PropertyTarget(nameof(Component), PropertyTargetType.Field)]
    public partial bool Enabled { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiPosition Position { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiOffset Offset { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiTranslate PositionTranslate { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiTranslate OffsetTranslate { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiPadding PositionPadding { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiPadding OffsetPadding { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiScale PositionScale { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiScale OffsetScale { get; set; }
    
    [PropertyTarget(nameof(RectTransform), PropertyTargetType.Property)]
    public partial UiRotation Rotation { get; set; }

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
        writer.AddField(JsonDefaults.Common.ComponentName, Reference.Name);
        if (mode == SerializeMode.Create)
        {
            writer.AddField(JsonDefaults.Common.ParentName, Reference.Parent);
        }
        writer.AddField(JsonDefaults.Common.FadeOutName, _fadeOut, mode);
        writer.AddField(JsonDefaults.Common.ActiveName, _active, mode);
        switch (Update)
        {
            case UpdateMode.Replace:
                writer.AddField(JsonDefaults.Common.Replace, Reference.Name);
                break;
            case UpdateMode.Update:
                writer.AddField(JsonDefaults.Common.Update, true);
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

    public void ShareSubComponent(ISubComponent component)
    {
        Component.RemoveSubComponent(component.ComponentType);
        Component.AddSubComponent(component);
        if (component is RectTransformComponent rect)
        {
            _rectTransform = rect;
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