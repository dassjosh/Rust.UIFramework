using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class CountdownComponent : ICountdownComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _startTime = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.StartTime);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _endTime = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.EndTime);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _step = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.Step);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _interval = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.Interval);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.TimerFormat> _timerFormat = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.TimerFormat);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _numberFormat = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.NumberFormat);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _destroyIfDone = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Countdown.DestroyIfDone);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _command = new();

	public float StartTime { get => _startTime.Value; set => _startTime.Value = value; }
	public float EndTime { get => _endTime.Value; set => _endTime.Value = value; }
	public float Step { get => _step.Value; set => _step.Value = value; }
	public float Interval { get => _interval.Value; set => _interval.Value = value; }
	public Oxide.Ext.UiFramework.Enums.TimerFormat TimerFormat { get => _timerFormat.Value; set => _timerFormat.Value = value; }
	public string NumberFormat { get => _numberFormat.Value; set => _numberFormat.Value = value; }
	public bool DestroyIfDone { get => _destroyIfDone.Value; set => _destroyIfDone.Value = value; }
	public string Command { get => _command.Value; set => _command.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<float> ICountdownComponentTrackable.StartTime => _startTime;
	Oxide.Ext.UiFramework.Types.Tracked<float> ICountdownComponentTrackable.EndTime => _endTime;
	Oxide.Ext.UiFramework.Types.Tracked<float> ICountdownComponentTrackable.Step => _step;
	Oxide.Ext.UiFramework.Types.Tracked<float> ICountdownComponentTrackable.Interval => _interval;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.TimerFormat> ICountdownComponentTrackable.TimerFormat => _timerFormat;
	Oxide.Ext.UiFramework.Types.Tracked<string> ICountdownComponentTrackable.NumberFormat => _numberFormat;
	Oxide.Ext.UiFramework.Types.Tracked<bool> ICountdownComponentTrackable.DestroyIfDone => _destroyIfDone;
	Oxide.Ext.UiFramework.Types.Tracked<string> ICountdownComponentTrackable.Command => _command;

	public Oxide.Ext.UiFramework.Components.CountdownComponent SetStartTime(float startTime)
	{
		StartTime = startTime;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetEndTime(float endTime)
	{
		EndTime = endTime;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetStep(float step)
	{
		Step = step;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetInterval(float interval)
	{
		Interval = interval;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetTimerFormat(Oxide.Ext.UiFramework.Enums.TimerFormat timerFormat)
	{
		TimerFormat = timerFormat;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetNumberFormat(string numberFormat)
	{
		NumberFormat = numberFormat;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetDestroyIfDone(bool destroyIfDone)
	{
		DestroyIfDone = destroyIfDone;
		return this;
	}
	public Oxide.Ext.UiFramework.Components.CountdownComponent SetCommand(string command)
	{
		Command = command;
		return this;
	}
	public ICountdownComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_startTime.HasChanged || _endTime.HasChanged || _step.HasChanged || _interval.HasChanged || _timerFormat.HasChanged || _numberFormat.HasChanged || _destroyIfDone.HasChanged || _command.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_startTime.ResetHasChanged();
		_endTime.ResetHasChanged();
		_step.ResetHasChanged();
		_interval.ResetHasChanged();
		_timerFormat.ResetHasChanged();
		_numberFormat.ResetHasChanged();
		_destroyIfDone.ResetHasChanged();
		_command.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_startTime.Reset();
		_endTime.Reset();
		_step.Reset();
		_interval.Reset();
		_timerFormat.Reset();
		_numberFormat.Reset();
		_destroyIfDone.Reset();
		_command.Reset();
	}
}


