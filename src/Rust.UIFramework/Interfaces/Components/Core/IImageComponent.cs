using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IImageComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    UiColor Color { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    float FadeIn { get; set; }
    
    [TrackedDefaults(null, null, typeof(JsonDefaults.BaseImage), nameof(JsonDefaults.BaseImage.Sprite))]
    string Sprite { get; set; }
    
    [TrackedDefaults(null, null, typeof(JsonDefaults.BaseImage), nameof(JsonDefaults.BaseImage.Material))]
    string Material { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.ImageType))]
    Image.Type ImageType { get; set; }
    
    UiReference PlaceholderFor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.FillCenter))]
    bool FillCenter { get; set; }
}