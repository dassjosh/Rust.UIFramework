using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.UiElements;

[UiFrameworkSerializer(typeof(UiComponentSerializer<UiPanel>))]
public class UiPanel : BaseUiImage<UiPanel>
{
    public readonly ImageComponent Image;

    public UiPanel() : this(new ImageComponent()) { }

    private UiPanel(ImageComponent component) : base(component)
    {
        Image = component;
    }
}