using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IButtonComponent
{
    string Command { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    UiColor Color { get; set; }
    float FadeIn { get; set; }
    string Sprite { get; set; }
    string Material { get; set; }
    Image.Type ImageType { get; set; }
}