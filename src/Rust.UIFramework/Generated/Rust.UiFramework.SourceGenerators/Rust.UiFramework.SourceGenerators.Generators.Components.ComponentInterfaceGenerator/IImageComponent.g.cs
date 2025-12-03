using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IImageComponent : IBaseTypedComponent
{
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	float FadeIn { get; set; }
	string Sprite { get; set; }
	string Material { get; set; }
	UnityEngine.UI.Image.Type ImageType { get; set; }
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; set; }
	bool FillCenter { get; set; }
}


