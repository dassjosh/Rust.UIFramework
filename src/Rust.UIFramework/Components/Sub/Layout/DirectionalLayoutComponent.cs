using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class DirectionalLayoutComponent : BaseLayoutComponent
{
    private readonly TrackedValue<float> _spacing = new(JsonDefaults.DirectionalLayout.Spacing);
    private readonly TrackedValue<bool> _childForceExpandWidth = new(JsonDefaults.DirectionalLayout.ChildForceExpandWidth);
    private readonly TrackedValue<bool> _childForceExpandHeight = new(JsonDefaults.DirectionalLayout.ChildForceExpandHeight);
    private readonly TrackedValue<bool> _childControlWidth = new(JsonDefaults.DirectionalLayout.ChildControlWidth);
    private readonly TrackedValue<bool> _childControlHeight = new(JsonDefaults.DirectionalLayout.ChildControlHeight);
    private readonly TrackedValue<bool> _childScaleWidth = new(JsonDefaults.DirectionalLayout.ChildScaleWidth);
    private readonly TrackedValue<bool> _childScaleHeight = new(JsonDefaults.DirectionalLayout.ChildScaleHeight);
    private readonly TrackedValue<LayoutDirection> _layoutDirection = new();
    
    public float Spacing { get => _spacing.Value; set => _spacing.Value = value; }
    public bool ChildForceExpandWidth { get => _childForceExpandWidth.Value; set => _childForceExpandWidth.Value = value; }
    public bool ChildForceExpandHeight { get => _childForceExpandHeight.Value; set => _childForceExpandHeight.Value = value; }
    public bool ChildControlWidth { get => _childControlWidth.Value; set => _childControlWidth.Value = value; }
    public bool ChildControlHeight { get => _childControlHeight.Value; set => _childControlHeight.Value = value; }
    public bool ChildScaleWidth { get => _childScaleWidth.Value; set => _childScaleWidth.Value = value; }
    public bool ChildScaleHeight { get => _childScaleHeight.Value; set => _childScaleHeight.Value = value; }
    public LayoutDirection Direction { get => _layoutDirection.Value; set => _layoutDirection.Value = value; }
    
    public override Utf8String Type => Direction == LayoutDirection.Horizontal ? JsonDefaults.DirectionalLayout.HorizontalType : JsonDefaults.DirectionalLayout.VerticalType;
    public override ComponentType ComponentType => ComponentType.DirectionalLayout;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.DirectionalLayout.SpacingName, _spacing, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandWidthName, _childForceExpandWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildForceExpandHeightName, _childForceExpandHeight, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlWidthName, _childControlWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildControlHeightName, _childControlHeight, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleWidthName, _childScaleWidth, mode);
        writer.AddField(JsonDefaults.DirectionalLayout.ChildScaleHeightName, _childScaleHeight, mode);
    }

    public DirectionalLayoutComponent SetSpacing(float spacing)
    {
        Spacing = spacing;
        return this;
    }

    public DirectionalLayoutComponent SetChildForceExpandWidth(bool childForceExpandWidth)
    {
        ChildForceExpandWidth = childForceExpandWidth;
        return this;
    }
    
    public DirectionalLayoutComponent SetChildForceExpandHeight(bool childForceExpandHeight)
    {
        ChildForceExpandHeight = childForceExpandHeight;
        return this;
    }

    public DirectionalLayoutComponent SetChildControlWidth(bool childControlWidth)
    {
        ChildControlWidth = childControlWidth;
        return this;
    }

    public DirectionalLayoutComponent SetChildControlHeight(bool childControlHeight)
    {
        ChildControlHeight = childControlHeight;
        return this;
    }

    public DirectionalLayoutComponent SetChildScaleWidth(bool childScaleWidth)
    {
        ChildScaleWidth = childScaleWidth;
        return this;
    }

    public DirectionalLayoutComponent SetChildScaleHeight(bool childScaleHeight)
    {
        ChildScaleHeight = childScaleHeight;
        return this;
    }
    
    public DirectionalLayoutComponent SetLayoutDirection(LayoutDirection layoutDirection)
    {
        Direction = layoutDirection;
        return this;
    }

    public override bool HasChanged()
    {
        return _spacing.HasChanged
               || _childForceExpandWidth.HasChanged
               || _childForceExpandHeight.HasChanged
               || _childControlWidth.HasChanged
               || _childControlHeight.HasChanged
               || _childScaleWidth.HasChanged
               || _childScaleHeight.HasChanged
               || _layoutDirection.HasChanged;
    }
    
    public override void ResetHasChanged()
    {
        _spacing.ResetHasChanged();
        _childForceExpandWidth.ResetHasChanged();
        _childForceExpandHeight.ResetHasChanged();
        _childControlWidth.ResetHasChanged();
        _childControlHeight.ResetHasChanged();
        _childScaleWidth.ResetHasChanged();
        _childScaleHeight.ResetHasChanged();
        _layoutDirection.ResetHasChanged();
    }

    public override void Reset()
    {
        base.Reset();
        _spacing.Reset();
        _childForceExpandWidth.Reset();
        _childForceExpandHeight.Reset();
        _childControlWidth.Reset();
        _childControlHeight.Reset();
        _childScaleWidth.Reset();
        _childScaleHeight.Reset();
        _layoutDirection.Reset();
    }
}