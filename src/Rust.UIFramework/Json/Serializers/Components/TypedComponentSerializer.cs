using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public abstract class TypedComponentSerializer<T> : BaseSerializer<T> where T : BaseTypedComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SerializeType(JsonFrameworkWriter writer, T component, T defaults)
    {
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, component.Type);
        writer.AddField(JsonDefaults.Common.EnabledName, component.Enabled, defaults.Enabled);
    }
}