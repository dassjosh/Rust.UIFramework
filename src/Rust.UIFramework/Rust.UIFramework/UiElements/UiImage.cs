using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiImage : BaseUiImage
    {
        public static UiImage CreateFileImage(UiPosition pos, UiOffset? offset, UiColor color, string png)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Png = png;
            return image;
        }
        
        public static UiImage CreateSpriteImage(UiPosition pos, UiOffset? offset, UiColor color, string sprite)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Sprite = sprite;
            return image;
        }
    }
}