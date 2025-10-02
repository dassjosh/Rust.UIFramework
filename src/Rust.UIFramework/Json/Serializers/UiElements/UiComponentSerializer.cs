using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Json;

public class UiComponentSerializer<TComponent> : BaseSerializer<TComponent> where TComponent : BaseUiComponent, new()
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void Serialize(JsonFrameworkWriter writer, TComponent component, TComponent defaults, SerializeMode mode)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, component.Reference.Name);
        if (mode == SerializeMode.Create && component.Update != UpdateMode.Update)
        {
            writer.AddFieldRaw(JsonDefaults.Common.ParentName, component.Reference.Parent);
        }
        writer.AddField(JsonDefaults.Common.FadeOutName, component.FadeOut, defaults.FadeOut);
        writer.AddField(JsonDefaults.Common.ActiveName, component.Active, defaults.Active);
        switch (component.Update)
        {
            case UpdateMode.Replace:
                writer.AddFieldRaw(JsonDefaults.Common.Replace, component.Reference.Name);
                break;
            case UpdateMode.Update:
                writer.AddFieldRaw(JsonDefaults.Common.Update, true);
                break;
            case UpdateMode.None:
                break;
        }
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        UiFrameworkSerializer.Serialize(writer, component._component, defaults._component, mode);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}