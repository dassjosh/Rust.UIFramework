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
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddFieldRaw(JsonDefaults.ItemIcon.ItemIdName, ItemId);
            writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinId, default(ulong));
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void Reset()
        {
            base.Reset();
            ItemId = default(int);
            SkinId = default(ulong);
        }
    }
}