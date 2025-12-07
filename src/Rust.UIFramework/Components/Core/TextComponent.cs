using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class TextComponent : CoreComponent, IGraphicalComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    public partial UiColor Color { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    public partial float FadeIn { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.FontSize))]
    public partial int FontSize { get; set; }
    [TrackedDefaults(null, null, typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.FontValue))]
    public partial string Font { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.Align))]
    public partial TextAnchor Align { get; set; }
    public partial string Text { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Text), nameof(JsonDefaults.Text.VerticalOverflow))]
    public partial VerticalWrapMode VerticalOverflow { get; set; }
    public partial UiReference PlaceholderFor { get; set; }
    
    public override Utf8String Type => JsonDefaults.Text.Type;
    public override ComponentType ComponentType => ComponentType.Text;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddTextField(JsonDefaults.Text.TextName, _text, mode);
        writer.AddField(JsonDefaults.Text.FontSizeName, _fontSize, mode);
        writer.AddField(JsonDefaults.Text.FontName, _font, mode);
        writer.AddField(JsonDefaults.Text.AlignName, _align, mode);
        writer.AddField(JsonDefaults.Text.VerticalOverflowName, _verticalOverflow, mode);
        writer.AddField(JsonDefaults.Color.ColorName, _color, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, _fadeIn, mode);
        
        if (_placeholderFor.ShouldSerialize(mode) && PlaceholderFor.IsValidName())
        {
            writer.AddField(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
}