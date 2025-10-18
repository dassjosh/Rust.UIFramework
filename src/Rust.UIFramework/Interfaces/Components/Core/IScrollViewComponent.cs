using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IScrollViewComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.MovementType))]
    ScrollRect.MovementType MovementType { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Elasticity))]
    float Elasticity { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Inertia))]
    bool Inertia { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.DecelerationRate))]
    float DecelerationRate { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.ScrollSensitivity))]
    float ScrollSensitivity { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.HorizontalScrollProgress))]
    float HorizontalScrollProgress { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.VerticalScrollProgress))]
    float VerticalScrollProgress { get; set; }
}