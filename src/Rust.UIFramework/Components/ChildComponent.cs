using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class ChildComponent : BasePoolable, IChildComponent
{
    public abstract void WriteComponent(JsonFrameworkWriter writer);

    public abstract void Reset();

    protected override void EnterPool()
    {
        Reset();
    }
}