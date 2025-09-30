using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ItemIconComponentSerializer : ImageComponentSerializer<ItemIconComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, ItemIconComponent component, ItemIconComponent defaults, SerializeMode mode)
    {
        base.SerializeComponent(writer, component, defaults, mode);
        writer.AddField(JsonDefaults.ItemIcon.ItemIdName, component.ItemId, defaults.ItemId);
        writer.AddField(JsonDefaults.ItemIcon.SkinIdName, component.SkinId, defaults.SkinId);
    }
}