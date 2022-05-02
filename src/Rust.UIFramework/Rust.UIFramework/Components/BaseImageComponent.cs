using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseImageComponent : FadeInComponent
    {
        public string Sprite;
        public string Material;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            JsonCreator.AddField(writer, JsonDefaults.BaseImage.SpriteName, Sprite, JsonDefaults.BaseImage.Sprite);
            JsonCreator.AddField(writer, JsonDefaults.BaseImage.MaterialName, Material, JsonDefaults.BaseImage.Material);
            base.WriteComponent(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Material = null;
        }
    }
}