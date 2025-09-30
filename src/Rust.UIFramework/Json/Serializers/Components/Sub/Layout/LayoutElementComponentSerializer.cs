using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class LayoutElementComponentSerializer : SubComponentSerializer<LayoutElementComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, LayoutElementComponent component, LayoutElementComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.LayoutElement.PreferredWidthName, component.PreferredWidth, defaults.PreferredWidth);
        writer.AddField(JsonDefaults.LayoutElement.PreferredHeightName, component.PreferredHeight, defaults.PreferredHeight);
        writer.AddField(JsonDefaults.LayoutElement.MinWidthName, component.MinWidth, defaults.MinWidth);
        writer.AddField(JsonDefaults.LayoutElement.MinHeightName, component.MinHeight, defaults.MinHeight);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleWidthName, component.FlexibleWidth, defaults.FlexibleWidth);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleHeightName, component.FlexibleHeight, defaults.FlexibleHeight);
        writer.AddField(JsonDefaults.LayoutElement.IgnoreLayoutName, component.IgnoreLayout, defaults.IgnoreLayout);
    }
}