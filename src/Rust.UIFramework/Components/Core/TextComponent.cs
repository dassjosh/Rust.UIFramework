using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(TextComponentSerializer))]
public class TextComponent : CoreComponent, IGraphicalComponent
{
    public UiColor Color;
    public float FadeIn { get; set; }
    public int FontSize;
    public string Font;
    public TextAnchor Align;
    public string Text;
    public VerticalWrapMode VerticalOverflow;
    public UiReference PlaceholderFor;

    public override Utf8String Type => JsonDefaults.Text.Type;
    public override ComponentType ComponentType => ComponentType.Text;

    public override void Reset()
    {
        base.Reset();
        Color = default;
        FadeIn = JsonDefaults.Common.FadeIn;
        FontSize = JsonDefaults.Text.FontSize;
        Font = null;
        Align = JsonDefaults.Text.Align;
        Text = null;
        VerticalOverflow = JsonDefaults.Text.VerticalOverflow;
        PlaceholderFor = default;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is TextComponent component)
        {
            Color = component.Color;
            FadeIn = component.FadeIn;
            FontSize = component.FontSize;
            Font = component.Font;
            Align = component.Align;
            Text = component.Text;
            VerticalOverflow = component.VerticalOverflow;
            PlaceholderFor = component.PlaceholderFor;
        }
    }

    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        TextComponent typedOther = (TextComponent)other!;
        return Color == typedOther.Color 
               && FadeIn == typedOther.FadeIn 
               && FontSize == typedOther.FontSize 
               && Font == typedOther.Font 
               && Align == typedOther.Align 
               && Text == typedOther.Text 
               && VerticalOverflow == typedOther.VerticalOverflow 
               && PlaceholderFor == typedOther.PlaceholderFor;
    }
}