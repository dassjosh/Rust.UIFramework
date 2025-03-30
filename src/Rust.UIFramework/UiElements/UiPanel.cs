using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPanel : BaseUiImage<UiPanel>
{
    public readonly ImageComponent Image = new();
    internal override CoreComponent Component => Image;
    
    public static UiPanel Create(UiColor color)
    {
        UiPanel panel = CreateBase<UiPanel>();
        panel.Image.Color = color;
        return panel;
    }
}