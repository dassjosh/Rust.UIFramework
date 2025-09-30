using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class DirectionalLayoutComponentSerializer<T> : BaseLayoutComponentSerializer<T> where T : BaseDirectionalLayoutComponent, new()
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, T component, T defaults, SerializeMode mode)
    {
        base.SerializeComponent(writer, component, defaults, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.SpacingName, component.Spacing, defaults.Spacing);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandWidthName, component.ChildForceExpandWidth, defaults.ChildForceExpandWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandHeightName, component.ChildForceExpandHeight, defaults.ChildForceExpandHeight);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlWidthName, component.ChildControlWidth, defaults.ChildControlWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlHeightName, component.ChildControlHeight, defaults.ChildControlHeight);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleWidthName, component.ChildScaleWidth, defaults.ChildScaleWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleHeightName, component.ChildScaleHeight, defaults.ChildScaleHeight);
    }
}