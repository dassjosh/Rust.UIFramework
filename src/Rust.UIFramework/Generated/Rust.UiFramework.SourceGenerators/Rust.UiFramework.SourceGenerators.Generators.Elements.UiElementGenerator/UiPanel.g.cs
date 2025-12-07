using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public partial class UiPanel : IUiPanel, IUiPanelTrackable
{
	public partial Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => Image.PlaceholderFor; set => Image.PlaceholderFor = value; }
	public partial bool FillCenter { get => Image.FillCenter; set => Image.FillCenter = value; }
	public partial UnityEngine.UI.Image.Type ImageType { get => Image.ImageType; set => Image.ImageType = value; }
	public partial string Sprite { get => Image.Sprite; set => Image.Sprite = value; }
	public partial string Material { get => Image.Material; set => Image.Material = value; }
	public partial float FadeIn { get => Image.FadeIn; set => Image.FadeIn = value; }
	public partial Oxide.Ext.UiFramework.Colors.UiColor Color { get => Image.Color; set => Image.Color = value; }
	IImageComponentTrackable IUiPanelTrackable.Image => Image.AsTrackable();

	public IUiPanelTrackable AsTrackable() => this;
	public UiPanel SetPlaceholderFor(in Oxide.Ext.UiFramework.UiElements.UiReference placeholderFor)
	{
		PlaceholderFor = placeholderFor;
		return this;
	}
	public UiPanel SetFillCenter(bool fillCenter)
	{
		FillCenter = fillCenter;
		return this;
	}
	public UiPanel SetImageType(UnityEngine.UI.Image.Type imageType)
	{
		ImageType = imageType;
		return this;
	}
	public UiPanel SetSprite(string sprite)
	{
		Sprite = sprite;
		return this;
	}
	public UiPanel SetMaterial(string material)
	{
		Material = material;
		return this;
	}
	public UiPanel SetFadeIn(float fadeIn)
	{
		FadeIn = fadeIn;
		return this;
	}
	public UiPanel SetColor(Oxide.Ext.UiFramework.Colors.UiColor color)
	{
		Color = color;
		return this;
	}
}


