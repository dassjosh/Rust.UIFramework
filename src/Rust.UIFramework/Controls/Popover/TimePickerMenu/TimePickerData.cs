using System;

namespace Oxide.Ext.UiFramework.Controls;

public readonly record struct TimePickerData(byte Hour, byte Minute, byte Second)
{
    public TimePickerData(DateTime time) : this(time.Hour, time.Minute, time.Second) { }
    public TimePickerData(TimeSpan time) : this(time.Hours, time.Minutes, time.Seconds) { }

    public TimePickerData(int hour, int minute, int second) : this(SafeAdjustHour(hour), SafeAdjust(minute), SafeAdjust(second)) { }
    
    internal TimePickerData Add(TimePickerSegment mode)
    {
        return mode switch
        {
            TimePickerSegment.Hour => this with { Hour = SafeAdjustHour(Hour + 1) },
            TimePickerSegment.Minute => this with { Minute = SafeAdjust(Minute + 1) },
            TimePickerSegment.Second => this with { Second = SafeAdjust(Second + 1) },
            _ => this
        };
    }
    
    internal TimePickerData Subtract(TimePickerSegment mode)
    {
        return mode switch
        {
            TimePickerSegment.Hour => this with { Hour = SafeAdjustHour(Hour - 1) },
            TimePickerSegment.Minute => this with { Minute = SafeAdjust(Minute - 1) },
            TimePickerSegment.Second => this with { Second = SafeAdjust(Second - 1) },
            _ => this
        };
    }
    
    private static byte SafeAdjustHour(int value) => SafeAdjust(value, 0, 23);
    
    private static byte SafeAdjust(int value) => SafeAdjust(value, 0, 59);

    private static byte SafeAdjust(int value, byte min, byte max)
    {
        if(value < min) return max;
        if(value > max) return min;
        return (byte)value;
    }

    public DateTime AsDateTime() => AsDateTime(DateTime.Now);

    public DateTime AsDateTime(DateTime time) => new(time.Year, time.Month, time.Day, Hour, Minute, Second);

    public TimeSpan AsTimeSpan() => new(Hour, Minute, Second);
}