using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiInputTests() : BaseTheoryUiElementsTests<UiInput, UiInputTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(InputMode Mode);
    
    private static readonly InputComponent Input = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        FontSize = 20,
        Font = UiFonts.Lcd,
        Align = TextAnchor.MiddleCenter,
        Text = "text",
        VerticalOverflow = VerticalWrapMode.Overflow,
        CharsLimit = 16,
        Command = "command",
        LineType = InputField.LineType.MultiLineNewline
    };

    private static void PopulateFluent(UiInput input, TheoryRow row)
    {
        input
            .SetColor(Input.Color)
            .SetFadeIn(Input.FadeIn)
            .SetFontSize(Input.FontSize)
            .SetFont(Input.Font)
            .SetAlign(Input.Align)
            .SetTextValue(Input.Text)
            .SetVerticalOverflow(Input.VerticalOverflow)
            .SetCharsLimit(Input.CharsLimit)
            .SetCommand(Input.Command)
            .SetLineType(Input.LineType)
            .SetMode(row.Mode, true);
    }

    private static void PopulateSetters(UiInput input, TheoryRow row)
    {
        input.Color = Input.Color;
        input.FadeIn = Input.FadeIn;
        input.FontSize = Input.FontSize;
        input.Font = Input.Font;
        input.Align = Input.Align;
        input.TextValue = Input.Text;
        input.VerticalOverflow = Input.VerticalOverflow;
        input.CharsLimit = Input.CharsLimit;
        input.Command = Input.Command;
        input.LineType = Input.LineType;
        input.Mode = row.Mode;
    }

    protected override void AssertValues(UiInput element, TheoryRow row)
    {
        element.Color.Should().Be(Input.Color);
        element.FadeIn.Should().Be(Input.FadeIn);
        element.FontSize.Should().Be(Input.FontSize);
        element.Font.Should().Be(Input.Font);
        element.Align.Should().Be(Input.Align);
        element.TextValue.Should().Be(Input.Text);
        element.VerticalOverflow.Should().Be(Input.VerticalOverflow);
        element.CharsLimit.Should().Be(Input.CharsLimit);
        element.Command.Should().Be(Input.Command);
        element.LineType.Should().Be(Input.LineType);
        element.Mode.Should().Be(row.Mode);
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, InputMode>(inputMode => new TheoryRow(inputMode));
}