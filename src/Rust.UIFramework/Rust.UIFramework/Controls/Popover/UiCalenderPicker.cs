using System;
using System.Collections.Generic;
using System.Globalization;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    public class UiCalenderPicker : BasePopoverControl
    {
        public UiButton PreviousYear;
        public UiButton PreviousMonth;        
        public UiButton NextYear;
        public UiButton NextMonth;
        public List<UiButton> DateButtons;

        private const int MenuPadding = 4;
        private const int ItemPadding = 2;
        private const int HorizontalButtonPadding = 5;
        private const int VerticalButtonPadding = 3;

        private int _width;
        private int _height;

        private DateTime _firstOfTheMonth;
        private DateTime _previousYear;
        private DateTime _previousMonth;
        private DateTime _nextMonth;
        private DateTime _nextYear;

        private int _daysInMonth;

        private const int NumColumns = 7;
        private int _numRows;
        private int _maxDays;

        private int _textHeight;
        private int _textWidth1;
        private int _textWidth2;
        private int _textWidth3;
        
        private string _yearText;
        private string _monthLabelText;

        private static readonly string[] DayOfWeekNames = { "Su", "M", "Tu", "W", "Th", "F", "Sa" };
        
        public static UiCalenderPicker Create(string parentName, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, PopoverPosition position, string menuSprite, string buttonSprite)
        {
            UiCalenderPicker control = CreateControl<UiCalenderPicker>();
            
            //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(Create)} Num Rows: {control.NumRows} Max Days: {control.MaxDays}");

            control.CalculateDates(date);
            control.CalculateSize(fontSize);
            
            CreateBuilder(control, parentName, new Vector2Int(control._width + MenuPadding * 2, control._height + MenuPadding * 2), backgroundColor, position, menuSprite);
            UiBuilder builder = control.Builder;
            
            control.CreateHeader(builder, fontSize, textColor, buttonColor, changeCommand, buttonSprite);
            control.CreateDayOfWeekHeader(builder, fontSize, textColor);
            control.CreateCalender(builder, fontSize, textColor, buttonColor, selectedDateColor, changeCommand, buttonSprite);
            return control;
        }

        public void CalculateDates(DateTime date)
        {
            _firstOfTheMonth = new DateTime(date.Year, date.Month, 1);
            _previousYear = _firstOfTheMonth.AddYears(-1);
            _previousMonth = _firstOfTheMonth.AddMonths(-1);
            _nextMonth = _firstOfTheMonth.AddMonths(1);
            _nextYear = _firstOfTheMonth.AddYears(1);
            _daysInMonth = DateTime.DaysInMonth(_firstOfTheMonth.Year, _firstOfTheMonth.Month);
            _yearText = StringCache<int>.ToString(_firstOfTheMonth.Year);
            _monthLabelText = StringCache<DateTime>.ToString(_firstOfTheMonth, "MMM");
            _numRows = GetWeekRows(date.Year, date.Month);
            _maxDays = _numRows * NumColumns;
        }

        public void CalculateSize(int fontSize)
        {
            _textHeight = UiHelpers.TextOffsetHeight(fontSize, VerticalButtonPadding);
            _textWidth1 = UiHelpers.TextOffsetWidth(1, fontSize, HorizontalButtonPadding);
            _textWidth2 = UiHelpers.TextOffsetWidth(2, fontSize, HorizontalButtonPadding);
            _textWidth3 = UiHelpers.TextOffsetWidth(3, fontSize, HorizontalButtonPadding);
            
            _width = NumColumns * _textWidth2 + ItemPadding * (NumColumns - 1);
            _height = (_numRows + 2) * _textHeight + ItemPadding * (_numRows + 1);
            //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(CalculateSize)} {Width} x {Height}");
        }

        public void CreateHeader(UiBuilder builder, int fontSize, UiColor textColor, UiColor buttonColor, string changeCommand, string buttonSprite)
        {
            UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, _textWidth3,  -MenuPadding);
            //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(CreateHeader)} {pos.ToString()}");
            PreviousYear = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, "<<<", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_previousYear)}");
            PreviousYear.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
            pos = pos.MoveXPadded(ItemPadding);
            
            pos = pos.SetWidth(_textWidth1);
            PreviousMonth = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, "<", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_previousMonth)}");
            PreviousMonth.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);

            float width = UiHelpers.TextOffsetWidth(_monthLabelText.Length + 5, fontSize, HorizontalButtonPadding);
            pos = new UiOffset(-width / 2, -MenuPadding - _textHeight, width / 2, -MenuPadding);
            builder.Label(builder.Root, UiPosition.TopMiddle, pos, $"{_monthLabelText} {_yearText}", fontSize, textColor);

            pos = new UiOffset(-MenuPadding - _textWidth3, -MenuPadding - _textHeight, -MenuPadding, -MenuPadding);
            NextMonth = builder.TextButton(builder.Root, UiPosition.TopRight, pos, ">>>", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_nextYear)}");
            NextMonth.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
            pos = pos.MoveXPadded(-ItemPadding);
            
            pos = pos.SetX(pos.Max.x - _textWidth1, pos.Max.x);
            NextYear = builder.TextButton(builder.Root, UiPosition.TopRight, pos, ">", fontSize, textColor, buttonColor, $"{changeCommand} {GetCommandArg(_nextMonth)}");
            NextYear.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
        }

        public void CreateDayOfWeekHeader(UiBuilder builder, int fontSize, UiColor textColor)
        {
            UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, MenuPadding + _textWidth2,  -MenuPadding);
            pos = pos.MoveYPadded(-ItemPadding);
            
            for (int i = 0; i < 7; i++)
            {
                builder.Label(builder.Root, UiPosition.TopLeft, pos, DayOfWeekNames[i], fontSize, textColor);
                pos = pos.MoveXPadded(ItemPadding);
            }
        }

        public void CreateCalender(UiBuilder builder, int fontSize, UiColor textColor, UiColor buttonColor, UiColor selectedDateColor, string changeCommand, string buttonSprite)
        {
            UiOffset pos = new UiOffset(MenuPadding, -MenuPadding - _textHeight, MenuPadding + _textWidth2,  -MenuPadding);
            pos = pos.MoveYPadded(-ItemPadding);
            pos = pos.MoveYPadded(-ItemPadding);
            
            int offset = 0;
            int dayOfWeek = (int)_firstOfTheMonth.DayOfWeek;
            for (int i = 0; i < dayOfWeek; i++)
            {
                DateTime date = _firstOfTheMonth.AddDays(-dayOfWeek + i);
                UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor.MultiplyAlpha(0.5f), buttonColor, $"{changeCommand} {GetCommandArg(date)}");
                button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                DateButtons.Add(button);
                pos = pos.MoveXPadded(ItemPadding);
                offset++;
            }

            for (int i = 0; i < _daysInMonth; i++)
            {
                if (offset != 0 && offset % 7 == 0)
                {
                    pos = pos.SetX(MenuPadding, MenuPadding + _textWidth2);
                    pos = pos.MoveYPadded(-ItemPadding);
                }
                
                DateTime date = _firstOfTheMonth.AddDays(i);
                UiColor color = date.Date == DateTime.Now.Date ? selectedDateColor : buttonColor;

                UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor, color, $"{changeCommand} {GetCommandArg(date)}");
                button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                DateButtons.Add(button);
                pos = pos.MoveXPadded(ItemPadding);
                offset++;
            }
            
            for (int i = 0; offset < _maxDays; i++)
            {
                if (offset != 0 && offset % 7 == 0)
                {
                    pos = pos.SetX(MenuPadding, MenuPadding + _textWidth2);
                    pos = pos.MoveYPadded(-ItemPadding);
                }
                
                DateTime date = _nextMonth.AddDays(i);
                UiButton button = builder.TextButton(builder.Root, UiPosition.TopLeft, pos, StringCache<int>.ToString(date.Day), fontSize, textColor.MultiplyAlpha(0.5f), buttonColor, $"{changeCommand} {GetCommandArg(date)}");
                button.SetSpriteMaterialImage(buttonSprite, null, Image.Type.Sliced);
                DateButtons.Add(button);
                pos = pos.MoveXPadded(ItemPadding);
                offset++;
            }
        }

        public int GetWeekRows(int year, int month)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;
            int lastWeek = calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int firstWeek = calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            //Interface.Oxide.LogDebug($"{nameof(UiCalenderPicker)}.{nameof(GetWeekRows)} First: {firstDayOfMonth} {firstWeek} Last: {lastDayOfMonth} {lastWeek}");
            return lastWeek - firstWeek + 1;
        }
        
        public string GetCommandArg(DateTime date)
        {
            return $"{StringCache<int>.ToString(date.Year)}/{StringCache<int>.ToString(date.Month)}/{StringCache<int>.ToString(date.Day)}";
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            DateButtons = UiFrameworkPool.GetList<UiButton>();
        }

        protected override void EnterPool()
        {
            _yearText = null;
            _monthLabelText = null;
            _width = 0;
            _height = 0;
            _numRows = 0;
            _maxDays = 0;

            _textHeight = 0;
            _textWidth1 = 0;
            _textWidth2 = 0;
            _textWidth3 = 0;
            _daysInMonth = 0;
            
            PreviousYear = null;
            PreviousMonth = null;        
            NextYear = null;
            NextMonth = null;
            UiFrameworkPool.FreeList(DateButtons);
        }
    }
}