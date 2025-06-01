using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiImageTests() : BaseTheoryUiElementsTests<UiImage, UiImageTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(Image.Type ImageType);
    
    private static readonly UiImage Image = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Sprite = UiSprites.Icons.Authorize,
        Material = UiMaterials.Icons.IconMaterial
    };

    private static void PopulateFluent(UiImage image, TheoryRow row)
    {
        image
            .SetColor(Image.Color)
            .SetFadeIn(Image.FadeIn)
            .SetSprite(Image.Sprite)
            .SetMaterial(Image.Material)
            .SetImageType(row.ImageType);
    }

    private static void PopulateSetters(UiImage image, TheoryRow row)
    {
        image.Color = Image.Color;
        image.FadeIn = Image.FadeIn;
        image.Sprite = Image.Sprite;
        image.Material = Image.Material;
        image.ImageType = row.ImageType;
    }

    protected override void AssertValues(UiImage element, TheoryRow row)
    {
        element.Color.Should().Be(Image.Color);
        element.FadeIn.Should().Be(Image.FadeIn);
        element.Sprite.Should().Be(Image.Sprite);
        element.Material.Should().Be(Image.Material);
        element.ImageType.Should().Be(row.ImageType);
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, Image.Type>(imageType => new TheoryRow(imageType));
}