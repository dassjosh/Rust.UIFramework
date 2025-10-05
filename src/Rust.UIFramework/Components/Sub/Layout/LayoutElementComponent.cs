using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class LayoutElementComponent : SubComponent
{
    private readonly TrackedValue<float> _preferredWidth = new(JsonDefaults.LayoutElement.PreferredWidth);
    private readonly TrackedValue<float> _preferredHeight = new(JsonDefaults.LayoutElement.PreferredHeight);
    private readonly TrackedValue<float> _minWidth = new(JsonDefaults.LayoutElement.MinWidth);
    private readonly TrackedValue<float> _minHeight = new(JsonDefaults.LayoutElement.MinHeight);
    private readonly TrackedValue<float> _flexibleWidth = new(JsonDefaults.LayoutElement.FlexibleWidth);
    private readonly TrackedValue<float> _flexibleHeight = new(JsonDefaults.LayoutElement.FlexibleHeight);
    private readonly TrackedValue<bool> _ignoreLayout = new(JsonDefaults.LayoutElement.IgnoreLayout);
    
    public float PreferredWidth { get => _preferredWidth.Value; set => _preferredWidth.Value = value; }
    public float PreferredHeight { get => _preferredHeight.Value; set => _preferredHeight.Value = value; }
    public float MinWidth { get => _minWidth.Value; set => _minWidth.Value = value; }
    public float MinHeight { get => _minHeight.Value; set => _minHeight.Value = value; }
    public float FlexibleWidth { get => _flexibleWidth.Value; set => _flexibleWidth.Value = value; }
    public float FlexibleHeight { get => _flexibleHeight.Value; set => _flexibleHeight.Value = value; }
    public bool IgnoreLayout { get => _ignoreLayout.Value; set => _ignoreLayout.Value = value; }

    public override Utf8String Type => JsonDefaults.LayoutElement.Type;
    public override ComponentType ComponentType => ComponentType.LayoutElement;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.LayoutElement.PreferredWidthName, _preferredWidth, mode);
        writer.AddField(JsonDefaults.LayoutElement.PreferredHeightName, _preferredHeight, mode);
        writer.AddField(JsonDefaults.LayoutElement.MinWidthName, _minWidth, mode);
        writer.AddField(JsonDefaults.LayoutElement.MinHeightName, _minHeight, mode);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleWidthName, _flexibleWidth, mode);
        writer.AddField(JsonDefaults.LayoutElement.FlexibleHeightName, _flexibleHeight, mode);
        writer.AddField(JsonDefaults.LayoutElement.IgnoreLayoutName, _ignoreLayout, mode);
    }

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
    
    public override bool HasChanged()
    {
        return _preferredWidth.HasChanged || _preferredHeight.HasChanged || _minWidth.HasChanged || _minHeight.HasChanged || _flexibleWidth.HasChanged || _flexibleHeight.HasChanged || _ignoreLayout.HasChanged;
    }
    
    public override void ResetHasChanged()
    {
        _preferredWidth.ResetHasChanged();
        _preferredHeight.ResetHasChanged();
        _minWidth.ResetHasChanged();
        _minHeight.ResetHasChanged();
        _flexibleWidth.ResetHasChanged();
        _flexibleHeight.ResetHasChanged();
        _ignoreLayout.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _preferredWidth.Reset();
        _preferredHeight.Reset();
        _minWidth.Reset();
        _minHeight.Reset();
        _flexibleWidth.Reset();
        _flexibleHeight.Reset();
        _ignoreLayout.Reset();
    }
}