using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiScrollView : BaseUiComponent
{
    public readonly ScrollViewComponent ScrollView = new();
    internal override CoreComponent Component => ScrollView;

    public UiReference ViewPort => _viewPort ??= Reference.WithChild($"{Reference.Name}___Viewport");
    public UiReference Content => _content ??= Reference.WithChild($"{Reference.Name}___Content");
    
    private UiReference? _viewPort;
    private UiReference? _content;
    
    public static UiScrollView Create(ScrollRect.MovementType movementType, float elasticity,
        bool inertia, float decelerationRate, float scrollSensitivity)
    {
        UiScrollView scroll = CreateBase<UiScrollView>();
        ScrollViewComponent comp = scroll.ScrollView;
        comp.MovementType = movementType;
        comp.Elasticity = elasticity;
        comp.Inertia = inertia;
        comp.DecelerationRate = decelerationRate;
        comp.ScrollSensitivity = scrollSensitivity;
        return scroll;
    }

    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null)
    {
        if (position.HasValue)
        {
            ScrollView.ContentTransform.Position = position.Value;
        }

        if (offset.HasValue)
        {
            ScrollView.ContentTransform.Offset = offset.Value;
        }
    }

    public void AddScrollBars(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f,
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        AddHorizontalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        AddVerticalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }
    
    public ScrollbarComponent AddHorizontalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent bar = CreateScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        ScrollView.HorizontalScrollbar = bar;
        return bar;
    }
    
    public ScrollbarComponent AddVerticalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent bar = CreateScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
        ScrollView.VerticalScrollbar = bar;
        return bar;
    }

    private static ScrollbarComponent CreateScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        ScrollbarComponent comp = UiFrameworkPool.Get<ScrollbarComponent>();
        comp.Invert = invert;
        comp.AutoHide = autoHide;
        comp.HandleSprite = handleSprite;
        comp.TrackSprite = trackSprite;
        comp.Size = size;
        if (handleColor.HasValue)
        {
            comp.HandleColor = handleColor.Value;
        }
        if (highlightColor.HasValue)
        {
            comp.HighlightColor = highlightColor.Value;
        }
        if (pressedColor.HasValue)
        {
            comp.PressedColor = pressedColor.Value;
        }
        if (trackColor.HasValue)
        {
            comp.TrackColor = trackColor.Value;
        }
        return comp;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _viewPort = null;
        _content = null;
    }
}