using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseTypedComponent : BaseComponent
{
    public bool Enabled;
    public abstract Utf8String Type { get; }
    
    public override void Reset() => Enabled = true;

    public override void CopyFrom(object value)
    {
        if (value is BaseTypedComponent component)
        {
            Enabled = component.Enabled;
        }
    }

    public override bool Equals(BaseComponent other)
    {
        if (other is null) return false;
        if(!base.Equals(other)) return false;
        BaseTypedComponent typedOther = (BaseTypedComponent)other;
        return Enabled == typedOther.Enabled;
    }
}