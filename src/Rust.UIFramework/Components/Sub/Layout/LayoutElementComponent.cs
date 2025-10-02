using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(LayoutElementComponentSerializer))]
public class LayoutElementComponent : SubComponent
{
    public float PreferredWidth;
    public float PreferredHeight;
    public float MinWidth;
    public float MinHeight;
    public float FlexibleWidth;
    public float FlexibleHeight;
    public bool IgnoreLayout;

    public override Utf8String Type => JsonDefaults.LayoutElement.Type;
    public override ComponentType ComponentType => ComponentType.LayoutElement;
    public override bool AllowMultiple => false;

    public LayoutElementComponent SetPreferredWidth(float preferredWidth)
    {
        PreferredWidth = preferredWidth;
        return this;
    }

    public LayoutElementComponent SetPreferredHeight(float preferredHeight)
    {
        PreferredHeight = preferredHeight;
        return this;
    }

    public LayoutElementComponent SetMinWidth(float minWidth)
    {
        MinWidth = minWidth;
        return this;
    }

    public LayoutElementComponent SetMinHeight(float minHeight)
    {
        MinHeight = minHeight;
        return this;
    }

    public LayoutElementComponent SetFlexibleWidth(float flexibleWidth)
    {
        FlexibleWidth = flexibleWidth;
        return this;
    }
    
    public LayoutElementComponent SetFlexibleHeight(float flexibleHeight)
    {
        FlexibleHeight = flexibleHeight;
        return this;
    }
    
    public LayoutElementComponent SetIgnoreLayout(bool ignoreLayout)
    {
        IgnoreLayout = ignoreLayout;
        return this;
    }
    
    public override void Reset()
    {
        base.Reset();
        PreferredWidth = JsonDefaults.LayoutElement.PreferredWidth;
        PreferredHeight = JsonDefaults.LayoutElement.PreferredHeight;
        MinWidth = JsonDefaults.LayoutElement.MinWidth;
        MinHeight = JsonDefaults.LayoutElement.MinHeight;
        FlexibleWidth = JsonDefaults.LayoutElement.FlexibleWidth;
        FlexibleHeight = JsonDefaults.LayoutElement.FlexibleHeight;
        IgnoreLayout = JsonDefaults.LayoutElement.IgnoreLayout;
    }

    public override void CopyFrom(object value)
    {
        base.CopyFrom(value); 
        if (value is LayoutElementComponent component)
        {
            PreferredWidth = component.PreferredWidth;
            PreferredHeight = component.PreferredHeight;
            MinWidth = component.MinWidth;
            MinHeight = component.MinHeight;
            FlexibleWidth = component.FlexibleWidth;
            FlexibleHeight = component.FlexibleHeight;
            IgnoreLayout = component.IgnoreLayout;
        }
    }

    public override bool AreEquivalent(BaseComponent other)
    {
        if (!base.AreEquivalent(other)) return false;
        LayoutElementComponent typedOther = (LayoutElementComponent)other!;
        return PreferredWidth == typedOther.PreferredWidth 
               && PreferredHeight == typedOther.PreferredHeight 
               && MinWidth == typedOther.MinWidth 
               && MinHeight == typedOther.MinHeight 
               && FlexibleWidth == typedOther.FlexibleWidth 
               && FlexibleHeight == typedOther.FlexibleHeight 
               && IgnoreLayout == typedOther.IgnoreLayout;
    }
}