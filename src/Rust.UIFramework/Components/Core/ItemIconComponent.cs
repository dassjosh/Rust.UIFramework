using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class ItemIconComponent : BaseImageComponent
{
    public int ItemId;
    public ulong SkinId;
    
    public override Utf8String Type => JsonDefaults.Input.Type;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddFieldRaw(JsonDefaults.ItemIcon.ItemIdName, ItemId);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinId, 0);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        base.Reset();
        ItemId = 0;
        SkinId = 0;
    }
}