using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class RawImageComponent : CoreComponent
{
    public UiColor Color;
    public float FadeIn;
    public string Image;
    public string Material;
    
    [Obsolete("Please use Image instead")]
    public string Url { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Png { get => Image; set => Image = value; }
    [Obsolete("Please use Image instead")]
    public string Texture { get => Image; set => Image = value; }
    
    

    public override Utf8String Type => JsonDefaults.RawImage.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        
        if (!string.IsNullOrEmpty(Image))
        {
            if (Image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                writer.AddFieldRaw(JsonDefaults.Image.UrlName, Image);
            } 
            else if (uint.TryParse(Image, out uint _))
            {
                writer.AddFieldRaw(JsonDefaults.Image.PngName, Image);
            }
            else if(Image.StartsWith("assets/", StringComparison.OrdinalIgnoreCase))
            {
                writer.AddField(JsonDefaults.BaseImage.SpriteName, Image, JsonDefaults.RawImage.TextureValue);
            }
            else
            {
                UiFrameworkExtension.GlobalLogger.Warning<string>("[UiFramework] RawImage.Image '{0}' is not a valid image. Should be a URL, PNG ID, or Texture.", Image);
            }
        }
    }

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = 0;
        Image = null;
        Material = null;
    }
}