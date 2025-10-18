using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiScrollView))]
public partial class UiScrollView : BaseUiComponent, IUiScrollView
{
    public readonly ScrollViewComponent ScrollView;

    public ScrollbarComponent HorizontalScrollbar => ScrollView.HorizontalScrollbar;
    public ScrollbarComponent VerticalScrollbar => ScrollView.VerticalScrollbar;
    
    public UiScrollView() : this(new ScrollViewComponent()) { }

    private UiScrollView(ScrollViewComponent component) : base(component)
    {
        ScrollView = component;
    }
    
    public UiScrollView Init(ScrollRect.MovementType movementType, float elasticity, bool inertia, float decelerationRate, float scrollSensitivity)
    {
        MovementType = movementType;
        Elasticity = elasticity;
        Inertia = inertia;
        DecelerationRate = decelerationRate;
        ScrollSensitivity = scrollSensitivity;
        return this;
    }
    
    public ScrollViewContentComponent GetOrCreateContentTransform() => ScrollView.GetOrCreateContentTransform();

    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null, in Vector2? pivot = null) => ScrollView.UpdateContentTransform(position, offset, pivot);

    public (ScrollbarComponent horizontal, ScrollbarComponent vertical) AddScrollBars(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size,
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        return ScrollView.AddScrollBars(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }
    
    public ScrollbarComponent AddHorizontalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        return ScrollView.AddHorizontalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }
    
    public ScrollbarComponent AddVerticalScrollBar(bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = JsonDefaults.ScrollBar.Size, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        return ScrollView.AddVerticalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }

    public UiScrollView SetHorizontalScrollbar()
    {
        ScrollView.GetOrCreateHorizontalScrollBar();
        return this;
    }
    
    public UiScrollView SetVerticalScrollbar()
    {
        ScrollView.GetOrCreateVerticalScrollBar();
        return this;
    }
    
    public UiScrollView SetScrollbar(ScrollbarTypes type)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal)) ScrollView.GetOrCreateHorizontalScrollBar();
        if(type.HasFlag(ScrollbarTypes.Vertical)) ScrollView.GetOrCreateVerticalScrollBar();
        return this;
    }
    
    public UiScrollView SetScrollbarInvert(ScrollbarTypes type, bool invert)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.Invert = invert;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.Invert = invert;
        return this;
    }
    
    public UiScrollView SetScrollbarAutoHide(ScrollbarTypes type, bool autoHide)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.AutoHide = autoHide;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.AutoHide = autoHide;
        return this;
    }
    
    public UiScrollView SetScrollbarHandleSprite(ScrollbarTypes type, string sprite)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.HandleSprite = sprite;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.HandleSprite = sprite;
        return this;
    }
    
    public UiScrollView SetScrollbarTrackSprite(ScrollbarTypes type, string sprite)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.TrackSprite = sprite;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.TrackSprite = sprite;
        return this;
    }
    
    public UiScrollView SetScrollbarSize(ScrollbarTypes type, float size)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.Size = size;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.Size = size;
        return this;
    }
    
    public UiScrollView SetScrollbarHandleColor(ScrollbarTypes type, UiColor color)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.HandleColor = color;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.HandleColor = color;
        return this;
    }
    
    public UiScrollView SetScrollbarHighlightColor(ScrollbarTypes type, UiColor color)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.HighlightColor = color;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.HighlightColor = color;
        return this;
    }
    
    public UiScrollView SetScrollbarPressedColor(ScrollbarTypes type, UiColor color)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.PressedColor = color;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.PressedColor = color;
        return this;
    }
    
    public UiScrollView SetScrollbarTrackColor(ScrollbarTypes type, UiColor color)
    {
        if(type.HasFlag(ScrollbarTypes.Horizontal) && HorizontalScrollbar != null) HorizontalScrollbar.TrackColor = color;
        if(type.HasFlag(ScrollbarTypes.Vertical) && VerticalScrollbar != null) VerticalScrollbar.TrackColor = color;
        return this;
    }
}