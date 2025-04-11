using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public abstract class RectTransformComponent : ChildComponent
{
    public UiPosition Position;
    public UiOffset Offset;

    public void UpdateContentTransform(in UiPosition? position = null, in UiOffset? offset = null)
    {
        if (position.HasValue)
        {
            Position = position.Value;
        }

        if (offset.HasValue)
        {
            Offset = offset.Value;
        }
    }
    
    public override void Reset()
    {
        Position = default;
        Offset = default;
    }
}