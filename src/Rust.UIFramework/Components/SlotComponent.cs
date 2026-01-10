using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class SlotComponent : IComponent
{
    private const string Type = "Slot";

    public string Filter;
    public bool Enabled = true;

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Slot.FilterName, Filter, null);
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
        writer.WriteEndObject();
    }

    public void Reset()
    {
        Filter = null;
        Enabled = true;
    }
}
