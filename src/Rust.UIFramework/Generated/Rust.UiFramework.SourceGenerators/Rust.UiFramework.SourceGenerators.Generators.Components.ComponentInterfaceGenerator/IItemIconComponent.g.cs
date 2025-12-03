using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IItemIconComponent : IImageComponent
{
	int ItemId { get; set; }
	ulong SkinId { get; set; }

	Oxide.Ext.UiFramework.Components.ItemIconComponent SetItemId(int itemId);
	Oxide.Ext.UiFramework.Components.ItemIconComponent SetSkinId(ulong skinId);
}


