using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiImage : IUiImage, IUiImageTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> _imageType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _sprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _fillCenter = new();

	public partial UnityEngine.UI.Image.Type ImageType { get => Image.ImageType; set => Image.ImageType = value; }
	public partial string Sprite { get => Image.Sprite; set => Image.Sprite = value; }
	public partial string Material { get => Image.Material; set => Image.Material = value; }
	public partial float FadeIn { get => Image.FadeIn; set => Image.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Image.Color; set => Image.Color = value; }
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => Image.PlaceholderFor; set => Image.PlaceholderFor = value; }
	public partial bool FillCenter { get => Image.FillCenter; set => Image.FillCenter = value; }
	IImageComponentTrackable IUiImageTrackable.Image => Image.AsTrackable();

	public IUiImageTrackable AsTrackable() => this;
	public UiImage SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiImage SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiImage SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiImage SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiImage SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
	public UiImage SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiImage SetFillCenter(bool fillCenter)
	{
		FillCenter = fillCenter;
		return this;
	}
}


