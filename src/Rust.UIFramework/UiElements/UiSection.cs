using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiSection : BaseUiComponent
{
    public readonly EmptyComponent Empty = new();
    internal override CoreComponent Component => Empty;
    
    public static UiSection Create(in UiPosition pos, in UiOffset offset) => CreateBase<UiSection>(pos, offset);
}