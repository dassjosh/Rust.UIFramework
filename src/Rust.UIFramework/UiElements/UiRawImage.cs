using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiRawImage))]
public partial class UiRawImage : BaseUiComponent, IUiRawImage
{
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