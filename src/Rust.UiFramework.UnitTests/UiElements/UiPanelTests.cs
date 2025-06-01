using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiPanelTests() : BasePopulateUiElementsTests<UiPanel>(PopulateFluent, PopulateSetters)
{
    private static readonly ImageComponent Image = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Sprite = UiSprites.Icons.Authorize,
        Material = UiMaterials.Icons.IconMaterial,
        ImageType = UnityEngine.UI.Image.Type.Tiled
    };

    private static void PopulateFluent(UiPanel image)
    {
        image
            .SetColor(Image.Color)
            .SetFadeIn(Image.FadeIn)
            .SetSprite(Image.Sprite)
            .SetMaterial(Image.Material)
            .SetImageType(Image.ImageType);
    }

    private static void PopulateSetters(UiPanel image)
    {
        image.Color = Image.Color;
        image.FadeIn = Image.FadeIn;
        image.Sprite = Image.Sprite;
        image.Material = Image.Material;
        image.ImageType = Image.ImageType;
    }

    protected override void AssertValues(UiPanel element)
    {
        element.Color.Should().Be(Image.Color);
        element.FadeIn.Should().Be(Image.FadeIn);
        element.Sprite.Should().Be(Image.Sprite);
        element.Material.Should().Be(Image.Material);
        element.ImageType.Should().Be(Image.ImageType);
    }
}