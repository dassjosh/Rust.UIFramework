using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiRawImage : BaseUiComponent, IMaterial<UiRawImage>, IFadeIn<UiRawImage>, IUiColor<UiRawImage>
{
    public partial string Image { get; set; }
    public partial UiReference PlaceholderFor { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
    public readonly RawImageComponent RawImage;
    
    public UiRawImage() : this(new RawImageComponent()) { }

    private UiRawImage(RawImageComponent component) : base(component)
    {
        RawImage = component;
    }
    
    public UiRawImage Init(string image, in UiColor color)
    {
        Color = color;
        Image = image;
        return this;
    }
    
    public UiRawImage SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
}