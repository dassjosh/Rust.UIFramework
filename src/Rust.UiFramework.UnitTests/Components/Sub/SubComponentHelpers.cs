using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Components.Sub;

public static class SubComponentHelpers
{
    public static void PopulatedCountdown(CountdownComponent countdown)
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
    
    public static void PopulatedDraggable(DraggableComponent draggable)
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

    public static void PopulatedOutline(OutlineComponent outline)
    {
        outline.Color = UiColors.Red;
        outline.Distance = new Vector2(0.5f, 0.4f);
        outline.UseGraphicAlpha = true;
    }
    
    public static void PopulatedSlot(SlotComponent slot)
    {
        slot.Filter = "filter";
    }
}