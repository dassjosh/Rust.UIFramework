using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IScrollViewContentComponent
{
	Oxide.Ext.UiFramework.Positions.UiPosition Position { get; set; }
	Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get; set; }
	UnityEngine.Vector2 Pivot { get; set; }
}


