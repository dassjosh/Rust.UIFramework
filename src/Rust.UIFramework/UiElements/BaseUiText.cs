using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiText<T>(TextComponent component) : BaseUiComponent(component), IFadeIn<T>, IUiColor<T> where T : BaseUiText<T>
{
    public float FadeIn { get => component.FadeIn; set => component.FadeIn = value; }
    public UiColor Color { get => component.Color; set => component.Color = value; }
    public int FontSize { get => component.FontSize; set => component.FontSize = value; }
    public string Font { get => component.Font; set => component.Font = value; }
    public TextAnchor Align { get => component.Align; set => component.Align = value; }
    public string TextValue { get => component.Text; set => component.Text = value; }
    public VerticalWrapMode VerticalOverflow { get => component.VerticalOverflow; set => component.VerticalOverflow = value; }
    
    public T SetFadeIn(float duration)
    {
        FadeIn = duration;
        return (T) this;
    }
    
    public T SetColor(UiColor color)
    {
        Color = color;
        return (T) this;
    }
    
    public T SetFontSize(int fontSize)
    {
        FontSize = fontSize;
        return (T) this;
    }
    
    public T SetFont(string font)
    {
        Font = font;
        return (T) this;
    }
    
    public T SetTextAlign(TextAnchor align)
    {
        Align = align;
        return (T) this;
    }
    
    public T SetText(string text)
    {
        TextValue = text;
        return (T) this;
    }
    
    public T SetVerticalOverflow(VerticalWrapMode verticalOverflow)
    {
        VerticalOverflow = verticalOverflow;
        return (T) this;
    }
}