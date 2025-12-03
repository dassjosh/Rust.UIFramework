using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class RawImageComponent : CoreComponent, IGraphicalComponent
{
    public partial UiColor Color { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    public partial float FadeIn { get; set; }
    public partial string Image { get; set; }
    public partial string Material { get; set; }
    public partial UiReference PlaceholderFor { get; set; }
    
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
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
        
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
                    writer.AddFieldRaw(JsonDefaults.BaseImage.SpriteName, image);
                }
                else
                {
                    UiFrameworkExtension.GlobalLogger.Warning("[UiFramework] RawImage.Image '{0}' is not a valid image. Should be a URL, PNG ID, or Texture.", image);
                }
            }
        }
    }
}