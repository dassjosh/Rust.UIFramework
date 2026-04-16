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
        writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTimeTracked, mode);
        writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTimeTracked, mode);
        writer.AddField(JsonDefaults.Countdown.StepName, StepTracked, mode);
        writer.AddField(JsonDefaults.Countdown.IntervalName, IntervalTracked, mode);
        writer.AddField(JsonDefaults.Countdown.TimerFormatName, TimerFormatTracked, mode);
        writer.AddField(JsonDefaults.Countdown.NumberFormatName, NumberFormatTracked, mode);
        writer.AddField(JsonDefaults.Countdown.DestroyIfDoneName, DestroyIfDoneTracked, mode);
        writer.AddField(JsonDefaults.Countdown.CountdownCommandName, CommandTracked, mode);
    }
}