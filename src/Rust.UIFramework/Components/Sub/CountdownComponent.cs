using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ICountdownComponent))]
[GenerateBuilderMethods]
public partial class CountdownComponent : SubComponent, ICountdownComponent
{
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