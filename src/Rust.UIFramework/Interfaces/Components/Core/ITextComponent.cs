using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ITextComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    UiColor Color { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    float FadeIn { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.FontSize))]
    int FontSize { get; set; }
    [TrackedDefaults(null, null, typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.FontValue))]
    string Font { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.Align))]
    TextAnchor Align { get; set; }
    string Text { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.VerticalOverflow))]
    VerticalWrapMode VerticalOverflow { get; set; }
    UiReference PlaceholderFor { get; set; }
}