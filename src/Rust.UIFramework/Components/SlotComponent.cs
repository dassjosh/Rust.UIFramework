using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public class SlotComponent: BasePoolable, IComponent
{
    private const string Type = "Slot";
    
    public string Filter = JsonDefaults.Common.NullValue;

    public void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Slot.FilterName, Filter, JsonDefaults.Common.NullValue);
        writer.WriteEndObject();
    }

    public void Reset()
    {
        Filter = null;
    }
    
    protected override void EnterPool()
    {
        Reset();
    }
}