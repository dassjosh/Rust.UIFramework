using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class SlotComponentSerializer : SubComponentSerializer<SlotComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, SlotComponent component, SlotComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Slot.FilterName, component.Filter, defaults.Filter);
    }
}