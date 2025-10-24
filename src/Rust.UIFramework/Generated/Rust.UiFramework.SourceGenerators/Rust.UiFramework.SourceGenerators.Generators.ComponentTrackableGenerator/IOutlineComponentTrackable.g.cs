using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IOutlineComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> Color { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.Vector2> Distance { get; }
	Oxide.Ext.UiFramework.Types.Tracked<bool> UseGraphicAlpha { get; }
}


