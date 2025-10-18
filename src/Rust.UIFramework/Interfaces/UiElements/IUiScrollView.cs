using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IUiScrollView : IBaseUiComponent
{
    ScrollRect.MovementType MovementType { get; set; }
    float Elasticity { get; set; }
    bool Inertia { get; set; }
    float DecelerationRate { get; set; }
    float ScrollSensitivity { get; set; }
    float HorizontalScrollProgress { get; set; }
    float VerticalScrollProgress { get; set; }
    
    [PropertyTarget(nameof(UiScrollView.GetOrCreateContentTransform), PropertyTargetType.Method)]
    [PropertyName(nameof(ScrollViewContentComponent.Position))]
    UiPosition ContentPosition { get; set; }
    
    [PropertyTarget(nameof(UiScrollView.GetOrCreateContentTransform), PropertyTargetType.Method)]
    [PropertyName(nameof(ScrollViewContentComponent.Offset))]
    UiOffset ContentOffset { get; set; }
    
    [PropertyTarget(nameof(UiScrollView.GetOrCreateContentTransform), PropertyTargetType.Method)]
    [PropertyName(nameof(ScrollViewContentComponent.Pivot))]
    Vector2 ContentPivot { get; set; }
}