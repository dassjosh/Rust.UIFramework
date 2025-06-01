using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiLabelTests() : BaseTheoryUiElementsTests<UiLabel, UiLabelTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(TextAnchor Align, VerticalWrapMode VerticalOverflow);
    
    private static readonly TextComponent Text = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        FontSize = 20,
        Font = UiFontCache.Lcd,
        Align = TextAnchor.MiddleCenter,
        Text = "text",
        VerticalOverflow = VerticalWrapMode.Overflow,
    };

    private static void PopulateFluent(UiLabel label, TheoryRow row)
    {
        label
            .SetColor(Text.Color)
            .SetFadeIn(Text.FadeIn)
            .SetFontSize(Text.FontSize)
            .SetFont(Text.Font)
            .SetTextAlign(row.Align)
            .SetText(Text.Text)
            .SetVerticalOverflow(row.VerticalOverflow);
    }

    private static void PopulateSetters(UiLabel label, TheoryRow row)
    {
        label.Color = Text.Color;
        label.FadeIn = Text.FadeIn;
        label.FontSize = Text.FontSize;
        label.Font = Text.Font;
        label.Align = row.Align;
        label.TextValue = Text.Text;
        label.VerticalOverflow = row.VerticalOverflow;
    }

    protected override void AssertValues(UiLabel element, TheoryRow row)
    {
        element.Color.Should().Be(Text.Color);
        element.FadeIn.Should().Be(Text.FadeIn);
        element.FontSize.Should().Be(Text.FontSize);
        element.Font.Should().Be(Text.Font);
        element.Align.Should().Be(row.Align);
        element.TextValue.Should().Be(Text.Text);
        element.VerticalOverflow.Should().Be(row.VerticalOverflow);
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, TextAnchor, VerticalWrapMode>((align, wrap) => new TheoryRow(align, wrap));
}