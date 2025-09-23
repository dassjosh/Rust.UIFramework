using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public class TextComponent : CoreComponent
{
    public UiColor Color;
    public float FadeIn;
    public int FontSize;
    public string Font;
    public TextAnchor Align;
    public string Text;
    public VerticalWrapMode VerticalOverflow;
    public UiReference PlaceholderFor;

    public override Utf8String Type => JsonDefaults.Text.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddTextField(JsonDefaults.Text.TextName, Text);
        writer.AddField(JsonDefaults.Text.FontSizeName, FontSize, JsonDefaults.Text.FontSize);
        writer.AddField(JsonDefaults.Text.FontName, Font, JsonDefaults.Text.FontValue);
        writer.AddField(JsonDefaults.Text.AlignName, Align, JsonDefaults.Text.Align);
        writer.AddField(JsonDefaults.Text.VerticalOverflowName, VerticalOverflow, JsonDefaults.Text.VerticalOverflow);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
        writer.AddField(JsonDefaults.Color.ColorName, Color);
        
        if (PlaceholderFor.IsValidReference())
        {
            writer.AddFieldRaw(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = 0;
        FontSize = JsonDefaults.Text.FontSize;
        Font = null;
        Align = JsonDefaults.Text.Align;
        Text = null;
        VerticalOverflow = JsonDefaults.Text.VerticalOverflow;
        PlaceholderFor = default;
    }
}