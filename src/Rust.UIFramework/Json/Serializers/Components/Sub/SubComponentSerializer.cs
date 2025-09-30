using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public abstract class SubComponentSerializer<T> : TypedComponentSerializer<T> where T : SubComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Serialize(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.WriteStartObject();
        SerializeType(writer, component, defaults);
        SerializeComponent(writer, component, defaults, mode);
        writer.WriteEndObject();
    }
}