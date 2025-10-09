using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ImageComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<string> _sprite = new(null, JsonDefaults.BaseImage.Sprite);
    private readonly TrackedValue<float> _fadein = new(JsonDefaults.Common.FadeIn);
    private readonly TrackedValue<string> _material = new(null, JsonDefaults.BaseImage.Material);
    private readonly TrackedValue<UiColor> _color = new(JsonDefaults.Color.ColorValue);
    private readonly TrackedValue<Image.Type> _imageType = new(JsonDefaults.Image.ImageType);
    private readonly TrackedValue<bool> _fillCenter = new(JsonDefaults.Image.FillCenter);
    private readonly TrackedValue<UiReference> _placeholderFor = new();
    
    public UiColor Color { get => _color.Value; set => _color.Value = value; }
    public float FadeIn { get => _fadein.Value; set => _fadein.Value = value; }
    public string Sprite { get => _sprite.Value; set => _sprite.Value = value; }
    public string Material  { get => _material.Value; set => _material.Value = value; }
    public Image.Type ImageType { get => _imageType.Value; set => _imageType.Value = value; }
    public UiReference PlaceholderFor  { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
    public bool FillCenter  { get => _fillCenter.Value; set => _fillCenter.Value = value; }
    
    public override Utf8String Type => JsonDefaults.Image.Type;
    public override ComponentType ComponentType => ComponentType.Image;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, _sprite, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadein, mode);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Image.ImageTypeName, _imageType, mode);
        writer.AddField(JsonDefaults.Image.FillCenterName, _fillCenter, mode);
        if (PlaceholderFor.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
    
    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        _color.ResetHasChanged();
        _fadein.ResetHasChanged();
        _sprite.ResetHasChanged();
        _material.ResetHasChanged();
        _imageType.ResetHasChanged();
        _fillCenter.ResetHasChanged();
        _placeholderFor.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _color.Reset();
        _fadein.Reset();
        _sprite.Reset();
        _material.Reset();
        _imageType.Reset();
        _fillCenter.Reset();
        _placeholderFor.Reset();
    }
}