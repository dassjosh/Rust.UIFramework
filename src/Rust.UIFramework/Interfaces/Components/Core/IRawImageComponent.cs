using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IRawImageComponent
{
    UiColor Color { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    float FadeIn { get; set; }
    string Image { get; set; }
    string Material { get; set; }
    UiReference PlaceholderFor { get; set; }
}