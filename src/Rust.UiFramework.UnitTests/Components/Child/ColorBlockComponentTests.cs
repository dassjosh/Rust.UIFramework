using Oxide.Ext.UiFramework.Components;

namespace Rust.UiFramework.UnitTests.Components.Child;

public class ColorBlockComponentTests() : BasePopulateComponentTests<ColorBlockComponent>(ChildComponentHelpers.PopulateColorBlock)
{
    protected override bool VerifyAsJson => false;
}