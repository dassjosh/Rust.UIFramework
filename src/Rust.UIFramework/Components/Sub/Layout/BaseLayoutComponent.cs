using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ILayoutComponent))]
public abstract partial class BaseLayoutComponent : SubComponent, ILayoutComponent
{
    public BaseUiComponent Owner { get; internal set; }
    public UiReference Reference => Owner.Reference;
    
    public override bool AllowMultiple => false;

    public override void Reset()
    {
        base.Reset();
        Owner = null;
    }
}