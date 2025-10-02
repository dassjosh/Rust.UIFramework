using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public abstract class CoreComponentSerializer<T> : TypedComponentSerializer<T> where T : CoreComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Serialize(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        SerializeCoreComponent(writer, component, defaults, mode);
        SerializeSubComponents(writer, component, defaults, mode);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void SerializeCoreComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.WriteStartObject();
        SerializeType(writer, component, defaults);
        SerializeComponent(writer, component, defaults, mode);
        if (mode == SerializeMode.Create && component is IGraphicalComponent graphical)
        {
            writer.AddField(JsonDefaults.Common.FadeInName, graphical.FadeIn, JsonDefaults.Common.FadeIn);
        }
        writer.WriteEndObject();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SerializeSubComponents(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        switch (mode)
        {
            case SerializeMode.Create:
                for (int index = 0; index < component.SubComponents.Count; index++)
                {
                    SubComponent subComponent = component.SubComponents[index];
                    UiFrameworkSerializer.Serialize(writer, subComponent);
                }
                break;
            case SerializeMode.Update:
                for (int index = 0; index < component.SubComponents.Count; index++)
                {
                    SubComponent subComponent = component.SubComponents[index];
                    SubComponent defaultsSubComponent = defaults.GetSubComponentByType(subComponent.ComponentType);
                    if (defaultsSubComponent == null || !subComponent.AreEquivalent(defaultsSubComponent))
                    {
                        UiFrameworkSerializer.Serialize(writer, subComponent, defaultsSubComponent, mode);
                    }
                }
                break;
        }
    }
}