using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IRawImageComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	float FadeIn { get; set; }
	string Image { get; set; }
	string Material { get; set; }
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; set; }

	Oxide.Ext.UiFramework.Components.RawImageComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color);
	Oxide.Ext.UiFramework.Components.RawImageComponent SetFadeIn(float fadeIn);
	Oxide.Ext.UiFramework.Components.RawImageComponent SetImage(string image);
	Oxide.Ext.UiFramework.Components.RawImageComponent SetMaterial(string material);
	Oxide.Ext.UiFramework.Components.RawImageComponent SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor);
}


