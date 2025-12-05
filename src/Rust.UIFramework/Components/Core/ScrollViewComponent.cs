using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ScrollViewComponent : CoreComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.MovementType))]
    public partial ScrollRect.MovementType MovementType { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Elasticity))]
    public partial float Elasticity { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Inertia))]
    public partial bool Inertia { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.DecelerationRate))]
    public partial float DecelerationRate { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.ScrollSensitivity))]
    public partial float ScrollSensitivity { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.HorizontalScrollProgress))]
    public partial float HorizontalScrollProgress { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.VerticalScrollProgress))]
    public partial float VerticalScrollProgress { get; set; }
    
    public ScrollViewContentComponent ContentTransform { get; private set; }
    public ScrollbarComponent HorizontalScrollbar { get; private set; }
    public ScrollbarComponent VerticalScrollbar { get; private set; }
    
    public override Utf8String Type => JsonDefaults.ScrollView.Type;
    public override ComponentType ComponentType => ComponentType.ScrollView;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ScrollView.Horizontal, HorizontalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, VerticalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.MovementTypeName, _movementType, mode);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, _elasticity, mode);
        writer.AddField(JsonDefaults.ScrollView.InertiaName, _inertia, mode);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, _decelerationRate, mode);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, _scrollSensitivity, mode);
        writer.AddField(JsonDefaults.ScrollView.HorizontalScrollProgressName, _horizontalScrollProgress, mode);
        writer.AddField(JsonDefaults.ScrollView.VerticalScrollProgressName, _verticalScrollProgress, mode);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar, mode, HorizontalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar, mode, VerticalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform, mode);
    }
    
    internal ScrollViewContentComponent GetOrCreateContentTransform() => ContentTransform ??= PluginPool.Get<ScrollViewContentComponent>();
    internal void UpdateContentTransform(in UiPosition? position, in UiOffset? offset, in Vector2? pivot) => GetOrCreateContentTransform().UpdateContentTransform(position, offset, pivot);
    
    internal (ScrollbarComponent horizontal, ScrollbarComponent vertical) AddScrollBars(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size,
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent horizontal = AddHorizontalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        ScrollbarComponent vertical = AddVerticalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        return (horizontal, vertical);
    }
    
    internal ScrollbarComponent AddHorizontalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent bar = GetOrCreateHorizontalScrollBar();
        PopulateScrollBar(bar, invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        return bar;
    }
    
    internal ScrollbarComponent AddVerticalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent bar = GetOrCreateVerticalScrollBar();
        PopulateScrollBar(bar, invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        return bar;
    }

    internal ScrollbarComponent GetOrCreateHorizontalScrollBar() => HorizontalScrollbar ??= PluginPool.Get<ScrollbarComponent>();
    internal ScrollbarComponent GetOrCreateVerticalScrollBar() => VerticalScrollbar ??= PluginPool.Get<ScrollbarComponent>();

    private static void PopulateScrollBar(ScrollbarComponent bar, bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        bar.Invert = invert;
        bar.AutoHide = autoHide;
        bar.HandleSprite = handleSprite;
        bar.TrackSprite = trackSprite;
        bar.Size = size;
        if (handleColor.HasValue)
        {
            bar.HandleColor = handleColor.Value;
        }
        if (highlightColor.HasValue)
        {
            bar.HighlightColor = highlightColor.Value;
        }
        if (pressedColor.HasValue)
        {
            bar.PressedColor = pressedColor.Value;
        }
        if (trackColor.HasValue)
        {
            bar.TrackColor = trackColor.Value;
        }
    }
    
    public override bool HasChanged()
    {
        return base.HasChanged() 
               || (ContentTransform?.HasChanged() ?? false)
               || (HorizontalScrollbar?.HasChanged() ?? false)
               || (VerticalScrollbar?.HasChanged() ?? false);
    }

    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        ContentTransform?.ResetHasChanged();
        HorizontalScrollbar?.ResetHasChanged();
        VerticalScrollbar?.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        ContentTransform?.Dispose();
        ContentTransform = null;
        HorizontalScrollbar?.Dispose();
        HorizontalScrollbar = null;
        VerticalScrollbar?.Dispose();
        VerticalScrollbar = null;
    }
}