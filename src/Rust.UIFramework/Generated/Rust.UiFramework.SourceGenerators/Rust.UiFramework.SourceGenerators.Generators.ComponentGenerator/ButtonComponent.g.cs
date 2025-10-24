using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Components;
public partial class ButtonComponent : IButtonComponentTrackable
{
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _command = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> _color = new(Oxide.Ext.UiFramework.Json.JsonDefaults.Color.ColorValue);
	private readonly Oxide.Ext.UiFramework.Types.Tracked<float> _fadeIn = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _sprite = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<string> _material = new();
	private readonly Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> _imageType = new();

	public string Command { get => _command.Value; set => _command.Value = value; }
	public Oxide.Ext.UiFramework.Colors.UiColor Color { get => _color.Value; set => _color.Value = value; }
	public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
	public string Sprite { get => _sprite.Value; set => _sprite.Value = value; }
	public string Material { get => _material.Value; set => _material.Value = value; }
	public UnityEngine.UI.Image.Type ImageType { get => _imageType.Value; set => _imageType.Value = value; }
	Oxide.Ext.UiFramework.Types.Tracked<string> IButtonComponentTrackable.Command => _command;
	Oxide.Ext.UiFramework.Types.Tracked<Oxide.Ext.UiFramework.Colors.UiColor> IButtonComponentTrackable.Color => _color;
	Oxide.Ext.UiFramework.Types.Tracked<float> IButtonComponentTrackable.FadeIn => _fadeIn;
	Oxide.Ext.UiFramework.Types.Tracked<string> IButtonComponentTrackable.Sprite => _sprite;
	Oxide.Ext.UiFramework.Types.Tracked<string> IButtonComponentTrackable.Material => _material;
	Oxide.Ext.UiFramework.Types.Tracked<UnityEngine.UI.Image.Type> IButtonComponentTrackable.ImageType => _imageType;

	public IButtonComponentTrackable AsTrackable() => this;
	protected override bool HasChangedGenerated() => base.HasChangedGenerated() || (_command.HasChanged || _color.HasChanged || _fadeIn.HasChanged || _sprite.HasChanged || _material.HasChanged || _imageType.HasChanged);
	protected override void ResetHasChangedGenerated()
	{
		base.ResetHasChangedGenerated();
		_command.ResetHasChanged();
		_color.ResetHasChanged();
		_fadeIn.ResetHasChanged();
		_sprite.ResetHasChanged();
		_material.ResetHasChanged();
		_imageType.ResetHasChanged();
	}
	protected override void ResetGenerated()
	{
		base.ResetGenerated();
		_command.Reset();
		_color.Reset();
		_fadeIn.Reset();
		_sprite.Reset();
		_material.Reset();
		_imageType.Reset();
	}
}


