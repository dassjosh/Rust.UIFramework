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

    public override void CopyFrom(object value)
    {
        if (value is BaseTypedComponent component)
        {
            Enabled = component.Enabled;
        }
    }

    public override bool AreEquivalent(BaseComponent other)
    {
        if (other is null) return false;
        if(!base.AreEquivalent(other)) return false;
        BaseTypedComponent typedOther = (BaseTypedComponent)other;
        return Enabled == typedOther.Enabled;
    }
}