using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : BasePoolable, IComponent
{
    public abstract ComponentType ComponentType { get; }
    protected BaseComponent() => EnterPool();
    public abstract void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode);
    protected sealed override void EnterPool() => Reset();

    public virtual bool HasChanged() => false;
    public virtual void ResetHasChanged() {}

    public virtual void Reset()
    {
        OnReset();
    }

    protected virtual void OnReset() {}
}