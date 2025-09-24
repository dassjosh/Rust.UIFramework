using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class ContentSizeFitterComponent : SubComponent
{
    public ContentSizeFitter.FitMode HorizontalFit;
    public ContentSizeFitter.FitMode VerticalFit;
    
    public override Utf8String Type => JsonDefaults.ContentSizeFitterData.Type;
    public override bool AllowMultiple => false;
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.ContentSizeFitterData.HorizontalFitName, HorizontalFit, JsonDefaults.ContentSizeFitterData.HorizontalFit);
        writer.AddField(JsonDefaults.ContentSizeFitterData.VerticalFitName, VerticalFit, JsonDefaults.ContentSizeFitterData.VerticalFit);
    }
    
    public override void Reset()
    {
        base.Reset();
        HorizontalFit = JsonDefaults.ContentSizeFitterData.HorizontalFit;
        VerticalFit = JsonDefaults.ContentSizeFitterData.VerticalFit;
    }
}