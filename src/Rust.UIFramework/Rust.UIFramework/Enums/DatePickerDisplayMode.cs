using System;

namespace Oxide.Ext.UiFramework.Enums
{
    [Flags]
    public enum DatePickerDisplayMode : byte
    {
        Day = 1 << 0,
        Month = 1 << 1,
        Year = 1 << 2,
        All = Day | Month | Year
    }
}