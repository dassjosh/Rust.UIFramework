using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

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
    
    public override void ResetHasChanged()
    {
        _horizontalFit.ResetHasChanged();
        _verticalFit.ResetHasChanged();
    }
    
    public override void Reset()
    {
        base.Reset();
        _horizontalFit.Reset();
        _verticalFit.Reset();
    }
}