using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiItemIcon : IUiItemIcon, IUiItemIconTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<int> _itemId = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<ulong> _skinId = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> _imageType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _sprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public partial int ItemId { get => Icon.ItemId; set => Icon.ItemId = value; }
	public partial ulong SkinId { get => Icon.SkinId; set => Icon.SkinId = value; }
	public partial UnityEngine.UI.Image.Type ImageType { get => Icon.ImageType; set => Icon.ImageType = value; }
	public partial string Sprite { get => Icon.Sprite; set => Icon.Sprite = value; }
	public partial string Material { get => Icon.Material; set => Icon.Material = value; }
	public partial float FadeIn { get => Icon.FadeIn; set => Icon.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Icon.Color; set => Icon.Color = value; }
	IItemIconComponentTrackable IUiItemIconTrackable.Icon => Icon.AsTrackable();

	public IUiItemIconTrackable AsTrackable() => this;
	public UiItemIcon SetItemId(int itemId)
	{
		ItemId = itemId;
		return this;
	}
	public UiItemIcon SetSkinId(ulong skinId)
	{
		SkinId = skinId;
		return this;
	}
	public UiItemIcon SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiItemIcon SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiItemIcon SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiItemIcon SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiItemIcon SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


