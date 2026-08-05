using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.Components.Core;

public class ButtonComponentTests() : BaseTheoryComponentTests<ButtonComponent, ButtonComponentTests.TheoryRow>(ComponentHelpers.PopulateButton)
{
    public record TheoryRow(ButtonType ButtonType, bool AddColorBlock);
    
    protected override void PopulateTheory(ButtonComponent component, TheoryRow row)
    {
        component.ButtonType = row.ButtonType;
        if(row.AddColorBlock)
        {
            component.AddColorBlock(UiColors.Blue, UiColors.Orange, UiColors.Green, UiColors.Magenta, 0.5f, 1.5f);
        }    
    }

    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate<TheoryRow, ButtonType, bool>((buttonType, addColorBlock) => new TheoryRow(buttonType, addColorBlock));
}