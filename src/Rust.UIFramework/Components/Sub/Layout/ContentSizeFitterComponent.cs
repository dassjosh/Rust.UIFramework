using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ContentSizeFitterComponentSerializer))]
public class ContentSizeFitterComponent : SubComponent
{
    public ContentSizeFitter.FitMode HorizontalFit;
    public ContentSizeFitter.FitMode VerticalFit;
    
    public override Utf8String Type => JsonDefaults.ContentSizeFitterData.Type;
    public override ComponentType ComponentType => ComponentType.ContentSizeFitter;
    public override bool AllowMultiple => false;
    
    public ContentSizeFitterComponent SetHorizontalFit(ContentSizeFitter.FitMode horizontalFit)
    {
        HorizontalFit = horizontalFit;
        return this;
    }
    
    public ContentSizeFitterComponent SetVerticalFit(ContentSizeFitter.FitMode verticalFit)
    {
        VerticalFit = verticalFit;
        return this;
    }
    
    public override void Reset()
    {
        base.Reset();
        HorizontalFit = JsonDefaults.ContentSizeFitterData.HorizontalFit;
        VerticalFit = JsonDefaults.ContentSizeFitterData.VerticalFit;
    }
    
    public override void CopyFrom(object value) 
    {
        base.CopyFrom(value);
        if (value is ContentSizeFitterComponent component)
        {
            HorizontalFit = component.HorizontalFit;
            VerticalFit = component.VerticalFit;
        }
    }
    
    public override bool Equals(BaseComponent other) 
    {
        if (!base.Equals(other)) return false;
        ContentSizeFitterComponent typedOther = (ContentSizeFitterComponent)other!;
        return HorizontalFit == typedOther.HorizontalFit 
               && VerticalFit == typedOther.VerticalFit;
    }
}