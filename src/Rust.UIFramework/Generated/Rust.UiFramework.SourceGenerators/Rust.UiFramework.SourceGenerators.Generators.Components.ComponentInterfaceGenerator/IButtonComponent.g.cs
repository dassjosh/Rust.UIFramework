using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;
public interface IButtonComponent : IBaseTypedComponent
{
	string Command { get; set; }
	Oxide.Ext.UiFramework.Colors.UiColor Color { get; set; }
	float FadeIn { get; set; }
	string Sprite { get; set; }
	string Material { get; set; }
	UnityEngine.UI.Image.Type ImageType { get; set; }
}


