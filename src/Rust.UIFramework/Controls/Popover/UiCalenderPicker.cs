using System;
using System.Collections.Generic;
using System.Globalization;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls;

public class UiCalenderPicker : BaseUiControl
{
    public UiButton PreviousYear;
    public UiButton PreviousMonth;        
    public UiButton NextYear;
    public UiButton NextMonth;
    public List<UiButton> DateButtons;

    private DateTime _firstOfTheMonth;
    private DateTime _nextMonth;

    private int _daysInMonth;

    private const int NumColumns = 7;
    private int _numRows;
    private int _maxDays;

    private static readonly UiPadding HeaderPadding = new(2, 0);
    private static readonly UiPadding DayPadding = new(5, 3);

    private static readonly string[] DayOfWeekNames = ["Su", "M", "Tu", "W", "Th", "F", "Sa"];
        
    public static UiCalenderPicker Create(BaseUiBuilder builder, in UiReference reference, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, ICommandBuilder<DateTime> changeCommand, string buttonSprite)
    {
        UiCalenderPicker control = CreateControl<UiCalenderPicker>();
        control.CreateCalender(builder, in reference, in pos, in offset, date, fontSize, textColor, buttonColor, selectedDateColor, changeCommand, buttonSprite);
        return control;
    }
    
    private void CreateCalender(BaseUiBuilder builder, in UiReference reference, in UiPosition pos, in UiOffset offset, DateTime date, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, ICommandBuilder<DateTime> changeCommand, string buttonSprite)
    {
        CalculateDates(date);
        UiDirectionalLayout layout = builder.DirectionalLayout(reference, pos, offset, _numRows + 2, LayoutDirection.Vertical, padding: HeaderPadding);
        CreateHeader(builder, layout, date, fontSize, textColor, buttonColor, changeCommand, buttonSprite);
        CreateDayOfWeekHeader(builder, layout, fontSize, textColor);
        CreateCalender(builder, layout, fontSize, textColor, buttonColor, selectedDateColor, changeCommand, buttonSprite);
    }

    public void CalculateDates(DateTime date)
    {
        _firstOfTheMonth = new DateTime(date.Year, date.Month, 1);
        _nextMonth = _firstOfTheMonth.AddMonths(1);
        _daysInMonth = DateTime.DaysInMonth(_firstOfTheMonth.Year, _firstOfTheMonth.Month);
        _numRows = GetWeekRows(date.Year, date.Month);
        _maxDays = _numRows * NumColumns;
    }

    public void CreateHeader(BaseUiBuilder builder, BaseLayout layout, DateTime value, int fontSize, UiColor textColor, UiColor buttonColor, ICommandBuilder<DateTime> changeCommand, string buttonSprite)
    {
        UiDirectionalLayout headerLayout = builder.DirectionalLayout(layout, 7);
        
        PreviousYear = builder.IconButton(headerLayout, buttonColor, Icons.StepBackward, changeCommand.Build(value.AddYears(-1)), textColor);
        StyleButton(PreviousYear, buttonSprite);
        
        PreviousMonth = builder.IconButton(headerLayout, buttonColor, Icons.Backward, changeCommand.Build(value.AddMonths(-1)), textColor);
        StyleButton(PreviousMonth, buttonSprite);
        
        UiLabel label = builder.Label(headerLayout.Reference, FormatCache<DateTime>.ToString(_firstOfTheMonth, "MMM yyyy"), fontSize, textColor);
        headerLayout.AddElement(label, 3f);
        
        NextMonth = builder.IconButton(headerLayout, buttonColor, Icons.Forward, changeCommand.Build(value.AddYears(1)), textColor);
        StyleButton(NextMonth, buttonSprite);
        
        NextYear = builder.IconButton(headerLayout, buttonColor, Icons.StepForward, changeCommand.Build(value.AddMonths(1)), textColor);
        StyleButton(NextYear, buttonSprite);
    }

    public void CreateDayOfWeekHeader(BaseUiBuilder builder, BaseLayout layout, int fontSize, UiColor textColor)
    {
        UiDirectionalLayout weekLayout = builder.DirectionalLayout(layout, NumColumns);
        for (int i = 0; i < 7; i++)
        {
            builder.Label(weekLayout, DayOfWeekNames[i], fontSize, textColor);
        }
    }

    public void CreateCalender(BaseUiBuilder builder, BaseLayout layout, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, ICommandBuilder<DateTime> changeCommand, string buttonSprite)
    {
        UiGridLayout daysLayout = builder.GridLayout(layout, NumColumns, _numRows, padding: DayPadding);

        UiColor disabledText = textColor * UiColors.DisabledButtonMultiplier;
        
        int dayOfWeek = (int)_firstOfTheMonth.DayOfWeek;
        for (int i = 0; i < dayOfWeek; i++)
        {
            DateTime date = _firstOfTheMonth.AddDays(-dayOfWeek + i);
            UiButton button = builder.TextButton(daysLayout, StringCache<int>.ToString(date.Day), fontSize, disabledText, buttonColor, changeCommand.Build(date));
            StyleButton(button, buttonSprite);
            DateButtons.Add(button);
        }

        for (int i = 0; i < _daysInMonth; i++)
        {
            DateTime date = _firstOfTheMonth.AddDays(i);
            UiColor color = date.Date == DateTime.Now.Date ? selectedDateColor : buttonColor;
            UiButton button = builder.TextButton(daysLayout, StringCache<int>.ToString(date.Day), fontSize, textColor, color, changeCommand.Build(date));
            StyleButton(button, buttonSprite);
            DateButtons.Add(button);
        }
            
        for (int i = DateButtons.Count; i < _maxDays; i++)
        {
            DateTime date = _nextMonth.AddDays(i);
            UiButton button = builder.TextButton(daysLayout, StringCache<int>.ToString(date.Day), fontSize, disabledText, buttonColor, changeCommand.Build(date));
            StyleButton(button, buttonSprite);
            DateButtons.Add(button);
        }
    }

    public int GetWeekRows(int year, int month)
    {
        DateTime firstDayOfMonth = new(year, month, 1);
        DateTime lastDayOfMonth = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        Calendar calendar = CultureInfo.CurrentCulture.Calendar;
        int lastWeek = calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        int firstWeek = calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        return lastWeek - firstWeek + 1;
    }

    private void StyleButton(UiButton button, string buttonSprite)
    {
        button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
    }

    protected override void LeavePool()
    {
        base.LeavePool();
        DateButtons = UiFrameworkPool.GetList<UiButton>();
    }

    protected override void EnterPool()
    {
        _numRows = 0;
        _maxDays = 0;
        _daysInMonth = 0;
        PreviousYear = null;
        PreviousMonth = null;        
        NextYear = null;
        NextMonth = null;
        UiFrameworkPool.FreeList(DateButtons);
    }
}