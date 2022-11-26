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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Image.PngName, Png, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Image.ImageType, ImageType);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Png = JsonDefaults.Common.NullValue;
            ImageType = Image.Type.Simple;
        }
    }
}