using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IGridLayoutComponent : ILayoutComponent
{
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.CellSize))]
    Vector2 CellSize { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.Spacing))]
    Vector2 Spacing { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.StartCorner))]
    GridLayoutGroup.Corner StartCorner { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.StartAxis))]
    GridLayoutGroup.Axis StartAxis { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.Constraint))]
    GridLayoutGroup.Constraint Constraint { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.GridLayout), nameof(JsonDefaults.GridLayout.ConstraintCount))]
    int ConstraintCount { get; set; }

}