using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class RawImageComponent : FadeInComponent
    {
        private const string Type = "UnityEngine.UI.RawImage";

        public string Url;
        public string Texture;
        public string Material;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
            JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
            if (!string.IsNullOrEmpty(Url))
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.UrlName, Url);
            }

            base.WriteComponent(writer);
            
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Url = null;
        }
    }
}