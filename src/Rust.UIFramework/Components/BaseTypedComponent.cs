using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseTypedComponent : BaseComponent
{
    public bool Enabled;
    public abstract Utf8String Type { get; }
    
    protected abstract void WriteComponentFields(JsonFrameworkWriter writer);
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
        WriteComponentFields(writer);
        writer.WriteEndObject();
    }
    
    public override void Reset() => Enabled = true;
}