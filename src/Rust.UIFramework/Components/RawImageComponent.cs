using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class RawImageComponent : IComponent
    {
        private const string Type = "UnityEngine.UI.RawImage";

        public UiColor Color;
        public float FadeIn;
        public string Url;
        public string Png;
        public string Texture;
        public string Material;

        public virtual void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
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
            
            writer.WriteEndObject();
        }

        public virtual void Reset()
        {
            Color = default;
            FadeIn = 0;
            Url = null;
            Texture = null;
            Material = null;
        }
    }
}