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

	Oxide.Ext.UiFramework.Components.ButtonComponent SetCommand(string command);
	Oxide.Ext.UiFramework.Components.ButtonComponent SetColor(Oxide.Ext.UiFramework.Colors.UiColor color);
	Oxide.Ext.UiFramework.Components.ButtonComponent SetFadeIn(float fadeIn);
	Oxide.Ext.UiFramework.Components.ButtonComponent SetSprite(string sprite);
	Oxide.Ext.UiFramework.Components.ButtonComponent SetMaterial(string material);
	Oxide.Ext.UiFramework.Components.ButtonComponent SetImageType(UnityEngine.UI.Image.Type imageType);
}


