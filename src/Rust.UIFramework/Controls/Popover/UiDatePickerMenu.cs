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

public class UiDatePickerMenu : BaseUiControl
{
    public UiPicker Year;
    public UiPicker Month;
    public UiPicker Day;

    public static UiDatePickerMenu Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<DateTime> changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order)
    {
        UiDatePickerMenu control = CreateControl<UiDatePickerMenu>();
        control.CreatePickers(builder, parent, pos, offset, date, fontSize, textColor, backgroundColor, changeCommand, displayMode, order);
        return control;
    }

    public void CreatePickers(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<DateTime> changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order)
    {
        UiDirectionalLayout layout = builder.DirectionalLayout(parent, pos, offset, GetPickerCount(displayMode), LayoutDirection.TopToBottom);
        
        //TODO: Localization
        switch (order)
        {
            case DatePickerDisplayOrder.MonthDayYear:
                CreateMonthPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateDayPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateYearPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                break;
            case DatePickerDisplayOrder.YearMonthDay:
                CreateYearPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateMonthPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateDayPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                break;
            case DatePickerDisplayOrder.DayMonthYear:
                CreateDayPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateMonthPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                CreateYearPicker(builder, layout, date, fontSize, textColor, backgroundColor, displayMode, changeCommand);
                break;
        }
    }

    public void CreateYearPicker(BaseUiBuilder builder, BaseLayout layout, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, DatePickerDisplayMode mode, ICommandBuilder<DateTime> changeCommand)
    {
        if (HasDatePickerDisplayModeFlag(mode, DatePickerDisplayMode.Year))
        {
            Year = CreatePicker(builder, layout, StringCache<int>.ToString(value.Year), fontSize, textColor, backgroundColor, changeCommand, value.AddYears(1), value.AddYears(-1));
        }
    }
        
    public void CreateMonthPicker(BaseUiBuilder builder, BaseLayout layout, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, DatePickerDisplayMode mode, ICommandBuilder<DateTime> changeCommand)
    {
        if (HasDatePickerDisplayModeFlag(mode, DatePickerDisplayMode.Month))
        {
            //TODO: Localization?
            Month = CreatePicker(builder, layout, FormatCache<DateTime>.ToString(value, "MMM"), fontSize, textColor, backgroundColor, changeCommand, value.AddMonths(1), value.AddMonths(-1));
        }
    }
        
    public void CreateDayPicker(BaseUiBuilder builder, BaseLayout layout, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, DatePickerDisplayMode mode, ICommandBuilder<DateTime> changeCommand)
    {
        if (HasDatePickerDisplayModeFlag(mode, DatePickerDisplayMode.Day))
        {
            Day = CreatePicker(builder, layout, StringCache<int>.ToString(value.Day), fontSize, textColor, backgroundColor, changeCommand, value.AddDays(1), value.AddDays(-1));
        }
    }

    public UiPicker CreatePicker(BaseUiBuilder builder, BaseLayout layout, string displayText, int fontSize, UiColor textColor, UiColor backgroundColor, ICommandBuilder<DateTime> changeCommand, DateTime increment, DateTime decrement)
    {
        UiDirectionalLayout pickerLayout = builder.DirectionalLayout(layout, 3, LayoutDirection.TopToBottom);
        return UiPicker.Create(builder, pickerLayout, displayText, fontSize, textColor, backgroundColor, changeCommand.Build(increment), changeCommand.Build(decrement));
    }
    
    private static int GetPickerCount(DatePickerDisplayMode mode)
    {
        int count = 0;
        if(HasDatePickerDisplayModeFlag(mode , DatePickerDisplayMode.Year))
        {
            count++;
        }
        
        if(HasDatePickerDisplayModeFlag(mode , DatePickerDisplayMode.Month))
        {
            count++;
        }
        
        if(HasDatePickerDisplayModeFlag(mode , DatePickerDisplayMode.Day))
        {
            count++;
        }
        
        return count;
    }
    
    private static bool HasDatePickerDisplayModeFlag(DatePickerDisplayMode mode, DatePickerDisplayMode flag)
    {
        return (mode & flag) != 0;
    }

    protected override void EnterPool()
    {
        Year = null;
        Month = null;
        Day = null;
    }
}