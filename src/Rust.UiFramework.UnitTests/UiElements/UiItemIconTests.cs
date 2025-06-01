using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiItemIconTests() : BaseTheoryUiElementsTests<UiItemIcon, UiItemIconTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(int ItemId, ulong SkinId);
    
    private static readonly ItemIconComponent Input = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Material = UiMaterials.Icons.IconMaterial,
        ImageType = Image.Type.Tiled
    };

    private static void PopulateFluent(UiItemIcon input, TheoryRow row)
    {
        input
            .SetColor(Input.Color)
            .SetFadeIn(Input.FadeIn)
            .SetMaterial(Input.Material)
            .SetImageType(Input.ImageType)
            .SetItemId(row.ItemId)
            .SetSkinId(row.SkinId);
    }

    private static void PopulateSetters(UiItemIcon input, TheoryRow row)
    {
        input.Color = Input.Color;
        input.FadeIn = Input.FadeIn;
        input.Material = Input.Material;
        input.ImageType = Input.ImageType;
        input.ItemId = row.ItemId;
        input.SkinId = row.SkinId;
    }

    protected override void AssertValues(UiItemIcon element, TheoryRow row)
    {
        element.Color.Should().Be(Input.Color);
        element.FadeIn.Should().Be(Input.FadeIn);
        element.Material.Should().Be(Input.Material);
        element.ImageType.Should().Be(Input.ImageType);
        element.ItemId.Should().Be(row.ItemId);
        element.SkinId.Should().Be(row.SkinId);
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, int, ulong>((itemId, skinId) => new TheoryRow(itemId, skinId), [123], [0, 456]);
}