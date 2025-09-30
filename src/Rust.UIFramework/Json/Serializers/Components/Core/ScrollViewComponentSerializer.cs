using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class ScrollViewComponentSerializer : CoreComponentSerializer<ScrollViewComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, ScrollViewComponent component, ScrollViewComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ScrollView.Horizontal, component.HorizontalScrollbar != null, defaults.HorizontalScrollbar != null);
        writer.AddField(JsonDefaults.ScrollView.Vertical, component.VerticalScrollbar != null, defaults.VerticalScrollbar != null);
        writer.AddField(JsonDefaults.ScrollView.MovementTypeName, component.MovementType, defaults.MovementType);
        writer.AddField(JsonDefaults.ScrollView.ElasticityName, component.Elasticity, defaults.Elasticity);
        writer.AddField(JsonDefaults.ScrollView.InertiaName, component.Inertia, defaults.Inertia);
        writer.AddField(JsonDefaults.ScrollView.DecelerationRateName, component.DecelerationRate, defaults.DecelerationRate);
        writer.AddField(JsonDefaults.ScrollView.ScrollSensitivityName, component.ScrollSensitivity, defaults.ScrollSensitivity);
        writer.AddField(JsonDefaults.ScrollView.HorizontalScrollProgressName, component.HorizontalScrollProgress, defaults.HorizontalScrollProgress);
        writer.AddField(JsonDefaults.ScrollView.VerticalScrollProgressName, component.VerticalScrollProgress, defaults.VerticalScrollProgress);
        writer.AddComponent(JsonDefaults.ScrollView.HorizontalScrollbar, component.HorizontalScrollbar, defaults.HorizontalScrollbar, mode, component.HorizontalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.VerticalScrollbar, component.VerticalScrollbar, defaults.HorizontalScrollbar, mode, component.VerticalScrollbar != null);
        writer.AddComponent(JsonDefaults.ScrollView.ContentTransform, component.ContentTransform, defaults.ContentTransform, mode);
    }
}