using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class SlotComponent : SubComponent
{
    private const string Type = "Slot";
    
    public string Filter = JsonDefaults.Common.NullValue;

    public override bool AllowMultiple => false;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Slot.FilterName, Filter, JsonDefaults.Common.NullValue);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }

    public override void Reset()
    {
        Filter = null;
    }
}