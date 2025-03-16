using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls;

public class UiTimePickerMenu : BaseUiControl
{
    public UiPicker Hour;
    public UiPicker Minute;
    public UiPicker Second;
    public UiPicker AmPm;
        
    public static UiTimePickerMenu Create(BaseUiBuilder builder, in UiReference reference, in UiPosition pos, in UiOffset offset, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<TimePickerData> changeCommand, TimePickerDisplayModes displayMode = TimePickerDisplayModes.All, ClockMode clockMode = ClockMode.Hour12)
    {
        UiTimePickerMenu control = CreateControl<UiTimePickerMenu>();
        control.CreateUi(builder, reference, pos, offset, time, fontSize, textColor, backgroundColor, changeCommand, displayMode, clockMode);
        return control;
    }

    public void CreateUi(BaseUiBuilder builder, in UiReference reference, in UiPosition pos, in UiOffset offset, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<TimePickerData> changeCommand, TimePickerDisplayModes displayMode = TimePickerDisplayModes.All, ClockMode clockMode = ClockMode.Hour12)
    {
        int numPickers = GetPickerCount(displayMode, clockMode);
        UiDirectionalLayout mainLayout = builder.DirectionalLayout(reference, pos, offset, numPickers);
            
        if (displayMode.HasFlag(TimePickerDisplayModes.Hours))
        {
            Hour = CreateTimePickerTimeSegment(builder, mainLayout, time, fontSize, textColor, backgroundColor, TimePickerSegment.Hour, clockMode, changeCommand);
        }
            
        if (displayMode.HasFlag(TimePickerDisplayModes.Hours))
        {
            Minute = CreateTimePickerTimeSegment(builder, mainLayout, time, fontSize, textColor, backgroundColor, TimePickerSegment.Minute, clockMode, changeCommand);
        }
            
        if (displayMode.HasFlag(TimePickerDisplayModes.Hours))
        {
            Second = CreateTimePickerTimeSegment(builder, mainLayout, time, fontSize, textColor, backgroundColor, TimePickerSegment.Second, clockMode, changeCommand);
        }
            
        if (clockMode == ClockMode.Hour12)
        {
            AmPm = CreateTimePickerAmPmSegment(builder, mainLayout, time, fontSize, textColor, backgroundColor, changeCommand);
        }
    }

    private static int GetPickerCount(TimePickerDisplayModes displayMode, ClockMode clockMode)
    {
        int numPickers = 0;
        if (displayMode.HasFlag(TimePickerDisplayModes.Hours))
        {
            numPickers++;
        }

        if (displayMode.HasFlag(TimePickerDisplayModes.Minutes))
        {
            numPickers++;
        }

        if (displayMode.HasFlag(TimePickerDisplayModes.Seconds))
        {
            numPickers++;
        }

        if (clockMode == ClockMode.Hour12)
        {
            numPickers++;
        }

        return numPickers;
    }

    private static UiPicker CreateTimePickerTimeSegment(BaseUiBuilder builder, BaseLayout layout, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, TimePickerSegment segment, ClockMode clockMode, ICommandBuilder<TimePickerData> changeCommand)
    {
        int value = segment switch
        {
            TimePickerSegment.Hour => FormatHour(time, clockMode),
            TimePickerSegment.Minute => time.Minute,
            TimePickerSegment.Second => time.Second,
            _ => throw new ArgumentOutOfRangeException(nameof(segment), segment, null)
        };

        UiDirectionalLayout pickerLayout = builder.DirectionalLayout(layout, 3, LayoutDirection.Vertical);
        string displayedValue = segment == TimePickerSegment.Hour ? StringCache<int>.ToString(value) : FormatCache<int>.ToString(value, "00");
        return UiPicker.Create(builder, pickerLayout, displayedValue, fontSize, textColor, backgroundColor, changeCommand.Build(time.Add(segment)), changeCommand.Build(time.Subtract(segment)));
    }

    private static int FormatHour(TimePickerData time, ClockMode clockMode)
    {
        int value = time.Hour;
        if (clockMode == ClockMode.Hour12)
        {
            value %= 12;
            value += 1;
        }

        return value;
    }

    public static UiPicker CreateTimePickerAmPmSegment(BaseUiBuilder builder, BaseLayout layout, TimePickerData time, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<TimePickerData> changeCommand)
    {
        byte value = time.Hour;
        TimePickerData newTime = value >= 12 ? time with { Hour = (byte)(value - 12) } : time with { Hour = (byte)(value + 12) };
        string command = changeCommand.Build(newTime);
        return UiPicker.Create(builder, layout, value >= 12 ? "PM" : "AM", fontSize, textColor, backgroundColor, command, command);
    }

    protected override void EnterPool()
    {
        Hour = null;
        Minute = null;
        Second = null;
        AmPm = null;
    }
}