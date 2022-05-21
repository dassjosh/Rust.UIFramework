using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiPanel : BaseUiImage
    {
        public static UiPanel Create(UiPosition pos, UiOffset? offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }
    }
}