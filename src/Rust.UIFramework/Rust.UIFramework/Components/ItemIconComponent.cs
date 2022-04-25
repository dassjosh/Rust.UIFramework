using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class ItemIconComponent : BaseImageComponent
    {
        public int ItemId;
        public ulong SkinId;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ItemIdName, ItemId);
            JsonCreator.AddField(writer, JsonDefaults.SkinIdName, SkinId, JsonDefaults.DefaultSkinId);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            ItemId = 0;
            SkinId = 0;
        }
    }
}