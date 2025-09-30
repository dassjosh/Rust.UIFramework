using System;
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

    public virtual bool Equals(BaseComponent other)
    {
        if (other is null) return false;
        return ComponentType == other.ComponentType;
    }
    
    public static bool operator ==(BaseComponent left, BaseComponent right) => ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
    public static bool operator !=(BaseComponent left, BaseComponent right) => !(left == right);
    
    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseComponent)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();
}