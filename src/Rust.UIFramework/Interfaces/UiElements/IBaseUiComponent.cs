using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

[SkipBuilder]
[SkipComponentField]
public interface IBaseUiComponent
{
    [PropertyTarget(null, PropertyTargetType.Self)]
    UiReference Reference { get; set; }
    
    [Tracked]
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeOut))]
    float FadeOut { get; set; }
    
    [PropertyTarget(null, PropertyTargetType.Self)]
    UpdateMode Update { get; set; }
    
    [Tracked]
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.Active))]
    bool Active { get; set; }
    
    [SkipProperty]
    string Name { get; set; }
    
    [SkipProperty]
    string Parent { get; set; }
    
    [PropertyTarget(nameof(BaseUiComponent.Component), PropertyTargetType.Field)]
    bool Enabled { get; set; }
    
    [PropertyTarget(nameof(BaseUiComponent.RectTransform), PropertyTargetType.Property)]
    UiPosition Position { get; set; }
    
    [PropertyTarget(nameof(BaseUiComponent.RectTransform), PropertyTargetType.Property)]
    UiOffset Offset { get; set; }
    
    [PropertyTarget(nameof(BaseUiComponent.RectTransform), PropertyTargetType.Property)]
    UiRotation Rotation { get; set; }
    
    [PropertyTarget(nameof(BaseUiComponent.RectTransform), PropertyTargetType.Property)]
    UiPadding Padding { get; set; }
}