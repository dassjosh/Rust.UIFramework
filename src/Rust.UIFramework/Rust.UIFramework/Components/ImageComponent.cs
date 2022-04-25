using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ImageComponent : BaseImageComponent
    {
        public string Png;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            if (!string.IsNullOrEmpty(Png))
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.PNGName, Png);
            }
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Png = null;
        }
    }
}