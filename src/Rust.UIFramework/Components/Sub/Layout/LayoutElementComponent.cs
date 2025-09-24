using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

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
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.LayoutElement.PreferredWidthName, PreferredWidth, JsonDefaults.LayoutElement.PreferredWidth);
        writer.AddField(JsonDefaults.LayoutElement.PreferredHeightName, PreferredHeight, JsonDefaults.LayoutElement.PreferredHeight);
        writer.AddField(JsonDefaults.LayoutElement.MinWidthName, MinWidth, JsonDefaults.LayoutElement.MinWidth);
        writer.AddField(JsonDefaults.LayoutElement.MinHeightName, MinHeight, JsonDefaults.LayoutElement.MinHeight);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleWidthName, FlexibleWidth, JsonDefaults.LayoutElement.FlexibleWidth);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleHeightName, FlexibleHeight, JsonDefaults.LayoutElement.FlexibleHeight);
        writer.AddField(JsonDefaults.LayoutElement.IgnoreLayoutName, IgnoreLayout, JsonDefaults.LayoutElement.IgnoreLayout);
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
}