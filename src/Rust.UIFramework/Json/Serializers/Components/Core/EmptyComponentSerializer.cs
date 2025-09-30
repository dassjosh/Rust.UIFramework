using System;
using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class EmptyComponentSerializer : CoreComponentSerializer<EmptyComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, EmptyComponent component, EmptyComponent defaults, SerializeMode mode)
    {
        throw new NotSupportedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeCoreComponent(JsonFrameworkWriter writer, EmptyComponent component, EmptyComponent defaults, SerializeMode mode)
    {
        //We don't send any UI for an empty component
    }
}