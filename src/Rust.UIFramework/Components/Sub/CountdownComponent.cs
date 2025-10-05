using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class CountdownComponent : SubComponent
{
    private readonly TrackedValue<float> _startTime = new(JsonDefaults.Countdown.StartTime);
    private readonly TrackedValue<float> _endTime = new(JsonDefaults.Countdown.EndTime);
    private readonly TrackedValue<float> _step = new(JsonDefaults.Countdown.Step);
    private readonly TrackedValue<float> _interval = new(JsonDefaults.Countdown.Interval);
    private readonly TrackedValue<TimerFormat> _timerFormat = new(JsonDefaults.Countdown.TimerFormat);
    private readonly TrackedValue<string> _numberFormat = new(JsonDefaults.Countdown.NumberFormat);
    private readonly TrackedValue<bool> _destroyIfDone = new(JsonDefaults.Countdown.DestroyIfDone);
    private readonly TrackedValue<string> _command = new();
    
    public float StartTime { get => _startTime.Value; set => _startTime.Value = value; }
    public float EndTime { get => _endTime.Value; set => _endTime.Value = value; }
    public float Step { get => _step.Value; set => _step.Value = value; }
    public float Interval { get => _interval.Value; set => _interval.Value = value; }
    public TimerFormat TimerFormat { get => _timerFormat.Value; set => _timerFormat.Value = value; }
    public string NumberFormat { get => _numberFormat.Value; set => _numberFormat.Value = value; }
    public bool DestroyIfDone { get => _destroyIfDone.Value; set => _destroyIfDone.Value = value; }
    public string Command { get => _command.Value; set => _command.Value = value; }

    public override Utf8String Type => JsonDefaults.Countdown.Type;
    public override ComponentType ComponentType => ComponentType.Countdown;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Countdown.StartTimeName, _startTime, mode);
        writer.AddField(JsonDefaults.Countdown.EndTimeName, _endTime, mode);
        writer.AddField(JsonDefaults.Countdown.StepName, _step, mode);
        writer.AddField(JsonDefaults.Countdown.IntervalName, _interval, mode);
        writer.AddField(JsonDefaults.Countdown.TimerFormatName, _timerFormat, mode);
        writer.AddField(JsonDefaults.Countdown.NumberFormatName, _numberFormat, mode);
        writer.AddField(JsonDefaults.Countdown.DestroyIfDoneName, _destroyIfDone, mode);
        writer.AddField(JsonDefaults.Countdown.CountdownCommandName, _command, mode);
    }

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
        _startTime.Reset();
        _endTime.Reset();
        _step.Reset();
        _interval.Reset();
        _timerFormat.Reset();
        _numberFormat.Reset();
        _destroyIfDone.Reset();
        _command.Reset();
    }
    
    public override bool HasChanged()
    {
        return _startTime.HasChanged ||
               _endTime.HasChanged ||
               _step.HasChanged ||
               _interval.HasChanged ||
               _timerFormat.HasChanged ||
               _numberFormat.HasChanged ||
               _destroyIfDone.HasChanged ||
               _command.HasChanged;
    }
}