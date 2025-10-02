using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseDirectionalLayoutComponent : BaseLayoutComponent
{
    public float Spacing;
    public bool ChildForceExpandWidth;
    public bool ChildForceExpandHeight;
    public bool ChildControlWidth;
    public bool ChildControlHeight;
    public bool ChildScaleWidth;
    public bool ChildScaleHeight;

    public override void Reset()
    {
        base.Reset();
        Spacing = JsonDefaults.DirectionalLayout.Spacing;
        ChildForceExpandWidth = JsonDefaults.DirectionalLayout.ChildForceExpandWidth;
        ChildForceExpandHeight = JsonDefaults.DirectionalLayout.ChildForceExpandHeight;
        ChildControlWidth = JsonDefaults.DirectionalLayout.ChildControlWidth;
        ChildControlHeight = JsonDefaults.DirectionalLayout.ChildControlHeight;
        ChildScaleWidth = JsonDefaults.DirectionalLayout.ChildScaleWidth;
        ChildScaleHeight = JsonDefaults.DirectionalLayout.ChildScaleHeight;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value);
        if (value is BaseDirectionalLayoutComponent component)
        {
            Spacing = component.Spacing;
            ChildForceExpandWidth = component.ChildForceExpandWidth;
            ChildForceExpandHeight = component.ChildForceExpandHeight;
            ChildControlWidth = component.ChildControlWidth;
            ChildControlHeight = component.ChildControlHeight;
            ChildScaleWidth = component.ChildScaleWidth;
            ChildScaleHeight = component.ChildScaleHeight;
        }
    }

    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        BaseDirectionalLayoutComponent typedOther = (BaseDirectionalLayoutComponent)other!;
        return Spacing == typedOther.Spacing 
               && ChildForceExpandWidth == typedOther.ChildForceExpandWidth 
               && ChildForceExpandHeight == typedOther.ChildForceExpandHeight 
               && ChildControlWidth == typedOther.ChildControlWidth 
               && ChildControlHeight == typedOther.ChildControlHeight 
               && ChildScaleWidth == typedOther.ChildScaleWidth 
               && ChildScaleHeight == typedOther.ChildScaleHeight;
    }
}