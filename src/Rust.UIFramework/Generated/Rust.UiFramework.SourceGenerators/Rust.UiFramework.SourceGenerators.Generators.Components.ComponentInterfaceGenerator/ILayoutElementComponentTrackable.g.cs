using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ILayoutElementComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<float> PreferredWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> PreferredHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> MinWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> MinHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FlexibleWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FlexibleHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> IgnoreLayout { get; }
}


