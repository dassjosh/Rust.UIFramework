using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ContentSizeFitterComponentSerializer : SubComponentSerializer<ContentSizeFitterComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, ContentSizeFitterComponent component, ContentSizeFitterComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ContentSizeFitterData.HorizontalFitName, component.HorizontalFit, defaults.HorizontalFit);
        writer.AddField(JsonDefaults.ContentSizeFitterData.VerticalFitName, component.VerticalFit, defaults.VerticalFit);
    }
}