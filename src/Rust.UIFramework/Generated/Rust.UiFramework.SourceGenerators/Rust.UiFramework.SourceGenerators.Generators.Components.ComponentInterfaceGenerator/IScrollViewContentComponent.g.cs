using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IScrollViewContentComponent
{
	Oxide.Ext.UiFramework.Positions.UiPosition Position { get; set; }
	Oxide.Ext.UiFramework.Offsets.UiOffset Offset { get; set; }
	UnityEngine.Vector2 Pivot { get; set; }

	Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetPosition(in Oxide.Ext.UiFramework.Positions.UiPosition position);
	Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetOffset(in Oxide.Ext.UiFramework.Offsets.UiOffset offset);
	Oxide.Ext.UiFramework.Components.ScrollViewContentComponent SetPivot(in UnityEngine.Vector2 pivot);
}


