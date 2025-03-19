using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public abstract class RectTransformComponent : ChildComponent
{
    public UiPosition Position;
    public UiOffset Offset;

    public override void Reset()
    {
        Position = default;
        Offset = default;
    }
}