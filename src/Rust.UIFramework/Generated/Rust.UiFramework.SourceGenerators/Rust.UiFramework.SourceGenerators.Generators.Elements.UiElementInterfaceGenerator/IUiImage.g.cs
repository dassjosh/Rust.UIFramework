using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiImage : Oxide.Ext.UiFramework.Interfaces.IImageType<Oxide.Ext.UiFramework.UiElements.UiImage>, Oxide.Ext.UiFramework.Interfaces.ISprite<Oxide.Ext.UiFramework.UiElements.UiImage>, Oxide.Ext.UiFramework.Interfaces.IMaterial<Oxide.Ext.UiFramework.UiElements.UiImage>, Oxide.Ext.UiFramework.Interfaces.IFadeIn<Oxide.Ext.UiFramework.UiElements.UiImage>, Oxide.Ext.UiFramework.Interfaces.IUiColor<Oxide.Ext.UiFramework.UiElements.UiImage>, IBaseUiComponent
{
	Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get; }
	bool FillCenter { get; }

	Oxide.Ext.UiFramework.UiElements.UiImage SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor);
	Oxide.Ext.UiFramework.UiElements.UiImage SetFillCenter(bool fillCenter);
}


