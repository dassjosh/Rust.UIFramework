using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface ILayoutComponent : ISubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Layout), nameof(JsonDefaults.Layout.ChildAlignment))]
    TextAnchor ChildAlignment { get; set; }
    UiPadding Padding { get; set; }
}