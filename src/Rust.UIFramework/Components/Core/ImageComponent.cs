using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ImageComponentSerializer))]
public class ImageComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<string> _sprite = new();
    private readonly TrackedValue<float> _fadein = new(JsonDefaults.Common.FadeIn);
    private readonly TrackedValue<string> _material = new();
    private readonly TrackedValue<UiColor> _color = new(JsonDefaults.Color.ColorValue);
    private readonly TrackedValue<Image.Type> _imageType = new(JsonDefaults.Image.ImageType);
    protected readonly TrackedValue<bool> _fillCenter = new(JsonDefaults.Image.FillCenter);
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

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is ImageComponent component)
        {
            Color = component.Color;
            FadeIn = component.FadeIn;
            Sprite = component.Sprite;
            Material = component.Material;
            ImageType = component.ImageType;
            PlaceholderFor = component.PlaceholderFor;
            FillCenter = component.FillCenter;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ImageComponent typedOther = (ImageComponent)other!;
        return Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && Sprite == typedOther.Sprite 
               && Material == typedOther.Material 
               && ImageType == typedOther.ImageType 
               && PlaceholderFor == typedOther.PlaceholderFor 
               && FillCenter == typedOther.FillCenter;
    }
}