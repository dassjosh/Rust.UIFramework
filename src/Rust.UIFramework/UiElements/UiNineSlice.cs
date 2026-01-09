using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

using ImageType = UnityEngine.UI.Image.Type;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiNineSlice : BaseUiComponent, IImageType<UiNineSlice>, ISprite<UiNineSlice>, IMaterial<UiNineSlice>, IFadeIn<UiNineSlice>, IUiColor<UiNineSlice>
{
    public partial string Png { get; set; }
    public partial UiBorderWidth Slice { get; set; }
    public partial UiReference PlaceholderFor { get; set; }
    public partial bool FillCenter { get; set; }
    public partial ImageType ImageType { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
    public readonly NineSliceComponent Image;
    
    public UiNineSlice() : this(new NineSliceComponent()) { }
    
    private UiNineSlice(NineSliceComponent component) : base(component)
    {
        Image = component;
    }
    
    public UiNineSlice Init(string png, in UiBorderWidth slice, bool fillCenter, UiColor color, Image.Type type)
    {
        Png = png;
        Slice = slice;
        FillCenter = fillCenter;
        Color = color;
        ImageType = type;
        return this;
    }
    
    [Obsolete("Use SetSprite().SetMaterial().SetImageType() instead.")]
    public UiNineSlice SetSpriteMaterialImage(string sprite = null, string material = null, ImageType type = ImageType.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
        return this;
    }
    
    public UiNineSlice SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
}