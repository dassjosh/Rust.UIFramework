using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class TextComponent : BaseTextComponent
    {
        private const string Type = "UnityEngine.UI.Text";

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
    }
}