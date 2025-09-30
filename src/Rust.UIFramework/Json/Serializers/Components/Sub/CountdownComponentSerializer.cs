using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class CountdownComponentSerializer : SubComponentSerializer<CountdownComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, CountdownComponent component, CountdownComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Countdown.StartTimeName, component.StartTime, defaults.StartTime);
        writer.AddField(JsonDefaults.Countdown.EndTimeName, component.EndTime, defaults.EndTime);
        writer.AddField(JsonDefaults.Countdown.StepName, component.Step, defaults.Step);
        writer.AddField(JsonDefaults.Countdown.IntervalName, component.Interval, defaults.Interval);
        writer.AddField(JsonDefaults.Countdown.TimerFormatName, component.TimerFormat, defaults.TimerFormat);
        writer.AddField(JsonDefaults.Countdown.NumberFormatName, component.NumberFormat, defaults.NumberFormat);
        writer.AddField(JsonDefaults.Countdown.DestroyIfDoneName, component.DestroyIfDone, defaults.DestroyIfDone);
        writer.AddField(JsonDefaults.Countdown.CountdownCommandName, component.Command, defaults.Command);
    }
}