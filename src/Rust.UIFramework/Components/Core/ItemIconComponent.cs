using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class ItemIconComponent : ImageComponent
{
    public int ItemId;
    public ulong SkinId;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddFieldRaw(JsonDefaults.ItemIcon.ItemIdName, ItemId);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, SkinId, 0);
        base.WriteComponentFields(writer);
    }

    public override void Reset()
    {
        base.Reset();
        ItemId = 0;
        SkinId = 0;
    }
}