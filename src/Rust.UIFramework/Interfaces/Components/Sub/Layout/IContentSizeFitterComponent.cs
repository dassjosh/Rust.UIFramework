using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IContentSizeFitterComponent : ISubComponent
{
    [TrackedDefaults(typeof(JsonDefaults.ContentSizeFitterData), nameof(JsonDefaults.ContentSizeFitterData.HorizontalFit))]
    public ContentSizeFitter.FitMode HorizontalFit { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.ContentSizeFitterData), nameof(JsonDefaults.ContentSizeFitterData.VerticalFit))]
    public ContentSizeFitter.FitMode VerticalFit { get; set; }
}