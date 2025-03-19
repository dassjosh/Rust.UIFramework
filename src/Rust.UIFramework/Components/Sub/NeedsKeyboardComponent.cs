using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class NeedsKeyboardComponent : SubComponent
{
    public override bool AllowMultiple => false;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
        writer.WriteEndObject();
    }
}