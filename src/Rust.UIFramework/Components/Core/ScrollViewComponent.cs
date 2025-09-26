using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ScrollViewComponent : CoreComponent
{
    public readonly ScrollViewContentComponent ContentTransform = new();
    public ScrollRect.MovementType MovementType;
    public float Elasticity;
    public bool Inertia;
    public float DecelerationRate;
    public float ScrollSensitivity;
    public float HorizontalScrollProgress;
    public float VerticalScrollProgress;
    public ScrollbarComponent HorizontalScrollbar { get; private set; }
    public ScrollbarComponent VerticalScrollbar { get; private set; }
    
    public override Utf8String Type => JsonDefaults.ScrollView.Type;

    internal void UpdateContentTransform(in UiPosition? position, in UiOffset? offset, in Vector2? pivot) => ContentTransform.UpdateContentTransform(position, offset, pivot);
    
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
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.ScrollView.Horizontal, HorizontalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.Vertical, VerticalScrollbar != null, false);
        writer.AddField(JsonDefaults.ScrollView.MovementTypeName, MovementType, JsonDefaults.ScrollView.MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, Elasticity, JsonDefaults.ScrollView.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.InertiaName, Inertia, JsonDefaults.ScrollView.Inertia);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, DecelerationRate, JsonDefaults.ScrollView.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, ScrollSensitivity, JsonDefaults.ScrollView.ScrollSensitivity);
        writer.AddField(JsonDefaults.ScrollView.HorizontalScrollProgressName, HorizontalScrollProgress, JsonDefaults.ScrollView.HorizontalScrollProgress);
        writer.AddField(JsonDefaults.ScrollView.VerticalScrollProgressName, VerticalScrollProgress, JsonDefaults.ScrollView.VerticalScrollProgress);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, HorizontalScrollbar, HorizontalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, VerticalScrollbar, VerticalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, ContentTransform);
    }

    public override void Reset()
    {
        base.Reset();
        ContentTransform.Reset();
        HorizontalScrollbar?.Dispose();
        HorizontalScrollbar = null;
        VerticalScrollbar?.Dispose();
        VerticalScrollbar = null;
        MovementType = JsonDefaults.ScrollView.MovementType;
        Elasticity = JsonDefaults.ScrollView.Elasticity;
        Inertia = JsonDefaults.ScrollView.Inertia;
        DecelerationRate = JsonDefaults.ScrollView.DecelerationRate;
        ScrollSensitivity = JsonDefaults.ScrollView.ScrollSensitivity;
        HorizontalScrollProgress = JsonDefaults.ScrollView.HorizontalScrollProgress;
        VerticalScrollProgress = JsonDefaults.ScrollView.VerticalScrollProgressOverride;
    }
}