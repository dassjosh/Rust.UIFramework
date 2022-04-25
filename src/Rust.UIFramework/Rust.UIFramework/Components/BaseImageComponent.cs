using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseImageComponent : FadeInComponent
    {
        private const string Type = "UnityEngine.UI.Image";

        public string Sprite;
        public string Material;

        public override void WriteComponent(JsonTextWriter writer)
        {
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.SpriteName, Sprite, JsonDefaults.SpriteValue);
            JsonCreator.AddField(writer, JsonDefaults.MaterialName, Material, JsonDefaults.MaterialValue);
            base.WriteComponent(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Material = null;
        }
    }
}