using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiImage : BaseUiImage
    {
        public static UiImage CreateSpriteImage(in UiPosition pos, in UiOffset offset, UiColor color, string sprite)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Sprite = sprite;
            return image;
        }
    }
}