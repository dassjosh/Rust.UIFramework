using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseDirectionalLayoutComponent : BaseLayoutComponent
{
    private readonly TrackedValue<float> _spacing = new(JsonDefaults.DirectionalLayout.Spacing);
    private readonly TrackedValue<bool> _childForceExpandWidth = new(JsonDefaults.DirectionalLayout.ChildControlWidth);
    private readonly TrackedValue<bool> _childForceExpandHeight = new(JsonDefaults.DirectionalLayout.ChildControlHeight);
    private readonly TrackedValue<bool> _childControlWidth = new(JsonDefaults.DirectionalLayout.ChildControlWidth);
    private readonly TrackedValue<bool> _childControlHeight = new(JsonDefaults.DirectionalLayout.ChildControlHeight);
    private readonly TrackedValue<bool> _childScaleWidth = new(JsonDefaults.DirectionalLayout.ChildScaleWidth);
    private readonly TrackedValue<bool> _childScaleHeight = new(JsonDefaults.DirectionalLayout.ChildScaleHeight);
    
    public float Spacing { get => _spacing.Value; set => _spacing.Value = value; }
    public bool ChildForceExpandWidth { get => _childForceExpandWidth.Value; set => _childForceExpandWidth.Value = value; }
    public bool ChildForceExpandHeight { get => _childForceExpandHeight.Value; set => _childForceExpandHeight.Value = value; }
    public bool ChildControlWidth { get => _childControlWidth.Value; set => _childControlWidth.Value = value; }
    public bool ChildControlHeight { get => _childControlHeight.Value; set => _childControlHeight.Value = value; }
    public bool ChildScaleWidth { get => _childScaleWidth.Value; set => _childScaleWidth.Value = value; }
    public bool ChildScaleHeight { get => _childScaleHeight.Value; set => _childScaleHeight.Value = value; }

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

    public override bool HasChanged()
    {
        return _spacing.HasChanged || _childForceExpandWidth.HasChanged || _childForceExpandHeight.HasChanged || _childControlWidth.HasChanged || _childControlHeight.HasChanged || _childScaleWidth.HasChanged || _childScaleHeight.HasChanged;
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