using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiImage : BaseUiImage<UiImage>
{
    public readonly ImageComponent Image;

    public UiImage() : this(new ImageComponent()) { }

    private UiImage(ImageComponent component) : base(component)
    {
        Image = component;
    }
    
    public UiImage Init(string sprite, UiColor color)
    {
        Color = color;
        Sprite = sprite;
        return this;
    }
}