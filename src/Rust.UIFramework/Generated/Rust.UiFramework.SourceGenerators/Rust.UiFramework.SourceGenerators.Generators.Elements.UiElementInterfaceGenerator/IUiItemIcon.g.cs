using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiItemIcon : Oxide.Ext.UiFramework.Interfaces.IMaterial<Oxide.Ext.UiFramework.UiElements.UiItemIcon>, Oxide.Ext.UiFramework.Interfaces.IFadeIn<Oxide.Ext.UiFramework.UiElements.UiItemIcon>, Oxide.Ext.UiFramework.Interfaces.IUiColor<Oxide.Ext.UiFramework.UiElements.UiItemIcon>, Oxide.Ext.UiFramework.Interfaces.IImageType<Oxide.Ext.UiFramework.UiElements.UiItemIcon>, IBaseUiComponent
{
	int ItemId { get; }
	ulong SkinId { get; }
	string Sprite { get; }

	Oxide.Ext.UiFramework.UiElements.UiItemIcon SetItemId(int itemId);
	Oxide.Ext.UiFramework.UiElements.UiItemIcon SetSkinId(ulong skinId);
	Oxide.Ext.UiFramework.UiElements.UiItemIcon SetSprite(string sprite);
}


