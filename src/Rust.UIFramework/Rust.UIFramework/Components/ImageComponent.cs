using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ImageComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Image";
        
        public string Png;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            if (!string.IsNullOrEmpty(Png))
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Image.PNGName, Png);
            }
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