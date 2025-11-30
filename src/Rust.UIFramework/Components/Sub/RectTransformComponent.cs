using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
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
        if (_position.ShouldSerialize(mode) 
            || _positionPadding.ShouldSerialize(mode) 
            || _positionScale.ShouldSerialize(mode) 
            || _positionTranslate.ShouldSerialize(mode))
        {
            UiPosition position = Position.Translate(PositionTranslate).Scale(PositionScale).WithPadding(PositionPadding);
            writer.AddField(position, mode);
        }
        
        if (_offset.ShouldSerialize(mode) 
            || _offsetPadding.ShouldSerialize(mode) 
            || _offsetScale.ShouldSerialize(mode) 
            || _offsetTranslate.ShouldSerialize(mode))
        {
            UiOffset offset = Offset.Translate(OffsetTranslate).Scale(OffsetScale).WithPadding(OffsetPadding);
            writer.AddField(offset, mode);
        }
        
        writer.AddField(JsonDefaults.RectTransform.RotationName, _rotation, mode);
        writer.AddField(JsonDefaults.RectTransform.SetParentName, _changeParent, mode);
        writer.AddField(JsonDefaults.RectTransform.SetTransformIndexName, _transformIndex, mode);
    }
}