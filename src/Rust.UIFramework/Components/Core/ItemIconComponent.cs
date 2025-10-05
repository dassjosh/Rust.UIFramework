using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ItemIconComponentSerializer))]
public class ItemIconComponent : ImageComponent
{
    private readonly TrackedValue<int> _itemId = new();
    private readonly TrackedValue<ulong> _skinId = new();
    
    public int ItemId { get => _itemId.Value; set => _itemId.Value = value; }
    public ulong SkinId { get => _skinId.Value; set => _skinId.Value = value; }
    
    public override ComponentType ComponentType => ComponentType.ItemIcon;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.ItemIcon.ItemIdName, _itemId, mode);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, _skinId, mode);
    }

    public override void Reset()
    {
        base.Reset();
        _itemId.Reset();
        _skinId.Reset();
    }
    
    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is ItemIconComponent component)
        {
            ItemId = component.ItemId;
            SkinId = component.SkinId;
        }
    }
    
    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        ItemIconComponent typedOther = (ItemIconComponent)other!;
        return ItemId == typedOther.ItemId 
               && SkinId == typedOther.SkinId;
    }
}