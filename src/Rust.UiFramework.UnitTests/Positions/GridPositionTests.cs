using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.Positions;

public class GridPositionTests
{
    [Fact]    
    public void Constructor_SetsCorrectValues()
    {
        // Arrange
        GridPosition grid = new(1f, 2f, 3f, 4f, 0.1f, 0.2f, 2, 4);
        
        // Act
        UiPosition position = grid.ToPosition();
        
        // Assert
        grid.NumCols.Should().Be(2);
        grid.NumRows.Should().Be(4);
        position.Should().Be(new UiPosition(1.1f, 2.2f, 2.9f, 3.8f)); 
    }
    
    [Theory]
    [MemberData(nameof(MoveColsData))]
    public void MoveCols_SetsCorrectValues(int moves, UiPosition expected)
    {
        // Arrange
        GridPosition grid = new GridPositionBuilder(4, 2).Build();
        
        // Act
        for (int i = 0; i < moves; i++)
        {
            grid.MoveCols(1);
        }

        UiPosition position = grid;
        
        // Assert
        position.Should().Be(expected); 
    }

    public static TheoryData<int, UiPosition> MoveColsData => 
        [
            (0, new UiPosition(0, 0.5f, 0.25f, 1f)),
            (1, new UiPosition(0.25f, 0.5f, 0.5f, 1f)),
            (2, new UiPosition(0.5f, 0.5f, 0.75f, 1f)),
            (3, new UiPosition(0.75f, 0.5f, 1f, 1f)),
            (4, new UiPosition(0, 0, 0.25f, 0.5f)),
            (5, new UiPosition(0.25f, 0, 0.5f, 0.5f)),
            (6, new UiPosition(0.5f, 0, 0.75f, 0.5f)),
            (7, new UiPosition(0.75f, 0, 1f, 0.5f))
        ];
    
    [Theory]
    [MemberData(nameof(MoveRowsData))]
    public void MoveRows_SetsCorrectValues(int moves, UiPosition expected)
    {
        // Arrange
        GridPosition grid = new GridPositionBuilder(4, 2).Build();
        
        // Act
        for (int i = 0; i < moves; i++)
        {
            grid.MoveRows(1);
        }

        UiPosition position = grid;
        
        // Assert
        position.Should().Be(expected); 
    }

    public static TheoryData<int, UiPosition> MoveRowsData => 
    [
        (0, new UiPosition(0, 0.5f, 0.25f, 1f)),
        (1, new UiPosition(0, 0, 0.25f, 0.5f))
    ];

    [Theory]
    [MemberData(nameof(ScrollViewVertical))]
    public void ApplyScrollViewContentVertical_SetsCorrectValues(int totalItems, UiPosition expected)
    {
        // Arrange
        GridPosition grid = new GridPositionBuilder(4, 2).Build();
        using UiScrollView scrollView = UnitTestHelpers.UnitTestPool.Get<UiScrollView>().Init(ScrollRect.MovementType.Unrestricted, 0f, false, 0f, 0f);

        // Act
        grid.ApplyScrollViewContentVertical(totalItems, scrollView);

        // Assert
        scrollView.ContentPosition.Should().Be(expected);
    }
    
    public static TheoryData<int, UiPosition> ScrollViewVertical => 
    [
        (1, new UiPosition(0, 0, 1, 1)),
        (2, new UiPosition(0, 0, 1, 1)),
        (3, new UiPosition(0, 0, 1, 1)),
        (4, new UiPosition(0, 0, 1, 1)),
        (5, new UiPosition(0, 0, 1, 1)),
        (6, new UiPosition(0, 0, 1, 1)),
        (7, new UiPosition(0, 0, 1, 1)),
        (8, new UiPosition(0, 0, 1, 1)),
        (9, new UiPosition(0, -0.5f, 1, 1)),
        (10, new UiPosition(0, -0.5f, 1, 1)),
        (11, new UiPosition(0, -0.5f, 1, 1)),
        (12, new UiPosition(0, -0.5f, 1, 1f)),
        (12, new UiPosition(0, -0.5f, 1, 1f)),
        (13, new UiPosition(0, -1f, 1, 1f)),
        (14, new UiPosition(0, -1f, 1, 1f)),
        (15, new UiPosition(0, -1f, 1, 1f)),
        (16, new UiPosition(0, -1f, 1, 1f)),
        (20, new UiPosition(0, -1.5f, 1, 1f))
    ];
    
    [Theory]
    [MemberData(nameof(ScrollViewHorizontal))]
    public void ApplyScrollViewContentHorizontal_SetsCorrectValues(int totalItems, UiPosition expected)
    {
        // Arrange
        GridPosition grid = new GridPositionBuilder(4, 2).Build();
        using UiScrollView scrollView = UnitTestHelpers.UnitTestPool.Get<UiScrollView>().Init(ScrollRect.MovementType.Unrestricted, 0f, false, 0f, 0f);

        // Act
        grid.ApplyScrollViewContentHorizontal(totalItems, scrollView);

        // Assert
        scrollView.ContentPosition.Should().Be(expected);
    }
    
    public static TheoryData<int, UiPosition> ScrollViewHorizontal => 
    [
        (2, new UiPosition(0, 0, 1, 1)),
        (1, new UiPosition(0, 0, 1, 1)),
        (3, new UiPosition(0, 0, 1, 1)),
        (4, new UiPosition(0, 0, 1, 1)),
        (5, new UiPosition(0, 0, 1, 1)),
        (6, new UiPosition(0, 0, 1, 1)),
        (7, new UiPosition(0, 0, 1, 1)),
        (8, new UiPosition(0, 0, 1, 1)),
        (9, new UiPosition(0, 0, 1.25f, 1)),
        (10, new UiPosition(0, 0, 1.25f, 1)),
        (11, new UiPosition(0, 0, 1.5f, 1)),
        (12, new UiPosition(0, 0, 1.5f, 1)),
        (16, new UiPosition(0, 0, 2f, 1f)),
        (20, new UiPosition(0, 0, 2.5f, 1f)),
    ];
}