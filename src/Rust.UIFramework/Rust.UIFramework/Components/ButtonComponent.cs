using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ButtonComponent : FadeInComponent
    {
        private const string Type = "UnityEngine.UI.Button";

        public string Command;
        public string Close;
        public string Sprite;
        public string Material;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.CommandName, Command, JsonDefaults.NullValue);
            JsonCreator.AddField(writer, JsonDefaults.CloseName, Close, JsonDefaults.NullValue);
            JsonCreator.AddField(writer, JsonDefaults.SpriteName, Sprite, JsonDefaults.SpriteValue);
            JsonCreator.AddField(writer, JsonDefaults.MaterialName, Material, JsonDefaults.MaterialValue);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Command = null;
            Close = null;
            Sprite = null;
            Material = null;
        }
    }
}