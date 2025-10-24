using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ICountdownComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.StartTime))]
    float StartTime { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.EndTime))]
    float EndTime { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.Step))]
    float Step { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.Interval))]
    float Interval { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.TimerFormat))]
    TimerFormat TimerFormat { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.NumberFormat))]
    string NumberFormat { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Countdown), nameof(JsonDefaults.Countdown.DestroyIfDone))]
    bool DestroyIfDone { get; set; }
    
    string Command { get; set; }
}