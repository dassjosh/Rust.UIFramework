using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IButtonComponentTrackable : IBaseTypedComponentTrackable
{
	Oxide.Ext.UiFramework.Types.Tracked<string> Command { get; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> Color { get; }
	Oxide.Ext.UiFramework.Types.Tracked<float> FadeIn { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Sprite { get; }
	Oxide.Ext.UiFramework.Types.Tracked<string> Material { get; }
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> ImageType { get; }
}


