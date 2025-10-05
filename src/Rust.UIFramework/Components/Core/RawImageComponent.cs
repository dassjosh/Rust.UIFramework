using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(RawImageComponentSerializer))]
public class RawImageComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<UiColor> _color = new();
    private readonly TrackedValue<float> _fadeIn = new(JsonDefaults.Common.FadeIn);
    private readonly TrackedValue<string> _image = new();
    private readonly TrackedValue<string> _material = new();
    private readonly TrackedValue<UiReference> _placeholderFor = new();
    
    public UiColor Color { get => _color.Value; set => _color.Value = value; }
    public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
    public string Image { get => _image.Value; set => _image.Value = value; }
    public string Material { get => _material.Value; set => _material.Value = value; }
    public UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }
    
    [Obsolete("Please use Image instead")]
    public string Url { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Png { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Texture { get => Image; set => Image = value; }

    public override Utf8String Type => JsonDefaults.RawImage.Type;
    public override ComponentType ComponentType => ComponentType.RawImage;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.MaterialName, _material, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        
        if (_placeholderFor.ShouldSerialize(mode) && PlaceholderFor.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }

        if (_image.ShouldSerialize(mode))
        {
            string image = Image;
            if (!string.IsNullOrEmpty(image))
            {
                if (image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    writer.AddFieldRaw(JsonDefaults.Image.UrlName, image);
                } 
                else if (uint.TryParse(image, out uint _))
                {
                    writer.AddFieldRaw(JsonDefaults.Image.PngName, image);
                }
                else if(image.StartsWith("assets/", StringComparison.OrdinalIgnoreCase))
                {
                    writer.AddField(JsonDefaults.BaseImage.SpriteName, image, JsonDefaults.RawImage.TextureValue);
                }
                else
                {
                    UiFrameworkExtension.GlobalLogger.Warning("[UiFramework] RawImage.Image '{0}' is not a valid image. Should be a URL, PNG ID, or Texture.", image);
                }
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        _color.Reset();
        _fadeIn.Reset();
        _image.Reset();
        _material.Reset();
        _placeholderFor.Reset();
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is RawImageComponent component)
        {
            Color = component.Color;
            FadeIn = component.FadeIn;
            Image = component.Image;
            Material = component.Material;
            PlaceholderFor = component.PlaceholderFor;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        RawImageComponent typedOther = (RawImageComponent)other!;
        return Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && Image == typedOther.Image 
               && Material == typedOther.Material 
               && PlaceholderFor == typedOther.PlaceholderFor;
    }
}