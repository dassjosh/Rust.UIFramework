using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.Components.Core;

public class ScrollViewComponentTests() : BaseTheoryComponentTests<ScrollViewComponent, ScrollViewComponentTests.TheoryRow>(ComponentHelpers.PopulateScrollView)
{
    public record TheoryRow(bool Horizontal, bool Vertical);
    
    protected override void PopulateTheory(ScrollViewComponent component, TheoryRow row)
    {
        if (row.Horizontal)
        {
            component.AddHorizontalScrollBar(true, true, UiSprites.Icons.Add, UiSprites.Icons.Ammunition, 50f, UiColors.Green, UiColors.Blue, UiColors.Purple, UiColors.Magenta);
        }
        
        if (row.Vertical)
        {
            component.AddVerticalScrollBar(false, false, UiSprites.Icons.Subtract, UiSprites.Icons.Bleeding, 60f, UiColors.Cyan, UiColors.Lime, UiColors.Navy, UiColors.Olive);
        }
    }
    
    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, bool, bool>((horizontal, vertical) => new TheoryRow(horizontal, vertical));
}