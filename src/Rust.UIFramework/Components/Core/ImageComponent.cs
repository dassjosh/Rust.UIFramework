using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class ImageComponent : CoreComponent, IGraphicalComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Color), nameof(JsonDefaults.Color.ColorValue))]
    public partial UiColor Color { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Common), nameof(JsonDefaults.Common.FadeIn))]
    public partial float FadeIn { get; set; }
    
    [TrackedDefaults(null, null, typeof(JsonDefaults.BaseImage), nameof(JsonDefaults.BaseImage.Sprite))]
    public partial string Sprite { get; set; }
    
    [TrackedDefaults(null, null, typeof(JsonDefaults.BaseImage), nameof(JsonDefaults.BaseImage.Material))]
    public partial string Material { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.ImageType))]
    public partial Image.Type ImageType { get; set; }
    
    public partial UiReference PlaceholderFor { get; set; }
    
    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.FillCenter))]
    public partial bool FillCenter { get; set; }

    [TrackedDefaults(typeof(JsonDefaults.Image), nameof(JsonDefaults.Image.PixelPerUnitMultiplier))]
    public partial float PixelPerUnitMultiplier { get; set; }

    public partial string Png { get; set; }
    
    public override Utf8String Type => JsonDefaults.Image.Type;
    public override ComponentType ComponentType => ComponentType.Image;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        writer.AddField(JsonDefaults.BaseImage.SpriteName, SpriteTracked, mode);
        writer.AddField(JsonDefaults.Common.FadeInName, FadeInTracked, mode);
        writer.AddField(JsonDefaults.BaseImage.MaterialName, MaterialTracked, mode);
        writer.AddField(JsonDefaults.Color.ColorName, ColorTracked, mode);
        writer.AddField(JsonDefaults.Image.ImageTypeName, ImageTypeTracked, mode);
        writer.AddField(JsonDefaults.Image.FillCenterName, FillCenterTracked, mode);
        writer.AddField(JsonDefaults.Image.PixelPerUnitMultiplierName, FillCenterTracked, mode);
        writer.AddField(JsonDefaults.Image.PngName, PngTracked, mode);
        if (PlaceholderFor.IsValidName())
        {
            writer.AddField(JsonDefaults.Common.PlaceholderInputId, PlaceholderFor.Name);
        }
    }
}