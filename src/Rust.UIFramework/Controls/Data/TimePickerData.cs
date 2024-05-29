using System;

namespace Oxide.Ext.UiFramework.Controls.Data;

public struct TimePickerData
{
    public byte Hour;
    public byte Minute;
    public byte Second;

    public TimePickerData(DateTime time)
    {
        Hour = (byte)time.Hour;
        Minute = (byte)time.Minute;
        Second = (byte)time.Second;
    }
        
    public TimePickerData(int hour, int minute, int second)
    {
        Hour = (byte)hour;
        Minute = (byte)minute;
        Second = (byte)second;
    }
        
    public void Update(int seconds)
    {
        int abs = Math.Abs(seconds);
        if (abs == 1)
        {
            Second += (byte)seconds;
        } 
        else if (abs == 60)
        {
            Minute += (byte)(seconds / 60);
        }
        else
        {
            Hour += (byte)(seconds / 3600);
        }
    }

    public DateTime AsDateTime()
    {
        DateTime now = DateTime.Now;
        return new DateTime(now.Year, now.Month, now.Day, Hour, Minute, Second);
    }
        
    public DateTime AsDateTime(DateTime time)
    {
        return new DateTime(time.Year, time.Month, time.Day, Hour, Minute, Second);
    }
}