using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : BasePoolable, ICopyFrom
{
    public abstract ComponentType ComponentType { get; }
    protected BaseComponent() => EnterPool();
    protected sealed override void EnterPool() => Reset();
    public abstract void Reset();
    public abstract void CopyFrom(object value);

    public virtual bool AreEquivalent(BaseComponent other)
    {
        if (other is null) return false;
        return ComponentType == other.ComponentType;
    }
}