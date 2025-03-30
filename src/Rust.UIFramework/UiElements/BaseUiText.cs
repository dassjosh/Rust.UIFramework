using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public abstract class BaseUiText<T> : BaseUiComponent, IFadeIn<T>, IUiColor<T> where T : BaseUiText<T>
{
    private TextComponent Text => (TextComponent)Component;
    
    public float FadeIn { get => Text.FadeIn; set => Text.FadeIn = value; }
    public UiColor Color { get => Text.Color; set => Text.Color = value; }
    
    public T SetFadeIn(float duration)
    {
        Text.FadeIn = duration;
        return (T) this;
    }
    
    public T SetColor(UiColor color)
    {
        Text.Color = color;
        return (T) this;
    }
    
    public T SetFontSize(int fontSize)
    {
        Text.FontSize = fontSize;
        return (T) this;
    }
    
    public T SetFont(string font)
    {
        Text.Font = font;
        return (T) this;
    }
    
    public T SetTextAlign(TextAnchor align)
    {
        Text.Align = align;
        return (T) this;
    }
    
    public T SetText(string text)
    {
        Text.Text = text;
        return (T) this;
    }
    
    public T SetVerticalOverflow(VerticalWrapMode verticalOverflow)
    {
        Text.VerticalOverflow = verticalOverflow;
        return (T) this;
    }
}