using Oxide.Ext.UiFramework.Components;
using Rust.UiFramework.UnitTests.Global.Generators;

namespace Rust.UiFramework.UnitTests.Components.Core;

public class RawRawImageComponentTests() : BaseTheoryComponentTests<RawImageComponent, RawRawImageComponentTests.TheoryRow>(ComponentHelpers.PopulateRawImage)
{
    public record TheoryRow(string Image);
    
    protected override void PopulateTheory(RawImageComponent component, TheoryRow row)
    {
        component.Image = row.Image;
    }
    
    public static TheoryData<TheoryRow> TheoryData => 
        TheoryDataGenerator.Generate(x => new TheoryRow(x), UnitTestsConstants.RawImages);
}