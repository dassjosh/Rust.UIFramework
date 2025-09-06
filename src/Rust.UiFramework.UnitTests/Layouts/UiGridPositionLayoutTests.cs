using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Layouts.GridPositions;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Layouts;

public class UiGridPositionLayoutTests
{
    [Theory]
    [MemberData(nameof(InitTheoryData))]
    public void UiGridPositionLayout_AllValues_MatchExpected(GridPosition grid, int expectedElements)
    {
        //Arrange
        UiReference reference = new("parent", "name");
        using UiSection section = UnitTestHelpers.UnitTestPool.Get<UiSection>().SetReference(reference);
        using UiGridPositionLayout layout = UiGridPositionLayout.CreateBase<UiGridPositionLayout>(UnitTestHelpers.UnitTestPool, section);
        layout.Init(grid);
        
        //Act
        
        //Assert
        layout.Reference.Should().Be(reference);
        layout.ScrollView.Should().BeNull();
        layout.NumElements.Should().Be(expectedElements);
        layout.Elements.Count.Should().Be(0);
    }

    public static TheoryData<GridPosition, int> InitTheoryData() =>
    [
        (new GridPositionBuilder(1,1).Build(), 1),
        (new GridPositionBuilder(2,2).Build(), 4),
        (new GridPositionBuilder(3,2).Build(), 6),
        (new GridPositionBuilder(2,4).Build(), 8),
    ];

    [Theory]
    [MemberData(nameof(ExpectedPositionTheoryData))]
    public async Task UiGridPositionLayout_ElementPosition_MatchExpected(GridPosition grid)
    {
        //Arrange
        List<BaseUiComponent> elements = [];
        UiReference reference = new("parent", "name");
        using UiSection layoutSection = UnitTestHelpers.UnitTestPool.Get<UiSection>().SetReference(reference);
        using UiGridPositionLayout layout = UiGridPositionLayout.CreateBase<UiGridPositionLayout>(UnitTestHelpers.UnitTestPool, layoutSection);
        layout.Init(grid);
        
        //Act
        for (int i = 0; i < layout.NumElements; i++)
        {
            UiSection section = UnitTestHelpers.UnitTestPool.Get<UiSection>();
            elements.Add(section);
            layout.AddElement(section);
        }
        
        layout.CalculateElementPositions();
        
        //Assert
        await Verify(elements.Select(x => x.Position).ToArray());
        
        //Cleanup
        elements.FreeValues();
    }

    public static TheoryData<GridPosition> ExpectedPositionTheoryData() =>
    [
        (new GridPositionBuilder(1,1).Build()),
        (new GridPositionBuilder(2,2).Build()),
        (new GridPositionBuilder(3,3).Build()), 
        (new GridPositionBuilder(4,8).Build()),
        (new GridPositionBuilder(8,4).Build()),
        
        // (new GridPositionBuilder(1,1).SetHorizontalPadding(0.01f).SetVerticalPadding(0.02f).Build()),
        // (new GridPositionBuilder(2,2).SetHorizontalPadding(0.01f).SetVerticalPadding(0.02f).Build()),
        // (new GridPositionBuilder(3,3).SetHorizontalPadding(0.01f).SetVerticalPadding(0.02f).Build()),
        // (new GridPositionBuilder(4,8).SetHorizontalPadding(0.01f).SetVerticalPadding(0.02f).Build()),
        // (new GridPositionBuilder(8,4).SetHorizontalPadding(0.01f).SetVerticalPadding(0.02f).Build()),
        //
        // (new GridPositionBuilder(2,2).SetColWidth(2).Build()),
        // (new GridPositionBuilder(3,3).SetColWidth(2).Build()),
        // (new GridPositionBuilder(4,16).SetColWidth(2).Build()),
        // (new GridPositionBuilder(16,4).SetColWidth(2).Build()),
        //
        // (new GridPositionBuilder(2,2).SetRowHeight(2).Build()),
        // (new GridPositionBuilder(3,3).SetRowHeight(2).Build()),
        // (new GridPositionBuilder(4,16).SetRowHeight(2).Build()),
        // (new GridPositionBuilder(16,4).SetRowHeight(2).Build()),
    ];
}