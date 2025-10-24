using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IRectTransformComponent))]
[GenerateBuilderMethods]
public partial class RectTransformComponent : SubComponent, IRectTransformComponent
{
    public override Utf8String Type => JsonDefaults.Common.RectTransformName;
    public override ComponentType ComponentType => ComponentType.RectTransform;
    public override bool AllowMultiple => false;


    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(_position, mode);

        if (_padding.ShouldSerialize(mode))
        {
            Offset += _padding.Value;
        }
        
        writer.AddField(_offset, mode);
        writer.AddField(JsonDefaults.RectTransform.RotationName, _rotation, mode);
        writer.AddField(JsonDefaults.RectTransform.SetParentName, _changeParent, mode);
        writer.AddField(JsonDefaults.RectTransform.SetTransformIndexName, _transformIndex, mode);
    }
}