using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IScrollViewComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.ScrollRect.MovementType> MovementType { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> Elasticity { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> Inertia { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> DecelerationRate { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> ScrollSensitivity { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> HorizontalScrollProgress { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> VerticalScrollProgress { get; }
}


