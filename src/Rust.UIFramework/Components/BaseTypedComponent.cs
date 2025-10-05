using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseTypedComponent : BaseComponent
{
    private TrackedValue<bool> _enabled = new(true);
    
    public bool Enabled { get => _enabled.Value; set => _enabled.Value = value; }
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
    
    public override void Reset() => Enabled = true;
}