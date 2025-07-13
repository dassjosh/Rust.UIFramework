using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiPanel : BaseUiImage<UiPanel>
{
    public readonly ImageComponent Image;

    public UiPanel() : this(new ImageComponent()) { }

    private UiPanel(ImageComponent component) : base(component)
    {
        Image = component;
    }
}