using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class TextComponent : CoreComponent, IGraphicalComponent
{
    private readonly TrackedValue<UiColor> _color = new(JsonDefaults.Color.ColorValue);
    private readonly TrackedValue<float> _fadeIn = new(JsonDefaults.Common.FadeIn);
    private readonly TrackedValue<int> _fontSize = new(JsonDefaults.Text.FontSize);
    private readonly TrackedValue<string> _font = new();
    private readonly TrackedValue<TextAnchor> _align = new(JsonDefaults.Text.Align);
    private readonly TrackedValue<string> _text = new();
    private readonly TrackedValue<VerticalWrapMode> _verticalOverflow = new(JsonDefaults.Text.VerticalOverflow);
    private readonly TrackedValue<UiReference> _placeholderFor = new();
    
    public UiColor Color { get => _color.Value; set => _color.Value = value; }
    public float FadeIn { get => _fadeIn.Value; set => _fadeIn.Value = value; }
    public int FontSize { get => _fontSize.Value; set => _fontSize.Value = value; }
    public string Font { get => _font.Value; set => _font.Value = value; }
    public TextAnchor Align { get => _align.Value; set => _align.Value = value; }
    public string Text { get => _text.Value; set => _text.Value = value; }
    public VerticalWrapMode VerticalOverflow { get => _verticalOverflow.Value; set => _verticalOverflow.Value = value; }
    public UiReference PlaceholderFor { get => _placeholderFor.Value; set => _placeholderFor.Value = value; }

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
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
    
    public override void ResetHasChanged()
    {
        base.ResetHasChanged();
        _color.ResetHasChanged();
        _fadeIn.ResetHasChanged();
        _fontSize.ResetHasChanged();
        _font.ResetHasChanged();
        _align.ResetHasChanged();
        _text.ResetHasChanged();
        _verticalOverflow.ResetHasChanged();
        _placeholderFor.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _color.Reset();
        _fadeIn.Reset();
        _fontSize.Reset();
        _font.Reset();
        _align.Reset();
        _text.Reset();
        _verticalOverflow.Reset();
        _placeholderFor.Reset();
    }
}