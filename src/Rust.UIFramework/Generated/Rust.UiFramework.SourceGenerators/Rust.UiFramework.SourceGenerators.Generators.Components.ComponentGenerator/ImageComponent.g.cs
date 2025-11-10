using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ImageComponent : IImageComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Color.ColorValue);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Common.FadeIn);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _sprite = new(null, Oxide.Ext.UiFramework.Json.JsonDefaults.BaseImage.Sprite);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new(null, Oxide.Ext.UiFramework.Json.JsonDefaults.BaseImage.Material);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> _imageType = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Image.ImageType);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> _placeholderFor = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<bool> _fillCenter = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Image.FillCenter);

	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public string Sprite { get => _sprite.Value; set => _sprite.Value = value; }
	public string Material { get => _material.Value; set => _material.Value = value; }
	public UnityEngine.UI.Image.Type ImageType { get => _imageType.Value; set => _imageType.Value = value; }
	public Oxide.Ext.UiFramework.UiElements.UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
	public bool FillCenter { get => _fillCenter.Value; set => _fillCenter.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IImageComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<float> IImageComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<string> IImageComponentTrackable.Sprite => _sprite;
	Oxide.Ext.UiFramework.Types.Tracked<string> IImageComponentTrackable.Material => _material;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> IImageComponentTrackable.ImageType => _imageType;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.UiElements.UiReference> IImageComponentTrackable.PlaceholderFor => _placeholderFor;
	Oxide.Ext.UiFramework.Types.Tracked<bool> IImageComponentTrackable.FillCenter => _fillCenter;

	public IImageComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_color.HasChanged || _fadeIn.HasChanged || _sprite.HasChanged || _material.HasChanged || _imageType.HasChanged || _placeholderFor.HasChanged || _fillCenter.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_color.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_sprite.ResetHasChanged();
		_material.ResetHasChanged();
		_imageType.ResetHasChanged();
		_placeholderFor.ResetHasChanged();
		_fillCenter.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_color.Reset();
		_fadeIn.Reset();
		_sprite.Reset();
		_material.Reset();
		_imageType.Reset();
		_placeholderFor.Reset();
		_fillCenter.Reset();
	}
}


