using System;

namespace Oxide.Ext.UiFramework.Enums;

[Flags]
public enum TimePickerDisplayMode : byte
{
    Hours = 1 << 0,
    Minutes = 1 << 1,
    Seconds = 1 << 2,
    HoursMinutes = Hours | Minutes,
    All = Hours | Minutes | Seconds
}