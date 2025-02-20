using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder;

public abstract partial class BaseUiBuilder
{
    #region ColorBlock
    public ColorBlockComponent ColorBlock(UiButton button, in UiColor? highlightColor = null, in UiColor? pressedColor = null, in UiColor? selectedColor = null, in float? colorMultiplier = null, in float? fadeDuration = null)
    {
        return button.AddColorBlock(highlightColor, pressedColor, selectedColor, colorMultiplier, fadeDuration);
    }
    #endregion

    #region ScrollBar
    public ScrollbarComponent AddHorizontalScrollBar(UiScrollView view, bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        return view.AddHorizontalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }
    
    public ScrollbarComponent AddVerticalScrollBar(UiScrollView view, bool invert = false, bool autoHide = false, string handleSprite = null, string trackSprite = null, float size = 20f, 
        UiColor? handleColor = null, UiColor? highlightColor = null, UiColor? pressedColor = null, UiColor? trackColor = null)
    {
        return view.AddVerticalScrollBar(invert, autoHide, handleSprite, trackSprite, size, handleColor, highlightColor, pressedColor, trackColor);
    }
    #endregion
}