using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IOutlineComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	UnityEngine.Vector2 Distance { get; set; }
	bool UseGraphicAlpha { get; set; }
}


