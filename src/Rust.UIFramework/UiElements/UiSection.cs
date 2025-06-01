using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiSection : BaseUiComponent
{
    public readonly EmptyComponent Empty;

    public UiSection() : this(new EmptyComponent()) { }

    private UiSection(EmptyComponent component) : base(component)
    {
        Empty = component;
    }
    
    public static UiSection Create() => CreateBase<UiSection>();
}