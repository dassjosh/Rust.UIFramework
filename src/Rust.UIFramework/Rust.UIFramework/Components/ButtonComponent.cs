using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ButtonComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Button";

        public string Command;
        public string Close;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            JsonCreator.AddField(writer, JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Command = null;
            Close = null;
            Sprite = null;
            Material = null;
        }
    }
}