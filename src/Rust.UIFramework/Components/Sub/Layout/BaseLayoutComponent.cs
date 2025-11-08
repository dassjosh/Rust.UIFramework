using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ILayoutComponent))]
public abstract partial class BaseLayoutComponent : SubComponent, ILayoutComponent
{
    public BaseUiComponent Owner { get; internal set; }
    public UiReference Reference => Owner.Reference;
    
    public override bool AllowMultiple => false;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Layout.ChildAlignmentName, _childAlignment, mode);
        writer.AddField(JsonDefaults.Layout.PaddingName, _padding, mode);
    }

    public static implicit operator UiReference(BaseLayoutComponent layout) => layout.Reference;
    
    public override void Reset()
    {
        base.Reset();
        Owner = null;
    }
}