using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IRawImageComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	float FadeIn { get; set; }
	string Image { get; set; }
	string Material { get; set; }
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; set; }
}


