using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(ILayoutElementComponent))]
[GenerateBuilderMethods]
public partial class LayoutElementComponent : SubComponent, ILayoutElementComponent
{
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
}