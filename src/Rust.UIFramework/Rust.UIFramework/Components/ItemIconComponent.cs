using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ItemIconComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Image";
        
        public int ItemId;
        public ulong SkinId;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ItemIcon.ItemIdName, ItemId);
            JsonCreator.AddField(writer, JsonDefaults.ItemIcon.SkinIdName, SkinId, JsonDefaults.ItemIcon.DefaultSkinId);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            ItemId = 0;
            SkinId = 0;
        }
    }
}