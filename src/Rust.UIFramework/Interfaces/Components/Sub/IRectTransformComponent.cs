using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IRectTransformComponent : ISubComponent
{
    [TrackedDefaults(typeof(UiPosition), nameof(UiPosition.Full))]
    UiPosition Position { get; set; }
    UiOffset Offset { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Rotation))]
    UiRotation Rotation { get; set; }
    UiPadding Padding { get; set; }
    string ChangeParent { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.SetTransformIndex))]
    int TransformIndex { get; set; }
}