using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IScrollViewContentComponent
{
    [TrackedDefaults(typeof(UiPosition), nameof(UiPosition.Full))]
    public UiPosition Position { get; set; }
    public UiOffset Offset { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ScrollView), nameof(JsonDefaults.ScrollView.Pivot))]
    public Vector2 Pivot { get; set; }
}