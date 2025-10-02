using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ItemIconComponentSerializer))]
public class ItemIconComponent : ImageComponent
{
    public int ItemId;
    public ulong SkinId;
    
    public override ComponentType ComponentType => ComponentType.ItemIcon;

    public override void Reset()
    {
        base.Reset();
        ItemId = 0;
        SkinId = 0;
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