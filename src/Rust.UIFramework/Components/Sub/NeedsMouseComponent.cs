using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class NeedsMouseComponent : SubComponent
{
    public override bool AllowMultiple => false;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
        writer.WriteEndObject();
    }
}