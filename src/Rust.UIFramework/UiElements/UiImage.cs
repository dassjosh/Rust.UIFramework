using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

using ImageType = UnityEngine.UI.Image.Type;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiImage))]
public partial class UiImage : BaseUiComponent, IUiImage
{
    public readonly ImageComponent Image;

    public UiImage() : this(new ImageComponent()) { }

    private UiImage(ImageComponent component) : base(component)
    {
        Image = component;
    }
    
    public UiImage Init(string sprite, UiColor color)
    {
        Color = color;
        Sprite = sprite;
        return this;
    }
    
    [Obsolete("Use SetSprite().SetMaterial().SetImageType() instead.")]
    public UiImage SetSpriteMaterialImage(string sprite = null, string material = null, ImageType type = ImageType.Simple)
    {
        Sprite = sprite;
        Material = material;
        ImageType = type;
        return this;
    }
    
    public UiImage SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
}