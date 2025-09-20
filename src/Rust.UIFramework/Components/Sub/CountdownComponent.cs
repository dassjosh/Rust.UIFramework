using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class CountdownComponent : SubComponent
{
    public float StartTime;
    public float EndTime;
    public float Step;
    public float Interval;
    public TimerFormat TimerFormat;
    public string NumberFormat;
    public bool DestroyIfDone;
    public string Command;

    public override Utf8String Type => JsonDefaults.Countdown.Type;
    public override bool AllowMultiple => false;

    public CountdownComponent SetStartTime(float startTime)
    {
        StartTime = startTime;
        return this;
    }

    public CountdownComponent SetEndTime(float endTime)
    {
        EndTime = endTime;
        return this;
    }

    public CountdownComponent SetStep(float step)
    {
        Step = step;
        return this;
    }

    public CountdownComponent SetInterval(float interval)
    {
        Interval = interval;
        return this;
    }

    public CountdownComponent SetTimerFormat(TimerFormat timerFormat)
    {
        TimerFormat = timerFormat;
        return this;
    }

    public CountdownComponent SetNumberFormat(string numberFormat)
    {
        NumberFormat = numberFormat;
        return this;
    }

    public CountdownComponent SetDestroyIfDone(bool destroyIfDone)
    {
        DestroyIfDone = destroyIfDone;
        return this;
    }
    
    public CountdownComponent SetCommand(string command)
    {
        Command = command;
        return this;
    }

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
        writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
        writer.AddField(JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
        writer.AddField(JsonDefaults.Countdown.IntervalName, Interval, JsonDefaults.Countdown.IntervalValue);
        writer.AddField(JsonDefaults.Countdown.TimerFormatName, TimerFormat, JsonDefaults.Countdown.TimeFormatValue);
        writer.AddField(JsonDefaults.Countdown.NumberFormatName, NumberFormat, JsonDefaults.Countdown.NumberFormatValue);
        writer.AddField(JsonDefaults.Countdown.DestroyIfDoneName, DestroyIfDone, JsonDefaults.Countdown.DestroyIfDone);
        writer.AddField(JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
    }

    public override void Reset()
    {
        base.Reset();
        StartTime = JsonDefaults.Countdown.StartTimeValue;
        EndTime = JsonDefaults.Countdown.EndTimeValue;
        Step = JsonDefaults.Countdown.StepValue;
        Interval = JsonDefaults.Countdown.IntervalValue;
        TimerFormat = TimerFormat.None;
        NumberFormat = JsonDefaults.Countdown.NumberFormatValue;
        DestroyIfDone = JsonDefaults.Countdown.DestroyIfDone;
        Command = null;
    }
}