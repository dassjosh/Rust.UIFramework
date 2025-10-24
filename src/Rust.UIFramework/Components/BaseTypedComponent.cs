using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ITypedComponent))]
public abstract partial class BaseTypedComponent : BaseComponent, ITypedComponent
{
    public abstract Utf8String Type { get; }

    public override void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
        WriteComponentFields(writer, mode);
        writer.WriteEndObject();
    }
    
    protected abstract void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode);
}