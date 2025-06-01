using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.UnitTests.Global.Generators;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiScrollViewTests() : BaseTheoryUiElementsTests<UiScrollView, UiScrollViewTests.TheoryRow>(PopulateFluent, PopulateSetters)
{
    public record TheoryRow(bool Horizontal, bool Vertical);
    
    private static readonly ScrollViewComponent Scroll = new()
    {
        Elasticity = 100f,
        MovementType = ScrollRect.MovementType.Elastic,
        Inertia = true,
        DecelerationRate = 200f,
        ScrollSensitivity = 300f
    };
    
    private static readonly ScrollViewContentComponent Content = new()
    {
        Position = new UiPosition(0.1f, 0.2f, 0.3f, 0.4f),
        Offset = new UiOffset(100, 200, 300, 400)
    };
    
    private static readonly ScrollbarComponent Horizontal = new()
    {
        Invert = true,
        AutoHide = true,
        HandleSprite = UiSprites.Icons.Add,
        TrackSprite = UiSprites.Icons.Ammunition,
        Size = 50f,
        HandleColor = UiColors.Green,
        HighlightColor = UiColors.Blue,
        PressedColor = UiColors.Purple,
        TrackColor = UiColors.Magenta
    };
    
    private static readonly ScrollbarComponent Vertical = new()
    {
        Invert = false,
        AutoHide = false,
        HandleSprite = UiSprites.Icons.Subtract,
        TrackSprite = UiSprites.Icons.Bleeding,
        Size = 60f,
        HandleColor = UiColors.Cyan,
        HighlightColor = UiColors.Lime,
        PressedColor = UiColors.Navy,
        TrackColor = UiColors.Olive
    };

    private static void PopulateFluent(UiScrollView scroll, TheoryRow row)
    {
        scroll
            .SetElasticity(Scroll.Elasticity)
            .SetMovementType(Scroll.MovementType)
            .SetInertia(Scroll.Inertia)
            .SetDecelerationRate(Scroll.DecelerationRate)
            .SetScrollSensitivity(Scroll.ScrollSensitivity)
            .SetContentPosition(Content.Position)
            .SetContentOffset(Content.Offset);
        if (row.Horizontal)
        {
            scroll.SetHorizontalScrollbar()
                .SetScrollbarInvert(ScrollbarTypes.Horizontal, Horizontal.Invert)
                .SetScrollbarAutoHide(ScrollbarTypes.Horizontal, Horizontal.AutoHide)
                .SetScrollbarHandleSprite(ScrollbarTypes.Horizontal, Horizontal.HandleSprite)
                .SetScrollbarTrackSprite(ScrollbarTypes.Horizontal, Horizontal.TrackSprite)
                .SetScrollbarSize(ScrollbarTypes.Horizontal, Horizontal.Size)
                .SetScrollbarHandleColor(ScrollbarTypes.Horizontal, Horizontal.HandleColor)
                .SetScrollbarHighlightColor(ScrollbarTypes.Horizontal, Horizontal.HighlightColor)
                .SetScrollbarPressedColor(ScrollbarTypes.Horizontal, Horizontal.PressedColor)
                .SetScrollbarTrackColor(ScrollbarTypes.Horizontal, Horizontal.TrackColor);
        }
        
        if (row.Vertical)
        {
            scroll.SetVerticalScrollbar()
                .SetScrollbarInvert(ScrollbarTypes.Vertical, Vertical.Invert)
                .SetScrollbarAutoHide(ScrollbarTypes.Vertical, Vertical.AutoHide)
                .SetScrollbarHandleSprite(ScrollbarTypes.Vertical, Vertical.HandleSprite)
                .SetScrollbarTrackSprite(ScrollbarTypes.Vertical, Vertical.TrackSprite)
                .SetScrollbarSize(ScrollbarTypes.Vertical, Vertical.Size)
                .SetScrollbarHandleColor(ScrollbarTypes.Vertical, Vertical.HandleColor)
                .SetScrollbarHighlightColor(ScrollbarTypes.Vertical, Vertical.HighlightColor)
                .SetScrollbarPressedColor(ScrollbarTypes.Vertical, Vertical.PressedColor)
                .SetScrollbarTrackColor(ScrollbarTypes.Vertical, Vertical.TrackColor);
        }
    }

    private static void PopulateSetters(UiScrollView scroll, TheoryRow row)
    {
        scroll.Elasticity = Scroll.Elasticity;
        scroll.MovementType = Scroll.MovementType;
        scroll.Inertia = Scroll.Inertia;
        scroll.DecelerationRate = Scroll.DecelerationRate;
        scroll.ScrollSensitivity = Scroll.ScrollSensitivity;
        scroll.ContentPosition = Content.Position;
        scroll.ContentOffset = Content.Offset;
        
        if (row.Horizontal)
        {
            scroll.AddHorizontalScrollBar(Horizontal.Invert, Horizontal.AutoHide, Horizontal.HandleSprite, Horizontal.TrackSprite, Horizontal.Size, Horizontal.HandleColor, Horizontal.HighlightColor, Horizontal.PressedColor, Horizontal.TrackColor);
        }
        
        if (row.Vertical)
        {
            scroll.AddVerticalScrollBar(Vertical.Invert, Vertical.AutoHide, Vertical.HandleSprite, Vertical.TrackSprite, Vertical.Size, Vertical.HandleColor, Vertical.HighlightColor, Vertical.PressedColor, Vertical.TrackColor);
        }
    }

    protected override void AssertValues(UiScrollView element, TheoryRow row)
    {
        element.Elasticity.Should().Be(Scroll.Elasticity);
        element.MovementType.Should().Be(Scroll.MovementType);
        element.Inertia.Should().Be(Scroll.Inertia);
        element.DecelerationRate.Should().Be(Scroll.DecelerationRate);
        element.ScrollSensitivity.Should().Be(Scroll.ScrollSensitivity);
    }

    public static TheoryData<TheoryRow> TheoryData =>
        TheoryDataGenerator.Generate((bool horizontal, bool vertical) => new TheoryRow(horizontal, vertical));
}