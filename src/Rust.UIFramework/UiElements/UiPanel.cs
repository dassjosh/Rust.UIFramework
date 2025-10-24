using System;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

using ImageType = UnityEngine.UI.Image.Type;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiPanel))]
public partial class UiPanel : BaseUiComponent, IUiPanel
{
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
}