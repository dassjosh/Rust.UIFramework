using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region Outline
    public OutlineComponent Outline(BaseUiComponent component, UiColor color, Vector2? distance = null, bool useGraphicAlpha = false)
    {
       return component.AddOutline(color, distance, useGraphicAlpha);
    }
    #endregion
    
    #region Countdown
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
        return component.AddDraggable(limitToParent, maxDistance, allowSwapping, dropAnywhere, dragAlpha, parentLimitIndex, filter, parentPadding, anchorOffset, keepOnTop, positionRpc, moveToAnchor, rebuildAnchor);
    }

    public SlotComponent Slot(BaseUiComponent component, string filter = null)
    {
        return component.AddSlot(filter);
    }
    #endregion
}