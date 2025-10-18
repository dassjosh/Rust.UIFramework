using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IDraggableComponent))]
[GenerateBuilderMethods]
public partial class DraggableComponent : SubComponent, IDraggableComponent
{
    public override Utf8String Type => JsonDefaults.Draggable.Type;
    public override ComponentType ComponentType => ComponentType.Draggable;
    public override bool AllowMultiple => false;
    
    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Draggable.LimitToParentName, _limitToParent, mode);
        writer.AddField(JsonDefaults.Draggable.MaxDistanceName, _maxDistance, mode);
        writer.AddField(JsonDefaults.Draggable.AllowSwappingName, _allowSwapping, mode);
        writer.AddField(JsonDefaults.Draggable.DropAnywhereName, _dropAnywhere, mode);
        writer.AddField(JsonDefaults.Draggable.DragAlphaName, _dragAlpha, mode);
        writer.AddField(JsonDefaults.Draggable.ParentLimitIndexName, _parentLimitIndex, mode);
        writer.AddField(JsonDefaults.Draggable.FilterName, _filter, mode);
        writer.AddField(JsonDefaults.Draggable.ParentPaddingName, _parentPadding, mode);
        writer.AddField(JsonDefaults.Draggable.AnchorOffsetName, _anchorOffset, mode);
        writer.AddField(JsonDefaults.Draggable.KeepOnTopName, _keepOnTop, mode);
        writer.AddField(JsonDefaults.Draggable.PositionRpcName, _positionRpc, mode);
        writer.AddKeyField(JsonDefaults.Draggable.MoveToAnchorName, _moveToAnchor.ShouldSerialize(mode) && _moveToAnchor.Value);
        writer.AddKeyField(JsonDefaults.Draggable.RebuildAnchorName, _rebuildAnchor.ShouldSerialize(mode) && _rebuildAnchor.Value);
    }
}