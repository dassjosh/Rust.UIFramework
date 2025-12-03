using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface ICountdownComponent : IBaseTypedComponent
{
	float StartTime { get; set; }
	float EndTime { get; set; }
	float Step { get; set; }
	float Interval { get; set; }
	Oxide.Ext.UiFramework.Enums.TimerFormat TimerFormat { get; set; }
	string NumberFormat { get; set; }
	bool DestroyIfDone { get; set; }
	string Command { get; set; }
}


