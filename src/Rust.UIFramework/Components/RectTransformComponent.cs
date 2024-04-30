using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.Components;

public abstract class RectTransformComponent : IComponent
{
    public UiPosition Position;
    public UiOffset Offset;

    public abstract void WriteComponent(JsonFrameworkWriter writer);

    public virtual void Reset()
    {
        Position = default;
        Offset = default;
    }
}