using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiSection : BaseUiComponent
{
    public readonly EmptyComponent Empty;

    public UiSection() : this(new EmptyComponent()) { }

    private UiSection(EmptyComponent component) : base(component)
    {
        Empty = component;
    }
}