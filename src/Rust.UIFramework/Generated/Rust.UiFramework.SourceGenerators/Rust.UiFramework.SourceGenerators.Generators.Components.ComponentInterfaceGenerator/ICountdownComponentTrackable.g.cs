using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ICountdownComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<float> StartTime { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> EndTime { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> Step { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> Interval { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.TimerFormat> TimerFormat { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> NumberFormat { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> DestroyIfDone { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Command { get; }
}


