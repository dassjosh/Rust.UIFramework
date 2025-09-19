using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiScrollView : BaseUiComponent
{
    public readonly ScrollViewComponent ScrollView;

    public UiScrollView() : this(new ScrollViewComponent()) { }

    private UiScrollView(ScrollViewComponent component) : base(component)
    {
        ScrollView = component;
    }

    public UiReference ViewPort => _viewPort ??= Reference.WithChild($"{Reference.Name}___Viewport");
    public UiReference Content => _content ??= Reference.WithChild($"{Reference.Name}___Content");
    
    private UiReference? _viewPort;
    private UiReference? _content;

    public ScrollRect.MovementType MovementType { get => ScrollView.MovementType; set => ScrollView.MovementType = value; }
    public float Elasticity { get => ScrollView.Elasticity; set => ScrollView.Elasticity = value; }
    public bool Inertia { get => ScrollView.Inertia; set => ScrollView.Inertia = value; }
    public float DecelerationRate { get => ScrollView.DecelerationRate; set => ScrollView.DecelerationRate = value; }
    public float ScrollSensitivity { get => ScrollView.ScrollSensitivity; set => ScrollView.ScrollSensitivity = value; }
    public UiPosition ContentPosition { get => ScrollView.ContentTransform.Position; set => ScrollView.ContentTransform.Position = value; }
    public UiOffset ContentOffset { get => ScrollView.ContentTransform.Offset; set => ScrollView.ContentTransform.Offset = value; }
    public Vector2 ContentPivot { get => ScrollView.ContentTransform.Pivot; set => ScrollView.ContentTransform.Pivot = value; }
    public ScrollbarComponent HorizontalScrollbar => ScrollView.HorizontalScrollbar;
    public ScrollbarComponent VerticalScrollbar => ScrollView.VerticalScrollbar;

    public UiScrollView Init(ScrollRect.MovementType movementType, float elasticity, bool inertia, float decelerationRate, float scrollSensitivity)
    {
        MovementType = movementType;
        Elasticity = elasticity;
        Inertia = inertia;
        DecelerationRate = decelerationRate;
        ScrollSensitivity = scrollSensitivity;
        return this;
    }

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

    public UiScrollView SetElasticity(float elasticity)
    {
        Elasticity = elasticity;
        return this;
    }

    public UiScrollView SetInertia(bool inertia)
    {
        Inertia = inertia;
        return this;
    }

    public UiScrollView SetDecelerationRate(float decelerationRate)
    {
        DecelerationRate = decelerationRate;
        return this;
    }

    public UiScrollView SetScrollSensitivity(float scrollSensitivity)
    {
        ScrollSensitivity = scrollSensitivity;
        return this;
    }

    public UiScrollView SetMovementType(ScrollRect.MovementType movementType)
    {
        MovementType = movementType;
        return this;
    }
    
    public UiScrollView SetContentPosition(in UiPosition position)
    {
        ContentPosition = position;
        return this;
    }
    
    public UiScrollView SetContentOffset(in UiOffset offset)
    {
        ContentOffset = offset;
        return this;
    }
    
    public UiScrollView SetContentPivot(in Vector2 pivot)
    {
        ContentPivot = pivot;
        return this;
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

    protected override void EnterPool()
    {
        base.EnterPool();
        _viewPort = null;
        _content = null;
    }
}