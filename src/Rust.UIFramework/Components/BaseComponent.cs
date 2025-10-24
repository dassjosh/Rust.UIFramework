using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : BasePoolable
{
    public abstract ComponentType ComponentType { get; }
    protected BaseComponent() => EnterPool();
    public abstract void WriteComponent(JsonFrameworkWriter writer, SerializeMode mode);
    protected sealed override void EnterPool() => Reset();

    protected virtual bool HasChangedGenerated() => false;
    protected virtual void ResetHasChangedGenerated() {}
    protected virtual void ResetGenerated() {}

    public virtual bool HasChanged() => HasChangedGenerated();
    public virtual void ResetHasChanged() => ResetHasChangedGenerated();
    public virtual void Reset() => ResetGenerated();
}