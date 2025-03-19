using Oxide.Ext.UiFramework.Colors;

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