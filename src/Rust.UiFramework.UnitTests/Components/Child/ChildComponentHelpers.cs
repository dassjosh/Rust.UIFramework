using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Components.Child;

public static class ChildComponentHelpers
{
    public static void PopulateColorBlock(ColorBlockComponent block)
    {
        block.HighlightedColor = UiColors.Red;
        block.PressedColor = UiColors.Green;
        block.SelectedColor = UiColors.Blue;
        block.ColorMultiplier = 0.5f;
        block.FadeDuration = 1.5f;
    }
    
    public static void PopulateScrollBar(ScrollbarComponent scrollbar)
    {
        scrollbar.Invert = JsonDefaults.ScrollBar.Invert;
        scrollbar.AutoHide = JsonDefaults.ScrollBar.AutoHide;
        scrollbar.HandleSprite = UiSprites.Icons.Add;
        scrollbar.TrackSprite = UiSprites.Icons.Subtract;
        scrollbar.Size = 30f;
        scrollbar.HandleColor = UiColors.Black;
        scrollbar.HighlightColor = UiColors.Maroon;
        scrollbar.PressedColor = UiColors.Navy;
        scrollbar.TrackColor = UiColors.Olive;
    }
    
    public static void PopulateScrollViewContent(ScrollViewContentComponent content)
    {
        content.Position = new UiPosition(0.25f, 0.25f, 0.75f, 0.75f);
        content.Offset = new UiOffset(100f, 200f, 300f, 400f);
        content.Pivot = new Vector2(0.25f, 0.75f);
    }
}