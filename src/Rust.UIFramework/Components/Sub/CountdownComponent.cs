using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(CountdownComponentSerializer))]
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
    public override ComponentType ComponentType => ComponentType.Countdown;
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

    public override void Reset()
    {
        base.Reset();
        StartTime = JsonDefaults.Countdown.StartTime;
        EndTime = JsonDefaults.Countdown.EndTime;
        Step = JsonDefaults.Countdown.Step;
        Interval = JsonDefaults.Countdown.Interval;
        TimerFormat = TimerFormat.None;
        NumberFormat = JsonDefaults.Countdown.NumberFormat;
        DestroyIfDone = JsonDefaults.Countdown.DestroyIfDone;
        Command = null;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is CountdownComponent component)
        {
            StartTime = component.StartTime;
            EndTime = component.EndTime;
            Step = component.Step;
            Interval = component.Interval;
            TimerFormat = component.TimerFormat;
            NumberFormat = component.NumberFormat;
            DestroyIfDone = component.DestroyIfDone;
            Command = component.Command;
        }
    }

    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        CountdownComponent typedOther = (CountdownComponent)other!;
        return StartTime == typedOther.StartTime 
               && EndTime == typedOther.EndTime 
               && Step == typedOther.Step 
               && Interval == typedOther.Interval 
               && TimerFormat == typedOther.TimerFormat 
               && NumberFormat == typedOther.NumberFormat 
               && DestroyIfDone == typedOther.DestroyIfDone 
               && Command == typedOther.Command;
    }
}