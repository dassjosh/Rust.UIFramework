using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IScrollViewContentComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Positions.UiPosition> Position { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Offsets.UiOffset> Offset { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> Pivot { get; }
}


