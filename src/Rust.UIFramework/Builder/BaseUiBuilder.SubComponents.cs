using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;

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
        float step = JsonDefaults.Countdown.Step, 
        float interval = JsonDefaults.Countdown.Interval, 
        TimerFormat timerFormat = JsonDefaults.Countdown.TimerFormat, 
        string numberFormat = JsonDefaults.Countdown.NumberFormat, 
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
        CommunityEntity.DraggablePositionSendType? positionRpc = null,
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

    #region Directional Layout
    public DirectionalLayoutComponent DirectionalLayout(BaseUiComponent component, LayoutDirection direction) => component.GetOrAddLayoutComponent<DirectionalLayoutComponent>().SetDirection(direction);
    
    public DirectionalLayoutComponent DirectionalLayout(BaseUiComponent component,
        LayoutDirection direction,
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
        DirectionalLayoutComponent layout = DirectionalLayout(component, direction);
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
    
    public UiTuple<UiSection, DirectionalLayoutComponent> DirectionalLayout(UiScrollView scroll, LayoutDirection direction, AutoSizeDirection autoSize = AutoSizeDirection.Horizontal)
    {
        UiSection section = AutoSizedScrollView(scroll, autoSize);
        DirectionalLayoutComponent layout = DirectionalLayout(section, direction);
        return UiTuple.Create(section, layout);
    }
    
    public UiTuple<UiSection, DirectionalLayoutComponent> DirectionalLayout(UiScrollView scroll,
        LayoutDirection direction,
        float spacing = JsonDefaults.DirectionalLayout.Spacing,
        TextAnchor childAlignment = JsonDefaults.Layout.ChildAlignment,
        bool childForceExpandWidth = JsonDefaults.DirectionalLayout.ChildForceExpandWidth,
        bool childForceExpandHeight = JsonDefaults.DirectionalLayout.ChildForceExpandHeight,
        bool childControlWidth = JsonDefaults.DirectionalLayout.ChildControlWidth,
        bool childControlHeight = JsonDefaults.DirectionalLayout.ChildControlHeight,
        bool childScaleWidth = JsonDefaults.DirectionalLayout.ChildScaleWidth,
        bool childScaleHeight = JsonDefaults.DirectionalLayout.ChildScaleHeight,
        in UiPadding padding = default,
        AutoSizeDirection autoSize = AutoSizeDirection.Horizontal)
    {
        UiSection section = AutoSizedScrollView(scroll, autoSize);
        DirectionalLayoutComponent layout = DirectionalLayout(section, direction, spacing, childAlignment, childForceExpandWidth, childForceExpandHeight, childControlWidth, childControlHeight, childScaleWidth, childScaleHeight, padding);
        return UiTuple.Create(section, layout);
    }
    #endregion
    
    #region Grid Layout
    public GridLayoutComponent GridLayout(BaseUiComponent component) => component.GetOrAddLayoutComponent<GridLayoutComponent>();

    public GridLayoutComponent GridLayout(BaseUiComponent component,
        Vector2 cellSize,
        Vector2 spacing = default,
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
    
    public UiTuple<UiSection, GridLayoutComponent> GridLayout(UiScrollView scroll, AutoSizeDirection autoSize = AutoSizeDirection.Vertical)
    {
        UiSection section = AutoSizedScrollView(scroll, autoSize);
        GridLayoutComponent layout = GridLayout(section);
        return UiTuple.Create(section, layout);
    }
    
    public UiTuple<UiSection, GridLayoutComponent> GridLayout(UiScrollView scroll,
        Vector2 cellSize,
        Vector2 spacing,
        TextAnchor childAlignment = JsonDefaults.Layout.ChildAlignment,
        GridLayoutGroup.Corner startCorner = JsonDefaults.GridLayout.StartCorner,
        GridLayoutGroup.Axis startAxis = JsonDefaults.GridLayout.StartAxis,
        GridLayoutGroup.Constraint constraint = JsonDefaults.GridLayout.Constraint,
        int constraintCount = JsonDefaults.GridLayout.ConstraintCount,
        in UiPadding padding = default, 
        AutoSizeDirection autoSize = AutoSizeDirection.Vertical)
    {
        UiSection section = AutoSizedScrollView(scroll, autoSize);
        GridLayoutComponent layout = GridLayout(section, cellSize, spacing, childAlignment, startCorner, startAxis, constraint, constraintCount, in padding);
        return UiTuple.Create(section, layout);
    }
    #endregion

    #region Content Size Fitter

    public ContentSizeFitterComponent ContentSizeFitter(BaseUiComponent component) => component.GetOrAddSubComponent<ContentSizeFitterComponent>();

    public ContentSizeFitterComponent ContentSizeFitter(BaseUiComponent component,
        FitMode horizontalFit = JsonDefaults.ContentSizeFitterData.HorizontalFit,
        FitMode verticalFit = JsonDefaults.ContentSizeFitterData.VerticalFit)
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

    #region Layout Helpers
    public UiSection AutoSizedScrollView(UiScrollView scrollView, AutoSizeDirection direction) => AutoSizedScrollView<UiSection>(scrollView, direction);
    
    public T AutoSizedScrollView<T>(UiScrollView scrollView, AutoSizeDirection direction) where T : BaseUiComponent, new()
    {
        T section = SelectScrollViewContent<T>(scrollView);
        ContentSizeFitterComponent fitter = ContentSizeFitter(section);
        if (direction.HasFlag(AutoSizeDirection.Horizontal))
        {
            fitter.SetHorizontalFit(FitMode.PreferredSize);
            scrollView.ContentPivot = scrollView.ContentPivot.WithX(0);
        }

        if (direction.HasFlag(AutoSizeDirection.Vertical))
        {
            fitter.SetVerticalFit(FitMode.PreferredSize);
            scrollView.ContentPivot = scrollView.ContentPivot.WithY(1);
        }

        return section;
    }

    public UiSection SelectScrollViewContent(UiScrollView scroll) => SelectScrollViewContent<UiSection>(scroll);
    
    /// <summary>
    /// Select the ScrollView___Content GameObject on the client to be able to modify.
    /// Setting the mode to update allows us to modify scroll view content when we normally cannot
    /// </summary>
    /// <param name="scroll"></param>
    /// <returns></returns>
    public T SelectScrollViewContent<T>(UiScrollView scroll) where T : BaseUiComponent, new()
    {
        return Component<T>(scroll).SetName(Singleton<ScrollViewContentCache>.Instance.GetContentName(scroll.Reference.Name)).SetUpdate(UpdateMode.Update);
    }
    #endregion

    #region Canvas Group
    public CanvasGroupComponent CanvasGroup(BaseUiComponent component) => SubComponent<CanvasGroupComponent>(component);

    public CanvasGroupComponent CanvasGroup(BaseUiComponent component, float alpha = 1f, bool allowRaycast = true, bool interactable = true, UiCanvasGroupFade? fade = null)
    {
        CanvasGroupComponent canvas = CanvasGroup(component);
        canvas.Alpha = alpha;
        canvas.AllowRaycast = allowRaycast;
        canvas.Interactable = interactable;
        if(fade.HasValue)
        {
            canvas.Fade = fade.Value;
        }
        return canvas;
    }
    #endregion

    #region Rect Mask 2D
    public RectMask2DComponent RectMask2D(BaseUiComponent component) => SubComponent<RectMask2DComponent>(component);

    public RectMask2DComponent RectMask2D(BaseUiComponent component, UiPadding padding)
    {
        RectMask2DComponent mask = RectMask2D(component);
        mask.Padding = padding;
        return mask;
    }
    #endregion

    #region Rect Mask 2D
    public MaskComponent Mask(BaseUiComponent component) => SubComponent<MaskComponent>(component);

    public MaskComponent Mask(BaseUiComponent component, bool showMaskGraphic)
    {
        MaskComponent mask = Mask(component);
        mask.ShowMaskGraphic = showMaskGraphic;
        return mask;
    }
    #endregion

    #region Rect Mask 2D
    public TooltipComponent Tooltip(BaseUiComponent component) => SubComponent<TooltipComponent>(component);

    public TooltipComponent Tooltip(BaseUiComponent component, string text, Tooltip.DelayType delay = JsonDefaults.ToolTip.Delay, TooltipContainer.PositionMode position = JsonDefaults.ToolTip.Position, Vector2? offset = null, bool useCenter = JsonDefaults.ToolTip.UseCenter)
    {
        TooltipComponent tooltip = Tooltip(component);
        tooltip.Text = text;
        tooltip.Delay = delay;
        tooltip.Position = position;
        if (offset.HasValue)
        {
            tooltip.Offset = offset.Value;
        }
        tooltip.UseCenter = useCenter;
        return tooltip;
    }
    #endregion
}