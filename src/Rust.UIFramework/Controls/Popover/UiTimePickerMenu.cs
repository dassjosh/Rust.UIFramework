using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.Popover;

public class UiTimePickerMenu : BasePopoverControl
{
    public UiPicker Hour;
    public UiPicker Minute;
    public UiPicker Second;
    public UiPicker AmPm;
        
    public static UiTimePickerMenu Create(in UiReference parent, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, TimePickerDisplayMode displayMode = TimePickerDisplayMode.All, ClockMode clockMode = ClockMode.Hour12,
        PopoverPosition position = PopoverPosition.Bottom, string menuSprite = UiConstants.Sprites.RoundedBackground2)
    {
        const int menuPadding = 5;
        const int itemPadding = 3;

        UiTimePickerMenu control = CreateControl<UiTimePickerMenu>();
            
        int numPickers = GetPickerCount(displayMode, clockMode);

        int segmentWidth = UiHelpers.TextOffsetWidth(2, fontSize);
        int width = menuPadding * 2 + (numPickers - 2) * itemPadding + numPickers * segmentWidth;
        int height = UiHelpers.TextOffsetHeight(fontSize) * 3;

        Vector2Int size = new(width + menuPadding * 2 + 1, height + menuPadding * 2);
        CreateBuilder(control, parent.Parent, size, backgroundColor, position, menuSprite);

        UiBuilder builder = control.Builder;
            
        UiOffset offset = new(menuPadding, menuPadding, segmentWidth + menuPadding * 2, height);
        //Interface.Oxide.LogDebug(offset.ToString());
            
        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Hours))
        {
            control.Hour = CreateTimePickerTimeSegment(builder, offset, time.Hour, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Hours, clockMode, changeCommand);
            offset = offset.MoveX(segmentWidth + itemPadding);
            //Interface.Oxide.LogDebug($"Hour:{offset.ToString()}");
        }
            
        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Minutes))
        {
            control.Minute = CreateTimePickerTimeSegment(builder, offset, time.Minute, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Minutes, clockMode, changeCommand);
            offset = offset.MoveX(segmentWidth + itemPadding);
            //Interface.Oxide.LogDebug($"Minute:{offset.ToString()}");
        }
            
        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Seconds))
        {
            control.Second = CreateTimePickerTimeSegment(builder, offset, time.Second, fontSize, textColor, backgroundColor, TimePickerDisplayMode.Seconds, clockMode, changeCommand);
            offset = offset.MoveX(segmentWidth + itemPadding);
            //Interface.Oxide.LogDebug($"Second:{offset.ToString()}");
        }
            
        if (clockMode == ClockMode.Hour12)
        {
            control.AmPm = CreateTimePickerAmPmSegment(builder, offset, time.Hour, fontSize, textColor, backgroundColor, changeCommand);
        }
            
        return control;
    }

    private static int GetPickerCount(TimePickerDisplayMode displayMode, ClockMode clockMode)
    {
        int numPickers = 0;
        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Hours))
        {
            numPickers++;
        }

        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Minutes))
        {
            numPickers++;
        }

        if (HasTimePickerDisplayModeFlag(displayMode, TimePickerDisplayMode.Seconds))
        {
            numPickers++;
        }

        if (clockMode == ClockMode.Hour12)
        {
            numPickers++;
        }

        return numPickers;
    }

    public static UiPicker CreateTimePickerTimeSegment(UiBuilder builder, in UiOffset pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, TimePickerDisplayMode mode, ClockMode clockMode, string changeCommand)
    {
        float height = pos.Height / 3f;
        string timeAmount = StringCache<int>.ToString(mode == TimePickerDisplayMode.Hours ? 3600 : mode == TimePickerDisplayMode.Minutes ? 60 : 1);
        if (clockMode == ClockMode.Hour12 && mode == TimePickerDisplayMode.Hours)
        {
            if (value > 12)
            {
                value -= 12;
            }

            if (value <= 0)
            {
                value = 1;
            }
        }

        string valueText = mode == TimePickerDisplayMode.Hours ? StringCache<int>.ToString(value) : value.ToString("00");
            
        return UiPicker.Create(builder, builder.Root, pos, valueText, fontSize, textColor, backgroundColor, height, $"{changeCommand} {timeAmount}", $"{changeCommand} -{timeAmount}");
    }
        
    public static UiPicker CreateTimePickerAmPmSegment(UiBuilder builder, in UiOffset pos, int value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
    {
        float height = pos.Height / 3f;
        string command = value >= 12 ? $"{changeCommand} -43200" : $"{changeCommand} 43200";
        return UiPicker.Create(builder, builder.Root, pos, value >= 12 ? "PM" : "AM", fontSize, textColor, backgroundColor, height, command, command);
    }

    private static bool HasTimePickerDisplayModeFlag(TimePickerDisplayMode mode, TimePickerDisplayMode flag)
    {
        return (mode & flag) != 0;
    }

    protected override void EnterPool()
    {
        Hour = null;
        Minute = null;
        Second = null;
        AmPm = null;
    }
}