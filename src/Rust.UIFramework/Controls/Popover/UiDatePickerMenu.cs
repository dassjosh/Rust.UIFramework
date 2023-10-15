using System;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls.Popover
{
    public class UiDatePickerMenu : BasePopoverControl
    {
        public UiPicker Year;
        public UiPicker Month;
        public UiPicker Day;

        public const int MenuPadding = 5;
        public const int ItemPadding = 3;

        private string _yearText;
        private string _monthLabelText;
        private string _monthValueText;
        private string _dayText;

        private int _yearWidth;
        private int _monthWidth;
        private int _dayWidth;

        private int _width;
        private int _height;

        public static UiDatePickerMenu Create(UiReference parent, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order, PopoverPosition position, string menuSprite)
        {
            UiDatePickerMenu control = CreateControl<UiDatePickerMenu>();
            
            control._width = control.PopulateVariables(displayMode, date, fontSize);
            control._height = UiHelpers.TextOffsetHeight(fontSize) * 3;

            Vector2Int size = new Vector2Int(control._width + MenuPadding * 2 + 1, control._height + MenuPadding * 2);
            CreateBuilder(control, parent.Parent, size, backgroundColor, position, menuSprite);

            UiBuilder builder = control.Builder;

            control.CreatePickers(builder, date, fontSize, textColor, backgroundColor, changeCommand, displayMode, order);

            return control;
        }

        public int PopulateVariables(DatePickerDisplayMode displayMode, DateTime date, int fontSize)
        {
            int width = 0;
            if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Year))
            {
                _yearText = StringCache<int>.ToString(date.Year);
                _yearWidth = UiHelpers.TextOffsetWidth(_yearText.Length, fontSize);
                width += _yearWidth;
            }

            if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Month))
            {
                if (width != 0)
                {
                    width += ItemPadding;
                }
                
                _monthLabelText = date.ToString("MMM");
                _monthValueText = StringCache<int>.ToString(date.Month);
                _monthWidth = UiHelpers.TextOffsetWidth(_monthLabelText.Length, fontSize);
                width += _monthWidth;
            }

            if (HasDatePickerDisplayModeFlag(displayMode, DatePickerDisplayMode.Day))
            {
                if (width != 0)
                {
                    width += ItemPadding;
                }
                
                _dayText = StringCache<int>.ToString(date.Day);
                _dayWidth = UiHelpers.TextOffsetWidth(_dayText.Length, fontSize);
                width += _dayWidth;
            }

            width += MenuPadding * 2;

            return width;
        }

        public void CreatePickers(UiBuilder builder, DateTime date, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand, DatePickerDisplayMode displayMode, DatePickerDisplayOrder order)
        {
            UiOffset offset = new UiOffset(MenuPadding, MenuPadding, MenuPadding, _height);

            switch (order)
            {
                case DatePickerDisplayOrder.MonthDayYear:
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    if (CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_dayWidth + ItemPadding);
                    CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
                case DatePickerDisplayOrder.YearMonthDay:
                    if (CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_yearWidth + ItemPadding);
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
                case DatePickerDisplayOrder.DayMonthYear:
                    if (CreateDayPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_dayWidth + ItemPadding);
                    if (CreateMonthPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand)) offset.MoveX(_monthWidth + ItemPadding);
                    CreateYearPicker(builder, offset, date, fontSize, textColor, backgroundColor, changeCommand);
                    break;
            }
        }

        public bool CreateYearPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
        {
            if (_yearWidth == 0)
            {
                return false;
            }
            
            string increment = $"{changeCommand} {StringCache<int>.ToString(value.Year + 1)}/{_monthValueText}/{_dayText}";
            string decrement = $"{changeCommand} {StringCache<int>.ToString(value.Year - 1)}/{_monthValueText}/{_dayText}";
            Year = UiPicker.Create(builder, builder.Root, pos, _yearText, fontSize, textColor, backgroundColor, _height, increment, decrement);
            return true;
        }
        
        public bool CreateMonthPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
        {
            if (_monthWidth == 0)
            {
                return false;
            }
            
            string increment = $"{changeCommand} {_yearText}/{StringCache<int>.ToString(value.Month % 12 + 1)}/{_dayText}";
            string decrement = $"{changeCommand} {_yearText}/{StringCache<int>.ToString(value.Month == 1 ? 12 : value.Month - 1)}/{_dayText}";
            Month = UiPicker.Create(builder, builder.Root, pos, _monthLabelText, fontSize, textColor, backgroundColor, _height, increment, decrement);
            return true;
        }
        
        public bool CreateDayPicker(UiBuilder builder, UiOffset pos, DateTime value, int fontSize, UiColor textColor, UiColor backgroundColor, string changeCommand)
        {
            if (_dayWidth == 0)
            {
                return false;
            }
            
            int numDays = DateTime.DaysInMonth(value.Year, value.Month);
            string increment = $"{changeCommand} {_yearText}/{_monthValueText}/{StringCache<int>.ToString(value.Day % numDays + 1)}";
            string decrement = $"{changeCommand} {_yearText}/{_monthValueText}/{StringCache<int>.ToString(value.Day == 1 ? numDays : value.Day - 1)}";
            Day = UiPicker.Create(builder, builder.Root, pos, _dayText, fontSize, textColor, backgroundColor, _height, increment, decrement);
            return true;
        }

        private bool HasDatePickerDisplayModeFlag(DatePickerDisplayMode mode, DatePickerDisplayMode flag)
        {
            return (mode & flag) != 0;
        }

        protected override void EnterPool()
        {
            Year = null;
            Month = null;
            Day = null;
            _yearText = null;
            _monthLabelText = null;
            _monthValueText = null;
            _dayText = null;
            _yearWidth = 0;
            _monthWidth = 0;
            _dayWidth = 0;
            _width = 0;
            _height = 0;
        }
    }
}