using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IOutlineComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	UnityEngine.Vector2 Distance { get; set; }
	bool UseGraphicAlpha { get; set; }

	Oxide.Ext.UiFramework.Components.OutlineComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color);
	Oxide.Ext.UiFramework.Components.OutlineComponent SetDistance(in UnityEngine.Vector2 distance);
	Oxide.Ext.UiFramework.Components.OutlineComponent SetUseGraphicAlpha(bool useGraphicAlpha);
}


