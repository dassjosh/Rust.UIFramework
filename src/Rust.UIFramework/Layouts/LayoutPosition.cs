using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public readonly struct LayoutPosition(UiReference reference, in UiPosition position, in UiOffset offset)
{
    public readonly UiReference Reference = reference;
    public readonly UiPosition Position = position;
    public readonly UiOffset Offset = offset;
}