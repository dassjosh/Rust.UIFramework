using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components
{
    public class RawImageComponent : BaseFadeInComponent
    {
        private const string Type = "UnityEngine.UI.RawImage";

        public string Url;
        public string Texture;
        public string Material;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.BaseImage.SpriteName, Texture, JsonDefaults.RawImage.TextureValue);
            writer.AddField(JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
            if (!string.IsNullOrEmpty(Url))
            {
                writer.AddFieldRaw(JsonDefaults.Image.UrlName, Url);
            }

            base.WriteComponent(writer);
            
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Url = null;
            Texture = null;
            Material = null;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}