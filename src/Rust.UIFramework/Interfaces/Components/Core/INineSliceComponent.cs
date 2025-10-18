using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface INineSliceComponent : IImageComponent
{
    public string Png { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.Slice))]
    public UiBorderWidth Slice { get; set; }
}