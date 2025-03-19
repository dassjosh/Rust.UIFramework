using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class RawImageComponent : CoreComponent
{
    public UiColor Color;
    public float FadeIn;
    public string Url;
    public string Png;
    public string Texture;
    public string Material;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.RawImage.Type);
        writer.AddField(JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
        if (!string.IsNullOrEmpty(Url))
        {
            writer.AddFieldRaw(JsonDefaults.Image.UrlName, Url);
        }
            
        if (!string.IsNullOrEmpty(Png))
        {
            writer.AddFieldRaw(JsonDefaults.Image.PngName, Png);
        }

        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        base.WriteComponent(writer);    
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = 0;
        Url = null;
        Png = null;
        Texture = null;
        Material = null;
    }
}