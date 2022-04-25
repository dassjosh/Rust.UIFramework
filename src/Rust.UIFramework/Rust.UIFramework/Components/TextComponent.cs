using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class TextComponent : BaseTextComponent
    {
        private const string Type = "UnityEngine.UI.Text";

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
    }
}