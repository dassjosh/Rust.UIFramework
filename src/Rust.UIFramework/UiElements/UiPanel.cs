using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using ImageType = UnityEngine.UI.Image.Type;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiPanel : BaseUiComponent, IImageType<UiPanel>, ISprite<UiPanel>, IMaterial<UiPanel>, IFadeIn<UiPanel>, IUiColor<UiPanel>
{
    public partial UiReference PlaceholderFor { get; set; }
    public partial bool FillCenter { get; set; }
    public partial ImageType ImageType { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    public partial string Png { get; set; }
    
    public readonly ImageComponent Image;

    public UiPanel() : this(new ImageComponent()) { }

    private UiPanel(ImageComponent component) : base(component)
    {
        Image = component;
    }
    
    [Obsolete("Use SetSprite().SetMaterial().SetImageType() instead.")]
    public UiPanel SetSpriteMaterialImage(string sprite = null, string material = null, ImageType type = ImageType.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
        return this;
    }
    
    public UiPanel SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);

    public void AddBorderRadius(in UiBorderRadius radius, bool antialiasing = true, float edgeWidth = 1) => AddBorderRadius(new UiDimensions2D(200, 200), radius, antialiasing, edgeWidth);

    public void AddBorderRadius(UiDimensions2D size, in UiBorderRadius radius, bool antialiasing = true, float edgeWidth = 1)
    {
        Png = Singleton<UiImageStorage>.Instance.GetBorderRadius(PluginPool?.PluginId.UiPlugin, size, radius, antialiasing, edgeWidth);
    }
}