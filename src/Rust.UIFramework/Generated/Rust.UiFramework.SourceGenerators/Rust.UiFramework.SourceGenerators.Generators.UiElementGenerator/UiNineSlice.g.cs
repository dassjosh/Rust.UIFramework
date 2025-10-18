using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;
public partial class UiNineSlice : IUiNineSliceTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _png = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Types.UiBorderWidth> _slice = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _fillCenter = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> _imageType = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _sprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new();

	public string Png { get => Image.Png; set => Image.Png = value; }
	public Oxide.Ext.UiFramework.Types.UiBorderWidth Slice { get => Image.Slice; set => Image.Slice = value; }
	public Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => Image.PlaceholderFor; set => Image.PlaceholderFor = value; }
	public bool FillCenter { get => Image.FillCenter; set => Image.FillCenter = value; }
	public UnityEngine.UI.Image.Type ImageType { get => Image.ImageType; set => Image.ImageType = value; }
	public string Sprite { get => Image.Sprite; set => Image.Sprite = value; }
	public string Material { get => Image.Material; set => Image.Material = value; }
	public float FadeIn { get => Image.FadeIn; set => Image.FadeIn = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => Image.Color; set => Image.Color = value; }
	INineSliceComponentTrackable IUiNineSliceTrackable.Image => Image.AsTrackable();

	public IUiNineSliceTrackable AsTrackable() => this;
	public UiNineSlice SetPng(string png)
	{
		Png = png;
		return this;
	}
	public UiNineSlice SetSlice(in Oxide.Ext.UiFramework.Types.UiBorderWidth slice)
	{
		Slice = slice;
		return this;
	}
	public UiNineSlice SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiNineSlice SetFillCenter(bool fillCenter)
	{
		FillCenter = fillCenter;
		return this;
	}
	public UiNineSlice SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiNineSlice SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiNineSlice SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiNineSlice SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiNineSlice SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


