using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components
{
    public class ImageComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Image";
        
        public string Png;
        public Image.Type ImageType;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            if (!string.IsNullOrEmpty(Png))
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.PngName, Png);
            }
            JsonCreator.AddField(writer, JsonDefaults.Image.ImageType, ImageType);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Png = null;
        }
    }
}