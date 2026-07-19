using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiButtonTests() : BaseTheoryUiElementsTests<UiButton, UiButtonTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(ButtonType ButtonType, bool AddColorBlock);
    
    private static readonly ButtonComponent Button = new()
    {
        Command = "command",
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Sprite = UiSprites.Icons.Authorize,
        Material = UiMaterials.Icons.IconMaterial,
        ImageType = Image.Type.Sliced
    };
    
    private static readonly ColorBlockComponent ColorBlock = new()
    {
        HighlightedColor = UiColors.Blue,
        PressedColor = UiColors.Orange,
        SelectedColor = UiColors.Green,
        DisabledColor = UiColors.Magenta,
        ColorMultiplier = 2f,
        FadeDuration = 3f
    };

    private static void PopulateFluent(UiButton button, TheoryRow row)
    {
        button
            .SetColor(Button.Color)
            .SetFadeIn(Button.FadeIn)
            .SetSprite(Button.Sprite)
            .SetMaterial(Button.Material)
            .SetImageType(Button.ImageType);
        if (row.ButtonType == ButtonType.Command)
        {
            button.SetCommand(Button.Command);
        }
        else
        {
            button.SetClose(Button.Command);
        }
        
        if(row.AddColorBlock)
        {
            button.SetHighlightedColor(ColorBlock.HighlightedColor)
                .SetPressedColor(ColorBlock.PressedColor)
                .SetSelectedColor(ColorBlock.SelectedColor)
                .SetColorMultiplier(ColorBlock.ColorMultiplier)
                .SetFadeDuration(ColorBlock.FadeDuration);
        }
    }

    private static void PopulateSetters(UiButton button, TheoryRow row)
    {
        button.Color = Button.Color;
        button.FadeIn = Button.FadeIn;
        button.Sprite = Button.Sprite;
        button.Material = Button.Material;
        button.ImageType = Button.ImageType;
        button.Command = Button.Command;
        button.ButtonType = row.ButtonType;
        if(row.AddColorBlock)
        {
            button.AddColorBlock(ColorBlock.HighlightedColor, ColorBlock.PressedColor, ColorBlock.SelectedColor, ColorBlock.DisabledColor, ColorBlock.ColorMultiplier, ColorBlock.FadeDuration);
        }
    }

    protected override void AssertValues(UiButton element, TheoryRow row)
    {
        element.Color.Should().Be(Button.Color);
        element.FadeIn.Should().Be(Button.FadeIn);
        element.Sprite.Should().Be(Button.Sprite);
        element.Material.Should().Be(Button.Material);
        element.ImageType.Should().Be(Button.ImageType);
        element.Command.Should().Be(Button.Command);
        element.ButtonType.Should().Be(row.ButtonType);
        if(row.AddColorBlock)
        {
            element.ColorBlock.HighlightedColor.Should().Be(ColorBlock.HighlightedColor);
            element.ColorBlock.PressedColor.Should().Be(ColorBlock.PressedColor);
            element.ColorBlock.SelectedColor.Should().Be(ColorBlock.SelectedColor);
            element.ColorBlock.ColorMultiplier.Should().Be(ColorBlock.ColorMultiplier);
            element.ColorBlock.FadeDuration.Should().Be(ColorBlock.FadeDuration);
        }
        else
        {
            element.ColorBlock.Should().BeNull();
        }
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, ButtonType, bool>((buttonType, addColorBlock) => new TheoryRow(buttonType, addColorBlock));
}