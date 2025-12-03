using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IDirectionalLayoutComponentTrackable : IBaseLayoutComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<float> Spacing { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildForceExpandWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildForceExpandHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildControlWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildControlHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildScaleWidth { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> ChildScaleHeight { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Enums.LayoutDirection> Direction { get; }
}


