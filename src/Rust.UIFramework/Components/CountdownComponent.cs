using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public class CountdownComponent : BasePoolable, IComponent
{
    private const string Type = "Countdown";
        
    public float StartTime;
    public float EndTime;
    public float Step;
    public float Interval;
    public TimerFormat TimerFormat;
    public string NumberFormat;
    public bool DestroyIfDone = true;
    public string Command;

    public virtual void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
        writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
        writer.AddField(JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
        writer.AddField(JsonDefaults.Countdown.IntervalName, Interval, JsonDefaults.Countdown.IntervalValue);
        writer.AddField(JsonDefaults.Countdown.TimerFormatName, TimerFormat);
        writer.AddField(JsonDefaults.Countdown.NumberFormatName, NumberFormat, JsonDefaults.Countdown.NumberFormatValue);
        writer.AddField(JsonDefaults.Countdown.DestroyIfDoneName, DestroyIfDone, true);
        writer.AddField(JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
        writer.WriteEndObject();
    }

    public virtual void Reset()
    {
        StartTime = JsonDefaults.Countdown.StartTimeValue;
        EndTime = JsonDefaults.Countdown.EndTimeValue;
        Step = JsonDefaults.Countdown.StepValue;
        Interval = JsonDefaults.Countdown.IntervalValue;
        TimerFormat = TimerFormat.None;
        NumberFormat = JsonDefaults.Countdown.NumberFormatValue;
        DestroyIfDone = true;
        Command = null;
    }

    protected override void EnterPool()
    {
        Reset();
    }
}