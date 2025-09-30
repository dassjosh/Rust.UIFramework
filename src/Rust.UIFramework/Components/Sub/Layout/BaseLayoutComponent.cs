using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseLayoutComponent : SubComponent
{
    public TextAnchor ChildAlignment;
    public UiPadding Padding;
    public BaseUiComponent Owner { get; internal set; }
    public UiReference Reference => Owner.Reference;
    
    public override bool AllowMultiple => false;

    public override void Reset()
    {
        base.Reset();
        ChildAlignment = JsonDefaults.Layout.ChildAlignment;
        Padding = default;
        Owner = null;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is BaseLayoutComponent component)
        {
            ChildAlignment = component.ChildAlignment;
            Padding = component.Padding;
        }
    }
    
    public override bool Equals(BaseComponent other)
    {
        if (!base.Equals(other)) return false;
        BaseLayoutComponent typedOther = (BaseLayoutComponent)other!;
        return ChildAlignment == typedOther.ChildAlignment 
               && Padding == typedOther.Padding;
    }
}