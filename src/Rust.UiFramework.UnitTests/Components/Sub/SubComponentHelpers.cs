using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.Components.Sub;

public static class SubComponentHelpers
{
    public static void PopulateCountdown(CountdownComponent countdown)
    {
        countdown.StartTime = 60;
        countdown.EndTime = 10;
        countdown.Step = 2;
        countdown.Interval = 5;
        countdown.TimerFormat = TimerFormat.HoursMinutes;
        countdown.NumberFormat = "##:##";
        countdown.DestroyIfDone = false;
        countdown.Command = "command";
    }
    
    public static void PopulateDraggable(DraggableComponent draggable)
    {
        draggable.LimitToParent = true;
        draggable.MaxDistance = 10;
        draggable.AllowSwapping = true;
        draggable.DropAnywhere = true;
        draggable.DragAlpha = 0.5f;
        draggable.ParentLimitIndex = 2;
        draggable.Filter = "filter";
        draggable.ParentPadding = new Vector2(0.5f, 0.4f);
        draggable.AnchorOffset = new Vector2(0.4f, 0.5f);
        draggable.KeepOnTop = true;
        draggable.PositionRpc = DraggablePositionSendType.Relative;
        draggable.MoveToAnchor = true;
        draggable.RebuildAnchor = true;
    }

    public static void PopulateOutline(OutlineComponent outline)
    {
        outline.Color = UiColors.Red;
        outline.Distance = new Vector2(0.5f, 0.4f);
        outline.UseGraphicAlpha = true;
    }
    
    public static void PopulateSlot(SlotComponent slot)
    {
        slot.Filter = "filter";
    }
    
    public static void PopulateContentSizeFitter(ContentSizeFitterComponent contentSizeFitter)
    {
        contentSizeFitter.HorizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.VerticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public static void PopulateGridLayout(GridLayoutComponent layout)
    {
        layout.CellSize = new Vector2(100, 200);
        layout.Spacing = new Vector2(40, 50);
        layout.StartCorner = GridLayoutGroup.Corner.LowerRight;
        layout.StartAxis = GridLayoutGroup.Axis.Vertical;
        layout.Constraint = GridLayoutGroup.Constraint.FixedRowCount;
        layout.ConstraintCount = 2;
    }

    public static void PopulateDirectionalLayout(BaseDirectionalLayoutComponent layout)
    {
        layout.Spacing = 10;
        layout.ChildForceExpandWidth = false;
        layout.ChildForceExpandHeight = false;
        layout.ChildControlWidth = true;
        layout.ChildControlHeight = true;
        layout.ChildScaleWidth = true;
        layout.ChildScaleHeight = true;
    }

    public static void PopulateLayoutElement(LayoutElementComponent layout)
    {
        layout.PreferredWidth = 10;
        layout.PreferredHeight = 20;
        layout.MinWidth = 30;
        layout.MinHeight = 40;
        layout.FlexibleWidth = 50;
        layout.FlexibleHeight = 60;
        layout.IgnoreLayout = true;
    }
}