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
    
    [TrackedDefaults(typeof(UiOffset), nameof(UiOffset.None), typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.FpOffset))]
    UiOffset Offset { get; set; }
    
    [TrackedDefaults(typeof(UiPadding), nameof(UiPadding.None))]
    UiPadding PositionPadding { get; set; }
    
    [TrackedDefaults(typeof(UiPadding), nameof(UiPadding.None))]
    UiPadding OffsetPadding { get; set; }
        
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Scale))]
    UiScale PositionScale { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Scale))]
    UiScale OffsetScale { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Translate))]
    UiTranslate PositionTranslate { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Translate))]
    UiTranslate OffsetTranslate { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.Rotation))]
    UiRotation Rotation { get; set; }
    
    string ChangeParent { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.RectTransform), nameof(JsonDefaults.RectTransform.SetTransformIndex))]
    int TransformIndex { get; set; }
}