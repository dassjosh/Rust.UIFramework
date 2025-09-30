using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class OutlineComponentSerializer : SubComponentSerializer<OutlineComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, OutlineComponent component, OutlineComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Outline.DistanceName, component.Distance, mode == SerializeMode.Create ? JsonDefaults.Outline.FpDistance : defaults.Distance);
        writer.AddKeyField(JsonDefaults.Outline.UseGraphicAlphaName, component.UseGraphicAlpha);
        writer.AddField(JsonDefaults.Color.ColorName, component.Color, defaults.Color);
    }
}