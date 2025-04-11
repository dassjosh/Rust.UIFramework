using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiImage : BaseUiImage<UiImage>
{
    public readonly ImageComponent Image;

    public UiImage() : this(new ImageComponent()) { }

    private UiImage(ImageComponent component) : base(component)
    {
        Image = component;
    }
    
    public static UiImage CreateSpriteImage(string sprite, UiColor color)
    {
        UiImage image = CreateBase<UiImage>();
        image.Image.Color = color;
        image.Image.Sprite = sprite;
        return image;
    }
}