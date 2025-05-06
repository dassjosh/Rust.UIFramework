using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiSection : BaseUiComponent
{
    public static UiSection Create(in UiPosition pos, in UiOffset offset)
    {
        UiSection panel = CreateBase<UiSection>(pos, offset);
        return panel;
    }
}