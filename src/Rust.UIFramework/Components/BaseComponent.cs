using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : BasePoolable
{
    protected BaseComponent() => Reset();
    protected override void EnterPool() => Reset();
    public abstract void WriteComponent(JsonFrameworkWriter writer);
    public abstract void Reset();
}