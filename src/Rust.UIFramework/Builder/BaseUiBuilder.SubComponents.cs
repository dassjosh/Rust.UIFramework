using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Outline
    public OutlineComponent Outline(BaseUiComponent component) => component.AddOutline();
    
    public OutlineComponent Outline(BaseUiComponent component, UiColor color, Vector2? distance = null, bool useGraphicAlpha = false)
    {
       return component.AddOutline(color, distance, useGraphicAlpha);
    }
    #endregion
    
    #region Countdown
    public CountdownComponent Countdown(UiLabel component) => component.AddCountdown();
    
    public CountdownComponent Countdown(UiLabel label, float startTime, float endTime, string command, 
        float step = JsonDefaults.Countdown.StepValue, 
        float interval = JsonDefaults.Countdown.IntervalValue, 
        TimerFormat timerFormat = JsonDefaults.Countdown.TimeFormatValue, 
        string numberFormat = JsonDefaults.Countdown.NumberFormatValue, 
        bool destroyIfDone = JsonDefaults.Countdown.DestroyIfDone)
    {
        return label.AddCountdown(startTime, endTime, command, step, interval, timerFormat, numberFormat, destroyIfDone);
    }
    #endregion
    
    #region Draggable
    public DraggableComponent Draggable(BaseUiComponent component) => component.GetOrAddSubComponent<DraggableComponent>();
    
    public DraggableComponent Draggable(BaseUiComponent component,
        bool limitToParent = JsonDefaults.Draggable.LimitToParent,
        float maxDistance = JsonDefaults.Draggable.MaxDistance,
        bool allowSwapping = JsonDefaults.Draggable.AllowSwapping,
        bool dropAnywhere = JsonDefaults.Draggable.DropAnywhere,
        float dragAlpha = JsonDefaults.Draggable.DragAlpha,
        int parentLimitIndex = JsonDefaults.Draggable.ParentLimitIndex,
        string filter = JsonDefaults.Common.NullValue,
        Vector2? parentPadding = null,
        Vector2? anchorOffset = null,
        bool keepOnTop = JsonDefaults.Draggable.KeepOnTop,
        DraggablePositionSendType? positionRpc = null,
        bool moveToAnchor = JsonDefaults.Draggable.MoveToAnchor,
        bool rebuildAnchor = JsonDefaults.Draggable.RebuildAnchor)
    {
        DraggableComponent draggable = component.GetOrAddSubComponent<DraggableComponent>();
        draggable.LimitToParent = limitToParent;
        draggable.MaxDistance = maxDistance;
        draggable.AllowSwapping = allowSwapping;
        draggable.DropAnywhere = dropAnywhere;
        draggable.DragAlpha = dragAlpha;
        draggable.ParentLimitIndex = parentLimitIndex;
        draggable.Filter = filter;
        draggable.ParentPadding = parentPadding ?? Vector2.zero;
        draggable.AnchorOffset = anchorOffset ?? Vector2.zero;
        draggable.KeepOnTop = keepOnTop;
        draggable.PositionRpc = positionRpc;
        draggable.MoveToAnchor = moveToAnchor;
        draggable.RebuildAnchor = rebuildAnchor;
        return draggable;
    }
    #endregion

    #region Slot
    public SlotComponent Slot(BaseUiComponent component, string filter = null)
    {
        SlotComponent slot = component.GetOrAddSubComponent<SlotComponent>();
        slot.Filter = filter;
        return slot;
    }
    #endregion

    #region Horizontal Layout
    public HorizontalLayoutComponent HorizontalLayout(BaseUiComponent component) => component.GetOrAddLayoutComponent<HorizontalLayoutComponent>();
    
    public HorizontalLayoutComponent HorizontalLayout(BaseUiComponent component,
        float spacing = JsonDefaults.DirectionalLayout.Spacing,
        TextAnchor childAlignment = JsonDefaults.Layout.ChildAlignment,
        bool childForceExpandWidth = JsonDefaults.DirectionalLayout.ChildForceExpandWidth,
        bool childForceExpandHeight = JsonDefaults.DirectionalLayout.ChildForceExpandHeight,
        bool childControlWidth = JsonDefaults.DirectionalLayout.ChildControlWidth,
        bool childControlHeight = JsonDefaults.DirectionalLayout.ChildControlHeight,
        bool childScaleWidth = JsonDefaults.DirectionalLayout.ChildScaleWidth,
        bool childScaleHeight = JsonDefaults.DirectionalLayout.ChildScaleHeight,
        in UiPadding padding = default)
    {
        HorizontalLayoutComponent layout = component.GetOrAddLayoutComponent<HorizontalLayoutComponent>();
        layout.Spacing = spacing;
        layout.ChildAlignment = childAlignment;
        layout.ChildForceExpandWidth = childForceExpandWidth;
        layout.ChildForceExpandHeight = childForceExpandHeight;
        layout.ChildControlWidth = childControlWidth;
        layout.ChildControlHeight = childControlHeight;
        layout.ChildScaleWidth = childScaleWidth;
        layout.ChildScaleHeight = childScaleHeight;
        layout.Padding = padding;
        return layout;
    }
    #endregion
    
    #region Horizontal Layout
    public VerticalLayoutComponent VerticalLayout(BaseUiComponent component) => component.GetOrAddLayoutComponent<VerticalLayoutComponent>();
    
    public VerticalLayoutComponent VerticalLayout(BaseUiComponent component,
        float spacing = JsonDefaults.DirectionalLayout.Spacing,
        TextAnchor childAlignment = JsonDefaults.Layout.ChildAlignment,
        bool childForceExpandWidth = JsonDefaults.DirectionalLayout.ChildForceExpandWidth,
        bool childForceExpandHeight = JsonDefaults.DirectionalLayout.ChildForceExpandHeight,
        bool childControlWidth = JsonDefaults.DirectionalLayout.ChildControlWidth,
        bool childControlHeight = JsonDefaults.DirectionalLayout.ChildControlHeight,
        bool childScaleWidth = JsonDefaults.DirectionalLayout.ChildScaleWidth,
        bool childScaleHeight = JsonDefaults.DirectionalLayout.ChildScaleHeight,
        in UiPadding padding = default)
    {
        VerticalLayoutComponent layout = component.GetOrAddLayoutComponent<VerticalLayoutComponent>();
        layout.Spacing = spacing;
        layout.ChildAlignment = childAlignment;
        layout.ChildForceExpandWidth = childForceExpandWidth;
        layout.ChildForceExpandHeight = childForceExpandHeight;
        layout.ChildControlWidth = childControlWidth;
        layout.ChildControlHeight = childControlHeight;
        layout.ChildScaleWidth = childScaleWidth;
        layout.ChildScaleHeight = childScaleHeight;
        layout.Padding = padding;
        return layout;
    }
    #endregion
    
    #region Grid Layout
    public GridLayoutComponent GridLayout(BaseUiComponent component) => component.GetOrAddLayoutComponent<GridLayoutComponent>();

    public GridLayoutComponent GridLayout(BaseUiComponent component,
        Vector2 cellSize,
        Vector2 spacing,
        TextAnchor childAlignment = JsonDefaults.Layout.ChildAlignment,
        GridLayoutGroup.Corner startCorner = JsonDefaults.GridLayout.StartCorner,
        GridLayoutGroup.Axis startAxis = JsonDefaults.GridLayout.StartAxis,
        GridLayoutGroup.Constraint constraint = JsonDefaults.GridLayout.Constraint,
        int constraintCount = JsonDefaults.GridLayout.ConstraintCount,
        in UiPadding padding = default)
    {
        GridLayoutComponent layout = component.GetOrAddLayoutComponent<GridLayoutComponent>();
        layout.CellSize = cellSize;
        layout.Spacing = spacing;
        layout.ChildAlignment = childAlignment;
        layout.StartCorner = startCorner;
        layout.StartAxis = startAxis;
        layout.Constraint = constraint;
        layout.ConstraintCount = constraintCount;
        layout.Padding = padding;
        return layout;
    }
    #endregion

    #region Content Size Fitter

    public ContentSizeFitterComponent ContentSizeFitter(BaseUiComponent component) => component.GetOrAddSubComponent<ContentSizeFitterComponent>();

    public ContentSizeFitterComponent ContentSizeFitter(BaseUiComponent component,
        ContentSizeFitter.FitMode horizontalFit = JsonDefaults.ContentSizeFitterData.HorizontalFit,
        ContentSizeFitter.FitMode verticalFit = JsonDefaults.ContentSizeFitterData.VerticalFit)
    {
        ContentSizeFitterComponent layout = component.GetOrAddSubComponent<ContentSizeFitterComponent>();
        layout.HorizontalFit = horizontalFit;
        layout.VerticalFit = verticalFit;
        return layout;
    }
    #endregion
    
    #region Layout Element
    public LayoutElementComponent LayoutElement(BaseUiComponent component) => component.GetOrAddSubComponent<LayoutElementComponent>();

    public LayoutElementComponent LayoutElement(BaseUiComponent component,
        float preferredWidth = JsonDefaults.LayoutElement.PreferredWidth,
        float preferredHeight = JsonDefaults.LayoutElement.PreferredHeight,
        float minWidth = JsonDefaults.LayoutElement.MinWidth,
        float minHeight = JsonDefaults.LayoutElement.MinHeight,
        float flexibleWidth = JsonDefaults.LayoutElement.FlexibleWidth,
        float flexibleHeight = JsonDefaults.LayoutElement.FlexibleHeight,
        bool ignoreLayout = JsonDefaults.LayoutElement.IgnoreLayout)
    {
        LayoutElementComponent layout = component.GetOrAddSubComponent<LayoutElementComponent>();
        layout.PreferredWidth = preferredWidth;
        layout.PreferredHeight = preferredHeight;
        layout.MinWidth = minWidth;
        layout.MinHeight = minHeight;
        layout.FlexibleWidth = flexibleWidth;
        layout.FlexibleHeight = flexibleHeight;
        layout.IgnoreLayout = ignoreLayout;
        return layout;
    }

    #endregion
}