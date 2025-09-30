using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public abstract class BaseLayoutComponentSerializer<T> : SubComponentSerializer<T> where T : BaseLayoutComponent, new()
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Layout.ChildAlignmentName, component.ChildAlignment, defaults.ChildAlignment);
        writer.AddField(JsonDefaults.Layout.PaddingName, component.Padding, defaults.Padding);
    }
}