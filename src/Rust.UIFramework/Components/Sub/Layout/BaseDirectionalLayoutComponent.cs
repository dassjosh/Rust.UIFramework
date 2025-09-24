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
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        base.WriteComponentFields(writer);
        writer.AddField(JsonDefaults.DirectionalLayout.SpacingName, Spacing, JsonDefaults.DirectionalLayout.Spacing);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandWidthName, ChildForceExpandWidth, JsonDefaults.DirectionalLayout.ChildForceExpandWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandHeightName, ChildForceExpandHeight, JsonDefaults.DirectionalLayout.ChildForceExpandHeight);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlWidthName, ChildControlWidth, JsonDefaults.DirectionalLayout.ChildControlWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlHeightName, ChildControlHeight, JsonDefaults.DirectionalLayout.ChildControlHeight);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleWidthName, ChildScaleWidth, JsonDefaults.DirectionalLayout.ChildScaleWidth);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleHeightName, ChildScaleHeight, JsonDefaults.DirectionalLayout.ChildScaleHeight);
    }

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
}