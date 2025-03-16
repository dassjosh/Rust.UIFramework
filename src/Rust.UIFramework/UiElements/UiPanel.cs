using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPanel : BaseUiImage<UiPanel>
{
    public static UiPanel Create(UiColor color)
    {
        UiPanel panel = CreateBase<UiPanel>();
        panel.Image.Color = color;
        return panel;
    }
}