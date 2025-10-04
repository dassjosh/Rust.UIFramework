using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ScrollViewComponentSerializer))]
public class ScrollViewComponent : CoreComponent
{
    public ScrollRect.MovementType MovementType;
    public float Elasticity;
    public bool Inertia;
    public float DecelerationRate;
    public float ScrollSensitivity;
    public float HorizontalScrollProgress;
    public float VerticalScrollProgress;
    
    public ScrollViewContentComponent ContentTransform { get; private set; }
    public ScrollbarComponent HorizontalScrollbar { get; private set; }
    public ScrollbarComponent VerticalScrollbar { get; private set; }
    
    public override Utf8String Type => JsonDefaults.ScrollView.Type;
    public override ComponentType ComponentType => ComponentType.ScrollView;

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

    public override void Reset()
    {
        base.Reset();
        ContentTransform?.Dispose();
        ContentTransform = null;
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
        VerticalScrollProgress = JsonDefaults.ScrollView.VerticalScrollProgress;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is ScrollViewComponent component)
        {
            MovementType = component.MovementType;
            Elasticity = component.Elasticity;
            Inertia = component.Inertia;
            DecelerationRate = component.DecelerationRate;
            ScrollSensitivity = component.ScrollSensitivity;
            HorizontalScrollProgress = component.HorizontalScrollProgress;
            VerticalScrollProgress = component.VerticalScrollProgress;
            ContentTransform = CopyChild(ContentTransform, component.ContentTransform);
            HorizontalScrollbar = CopyChild(HorizontalScrollbar, component.HorizontalScrollbar);
            VerticalScrollbar = CopyChild(VerticalScrollbar, component.VerticalScrollbar);
        }
    }

    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ScrollViewComponent typedOther = (ScrollViewComponent)other!;
        return MovementType == typedOther.MovementType 
               && Elasticity == typedOther.Elasticity 
               && Inertia == typedOther.Inertia 
               && DecelerationRate == typedOther.DecelerationRate 
               && ScrollSensitivity == typedOther.ScrollSensitivity 
               && HorizontalScrollProgress == typedOther.HorizontalScrollProgress 
               && VerticalScrollProgress == typedOther.VerticalScrollProgress 
               && ContentTransform.TryAreEquivalent(typedOther.ContentTransform) 
               && HorizontalScrollbar.TryAreEquivalent(typedOther.HorizontalScrollbar) 
               && VerticalScrollbar.TryAreEquivalent(typedOther.VerticalScrollbar);
    }
}