using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class RawImageComponent : FadeInComponent
    {
        private const string Type = "UnityEngine.UI.RawImage";

        public string Sprite;
        public string Url;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.SpriteName, Sprite, JsonDefaults.SpriteImageValue);
            if (!string.IsNullOrEmpty(Url))
            {
                JsonCreator.AddField(writer, JsonDefaults.UrlName, Url, JsonDefaults.EmptyString);
            }

            base.WriteComponent(writer);
            
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Url = null;
        }
    }
}