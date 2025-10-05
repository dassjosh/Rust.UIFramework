using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[UiFrameworkSerializer(typeof(ContentSizeFitterComponentSerializer))]
public class ContentSizeFitterComponent : SubComponent
{
    private readonly TrackedValue<ContentSizeFitter.FitMode> _horizontalFit = new(JsonDefaults.ContentSizeFitterData.HorizontalFit);
    private readonly TrackedValue<ContentSizeFitter.FitMode> _verticalFit = new(JsonDefaults.ContentSizeFitterData.VerticalFit);
    
    public ContentSizeFitter.FitMode HorizontalFit { get => _horizontalFit.Value; set => _horizontalFit.Value = value; }
    public ContentSizeFitter.FitMode VerticalFit { get => _verticalFit.Value; set => _verticalFit.Value = value; }
    
    public override Utf8String Type => JsonDefaults.ContentSizeFitterData.Type;
    public override ComponentType ComponentType => ComponentType.ContentSizeFitter;
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.ContentSizeFitterData.HorizontalFitName, _horizontalFit, mode);
        writer.AddField(JsonDefaults.ContentSizeFitterData.VerticalFitName, _verticalFit, mode);
    }
    
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
    
    public override bool HasChanged()
    {
        return _horizontalFit.HasChanged || _verticalFit.HasChanged;
    }
    
    public override void Reset()
    {
        base.Reset();
        _horizontalFit.Reset();
        _verticalFit.Reset();
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
    
    public override bool AreEquivalent(BaseComponent other) 
    {
        if (!base.AreEquivalent(other)) return false;
        ContentSizeFitterComponent typedOther = (ContentSizeFitterComponent)other!;
        return HorizontalFit == typedOther.HorizontalFit 
               && VerticalFit == typedOther.VerticalFit;
    }
}