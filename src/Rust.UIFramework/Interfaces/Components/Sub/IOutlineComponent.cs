using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IOutlineComponent : ISubComponent
{
    UiColor Color { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Outline), nameof(JsonDefaults.Outline.Distance))]
    Vector2 Distance { get; set; }
    
    bool UseGraphicAlpha { get; set; }
}