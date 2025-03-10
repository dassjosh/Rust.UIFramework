using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Layouts;

public readonly struct LayoutSlice(BaseLayout layout, in UiPosition position, in UiOffset offset)
{
    public readonly BaseLayout Layout = layout;
    public readonly UiPosition Position = position;
    public readonly UiOffset Offset = offset;
}