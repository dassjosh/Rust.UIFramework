using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiRawImageTests() : BaseTheoryUiElementsTests<UiRawImage, UiRawImageTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(string Image);
    
    private static readonly RawImageComponent Image = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Material = UiMaterials.Icons.IconMaterial,
    };

    private static void PopulateFluent(UiRawImage image, TheoryRow row)
    {
        image
            .SetColor(Image.Color)
            .SetFadeIn(Image.FadeIn)
            .SetMaterial(Image.Material)
            .SetImage(row.Image);
    }

    private static void PopulateSetters(UiRawImage image, TheoryRow row)
    {
        image.Color = Image.Color;
        image.FadeIn = Image.FadeIn;
        image.Material = Image.Material;
        image.Image = row.Image;
    }

    protected override void AssertValues(UiRawImage element, TheoryRow row)
    {
        element.Color.Should().Be(Image.Color);
        element.FadeIn.Should().Be(Image.FadeIn);
        element.Material.Should().Be(Image.Material);
        element.Image.Should().Be(row.Image);
    }

    public static TheoryData<TheoryRow> TheoryData =>
        TheoryDataGenerator.Generate(x => new TheoryRow(x), UnitTestsConstants.RawImages);
}