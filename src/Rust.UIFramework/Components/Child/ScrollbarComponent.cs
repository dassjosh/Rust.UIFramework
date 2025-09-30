using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ScrollBarComponentSerializer))]
public class ScrollbarComponent : ChildComponent
{
    public bool Invert;
    public bool AutoHide;
    public string HandleSprite;
    public string TrackSprite;
    public float Size;
    public UiColor HandleColor;
    public UiColor HighlightColor;
    public UiColor PressedColor;
    public UiColor TrackColor;
    
    public override ComponentType ComponentType => ComponentType.ScrollBar;

    public override void Reset()
    {
        Invert = JsonDefaults.ScrollBar.Invert;
        AutoHide = JsonDefaults.ScrollBar.AutoHide;
        HandleSprite = JsonDefaults.Common.NullValue;
        TrackSprite = JsonDefaults.Common.NullValue;
        Size = JsonDefaults.ScrollBar.Size;
        HandleColor = JsonDefaults.ScrollBar.HandleColor;
        HighlightColor = JsonDefaults.ScrollBar.HighlightColor;
        PressedColor = JsonDefaults.ScrollBar.PressedColor;
        TrackColor = JsonDefaults.ScrollBar.TrackColor;
    }

    public override void CopyFrom(object value)
    {
        if (value is ScrollbarComponent component)
        {
            Invert = component.Invert;
            AutoHide = component.AutoHide;
            HandleSprite = component.HandleSprite;
            TrackSprite = component.TrackSprite;
            Size = component.Size;
            HandleColor = component.HandleColor;
            HighlightColor = component.HighlightColor;
            PressedColor = component.PressedColor;
            TrackColor = component.TrackColor;
        } 
    }

    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        ScrollbarComponent typedOther = (ScrollbarComponent)other!;
        return Invert == typedOther.Invert 
               && AutoHide == typedOther.AutoHide 
               && HandleSprite == typedOther.HandleSprite 
               && TrackSprite == typedOther.TrackSprite 
               && Size == typedOther.Size 
               && HandleColor == typedOther.HandleColor 
               && HighlightColor == typedOther.HighlightColor 
               && PressedColor == typedOther.PressedColor 
               && TrackColor == typedOther.TrackColor;
    }
}