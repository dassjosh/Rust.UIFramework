using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class DraggableComponentSerializer : SubComponentSerializer<DraggableComponent>
{
    protected override void SerializeComponent(JsonFrameworkWriter writer, DraggableComponent component, DraggableComponent defaults, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.Draggable.LimitToParentName, component.LimitToParent, defaults.LimitToParent);
        writer.AddField(JsonDefaults.Draggable.MaxDistanceName, component.MaxDistance, defaults.MaxDistance);
        writer.AddField(JsonDefaults.Draggable.AllowSwappingName, component.AllowSwapping, defaults.AllowSwapping);
        writer.AddField(JsonDefaults.Draggable.DropAnywhereName, component.DropAnywhere, defaults.DropAnywhere);
        writer.AddField(JsonDefaults.Draggable.DragAlphaName, component.DragAlpha, defaults.DragAlpha);
        writer.AddField(JsonDefaults.Draggable.ParentLimitIndexName, component.ParentLimitIndex, defaults.ParentLimitIndex);
        writer.AddField(JsonDefaults.Draggable.FilterName, component.Filter, defaults.Filter);
        writer.AddField(JsonDefaults.Draggable.ParentPaddingName, component.ParentPadding, defaults.ParentPadding);
        writer.AddField(JsonDefaults.Draggable.AnchorOffsetName, component.AnchorOffset, defaults.AnchorOffset);
        writer.AddField(JsonDefaults.Draggable.KeepOnTopName, component.KeepOnTop, defaults.KeepOnTop);
        writer.AddField(JsonDefaults.Draggable.PositionRpcName, component.PositionRpc, defaults.PositionRpc);
        if (mode == SerializeMode.Update)
        {
            writer.AddKeyField(JsonDefaults.Draggable.MoveToAnchorName, component.MoveToAnchor);
            writer.AddKeyField(JsonDefaults.Draggable.RebuildAnchorName, component.RebuildAnchor);
        }
    }
}