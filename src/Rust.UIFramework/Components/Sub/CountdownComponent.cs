using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class CountdownComponent : SubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.StartTime))]
    public partial float StartTime { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.EndTime))]
    public partial float EndTime { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.Step))]
    public partial float Step { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.Interval))]
    public partial float Interval { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.TimerFormat))]
    public partial TimerFormat TimerFormat { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.NumberFormat))]
    public partial string NumberFormat { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.DestroyIfDone))]
    public partial bool DestroyIfDone { get; set; }
    
    public partial string Command { get; set; }
    
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
}